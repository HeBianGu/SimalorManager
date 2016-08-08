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
using OPT.Product.SimalorManager.RegisterKeys.Eclipse;
using OPT.Product.SimalorManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace OPT.Product.SimalorManager
{
    /// <summary> ECLIPSE主文件操作类 </summary>
    public class PetrelData : BaseFile
    {
        public PetrelData()
        {

        }

        public PetrelData(string _filePath)
            : base(_filePath)
        {

        }

        public PetrelData(string _filePath, WhenUnkownKey UnkownEvent, bool isReadInclud = true)
            : base(_filePath, UnkownEvent, isReadInclud)
        {

        }

        INCLUDE include;

        /// <summary> 初始化类(树形结构) </summary>
        protected override void InitializeComponent()
        {
            INCLUDE inclue = new INCLUDE("INCLUDE");

            inclue.BaseFile = this;

            include = INCLUDE.ThreadLoadFromFile(inclue, FilePath);

            this.Key.Add(include);

        }

        /// <summary> 保存 </summary>
        public override void Save()
        {
            this.SaveAs(this.FilePath);
        }

        /// <summary> 写入文件 文件全路径 </summary>
        public override void SaveAs(string pathName)
        {

            include.WriteToFile(pathName);

        }

    }

}
