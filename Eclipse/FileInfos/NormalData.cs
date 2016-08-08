#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/27 13:24:46
 * 文件名：EclipseData
 * 说明：使用方法 = 传递ECLIPSE主文件名，将关键字读写到树形类型内存中操作
 *       调用Save()方法存储文件
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child;
using OPT.Product.SimalorManager.Eclipse.RegisterKeys.INCLUDE;
using OPT.Product.SimalorManager.Eclipse.RegisterKeys.Parent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OPT.Product.SimalorManager.Eclipse.FileInfos
{
    /// <summary> ECLIPSE主文件操作类 </summary>
    public class NormalData : BaseFile
    {
        public NormalData()
        {

        }

        public NormalData(string _filePath)
            : base(_filePath)
        {

        }

        public NormalData(string _filePath, WhenUnkownKey UnkownEvent)
            : base(_filePath, UnkownEvent)
        {

        }

        /// <summary> 初始化类(树形结构) </summary>
        protected override void InitializeComponent()
        {
            string tempStr = string.Empty;
            //  打开子文件并读取子文件关键字内容
            using (FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamRead = new StreamReader(fileStream, Encoding.Default))
                {
                    while (!streamRead.EndOfStream)
                    {
                        tempStr = streamRead.ReadLine().TrimEnd();

                        bool isParenRegister = KeyConfigerFactroy.Instance.IsParentRegisterKey(tempStr);
                        //  读到了父节点
                        if (isParenRegister)
                        {
                            ParentKey findkey = KeyConfigerFactroy.Instance.CreateParentKey<ParentKey>(tempStr);
                            this.Key.Keys.Add(findkey);
                            findkey.BaseFile = this;
                            findkey.ParentKey = this.Key;
                            findkey.ReadKeyLine(streamRead);
                        }
                        else
                        {
                            bool isChildRegister = KeyConfigerFactroy.Instance.IsChildRegisterKey(tempStr);

                            if (isChildRegister)
                            {

                                //  读到下一关注关键字终止
                                BaseKey TempKey = KeyConfigerFactroy.Instance.CreateChildKey<BaseKey>(tempStr);
                                this.Key.Keys.Add(TempKey);
                                TempKey.BaseFile = this;
                                TempKey.ParentKey = this.Key;
                                TempKey.ReadKeyLine(streamRead);

                            }
                            else
                            {

                                //  普通关键字下面可能存在INCLUDE关键字
                                bool isIncludeKey = KeyConfigerFactroy.Instance.IsINCLUDERegisterKey(tempStr);

                                if (isIncludeKey)
                                {
                                    INCLUDE includeKey = KeyConfigerFactroy.Instance.CreateIncludeKey<INCLUDE>(tempStr);
                                    includeKey.BaseFile = this;
                                    this.Key.Add(includeKey);
                                    includeKey.ParentKey = this.Key;
                                    includeKey.ReadKeyLine(streamRead);
                                }
                                else
                                {
                                    if (tempStr.IsKeyFormat())
                                    {
                                        //  添加普通关键字
                                        Key normalKey = new Key(KeyChecker.FormatKey(tempStr));
                                        normalKey.BaseFile = this;
                                        normalKey.ParentKey = this.Key;
                                        this.Lines.Add(tempStr);
                                        normalKey.ReadKeyLine(streamRead);
                                    }
                                    else
                                    {
                                        this.Lines.Add(tempStr);
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }

        /// <summary> 保存 </summary>
        public override void Save()
        {
            this.SaveAs(this.FilePath);
        }

        /// <summary> 写入文件 文件全路径 </summary>
        public override void SaveAs(string pathName)
        {
            using (FileStream fileStream = new FileStream(pathName, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter streamWrite = new StreamWriter(fileStream, Encoding.Default))
                {
                    //  读写备注
                    foreach (var str in Lines)
                    {
                        streamWrite.WriteLine(str);
                    }

                    //  写关键字
                    this.Key.Keys.ForEach(l => l.WriteKey(streamWrite));

                }
            }
        }



    }

}
