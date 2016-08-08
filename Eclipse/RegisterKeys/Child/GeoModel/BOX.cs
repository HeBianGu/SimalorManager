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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child
{
    /// <summary> 范围类 </summary>
    public class BOX : ModifyKey
    {
        public BOX(string _name)
            : base(_name)
        {
            base.endFlag = BoxEndFlag;
        }

        public const string BoxEndFlag = "ENDBOX";

        RegionParam region;

        public RegionParam Region
        {
            get { return region; }
            set { region = value; }
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

                //  首先读取DefautRegion  1 40 1 20 1 4 /
                if (region == null)
                {
                    region = new RegionParam();
                    region.BuildExtend(tempStr);
                    this.DefautRegion = region;
                    this.BaseFile.TempRegion = region;
                }

                //  读到结束符结束
                if (tempStr == endFlag)
                    break;

                bool isChildRegister = KeyConfigerFactroy.Instance.IsChildRegisterKey(tempStr);

                if (isChildRegister)
                {
                    //  读到下一关注关键字终止
                    BaseKey tempKey = KeyConfigerFactroy.Instance.CreateChildKey<BaseKey>(tempStr);

                    //  是修正关键字
                    if (tempKey is ModifyKey)
                    {
                        tempKey.BaseFile = this.BaseFile;
                        tempKey.ParentKey = this;

                        ModifyKey mk = tempKey as ModifyKey;
                        mk.DefautRegion = this.region;
                        this.Keys.Add(mk);
                        mk.BaseFile = this.BaseFile;
                        mk.ReadKeyLine(reader);

                        ////  执行更改
                        //mk.RunModify();
                    }
                    else if (tempKey is TableKey)
                    {
                        ReadBOX(reader, tempStr);

                        return null;
                    }

                    else
                    {

                        ReadBOX(reader,tempStr);

                        return null;
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
            return null;
        }

        /// <summary> 循环读取BOX修改的关键字 </summary>
        public void ReadBOX(System.IO.StreamReader reader, string keyName)
        {
            TableKey tk = KeyConfigerFactroy.Instance.CreateChildKey<TableKey>(keyName);

            string tempStr;

            while (!reader.EndOfStream)
            {
                tempStr = reader.ReadLine().TrimEnd();

                //  遇到结束符退出
                if (tempStr == KeyConfiger.EndFlag || tempStr == BOX.BoxEndFlag)
                {
                    break;
                }

                bool isChildRegister = KeyConfigerFactroy.Instance.IsChildRegisterKey(tempStr);

                if (isChildRegister)
                {
                    //  读到下一关注关键字终止
                    BaseKey tempKey = KeyConfigerFactroy.Instance.CreateChildKey<BaseKey>(tempStr);

                    //  是修正关键字
                    if (tempKey is ModifyKey)
                    {
                        tempKey.BaseFile = this.BaseFile;
                        tempKey.ParentKey = this;

                        ModifyKey mk = tempKey as ModifyKey;
                        this.Keys.Add(mk);
                        mk.BaseFile = this.BaseFile;
                        mk.ReadKeyLine(reader);

                        ////  执行更改
                        //mk.RunModify();
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

            ModifyBoxModel model = new ModifyBoxModel(tk, region);

            StringBuilder sb=new StringBuilder();
            tk.Lines.ForEach(l => sb.AppendLine(l.Trim(KeyConfiger.splitKeyWord)));

            model.Value = sb.ToString();

            tk.Build(region.ZTo - region.ZFrom, region.XTo - region.XFrom, region.YTo - region.YFrom);

            model.ParentKey = this;
            this.ObsoverModel.Add(model);

        }

        public override void WriteKey(StreamWriter writer)
        {
            //  没有修正参数不写 其他修正关键字写在外面
            if (this.ObsoverModel.Count == 0) return;

            base.startStr = this.region.ToString();

            base.WriteKey(writer);
        }
    }
}
