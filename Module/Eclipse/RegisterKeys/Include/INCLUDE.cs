#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/11/26 15:59:00

 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Product.SimalorManager.Base.AttributeEx;
using HeBianGu.Product.SimalorManager.Eclipse.FileInfos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> INCLUDE 关键字 </summary>
    public class INCLUDE : BaseKey, IRootNode
    {
        public INCLUDE(string _name)
            : base(_name)
        {
            this.BuilderHandler += (l, k) =>
                {
                    return this;
                };
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

            int index = this.Name.Trim().IndexOf(' ');


            if (index > 0)
            {
                string nameValues = this.Name.Substring(index).Trim();

                //  过滤第一条信息
                this.Lines.Add(nameValues);

                this.Lines.RemoveAll(l => !l.IsWorkLine());

                if (this.Lines.Count > 0)
                {
                    //  加载文件名
                    //fileName = this.Lines[0].Split(new char[] { '\'', '/' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    fileName = strTemp.Trim(new char[] { '\'', '/', ' ' });
                    //  加载文件路径
                    filePath = Path.GetDirectoryName(this.BaseFile.FilePath) + "\\" + fileName.Trim();

                    //  不读取INCLUDE文件
                    if (!this.BaseFile.IsReadIncHandle(this)) return this;

                    this.ReadFromStream();

                    return this;
                }
            }

            //  从下一行有效数据读取
            while (!reader.EndOfStream)
            {
                strTemp = reader.ReadLine().TrimEnd();

                if (strTemp.IsWorkLine())
                {
                    //  加载文件名
                    //fileName = strTemp.Split(new char[] { '\'', '/' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    fileName = strTemp.Trim(new char[] { '\'', '/', ' ' });
                    //  加载文件路径
                    filePath = Path.GetDirectoryName(this.BaseFile.FilePath) + "\\" + fileName.Trim();
                    break;
                }
            }

            //  不读取INCLUDE文件
            if (!this.BaseFile.IsReadIncHandle(this)) return this;

            this.ReadFromStream();

            return this;

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
                using (StreamWriter newWriter = new StreamWriter(fileStream, KeyConfiger.SimONEncoder))
                {
                    newWriter.WriteLine(this.FielDetail);
                    //  写子关键字
                    foreach (BaseKey key in this.Keys)
                    {
                        key.WriteKey(newWriter);
                    }
                }
            }
        }

        /// <summary> 说明 </summary>
        public string FielDetail
        {
            get
            {
                var keys = this.FindAll<BaseKey>();
                //  删除本节点
                keys.Remove(this);

                //  查找所有类型的名称
                //List<string> names = keys.Select(l => l.GetType().Name.Trim()).ToList();

                var group = keys.GroupBy(l => l.GetType().Name.Trim());

                List<string> names= group.Select(l => l.First().GetType().Name + "(" + l.Count() + ")").ToList();

                StringBuilder sb = new StringBuilder();

                string version = string.Empty;
                try
                {
                    version = Assembly.GetEntryAssembly().GetName().Version.ToString();
                }
                catch (Exception)
                {
                    
                }
                  

                //  添加到文本中
                names.ForEach(l => sb.Append(l.ToD()));

                if (this.BaseFile != null)
                {
                    return string.Format(KeyConfiger.IncludeFileDetial, this.fileName, this.FilePath, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), this.BaseFile.FilePath, sb.ToString(), version);

                }
                else
                {
                    return string.Format(KeyConfiger.IncludeFileDetial, this.fileName, this.FilePath, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), string.Empty, sb.ToString(), version);

                }

            }

            //get
            //{
            //    return string.Empty;
            //}
        }

        /// <summary> 从流中读取文件 </summary>
        public void ReadFromStream()
        {
            string strTemp = string.Empty;

            if (string.IsNullOrEmpty(Path.GetExtension(this.FilePath)))
            {
                this.FilePath = this.FilePath.Trim() + KeyConfiger.SimONExtend;
            }


            //  打开子文件并读取子文件关键字内容
            using (FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                //using (StreamReader streamRead = new StreamReader(fileStream, System.Text.Encoding.GetEncoding("GB2312")))
                using (StreamReader streamRead = new StreamReader(fileStream, System.Text.Encoding.Default))
                {
                    while (!streamRead.EndOfStream)
                    {
                        //  直接调用基类读取方法
                        base.ReadKeyLine(streamRead);
                    }
                }
            }

            if (this.Keys.Count == 0) return;

            //  触发最后一个关键字的构造
            BaseKey lastKey = this.Keys.Last();

            if (lastKey.BuilderHandler != null)
            {
                lastKey.BuilderHandler(lastKey, lastKey);
            }

        }

        /// <summary> 写入关键字内容的方法 = 创建新文件将子节点写入子文件中 </summary>
        public override void WriteKey(StreamWriter writer)
        {
            //  写主文件
            writer.WriteLine();
            writer.WriteLine(this.Name);

            if (this.BaseFile != null && this.BaseFile.SimKeyType == SimKeyType.SimON)
            {
                //  SimON的INCLUDE
                writer.WriteLine(string.Format("'{0}'", this.fileName.GetFileName()));
            }
            else
            {
                writer.WriteLine(string.Format(format, this.fileName.GetFileName()));
            }


            ////  不读取INCLUDE文件
            //if (!this.BaseFile.IsReadIclude) return;

            if (!IsCreateFile) return;

            //  写入子文件
            FileStream stream = writer.BaseStream as FileStream;

            //  记录新文件路径
            string newFile = stream.Name.GetFileFullPathEx(this.fileName);

            //bool isExist = this.Keys.Exists(l => l is BigDataKey);

            ////  存在且不读取大数据 = 拷贝文件
            //if (isExist && !this.BaseFile.IsReadBigData)
            //{
            //    CopyFileTo(newFile);
            //}
            //else
            //{

            WriteToFile(newFile);

            //}



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

        /// <summary> 保存成文件 </summary>
        public void Save()
        {
            this.WriteToFile(this.filePath);
        }

        public List<string> GetChildKeys()
        {
            return null;
        }
    }
}
