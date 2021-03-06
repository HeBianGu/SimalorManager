﻿#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/1 13:39:53

 * 说明：
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;  

namespace HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 平衡计算选项 </summary>
     
    public class EQLOPTS : ConfigerKey
    {
        public EQLOPTS(string _name)
            : base(_name)
        {

        }

        private string thpres0 = "THPRES";
        /// <summary> 开启门槛压力选项，如果设置了断层开启压力的话，也需要设置该参数进行开启 </summary>
        public string Phfqs0
        {
            get { return thpres0; }
            set { thpres0 = value; }
        }

        private string irrevers1 = "IRREVERS";
        /// <summary> 允许每个方向的门槛压力不同，因此需要为每个方向设置启动压力 </summary>
        public string Ylsdbzds1
        {
            get { return irrevers1; }
            set { irrevers1 = value; }
        }


        string formatStr = "{0}{1} /";
        /// <summary> 转换成字符串 </summary>
        public override string ToString()
        {
            return string.Format(formatStr, thpres0.ToD(), irrevers1.ToD());
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
                        this.thpres0 = newStr[0];
                        break;
                    case 1:
                        this.irrevers1 = newStr[1];
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
