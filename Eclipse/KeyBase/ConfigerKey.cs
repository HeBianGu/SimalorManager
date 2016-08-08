#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/26 15:58:43
 * 文件名：NormalKey
 * 说明：
 * 
 * 此方法和Key读取方法区别在于屏蔽掉 读取NormalKey 
   TITLE
   LN3T3
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child;
using OPT.Product.SimalorManager.Eclipse.RegisterKeys.INCLUDE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    /// <summary> 写入方法是直接调用this.ToString()方法 </summary>
    public abstract class ConfigerKey : BaseKey
    {
        public ConfigerKey(string _name)
            : base(_name)
        {

        }

        public override BaseKey ReadKeyLine(StreamReader reader)
        {
            base.ReadKeyLine(reader);

            string tempStr = string.Empty;

            while (!reader.EndOfStream)
            {
                tempStr = reader.ReadLine().TrimEnd();

                bool isParenRegister = KeyConfigerFactroy.Instance.IsParentRegisterKey(tempStr);
                //  读到了父节点
                if (isParenRegister)
                {
                    this.CmdToItems();

                    ParentKey findkey = KeyConfigerFactroy.Instance.CreateParentKey<ParentKey>(tempStr);
                    this.BaseFile.Key.Add(findkey);
                    //findkey.BaseFile = this.ParentKey.BaseFile;
                    findkey.ParentKey = this.BaseFile.Key;
                    findkey.BaseFile = this.BaseFile;
                    findkey.ReadKeyLine(reader);
                }
                else
                {
                    bool isChildRegister = KeyConfigerFactroy.Instance.IsChildRegisterKey(tempStr);

                    if (isChildRegister)
                    {
                        this.CmdToItems();

                        //  读到下一关注关键字终止
                        BaseKey TempKey = KeyConfigerFactroy.Instance.CreateChildKey<BaseKey>(tempStr);

                        //  插入到DATES的同一级
                        if (TempKey is DATES)
                        {
                            if (this.ParentKey is DATES)
                            {
                                this.ParentKey.ParentKey.Add(TempKey);
                            }
                            else
                            {
                                this.ParentKey.Add(TempKey);
                            }
                        }
                        else
                        {
                            this.ParentKey.Add(TempKey);
                        }

                        TempKey.BaseFile = this.ParentKey.BaseFile;
                        TempKey.ReadKeyLine(reader);

                    }
                    else
                    {
                        //  普通关键字下面可能存在INCLUDE关键字
                        bool isIncludeKey = KeyConfigerFactroy.Instance.IsINCLUDERegisterKey(tempStr);

                        if (isIncludeKey)
                        {
                            this.CmdToItems();

                            INCLUDE includeKey = KeyConfigerFactroy.Instance.CreateIncludeKey<INCLUDE>(tempStr);
                            this.ParentKey.Add(includeKey);
                            includeKey.BaseFile = this.BaseFile;
                            includeKey.ReadKeyLine(reader);
                        }
                        else
                        {
                            if (this.Match(tempStr)) //if (tempStr.IsKeyFormat())
                            {

                                this.CmdToItems();

                                //  添加普通关键字
                                UnkownKey normalKey = new UnkownKey(KeyChecker.FormatKey(tempStr));
                                normalKey.ParentKey = this;
                                normalKey.BaseFile = this.BaseFile;

                                //  触发事件
                                if (normalKey.BaseFile != null && normalKey.BaseFile.OnUnkownKey != null)
                                {
                                    normalKey.BaseFile.OnUnkownKey(normalKey.BaseFile, normalKey);
                                }
                                this.Keys.Add(normalKey);
                                normalKey.ReadKeyLine(reader);
                            }
                            else
                            {

                                if (tempStr.IsWorkLine())
                                {
                                    this.Lines.Add(tempStr);
                                }

                            }
                        }
                    }

                }
            }

            CmdToItems();

            //  读到末尾返回空值
            return null;


        }

        void CmdToItems()
        {
            string str = null;

            this.Lines.RemoveAll(l => !l.IsWorkLine());

            for (int i = 0; i < Lines.Count; i++)
            {
                if (i == 0)
                {
                    str = Lines[i];
                    List<string> newStr = str.EclExtendToArray();
                    Build(newStr);
                }
            }
        }

        /// <summary> 只调用ToString()方法 </summary>
        public override void WriteKey(StreamWriter writer)
        {
            writer.WriteLine();
            writer.WriteLine(this.Name);
            writer.WriteLine(this.ToString());
            writer.WriteLine();
        }


        public abstract void Build(List<string> newStr);
    }
}
