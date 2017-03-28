#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 13:39:53

 * 说明：
示例：
GRIDUNIT
METRES MAP /

 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Eclipse.FileInfos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 模型维数 </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class GRIDUNIT : ConfigerKey
    {
        public GRIDUNIT(string _name)
            : base(_name)
        {

        }

        /// <summary> 网格数据单位 </summary>
        public string wgsjdw0;

        /// <summary> 坐标匹配标识 </summary>
        public string zbppbz1;

        /// <summary> 是否使用相对坐标 </summary>
        public bool IsUseMap
        {
            get
            {
                return string.IsNullOrEmpty(zbppbz1);
            }
        }


        string formatStr = "{0}{1} /";

        public override string ToString()
        {
            return string.Format(formatStr, wgsjdw0.ToD(), zbppbz1.ToD());
        }
        /// <summary> 解析字符串 </summary>
        public override void Build(List<string> newStr)
        {
            this.ID = Guid.NewGuid().ToString();

            for (int i = 0; i < newStr.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        this.wgsjdw0 = newStr[0];
                        break;
                    case 1:
                        this.zbppbz1 = newStr[1];
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
