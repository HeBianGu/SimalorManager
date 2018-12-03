#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/1 13:39:53

 * 说明：
界面参数	描述	关键字	备注
1	每个表流动值的最大数	VFPPDIMS	默认值0/1
2	每个表油管压力的最大数		默认值0/1
3	每个表持液率的最大数		默认值0/1
4	每个表持气率的最大数		默认值0/1
5	每个表人工举升量的最大数		默认值0/1
6	生产井VFP表最大数		默认值0/1

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
    /// <summary> 生产井VFP表维数定义 </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class VFPPDIMS : ConfigerKey
    {
        public VFPPDIMS(string _name)
            : base(_name)
        {

        }

        /// <summary> 每个表流动值的最大数	VFPPDIMS	默认值0/1 </summary>
        private string bldzzds0 = "0";
        [DisplayName("1.每个表流动值的最大数"), DescriptionAttribute("每个表流动值的最大数	VFPPDIMS	默认值0/1"), CategoryAttribute("基本信息"), ReadOnly(true)]
        public string Bldzzds0
        {
            get { return bldzzds0; }
            set { bldzzds0 = value; }
        }
        /// <summary> 每个表油管压力的最大数		默认值0/1 </summary>
        private string bygylzds1 = "0";
        [DisplayName("2.每个表油管压力的最大数"), DescriptionAttribute("每个表油管压力的最大数		默认值0/1"), CategoryAttribute("基本信息"), ReadOnly(true)]
        public string Bygylzds1
        {
            get { return bygylzds1; }
            set { bygylzds1 = value; }
        }
        /// <summary> 每个表持液率的最大数		默认值0/1 </summary>
        private string bcylzds2 = "0";
        [DisplayName("3.每个表持液率的最大数"), DescriptionAttribute("每个表持液率的最大数		默认值0/1"), CategoryAttribute("基本信息"), ReadOnly(true)]
        public string Bcylzds2
        {
            get { return bcylzds2; }
            set { bcylzds2 = value; }
        }
        /// <summary> 每个表持气率的最大数		默认值0/1 </summary>
        private string bcqlzds3 = "0";
        [DisplayName("4.每个表持气率的最大数"), DescriptionAttribute("每个表持气率的最大数		默认值0/1"), CategoryAttribute("基本信息"), ReadOnly(true)]
        public string Bcqlzds3
        {
            get { return bcqlzds3; }
            set { bcqlzds3 = value; }
        }
        /// <summary> 每个表人工举升量的最大数		默认值0/1 </summary>
        private string brgjslzds4 = "0";
        [DisplayName("5.每个表人工举升量的最大数"), DescriptionAttribute("每个表人工举升量的最大数		默认值0/1"), CategoryAttribute("基本信息"), ReadOnly(true)]
        public string Brgjslzds4
        {
            get { return brgjslzds4; }
            set { brgjslzds4 = value; }
        }
        /// <summary> 生产井VFP表最大数		默认值0/1 </summary>
        private string vfpzds5 = "0";
        [DisplayName("6.生产井VFP表最大数"), DescriptionAttribute("生产井VFP表最大数		默认值0/1"), CategoryAttribute("基本信息"), ReadOnly(true)]
        public string Vfpzds5
        {
            get { return vfpzds5; }
            set { vfpzds5 = value; }
        }


        string formatStr = "{0}{1}{2}{3}{4}{5} /";
        /// <summary> 转换成字符串 </summary>
        public override string ToString()
        {
            return string.Format(formatStr, bldzzds0.ToDD(), bygylzds1.ToDD(), bcylzds2.ToDD(), bcqlzds3.ToDD(), brgjslzds4.ToDD(), vfpzds5.ToDD());
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
                        this.bldzzds0 = newStr[0];
                        break;
                    case 1:
                        this.bygylzds1 = newStr[1];
                        break;
                    case 2:
                        this.bcylzds2 = newStr[2];
                        break;
                    case 3:
                        this.bcqlzds3 = newStr[3];
                        break;
                    case 4:
                        this.brgjslzds4 = newStr[4];
                        break;
                    case 5:
                        this.vfpzds5 = newStr[5];
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
