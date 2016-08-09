#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 13:39:53
 * 文件名：COORD
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 文件名称 </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Include, IsBigDataKey = true)]
    public class TITLE : ConfigerKey
    {
        public TITLE(string _name)
            : base(_name)
        {
            //  指定当前名称始终不是关键字
            this.Match += l => false;
        }
        string fileName = "Case";
        [CategoryAttribute("基本信息"), DescriptionAttribute("案例文件名称"), DisplayName("文件名称")]
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        public override string ToString()
        {
            return FileName;
        }


        public override void Build(List<string> newStr)
        {
            for (int i = 0; i < newStr.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        this.fileName = newStr[0];
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
