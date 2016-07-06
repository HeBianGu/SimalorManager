#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/26 15:59:00
 * 文件名：INCLUDE
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Base.AttributeEx;
using OPT.Product.SimalorManager.Eclipse.FileInfos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.Eclipse.RegisterKeys.INCLUDE
{
    /// <summary> INCLUDE 关键字 </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Include)]
    public class INCLUDE : BaseKey
    {
        public INCLUDE(string _name)
            : base(_name)
        {

        }

        string format = @"'{0}'  /";
        public override string ToString()
        {
            //if (this.Lines.Count >= 1)
            //{
            //    string[] split = this.Lines[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //    return split[0].Trim('\'');
            //}
            //else
            //{
            //    return fileName;
            //}
            return "INCLUDE:" + this.fileName;
        }

        string filePath = string.Empty;
        /// <summary> 文件全路径 包含文件名 </summary>
        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
            }
        }

        string fileName = string.Empty;
        /// <summary> 文件名称 </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        bool isCreateFile = true;
        /// <summary> 保存时是否创建文件 </summary>
        public bool IsCreateFile
        {
            get { return isCreateFile; }
            set { isCreateFile = value; }
        }

        /// <summary> 读取关键字内容的方法 = 打开INCLUDE的文件并读取子文件关键字 </summary>
        public override BaseKey ReadKeyLine(System.IO.StreamReader reader)
        {
            string strTemp = string.Empty;

            //  查找INCLUDE文件
            while (!reader.EndOfStream)
            {
                strTemp = reader.ReadLine().TrimEnd();

                if (strTemp.IsWorkLine())
                {
                    //  加载文件名
                    fileName = strTemp.Split(new char[] { '\'', '/' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    //  加载文件路径
                    filePath = BaseFile.FilePath.GetFileFullPathEx(fileName);
                    break;
                }
            }

            if (!this.BaseFile.IsReadIclude)
                return null;


            this.ReadFromStream();

            return null;

        }


        /// <summary> 从文件中创建INCLUDE  </summary>
        public static INCLUDE LoadFromFile(string pfilePath)
        {
            INCLUDE include = new INCLUDE("INCLUDE");
            include.fileName = Path.GetFileName(pfilePath);
            include.filePath = pfilePath;
            include.ReadFromStream();
            return include;
        }

        /// <summary> 写入文件 </summary>
        public void WriteToFile(string newFile)
        {
            using (FileStream fileStream = new FileStream(newFile, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter newWriter = new StreamWriter(fileStream))
                {
                    //  写子关键字
                    foreach (BaseKey key in this.Keys)
                    {
                        key.WriteKey(newWriter);
                    }
                }
            }
        }

        public void ReadFromStream()
        {
            string strTemp = string.Empty;

            //  打开子文件并读取子文件关键字内容
            using (FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamRead = new StreamReader(fileStream))
                {
                    while (!streamRead.EndOfStream)
                    {

                        strTemp = streamRead.ReadLine().TrimEnd();

                        bool isRegister = KeyConfigerFactroy.Instance.IsChildRegisterKey(strTemp);

                        //  注册的关键字
                        if (isRegister)
                        {
                            BaseKey pKey = KeyConfigerFactroy.Instance.CreateChildKey<BaseKey>(strTemp);
                            pKey.BaseFile = this.BaseFile;
                            pKey.ParentKey = this;
                            this.Keys.Add(pKey);
                            pKey.ReadKeyLine(streamRead);

                        }
                        else
                        {
                            if (strTemp.IsKeyFormat())
                            {
                                //  添加普通关键字
                                UnkownKey normalKey = new UnkownKey(KeyChecker.FormatKey(strTemp));
                                normalKey.ParentKey = this;
                                normalKey.BaseFile = this.BaseFile;

                                //  触发事件
                                if (normalKey.BaseFile != null && normalKey.BaseFile.OnUnkownKey != null)
                                {
                                    normalKey.BaseFile.OnUnkownKey(normalKey.BaseFile, normalKey);
                                }
                                this.Keys.Add(normalKey);
                                normalKey.ReadKeyLine(streamRead);
                            }
                            else
                            {

                                if (strTemp.IsWorkLine())
                                {
                                    this.Lines.Add(strTemp);
                                }

                            }

                        }
                    }
                }
            }
        }

        /// <summary> 写入关键字内容的方法 = 创建新文件将子节点写入子文件中 </summary>
        public override void WriteKey(StreamWriter writer)
        {
            //  写主文件
            writer.WriteLine(this.Name);
            writer.WriteLine(string.Format(format, this.fileName.GetFileName()));
            writer.WriteLine();

            if (!IsCreateFile)
                return;

            //  写入子文件
            FileStream stream = writer.BaseStream as FileStream;

            //  记录新文件路径
            string newFile = stream.Name.GetFileFullPathEx(this.fileName);

            bool isExist = this.Keys.Exists(l => l is BigDataKey);

            //  存在且不读取大数据 = 拷贝文件
            if (isExist && !this.BaseFile.IsReadBigData)
            {
                CopyFileTo(newFile);
            }
            else
            {

                WriteToFile(newFile);

            }



        }

        /// <summary> 拷贝INCLUDE文件 = 必须进行写操作才会有新文件路径 </summary>
        private void CopyFileTo(string newPath)
        {
            string newFile = newPath.GetFileFullPathEx(this.fileName);

            string oldFile = this.BaseFile.FilePath.GetFileFullPathEx(this.fileName);

            if (!newFile.Equals(oldFile))
            {
                File.Copy(oldFile, newPath, true);
            }
        }

        /// <summary> 利用数模文件异步创建指定大小栈的内存模型 </summary>
        public static INCLUDE ThreadLoadFromFile(string pfilePath, int stactSize = 4194304)
        {
            INCLUDE include = new INCLUDE("INCLUDE");

            return INCLUDE.ThreadLoadFromFile(include, pfilePath, stactSize);
        }

        /// <summary> 利用数模文件异步创建指定大小栈的内存模型 </summary>
        public static INCLUDE ThreadLoadFromFile(INCLUDE include, string pfilePath, int stactSize = 4194304)
        {
            include.FileName = Path.GetFileName(pfilePath);
            include.FilePath = pfilePath;

            Thread thread = new Thread(() => include.ReadFromStream(), stactSize);// 4mb栈

            thread.Start();

            while (true)
            {
                if (thread.ThreadState == ThreadState.Stopped)
                {
                    break;
                }
            }
            return include;
        }
    }
}
