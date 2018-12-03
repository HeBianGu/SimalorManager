#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/1 13:39:53 

 * 说明：
界面参数	描述	关键字	备注
1	平衡分区数	EQLDIMS	
2	压力VS深度表的深度段最大数		默认值100/50
3	在RSVD、RVVD、PBVD、PDVD、RTEMPVD、RSWVD中深度数据点的最大数		默认值20/50
4	TVDP表的最大数		默认值1
5	在TVDP中深度数据点的最大数		默认值20/50
	

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
    /// <summary> 平衡表维数定义 </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class EQLDIMS : ConfigerKey
    {
        public EQLDIMS(string _name)
            : base(_name)
        {

        }

        /// <summary> 平衡分区数 </summary>
        private string phfqs0="1";
        [CategoryAttribute("基本信息"), DescriptionAttribute("平衡分区数"), DisplayName("1.平衡分区数"), ReadOnly(true)]
        public string Phfqs0
        {
            get { return phfqs0; }
            set { phfqs0 = value; }
        }
        /// <summary> 压力VS深度表的深度段最大数		默认值100/50 </summary>
        private string ylsdbzds1 = "100";
        [CategoryAttribute("基本信息"), DescriptionAttribute("压力VS深度表的深度段最大数		默认值100/50"), DisplayName("2.压力VS深度表的深度段最大数"), ReadOnly(true)]
        public string Ylsdbzds1
        {
            get { return ylsdbzds1; }
            set { ylsdbzds1 = value; }
        }
        /// <summary> 在RSVD、RVVD、PBVD、PDVD、RTEMPVD、RSWVD中深度数据点的最大数		默认值20/50 </summary>
        private string sdsjdzds2 = "20";
        [CategoryAttribute("基本信息"), DescriptionAttribute("在RSVD、RVVD、PBVD、PDVD、RTEMPVD、RSWVD中深度数据点的最大数		默认值20/50"), DisplayName("3.在RSVD、RVVD、PBVD、PDVD、RTEMPVD、RSWVD中深度数据点的最大数"), ReadOnly(true)]
        public string Sdsjdzds2
        {
            get { return sdsjdzds2; }
            set { sdsjdzds2 = value; }
        }
        /// <summary> TVDP表的最大数		默认值1</summary>
        private string tvdpzds3 = "1";
        [CategoryAttribute("基本信息"), DescriptionAttribute("TVDP表的最大数		默认值1"), DisplayName("4.TVDP表的最大数"), ReadOnly(true)]
        public string Tvdpzds3
        {
            get { return tvdpzds3; }
            set { tvdpzds3 = value; }
        }
        /// <summary> 在TVDP中深度数据点的最大数		默认值20/50 </summary>
        private string tvdpsdsjdzds4 = "20";
        [CategoryAttribute("基本信息"), DescriptionAttribute("在TVDP中深度数据点的最大数		默认值20/50"), DisplayName("5.在TVDP中深度数据点的最大数"), ReadOnly(true)]
        public string Tvdpsdsjdzds4
        {
            get { return tvdpsdsjdzds4; }
            set { tvdpsdsjdzds4 = value; }
        }


        string formatStr = "{0}{1}{2}{3}{4} /";
        /// <summary> 转换成字符串 </summary>
        public override string ToString()
        {
            return string.Format(formatStr, phfqs0.ToDD(), ylsdbzds1.ToDD(), sdsjdzds2.ToDD(), tvdpzds3.ToDD(), tvdpsdsjdzds4.ToDD());
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
                        this.phfqs0 = newStr[0];
                        break;
                    case 1:
                        this.ylsdbzds1 = newStr[1];
                        break;
                    case 2:
                        this.sdsjdzds2 = newStr[2];
                        break;
                    case 3:
                        this.tvdpzds3 = newStr[3];
                        break;
                    case 4:
                        this.tvdpsdsjdzds4 = newStr[4];
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
