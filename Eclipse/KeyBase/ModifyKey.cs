#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 17:43:17
 * 文件名：GCONPROD
 * 说明：
 * 

BOX
1 40 1 20 5 8 /
PERMX
3200*0.03 /
ENDBOX


 * 
 * 读取规则：找到ENDBOX退出读取，读到要修改的关键字放到ObsoverKey观察对象中
 * 

 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Eclipse.FileInfos;
using OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager
{
    /// <summary> 修改参数抽象类 （ 兼容BOX和修改参数关键字 ） </summary>
    public abstract class ModifyKey : Key
    {
        public ModifyKey(string _name)
            : base(_name)
        {

        }

        /// <summary> 操作符的执行操作公式 </summary>
        public Func<double, double, double> func;

        //  结束标识符 子类修改该标识
        protected string endFlag = KeyConfiger.EndFlag;


        //  结束标识符 子类修改该标识
        protected string startStr = string.Empty;


        List<IModifyModel> obsoverModel = new List<IModifyModel>();

        /// <summary>  </summary>
        protected List<ModifyItem> Items = new List<ModifyItem>();

        /// <summary> 要修改的属性关键字 </summary>
        public List<IModifyModel> ObsoverModel
        {
            get { return obsoverModel; }
            set { obsoverModel = value; }
        }

        private RegionParam defautRegion;

        /// <summary> 默认范围 用于设置默认范围（BOX不为空，修改参数该值为空） </summary>
        public RegionParam DefautRegion
        {
            get { return defautRegion; }
            set { defautRegion = value; }
        }


        /// <summary> 是否包含关键字 SATNUM  3  1 11 1 19 2 2 / </summary>
        public bool IsKeyLine(string line)
        {
            List<string> str = line.EclExtendToArray();

            if (str == null || str.Count == 0)
                return false;

            return str[0].Trim().IsKeyFormat();
        }

        /// <summary> 抽象方法 子类用来扩展 </summary>
        protected virtual void ConvertToModel()
        {
            foreach (ModifyItem it in this.Items)
            {
                ModifyApplyModel model = it.ToModel(func, this);
                model.ParentKey = this;
                obsoverModel.Add(model);
            }
        }

        void CmdGetItems()
        {

            string str = string.Empty;

            for (int i = 0; i < Lines.Count; i++)
            {
                str = Lines[i];
                //  读到结束符不继续读取
                if (str.StartsWith("/") && str.EndsWith("/"))
                {
                    break;
                }

                //  不为空的行
                if (str.IsWorkLine())
                {
                    List<string> newStr = str.EclExtendToArray();

                    if (newStr.Count > 0)
                    {
                        ModifyItem pitem = new ModifyItem();
                        pitem.Build(newStr);
                        //  标记行的ID位置
                        //Lines[i] = pitem.ID;
                        if (pitem != null)
                        {
                            Items.Add(pitem);
                        }
                    }
                }

            }

            ConvertToModel();
        }

        /// <summary> 读取关键字 本节点读取关键字读到下一个关键字位置 </summary>
        public override BaseKey ReadKeyLine(StreamReader reader)
        {
            string tempStr = string.Empty;

            while (!reader.EndOfStream)
            {
                tempStr = reader.ReadLine().Trim();

                //  读到结束符结束
                if (tempStr == endFlag)
                    break;

                //  过滤无效行
                if (!IsKeyLine(tempStr))
                    continue;

                ////  读到关键字返回
                //if (tempStr.IsKeyFormat())
                //    break;

                this.Lines.Add(tempStr);

            }

            CmdGetItems();

            //  读到末尾返回空值
            return null;
        }

        public override void WriteKey(StreamWriter writer)
        {
            writer.WriteLine(this.Name);

            if (!string.IsNullOrEmpty(this.startStr))
            {
                writer.WriteLine(this.startStr);
            }
            foreach (var item in this.ObsoverModel)
            {
                writer.WriteLine(item.ToModelString());
            }
            writer.WriteLine(this.endFlag);
            writer.WriteLine();
        }

        public class ModifyItem
        {
            /// <summary> 参数1 </summary>
            string p1;
            /// <summary> 参数2 </summary>
            string p2;
            /// <summary> X1 </summary>
            string xf3;
            /// <summary> X2 </summary>
            string xt4;
            /// <summary> Y1 </summary>
            string yf5;
            /// <summary> Y2 </summary>
            string yt6;
            /// <summary> Z1 </summary>
            string zf7;
            /// <summary> Z2 </summary>
            string zt8;

            string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7} /";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, p1.ToD(), p2.ToD(), xf3.ToD(), xt4.ToD(), yf5.ToD(), yt6.ToD(), zf7.ToD(), zt8.ToD());
            }

            /// <summary> 解析字符串 </summary>
            public void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.p1 = newStr[0];
                            break;
                        case 1:
                            this.p2 = newStr[1];
                            break;
                        case 2:
                            this.xf3 = newStr[2];
                            break;
                        case 3:
                            this.xt4 = newStr[3];
                            break;
                        case 4:
                            this.yf5 = newStr[4];
                            break;
                        case 5:
                            this.yt6 = newStr[5];
                            break;
                        case 6:
                            this.zf7 = newStr[6];
                            break;
                        case 7:
                            this.zt8 = newStr[7];
                            break;
                        default:
                            break;
                    }
                }
            }

            public ModifyApplyModel ToModel(Func<double, double, double> func, ModifyKey modify)
            {
                RegionParam region = null;

                if (
                !string.IsNullOrEmpty(xf3) &&
                !string.IsNullOrEmpty(xt4) &&
                !string.IsNullOrEmpty(yf5) &&
                !string.IsNullOrEmpty(yt6) &&
                !string.IsNullOrEmpty(zf7) &&
                !string.IsNullOrEmpty(zt8))
                {
                    region = new RegionParam();
                    region.XFrom = xf3.ToInt();
                    region.XTo = xt4.ToInt();
                    region.YFrom = yf5.ToInt();
                    region.YTo = yt6.ToInt();
                    region.ZFrom = zf7.ToInt();
                    region.ZTo = zt8.ToInt();

                    modify.DefautRegion = region;
                    modify.BaseFile.TempRegion = region;
                }
                else
                {

                    //region = modify.DefautRegion;
                    region = modify.BaseFile.TempRegion;
                }

                ModifyApplyModel model = new ModifyApplyModel(this.p1, region, this.p2, func);

                return model;
            }

            public ModifyCopyModel ToModel(COPY copy)
            {
                RegionParam region = null;

                if (
                !string.IsNullOrEmpty(xf3) &&
                !string.IsNullOrEmpty(xt4) &&
                !string.IsNullOrEmpty(yf5) &&
                !string.IsNullOrEmpty(yt6) &&
                !string.IsNullOrEmpty(zf7) &&
                !string.IsNullOrEmpty(zt8))
                {
                    region = new RegionParam();
                    region.XFrom = xf3.ToInt();
                    region.XTo = xt4.ToInt();
                    region.YFrom = yf5.ToInt();
                    region.YTo = yt6.ToInt();
                    region.ZFrom = zf7.ToInt();
                    region.ZTo = zt8.ToInt();

                    copy.BaseFile.TempRegion = region;
                }
                else
                {
                    region = copy.BaseFile.TempRegion;
                }

                ModifyCopyModel model = new ModifyCopyModel(this.p2, region, this.p1);

                return model;
            }
        }

        /// <summary> 删除指定关键字的修改参数 </summary>
        public void RemoveKey(string keyName)
        {
            this.obsoverModel.RemoveAll(l => l.KeyName == keyName);
        }

    }

    /// <summary> 修改的参数的修改 </summary>
    public interface IModifyModel
    {
        /// <summary> 要修改的关键字名称 </summary>
        string KeyName
        {
            get;
        }
        /// <summary> 所要修改的范围 </summary>
        RegionParam Region
        {
            get;
            set;
        }

        string ToModelString();


        ModifyKey ParentKey
        {
            get;
            set;
        }
    }

    /// <summary> 修改的参数的修改 </summary>
    public class ModifyApplyModel : IModifyModel
    {
        public ModifyApplyModel(string k, RegionParam r, string v, Func<double, double, double> f)
        {
            key = k;
            region = r;
            value = v;
            func = f;

        }

        string key;

        public string KeyName
        {
            get { return key; }
            set { key = value; }
        }

        string value;
        /// <summary> 修改的参数值 </summary>
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        RegionParam region;

        public RegionParam Region
        {
            get { return region; }
            set { region = value; }
        }


        Func<double, double, double> func;
        /// <summary> 操作符的执行操作公式 </summary>
        public Func<double, double, double> Func
        {
            get { return func; }
            set { func = value; }
        }

        /// <summary> 执行更改 </summary>
        public void RunModify(TableKey funcKey)
        {

            if (CheckRegion(funcKey))
            {
                double temp;

                bool isDouble = double.TryParse(value, out temp);

                if (isDouble)
                {
                    //  遍历更改区域 
                    for (int x = region.XFrom; x < region.XTo; x++)
                    {
                        for (int y = region.YFrom; y < region.YTo; y++)
                        {
                            for (int z = region.ZFrom; z < region.ZTo; z++)
                            {
                                //  对值执行func操作
                                funcKey.Tables[z].Matrix.Mat[y, x] = func(funcKey.Tables[z].Matrix.Mat[y, x].ToString().ToDouble(), temp);
                            }
                        }
                    }
                }
                else
                {

                }
            }
        }

        /// <summary> 检查修改分区是否合法 </summary>
        public bool CheckRegion(TableKey funcKey)
        {
            if (region.ZTo > funcKey.Z ||
                region.XTo > funcKey.X ||
                region.YTo > funcKey.Y
                )
            {
                throw new Exception("CheckRegion() err Dimens Count");
            }
            else
            {
                return true;
            }
        }
        string formatStr = "{0}{1}      ";

        /// <summary> 转换成字符串 </summary>
        public string ToModelString()
        {
            return string.Format(formatStr, key.ToEclStr(), value.ToD()) + region.ToString();
        }

        ModifyKey _parentKey;

        public ModifyKey ParentKey
        {
            get
            {
                return _parentKey;
            }
            set
            {
                _parentKey = value;
            }
        }
    }


    /// <summary> BOX的修改模型 </summary>
    public class ModifyBoxModel : IModifyModel
    {
        public ModifyBoxModel(TableKey k, RegionParam r)
        {
            key = k;
            region = r;
        }

        TableKey key;

        public TableKey Key
        {
            get { return key; }
            set { key = value; }
        }

        RegionParam region;
        /// <summary> 范围 </summary>
        public RegionParam Region
        {
            get { return region; }
            set { region = value; }
        }

        string _value;
        /// <summary> 值 </summary>
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary> 执行复值 </summary>
        public void RunModify(TableKey funcKey)
        {
            if (CheckRegion(funcKey))
            {
                //  遍历更改区域 
                for (int x = region.XFrom; x < region.XTo; x++)
                {
                    for (int y = region.YFrom; y < region.YTo; y++)
                    {
                        for (int z = region.ZFrom; z < region.ZTo; z++)
                        {
                            //  对值执行func操作
                            funcKey.Tables[z].Matrix.Mat[y, x] = key.Tables[z - region.ZFrom].Matrix.Mat[y - region.YFrom, x - region.XFrom];
                        }
                    }
                }
            }
        }

        /// <summary> 检查修改分区是否合法 </summary>
        public bool CheckRegion(TableKey funcKey)
        {
            if (region.ZTo > funcKey.Z ||
                region.XTo > funcKey.X ||
                region.YTo > funcKey.Y
                )
            {
                throw new Exception("CheckRegion() err Dimens Count");
            }
            else
            {
                return true;
            }
        }
        public string KeyName
        {
            get
            {
                return this.key.Name;
            }
        }

        public string ToModelString()
        {

            StringBuilder sb = new StringBuilder();

            sb.Append(this.KeyName);
            sb.Append(KeyConfiger.NewLine);

            foreach (GridTable v in Key.Tables)
            {

                for (int i = 0; i < v.Matrix.Row; i++)
                {
                    for (int j = 0; j < v.Matrix.Col; j++)
                    {
                        sb.Append(v.Matrix[i, j].ToString().ToD().PadLeft(KeyConfiger.TableLenght));
                    }

                    if (i == v.Matrix.Row - 1 && v.IndexNum == Key.Tables.Count)
                    {
                        sb.Append(KeyConfiger.EndFlag);
                    }

                    sb.Append(KeyConfiger.NewLine);

                }
            }

            return sb.ToString();
        }


        ModifyKey _parentKey;

        public ModifyKey ParentKey
        {
            get
            {
                return _parentKey;
            }
            set
            {
                _parentKey = value;
            }
        }

    }


    /// <summary> COPY的修改模型 </summary>
    public class ModifyCopyModel : IModifyModel
    {
        public ModifyCopyModel(string k, RegionParam r, string v)
        {
            key = k;
            region = r;
            value = v;
        }

        string key;

        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        string value;

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        RegionParam region;

        public RegionParam Region
        {
            get { return region; }
            set { region = value; }
        }

        /// <summary> 执行复值  p1=被复值参数 </summary>
        public void RunModify(TableKey toKey, TableKey fromKey)
        {
            if (CheckRegion(toKey))
            {

                //  遍历更改区域 
                for (int x = region.XFrom; x < region.XTo; x++)
                {
                    for (int y = region.YFrom; y < region.YTo; y++)
                    {
                        for (int z = region.ZFrom; z < region.ZTo; z++)
                        {
                            //  对值执行func操作
                            toKey.Tables[z].Matrix.Mat[y, x] = fromKey.Tables[z].Matrix.Mat[y, x];
                        }
                    }
                }
            }
        }

        /// <summary> 检查修改分区是否合法 </summary>
        public bool CheckRegion(TableKey funcKey)
        {
            if (region.ZTo > funcKey.Z ||
                region.XTo > funcKey.X ||
                region.YTo > funcKey.Y
                )
            {
                throw new Exception("CheckRegion() err Dimens Count");
            }
            else
            {
                return true;
            }
        }


        public string KeyName
        {
            get
            {
                return this.key;
            }
        }

        string formatStr = "{0}{1}      ";

        public string ToModelString()
        {
            return string.Format(formatStr, value.ToEclStr(), key.ToEclStr()) + region.ToString();
        }

        ModifyKey _parentKey;

        public ModifyKey ParentKey
        {
            get
            {
                return _parentKey;
            }
            set
            {
                _parentKey = value;
            }
        }
    }




    /// <summary> 范围参数 1 31 1 25 1 2 / 从1开始 熟悉中减一处理索引  1 1 /取值1 所以To属性不减一 </summary>
    public class RegionParam
    {
        public RegionParam()
        {
        }
        int xFrom = 0;

        public int XFrom
        {
            get { return xFrom; }
            set { xFrom = value - 1; }
        }
        int xTo = 1;

        public int XTo
        {
            get { return xTo; }
            set { xTo = value; }
        }

        int yFrom = 0;

        public int YFrom
        {
            get { return yFrom; }
            set { yFrom = value - 1; }
        }
        int yTo = 1;

        public int YTo
        {
            get { return yTo; }
            set { yTo = value; }
        }


        int zFrom = 0;

        public int ZFrom
        {
            get { return zFrom; }
            set { zFrom = value - 1; }
        }
        int zTo = 1;

        public int ZTo
        {
            get { return zTo; }
            set { zTo = value; }
        }

        /// <summary> 创建范围 示例：line =  SATNUM  3  1 11 1 19 2 2 / </summary>
        public void Build(string line)
        {
            List<string> str = line.EclExtendToArray();
            if (str.Count < 8)
            {
                throw new Exception(string.Format(KeyConfiger.exceptionFormat, System.Reflection.MethodBase.GetCurrentMethod().Name, line));
            }
            XFrom = str[2].ToInt();
            XTo = str[3].ToInt();
            YFrom = str[4].ToInt();
            YTo = str[5].ToInt();
            ZFrom = str[6].ToInt();
            ZTo = str[7].ToInt();

        }

        /// <summary> 创建范围 示例：line =  1 40 1 20 1 4 / </summary>
        public void BuildExtend(string line)
        {
            List<string> str = line.EclExtendToArray();
            if (str.Count < 6)
            {
                throw new Exception(string.Format(KeyConfiger.exceptionFormat, System.Reflection.MethodBase.GetCurrentMethod().Name, line));
            }
            XFrom = str[0].ToInt();
            XTo = str[1].ToInt();
            YFrom = str[2].ToInt();
            YTo = str[3].ToInt();
            ZFrom = str[4].ToInt();
            ZTo = str[5].ToInt();
        }


        string formatStr = "{0}{1}{2}{3}{4}{5} /";

        public override string ToString()
        {
            return string.Format(formatStr, (this.xFrom + 1).ToD(), this.xTo.ToD(), (this.yFrom + 1).ToD(), this.yTo.ToD(), (this.zFrom + 1).ToD(), this.zTo.ToD());
        }
    }
}
