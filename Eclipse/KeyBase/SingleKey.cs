#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/18 11:28:21
 * 文件名：SingleKey
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    /// <summary> 只有名称没有内容的关键字 </summary>
    public abstract class SingleKey : BaseKey, OutPutBindKey
    {
        public SingleKey(string _name)
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
                                //throw new ArgumentException(this.Name + "关键字解析错误，错误内容，无内容关键字读取到了内容！" + tempStr);
                                //  不是子节点信息 = 增加到本节点信息中
                                //this.Lines.Add();
                            }
                        }

                    }

                }
            }

            return null;

        }


        public override void WriteKey(System.IO.StreamWriter writer)
        {
            //  是否输出
            if (isCheck)
            {
                writer.WriteLine(this.Name);
                writer.WriteLine(writer.NewLine);
                base.WriteKey(writer);
            }
        }

        public bool isCheck = true;

        public object IsCheck
        {
            get
            {
                return isCheck;
            }
            set
            {
                //  是否等于1
                bool r = false ;

                if (value==null)
                {
                    isCheck = false;
                    return;
                }
                if (bool.TryParse(value.ToString(), out r))
                {
                    isCheck = r;
                }
                else
                {
                    isCheck = r;
                }
            }
        }

        public string OutName
        {
            get
            {
               return this.Name;
            }
            set
            {
                this.Name=value;
            }
        }
    }
}
