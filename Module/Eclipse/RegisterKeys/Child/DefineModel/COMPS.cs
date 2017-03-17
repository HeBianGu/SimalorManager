#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 13:39:53
 * 文件名：COORD
 * 说明：
 组分个数	COMPS	设置油藏中的组分数	COMPS
   2 /

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
    /// <summary> 组分个数 </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class COMPS : Key
    {
        /// <summary> 组分个数 </summary>
        public COMPS(string _name)
            : base(_name)
        {

        }

        string format = "{0} /";

        string zfgs = "2";
        [DisplayName("组分个数"), DescriptionAttribute("设置油藏中的组分数"), CategoryAttribute("基本信息"), ReadOnly(true)]
        public string Zfgs
        {
            get { return zfgs; }
            set { zfgs = value; }
        }

        public override string ToString()
        {
            return string.Format(format, zfgs.ToDD());
        }
    }
}
