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
    /// <summary> 关键字Normal类 通常用来表示不关注的关键字节点 </summary>
    public class Key : BaseKey
    {
        public Key(string _name)
            : base(_name)
        {

        }

        /// <summary> 读取关键字 本节点读取关键字读到下一个关键字位置 </summary>
        public override BaseKey ReadKeyLine(StreamReader reader)
        {
            //GC.Collect();
            base.ReadKeyLine(reader);

            string tempStr = string.Empty;

            while (!reader.EndOfStream)
            {
                tempStr = reader.ReadLine().TrimEnd();

                bool isParenRegister = KeyConfigerFactroy.Instance.IsParentRegisterKey(tempStr);
                //  读到了父节点
                if (isParenRegister)
                {
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
                        //  读到下一关注关键字终止
                        BaseKey bk= KeyConfigerFactroy.Instance.CreateChildKey<BaseKey>(tempStr);

                        //  插入到DATES的同一级
                        if (bk is DATES)
                        {
                            if (this.ParentKey is DATES)
                            {
                                this.ParentKey.ParentKey.Add(bk);
                            }
                            else
                            {
                                this.ParentKey.Add(bk);
                            }
                        }
                        else
                        {
                            this.ParentKey.Add(bk);
                        }
                        bk.BaseFile = this.BaseFile;// this.ParentKey.BaseFile;
                        //CatcheKeyFactroy.Instance.TempKey.ParentKey = this.ParentKey;
                        bk.ReadKeyLine(reader);

                    }
                    else
                    {
                        //  普通关键字下面可能存在INCLUDE关键字
                        bool isIncludeKey = KeyConfigerFactroy.Instance.IsINCLUDERegisterKey(tempStr);

                        if (isIncludeKey)
                        {
                            INCLUDE includeKey = KeyConfigerFactroy.Instance.CreateIncludeKey<INCLUDE>(tempStr);
                            this.ParentKey.Add(includeKey);
                            includeKey.BaseFile = this.BaseFile;
                            //this.Keys.Add(includeKey);
                            //includeKey.ParentKey = this;
                            includeKey.ReadKeyLine(reader);
                        }
                        else if (tempStr.IsKeyFormat())
                        {
                            UnkownKey findKey = new UnkownKey(KeyChecker.FormatKey(tempStr));

                            this.ParentKey.Add(findKey);
                            findKey.ParentKey = this.ParentKey;

                            findKey.BaseFile = this.BaseFile;
                            //  触发事件
                            if (findKey.BaseFile != null && findKey.BaseFile.OnUnkownKey != null)
                            {
                                findKey.BaseFile.OnUnkownKey(findKey.BaseFile, findKey);
                            }
                            //this.ParentKey.Add(findKey);
                            //  调用子节点读取方法
                            findKey.ReadKeyLine(reader);
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
            }
            //  读到末尾返回空值
            return null;
        }


        public override void WriteKey(StreamWriter writer)
        {
            writer.WriteLine();
            writer.WriteLine(this.Name);
            base.WriteKey(writer);
        }

    }
}
