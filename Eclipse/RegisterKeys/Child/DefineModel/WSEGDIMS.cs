#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 13:39:53
 * 文件名：COORD
 * 说明：

界面参数	描述	关键字	备注
1	最大多段井数	WSEGDIMS	默认值0
2	每口井的最大段数		默认值1
3	每个多段井的最大分支数		默认值1
4	每口井的段连接的最大数		默认值0

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

namespace OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child
{
    /// <summary> 多段井维数定义 </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Include, IsBigDataKey = true)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class WSEGDIMS : ConfigerKey
    {
        public WSEGDIMS(string _name)
            : base(_name)
        {

        }

        /// <summary> 最大多段井数	WSEGDIMS	默认值0 </summary>
        private string zdddjs0 = "0";
        [CategoryAttribute("基本信息"), DescriptionAttribute("最大多段井数	WSEGDIMS	默认值0"), DisplayName("1.最大多段井数"), ReadOnly(true)]
        public string Zdddjs0
        {
            get { return zdddjs0; }
            set { zdddjs0 = value; }
        }
        /// <summary> 每口井的最大段数		默认值1 </summary>
        private string zdds1 = "1";
        [CategoryAttribute("基本信息"), DescriptionAttribute("每口井的最大段数		默认值1"), DisplayName("2.每口井的最大段数"), ReadOnly(true)]
        public string Zdds1
        {
            get { return zdds1; }
            set { zdds1 = value; }
        }
        /// <summary> 每个多段井的最大分支数		默认值1 </summary>
        private string zdfzs2 = "1";
        [CategoryAttribute("基本信息"), DescriptionAttribute("每个多段井的最大分支数		默认值1"), DisplayName("3.每个多段井的最大分支数"), ReadOnly(true)]
        public string Zdfzs2
        {
            get { return zdfzs2; }
            set { zdfzs2 = value; }
        }
        /// <summary> 每口井的段连接的最大数		默认值0</summary>
        private string zdljs3 = "0";
        [CategoryAttribute("基本信息"), DescriptionAttribute("每口井的段连接的最大数		默认值0"), DisplayName("4.每口井的段连接的最大数"), ReadOnly(true)]
        public string Zdljs3
        {
            get { return zdljs3; }
            set { zdljs3 = value; }
        }


        string formatStr = "{0}{1}{2}{3} /";
        /// <summary> 转换成字符串 </summary>
        public override string ToString()
        {
            return string.Format(formatStr, zdddjs0.ToDD(), zdds1.ToDD(), zdfzs2.ToDD(), zdljs3.ToDD());
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
                        this.zdddjs0 = newStr[0];
                        break;
                    case 1:
                        this.zdds1 = newStr[1];
                        break;
                    case 2:
                        this.zdfzs2 = newStr[2];
                        break;
                    case 3:
                        this.zdljs3 = newStr[3];
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
