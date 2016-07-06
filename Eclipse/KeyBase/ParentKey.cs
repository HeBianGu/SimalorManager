#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/28 13:53:15
 * 文件名：ParentBaseKey
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
    /// <summary> 八种父关键字节点基类 </summary>
    public abstract class ParentKey : BaseKey
    {
        public ParentKey(string _name)
            : base(_name)
        {

        }
        /// <summary> 父节点读取方法 = 读到下一个父节点结束，读到末尾返回空 </summary>
        public override BaseKey ReadKeyLine(System.IO.StreamReader reader)
        {
            base.ReadKeyLine(reader);

            string tempStr = string.Empty;
            while (!reader.EndOfStream)
            {

                tempStr = reader.ReadLine().TrimEnd();

                bool isParentKey = KeyConfigerFactroy.Instance.IsParentRegisterKey(tempStr);

                //  读到下一个父节点 = 继续
                if (isParentKey)
                {
                    ParentKey findKey = KeyConfigerFactroy.Instance.CreateParentKey<ParentKey>(tempStr);
                    findKey.BaseFile = this.BaseFile;
                    findKey.ParentKey = this.ParentKey;
                    this.BaseFile.Key.Keys.Add(findKey);
                    findKey.ReadKeyLine(reader);

                }
                else
                {

                    bool isChildKey = KeyConfigerFactroy.Instance.IsChildRegisterKey(tempStr);
                    //  读取子节点 = 增加子节点
                    if (isChildKey)
                    {
                        BaseKey findKey = KeyConfigerFactroy.Instance.CreateChildKey<BaseKey>(tempStr);
                        findKey.BaseFile = this.BaseFile;
                        findKey.ParentKey = this;
                        this.Keys.Add(findKey);
                        //  调用子节点读取方法
                        findKey.ReadKeyLine(reader);
                    }
                    else
                    {
                        bool isIncludeKey = KeyConfigerFactroy.Instance.IsINCLUDERegisterKey(tempStr);

                        if (isIncludeKey)
                        {
                            INCLUDE includeKey = KeyConfigerFactroy.Instance.CreateIncludeKey<INCLUDE>(tempStr);

                            includeKey.BaseFile = this.BaseFile;
                            this.Keys.Add(includeKey);
                            includeKey.ParentKey = this;
                            includeKey.ReadKeyLine(reader);
                        }

                        else if (tempStr.IsKeyFormat())
                        {
                            UnkownKey findKey = new UnkownKey(KeyChecker.FormatKey(tempStr));
                            findKey.BaseFile = this.BaseFile;
                            findKey.ParentKey = this;
                            //  触发事件
                            if (findKey.BaseFile != null && findKey.BaseFile.OnUnkownKey != null)
                            {
                                findKey.BaseFile.OnUnkownKey(findKey.BaseFile, findKey);
                            }
                            this.Keys.Add(findKey);
                            //  调用子节点读取方法
                            findKey.ReadKeyLine(reader);
                        }
                        else
                        {

                            //  过滤空行
                            if (tempStr.IsWorkLine())
                            {
                                //  不是子节点信息 = 增加到本节点信息中
                                this.Lines.Add(tempStr);
                            }

                        }

                    }

                }
            }

            return null;

        }


        public override void WriteKey(System.IO.StreamWriter writer)
        {
            writer.WriteLine(this.Name);
            writer.WriteLine();

            //base.WriteKey(writer);

            //  写子关键字
            foreach (BaseKey key in this.Keys)
            {
                key.WriteKey(writer);
            }
        }

        string filePath = string.Empty;
        /// <summary> 获取Include文件路径 </summary>
        public string FilePath
        {
            get
            {
                return filePath;
            }
        }

        string fileName = string.Empty;
        /// <summary> 文件名称 </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public override string ToString()
        {
            return "Parent:" + this.Name;
        }

   
    }
}
