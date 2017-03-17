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
using OPT.Product.SimalorManager.RegisterKeys.Eclipse;
//using OPT.Product.SimalorManager.Eclipse.RegisterKeys.INCLUDE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    /// <summary> 八种父关键字节点基类 </summary>
    public class ParentKey : BaseKey, IStandardized
    {
        public ParentKey(string _name)
            : base(_name)
        {
        }

        public override void WriteKey(System.IO.StreamWriter writer)
        {
            writer.WriteLine();
            writer.WriteLine(this.Name);

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


        /// <summary> 格式化关键字格式 </summary>
        public virtual void Standardized()
        {
            //  构建INCLUDE
            string filename = this.BaseFile.FileName.GetFileNameWithoutExtension();

            INCLUDE include = new INCLUDE("INCLUDE");

            string tempExtend = this.Name.Substring(0, 3);

            include.FileName = string.Format("{0}_{1}.INC", filename, tempExtend);

            include.BaseFile = this.BaseFile;

            //  将所有关键字合并到一个INCLUDE中
            List<BaseKey> allKeys = IncludeKeyService.Instance.ConvertToInclue(this.Keys);

            include.AddRange<BaseKey>(allKeys);

            //  清理原有节点
            this.Clear();

            //  增加INLCUDE
            this.Add(include);
        }

    }
}
