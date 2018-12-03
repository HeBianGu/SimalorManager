#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/27 13:24:46
 * 说明：使用方法 = 传递ECLIPSE主文件名，将关键字读写到树形类型内存中操作
 *       调用Save()方法存储文件
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse;
using HeBianGu.Product.SimalorManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HeBianGu.Product.SimalorManager.Eclipse.FileInfos
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
                using (StreamReader streamRead = new StreamReader(fileStream, System.Text.Encoding.Default))
                {
                    while (!streamRead.EndOfStream)
                    {
                        this.Key.ReadKeyLine(streamRead);
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
        public override void SaveAsExtend(string pathName)
        {
            using (FileStream fileStream = new FileStream(pathName, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter streamWrite = new StreamWriter(fileStream, System.Text.Encoding.Default))
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
