#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/1 13:39:53

 * 说明：
 *  界面参数	描述	关键字	备注
1	最大井数，默认值0/10	WELLDIMS	
2	每口井的最大连接数，默认值0/模型层数		
3	最大井组数，默认值0/5		
4	每个井组包含的最大井数，默认值0/10		
6	最大井内蒸汽数，仅E300，默认值10		
7	最大混合物数，仅E300，默认值5		

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
    /// <summary> 维数定义 </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class WELLDIMS : ConfigerKey
    {
        public WELLDIMS(string _name)
            : base(_name)
        {

        }

        /// <summary> 最大井数，默认值0/10 </summary>
        private string zdjks0 = "0";
        [DisplayName("1.最大井数"), DescriptionAttribute("最大井数，默认值0/10"), CategoryAttribute("基本信息")]
        public string Zdjks0
        {
            get { return zdjks0; }
            set { zdjks0 = value; }
        }
        /// <summary> 每口井的最大连接数，默认值0/模型层数 </summary>
        private string zdljs1 = "0";
        [DisplayName("2.每口井的最大连接数"), DescriptionAttribute("每口井的最大连接数，默认值0/模型层数"), CategoryAttribute("基本信息")]
        public string Zdljs1
        {
            get { return zdljs1; }
            set { zdljs1 = value; }
        }
        /// <summary> 最大井组数，默认值0/5 </summary>
        private string zdjzs2 = "0";
        [DisplayName("3.最大井组数"), DescriptionAttribute("最大井组数，默认值0/5"), CategoryAttribute("基本信息")]
        public string Zdjzs2
        {
            get { return zdjzs2; }
            set { zdjzs2 = value; }
        }
        /// <summary> 每个井组包含的最大井数，默认值0/10 </summary>
        private string zdjs3 = "0";
        [DisplayName("4.每个井组包含的最大井数"), DescriptionAttribute("每个井组包含的最大井数，默认值0/10"), CategoryAttribute("基本信息")]
        public string Zdjs3
        {
            get { return zdjs3; }
            set { zdjs3 = value; }
        }
        /// <summary> 最大井内蒸汽数，仅E300，默认值10 </summary>
        private string zdzqs4 = "10";
        [DisplayName("5.最大井内蒸汽数"), DescriptionAttribute("最大井内蒸汽数，仅E300，默认值10 "), CategoryAttribute("基本信息")]
        public string Zdzqs4
        {
            get { return zdzqs4; }
            set { zdzqs4 = value; }
        }
        /// <summary> 最大混合物数，仅E300，默认值5 </summary>
        private string zdhhws5 = "5";
        [DisplayName("6.最大混合物数"), DescriptionAttribute("最大混合物数，仅E300，默认值5"), CategoryAttribute("基本信息")]
        public string Zdhhws5
        {
            get { return zdhhws5; }
            set { zdhhws5 = value; }
        }


        string formatStr = "{0}{1}{2}{3}{4}{5} /";
        /// <summary> 转换成字符串 </summary>
        public override string ToString()
        {
            return string.Format(formatStr, zdjks0.ToDD(), zdljs1.ToDD(), zdjzs2.ToDD(), zdjs3.ToDD(), zdzqs4.ToDD(), zdhhws5.ToDD());
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
                        this.zdjks0 = newStr[0];
                        break;
                    case 1:
                        this.zdljs1 = newStr[1];
                        break;
                    case 2:
                        this.zdjzs2 = newStr[2];
                        break;
                    case 3:
                        this.zdjs3 = newStr[3];
                        break;
                    case 4:
                        this.zdzqs4 = newStr[4];
                        break;
                    case 5:
                        this.zdhhws5 = newStr[5];
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
