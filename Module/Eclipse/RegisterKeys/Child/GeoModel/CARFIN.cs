#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 17:43:17

 * 说明：
 * 
 * 读取规则：找到ENDBOX退出读取，读到要修改的关键字放到ObsoverKey观察对象中
 * 

 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 局部网格加密 </summary>
    public class CARFIN : Key
    {
        public CARFIN(string _name)
            : base(_name)
        {

        }

        /// <summary> 介素标识 </summary>
        public const string _carfinEndFlag = "ENDFIN";

        /// <summary> 网格加密名称 </summary>
        private string carName;

        public string CarName
        {
            get { return carName; }
            set { carName = value; }
        }

        /// <summary> 网格加密范围 </summary>
        RegionParam region = new RegionParam();


        public int XFrom
        {
            get
            {
                return region.XFrom + 1;
            }
            set
            {
                region.XFrom = value;
            }
        }

        public int XTo
        {
            get
            {
                return region.XTo;
            }
            set
            {
                region.XTo = value;
            }
        }

        public int YFrom
        {
            get
            {
                return region.YFrom + 1;
            }
            set
            {
                region.YFrom = value;
            }
        }

        public int YTo
        {
            get
            {
                return region.YTo;
            }
            set
            {
                region.YTo = value;
            }
        }

        public int ZFrom
        {
            get
            {
                return region.ZFrom + 1;
            }
            set
            {
                region.ZFrom = value;
            }
        }

        public int ZTo
        {
            get
            {
                return region.ZTo;
            }
            set
            {
                region.ZTo = value;
            }
        }


        private int x;
        /// <summary> 网格X方向维数 </summary>
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        private int y;
        /// <summary> 网格Y方向维数 </summary>
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        private int z;
        /// <summary> 网格Z方向维数 </summary>
        public int Z
        {
            get { return z; }
            set { z = value; }
        }

        /// <summary> 用来表示本加密网格的范围 </summary>
        RegionParam newRegion = new RegionParam();



        /// <summary> 默认范围</summary>
        RegionParam defaultRegion = new RegionParam();


        List<ModifyBoxModel> obsoverModel = new List<ModifyBoxModel>();

        /// <summary> 要修改的属性关键字 </summary>
        public List<ModifyBoxModel> ObsoverModel
        {
            get { return obsoverModel; }
            set { obsoverModel = value; }
        }

        List<ModifyKey> _modifyKeyCar = new List<ModifyKey>();
        /// <summary> 局部网格加密的修正关键字 </summary>
        public List<ModifyKey> ModifyKeyCar
        {
            get { return _modifyKeyCar; }
            set { _modifyKeyCar = value; }
        }

        /// <summary> 读取关键字 </summary>
        public override BaseKey ReadKeyLine(StreamReader reader)
        {

            string tempStr = string.Empty;

            while (!reader.EndOfStream)
            {
                tempStr = reader.ReadLine().TrimEnd();

                if (!tempStr.IsWorkLine())
                    continue;

                #region - 构建结构 -

                if (string.IsNullOrEmpty(this.carName))
                {
                    //  首先读取结构参数  'LGR1' 2 2 3 3 3 4   5   5   8 5 /
                    List<string> strs = tempStr.EclExtendToArray();

                    if (this.carName == null)
                    {
                        this.carName = strs[0];

                        region = new RegionParam();
                        region.XFrom = strs[1].ToInt();
                        region.XTo = strs[2].ToInt();
                        region.YFrom = strs[3].ToInt();
                        region.YTo = strs[4].ToInt();
                        region.ZFrom = strs[5].ToInt();
                        region.ZTo = strs[6].ToInt();

                        this.X = strs[7].ToInt();
                        this.Y = strs[8].ToInt();
                        this.Z = strs[9].ToInt();

                        newRegion.XFrom = 1;
                        newRegion.XTo = this.x;
                        newRegion.YFrom = 1;
                        newRegion.YTo = this.y;
                        newRegion.ZFrom = 1;
                        newRegion.ZTo = this.z;


                        defaultRegion = newRegion;

                    }
                }

                #endregion

                //  读到结束符结束
                if (tempStr == _carfinEndFlag)
                    break;

                //  读到本关键字信息 处理两个CARFIN用一个ENDFIN问题
                if (tempStr == this.Name)
                {
                    BaseKey findKey = KeyConfigerFactroy.Instance.CreateKey<BaseKey>(tempStr, this.BaseFile.SimKeyType);
                    findKey.BaseFile = this.BaseFile;
                    findKey.ParentKey = this.ParentKey;
                    this.ParentKey.Add(findKey);
                    //  调用子节点读取方法
                    findKey.ReadKeyLine(reader);
                    break;
                }

                bool isChildRegister = KeyConfigerFactroy.Instance.IsRegister(tempStr, this.BaseFile.SimKeyType);

                if (isChildRegister)
                {
                    //  读到下一关注关键字终止
                    BaseKey tempKey = KeyConfigerFactroy.Instance.CreateKey<BaseKey>(tempStr, this.BaseFile.SimKeyType);

                    //  是修正关键字
                    if (tempKey is ModifyKey)
                    {
                        tempKey.BaseFile = this.BaseFile;
                        tempKey.ParentKey = this;

                        ModifyKey mk = tempKey as ModifyKey;
                        mk.DefautRegion = this.defaultRegion;
                        //  放入本定义下面
                        this._modifyKeyCar.Add(mk);
                        mk.BaseFile = this.BaseFile;
                        mk.ReadKeyLine(reader);

                        this.defaultRegion = mk.DefautRegion;

                        //  增加子修正关键字 如BOX
                        //this._modifyKeyCar.AddRange(mk.FindAll<ModifyKey>());

                    }
                    else if (tempKey is DynamicKey)
                    {
                        tempKey.BaseFile = this.BaseFile;
                        tempKey.ParentKey = this;

                        //  放入本子节点下面
                        this.Keys.Add(tempKey);
                        tempKey.ReadKeyLine(reader);
                    }
                    else if (tempKey is TableKey)
                    {
                        ReadBOX(reader, tempStr);

                        return this;
                    }

                    else
                    {

                        ReadBOX(reader, tempStr);

                        return this;
                    }
                }
                else
                {
                    if (tempStr.IsKeyFormat())
                    {
                        UnkownKey findKey = new UnkownKey(KeyChecker.FormatKey(tempStr));

                        findKey.BaseFile = this.BaseFile;

                        //  触发事件
                        if (findKey.BaseFile != null && findKey.BaseFile.OnUnkownKey != null)
                        {
                            findKey.BaseFile.OnUnkownKey(findKey.BaseFile, findKey);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(tempStr))
                        {
                            //  不是记录行
                            this.Lines.Add(tempStr);
                        }
                    }
                }
            }

            //  读到末尾返回空值
            return this;
        }

        /// <summary> 获取全部修正模型 </summary>
        public List<IModifyModel> GetAllModifyModels()
        {

            List<IModifyModel> allPropertys = new List<IModifyModel>();

            ////  加载直接修改的关键字
            //allPropertys.AddRange(this.ObsoverModel);

            //  加载修改参数修改的关键字
            foreach (ModifyKey m in this.ModifyKeyCar)
            {
                //  加载本修改模型中关键字
                m.ObsoverModel.ForEach(k => allPropertys.Add(k));

                //  加载子修改模型中关键字 如BOX
                List<ModifyKey> modifys = m.FindAll<ModifyKey>();

                modifys.Remove(m);

                modifys.ForEach(l => allPropertys.AddRange(l.ObsoverModel));
            }

            return allPropertys;
        }


        /// <summary> 循环读取BOX修改的关键字 </summary>
        public void ReadBOX(System.IO.StreamReader reader, string keyName)
        {
            TableKey tk = KeyConfigerFactroy.Instance.CreateKey<TableKey>(keyName,this.BaseFile.SimKeyType) as TableKey;

            ModifyBoxModel model = new ModifyBoxModel(tk, this.newRegion);
            BOX box = new BOX("BOX");
            box.Region = this.newRegion;
            box.ObsoverModel.Add(model);
            model.ParentKey = box;
            this.ModifyKeyCar.Add(box);

            string tempStr;

            while (!reader.EndOfStream)
            {
                tempStr = reader.ReadLine().TrimEnd();

                ////  遇到结束符退出
                //if (tempStr == KeyConfiger.EndFlag || tempStr == _carfinEndFlag)
                //{
                //    break;
                //}

                //  遇到结束符退出
                if (tempStr == _carfinEndFlag)
                {
                    break;
                }

                bool isChildRegister = KeyConfigerFactroy.Instance.IsRegister(tempStr);

                if (isChildRegister)
                {
                    //  读到下一关注关键字终止
                    BaseKey tempKey = KeyConfigerFactroy.Instance.CreateKey<BaseKey>(tempStr);

                    //  是修正关键字
                    if (tempKey is ModifyKey)
                    {
                        tempKey.BaseFile = this.BaseFile;
                        tempKey.ParentKey = this;

                        ModifyKey mk = tempKey as ModifyKey;
                        //  放入本定义下面
                        this._modifyKeyCar.Add(mk);
                        mk.BaseFile = this.BaseFile;
                        mk.ReadKeyLine(reader);

                        ////  执行更改
                        //mk.RunModify();
                    }
                    else if (tempKey is DynamicKey)
                    {
                        tempKey.BaseFile = this.BaseFile;
                        tempKey.ParentKey = this;

                        //  放入本子节点下面
                        this.Keys.Add(tempKey);
                        tempKey.ReadKeyLine(reader);
                    }
                    else if (tempKey is TableKey)
                    {

                        ReadBOX(reader, tempStr);

                        break;
                    }

                    else
                    {

                        ReadBOX(reader, tempStr);

                        break;
                    }
                }
                else
                {
                    //  PERMX 2 /
                    if (tempStr.Trim().IsKeyFormat())
                    {
                        ReadBOX(reader, tempStr);

                        break;
                    }
                    else
                    {
                        //  有效行插入到集合
                        if (tempStr.IsWorkLine())
                        {
                            tk.Lines.Add(tempStr);
                        }
                    }
                }
            }

            //  构建数据
            StringBuilder sb = new StringBuilder();
            tk.Lines.ForEach(l => sb.AppendLine(l.Trim(KeyConfiger.splitKeyWord)));
            model.Value = sb.ToString();
            tk.Build(this.Z, this.X, this.Y);

            //this.ObsoverModel.Add(model);

        }

        /// <summary> 增加并读取BOX </summary>
        /// <param name="tk"> 要新增的表 </param>
        /// <param name="reader"> 数据流 </param>
        /// <param name="tempStr"> 后续新增表的名  </param>
        public void AddAndReadBox(TableKey tk, System.IO.StreamReader reader, string tempStr)
        {
            ModifyBoxModel model = new ModifyBoxModel(tk, region);
            StringBuilder sb = new StringBuilder();
            tk.Lines.ForEach(l => sb.AppendLine(l.Trim(KeyConfiger.splitKeyWord)));
            model.Value = sb.ToString();
            tk.Build(this.Z, this.X, this.Y);
            BOX box = new BOX("BOX");
            box.Region = this.region;
            box.ObsoverModel.Add(model);
            model.ParentKey = box;
            this.ModifyKeyCar.Add(box);

            ReadBOX(reader, tempStr);
        }

        string formatStr = "    {0}{1}{2}{3}{4}{5}{6}{7}{8}{9}/";

        public override void WriteKey(StreamWriter writer)
        {
            writer.WriteLine();
            writer.WriteLine(this.Name);
            //  写范围
            writer.WriteLine(string.Format(formatStr, this.carName.ToDD(), (region.XFrom + 1).ToString().ToDD(), region.XTo.ToString().ToDD(),
                (region.YFrom + 1).ToString().ToDD(), region.YTo.ToString().ToDD(), (region.ZFrom + 1).ToString().ToDD(), region.ZTo.ToString().ToDD(),
                this.x.ToString().ToDD(), this.y.ToString().ToDD(), this.z.ToString().ToDD()));

            //  写子关键字
            foreach (BaseKey key in this.Keys)
            {
                key.WriteKey(writer);
            }

            //foreach (ModifyBoxModel m in this.obsoverModel)
            //{
            //    m.Key.WriteKey(writer);
            //    //writer.WriteLine(m.ToModelString());
            //}

            //  写修正参数

            foreach (ModifyKey mk in this.ModifyKeyCar)
            {
                mk.WriteKey(writer);

                List<ModifyKey> temp = mk.FindAll<ModifyKey>();

                temp.Remove(mk);

                //  写子关键字 如BOX
                foreach (ModifyKey m in temp)
                {
                    m.WriteKey(writer);

                }

            }

            writer.WriteLine(_carfinEndFlag);
        }

        /// <summary> 清理所有属性 </summary>
        public void ClearAllProperty()
        {
            this.obsoverModel.Clear();

            this.ModifyKeyCar.Clear();
        }
    }
}
