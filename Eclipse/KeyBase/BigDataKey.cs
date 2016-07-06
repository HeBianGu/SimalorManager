#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 13:46:54
 * 文件名：BigDataKey
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Eclipse.RegisterKeys.INCLUDE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    /// <summary> 大数据用于标识是否读取 </summary>
    public class BigDataKey : Key
    {
        public BigDataKey(string _name)
            : base(_name)
        {

        }
        /// <summary> 大数据读取方法 = 核心： 读到数据不做存储 </summary>
        public override BaseKey ReadKeyLine(System.IO.StreamReader reader)
        {
            if (this.BaseFile != null && this.BaseFile.IsReadBigData)
            {
                return base.ReadKeyLine(reader);
            }
            else
            {
                string tempStr = string.Empty;
                //  不读取不做处理 = 读到下一个关键字为止
                while (!reader.EndOfStream)
                {
                    tempStr = reader.ReadLine().TrimEnd();

                    bool isChildRegister = KeyConfigerFactroy.Instance.IsChildRegisterKey(tempStr);

                    if (isChildRegister)
                    {
                        //  读到下一关注关键字终止
                        BaseKey findKey = KeyConfigerFactroy.Instance.CreateChildKey<BaseKey>(tempStr);
                        this.ParentKey.Keys.Add(findKey);
                        findKey.BaseFile = this.ParentKey.BaseFile;
                        findKey.ParentKey = this.ParentKey;
                        this.Lines.Add(findKey.Name);
                        findKey.ReadKeyLine(reader);
                    }
                    else
                    {
                        bool isParenRegister = KeyConfigerFactroy.Instance.IsParentRegisterKey(tempStr);
                        //  读到了父节点
                        if (isParenRegister)
                        {
                            ParentKey findkey = KeyConfigerFactroy.Instance.CreateParentKey<ParentKey>(tempStr);
                            this.BaseFile.Key.Keys.Add(findkey);
                            findkey.BaseFile = this.ParentKey.BaseFile;
                            findkey.ParentKey = this.BaseFile.Key;
                            this.Lines.Add(findkey.Name);
                            findkey.ReadKeyLine(reader);
                        }
                        else
                        {
                            //  普通关键字下面可能存在INCLUDE关键字
                            bool isIncludeKey = KeyConfigerFactroy.Instance.IsINCLUDERegisterKey(tempStr);

                            if (isIncludeKey)
                            {
                                INCLUDE includeKey = KeyConfigerFactroy.Instance.CreateIncludeKey<INCLUDE>(tempStr);
                                includeKey.BaseFile = this.BaseFile;
                                this.Keys.Add(includeKey);
                                includeKey.ParentKey = this;
                                this.Lines.Add(includeKey.Name);
                                includeKey.ReadKeyLine(reader);
                            }
                            else
                            {
                                //  核心： 读到数据不做存储
                                //if (!string.IsNullOrEmpty(tempStr))
                                //{
                                //    //  不是记录行
                                //    this.Lines.Add(tempStr);
                                //}

                            }
                        }

                    }
                }

                return null;
            }

        }

        /// <summary> 大数据写入方法 = 拷贝大数据文件 </summary>
        public override void WriteKey(System.IO.StreamWriter writer)
        {
            base.WriteKey(writer);
        }
    }
}
