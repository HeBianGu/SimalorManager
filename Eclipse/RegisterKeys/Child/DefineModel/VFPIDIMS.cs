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
using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child
{
    /// <summary> 生产井VFP表维数定义 </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Include, IsBigDataKey = true)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class VFPIDIMS : ConfigerKey
    {
        public VFPIDIMS(string _name)
            : base(_name)
        {

        }

        string llzzdgs0 = "0";
        /// <summary> 流量值的最大个数 </summary>
        [DisplayName("1.流量值的最大个数"), DescriptionAttribute("流量值的最大个数	默认值0/1"), CategoryAttribute("基本信息")]
        public string Llzzdgs0
        {
            get { return llzzdgs0; }
            set { llzzdgs0 = value; }
        }
        string jkylzdge1 = "0";
        /// <summary> 井口压力的最大个数 </summary>
        [DisplayName("2.井口压力的最大个数"), DescriptionAttribute("井口压力的最大个数	默认值0/1"), CategoryAttribute("基本信息")]
        public string Jkylzdge1
        {
            get { return jkylzdge1; }
            set { jkylzdge1 = value; }
        }
        string jdlyzdgs2 = "0";
        /// <summary> 井底流压的最大个数 </summary>
        [DisplayName("3.井底流压的最大个数"), DescriptionAttribute("井底流压的最大个数	默认值0/1"), CategoryAttribute("基本信息")]
        public string Jdlyzdgs2
        {
            get { return jdlyzdgs2; }
            set { jdlyzdgs2 = value; }
        }


        string formatStr = "{0}{1}{2} /";
        /// <summary> 转换成字符串 </summary>
        public override string ToString()
        {
            return string.Format(formatStr, this.llzzdgs0.ToD(), this.jkylzdge1.ToD(), this.jdlyzdgs2.ToD());
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
                        this.llzzdgs0 = newStr[0];
                        break;
                    case 1:
                        this.jkylzdge1 = newStr[1];
                        break;
                    case 2:
                        this.jdlyzdgs2 = newStr[2];
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
