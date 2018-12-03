#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 13:39:53
 * 说明：
界面参数	描述	关键字	备注
1	数值水体数据行最大数	AQUDIMS	默认值1
2	数值水体连接数据行最大数		默认值1
3	CT水体影响函数表最大数		默认值1
4	CT水体每个影响函数表的最大行数		默认值36
5	解析水体最大数		默认值1
6	E100：连接单个解析水体的网格最大数
E300: 解析水体连接数据行最大数		默认值1
7	水体列表最大数		默认值0
8	单个水体列表中解析水体最大个数		默认值0

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
    /// <summary> 水体维数定义 </summary>
     
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class AQUDIMS : ConfigerKey
    {
        public AQUDIMS(string _name)
            : base(_name)
        {

        }

        /// <summary> 数值水体数据行最大数	AQUDIMS	默认值1 </summary>
        public string szstzdhs0 = "1";
        [CategoryAttribute("基本信息"), DescriptionAttribute("数值水体数据行最大数  默认值1"), DisplayName("1.数值水体数据行最大数"), ReadOnly(true)]
        public string Szstzdhs0
        {
            get { return szstzdhs0; }
            set { szstzdhs0 = value; }
        }
        /// <summary> 数值水体连接数据行最大数		默认值1 </summary>
        private string szstljs1 = "1";
        [CategoryAttribute("基本信息"), DescriptionAttribute("数值水体连接数据行最大数		默认值1"), DisplayName("2.数值水体连接数据行最大数"), ReadOnly(true)]
        public string Szstljs1
        {
            get { return szstljs1; }
            set { szstljs1 = value; }
        }
        /// <summary> CT水体影响函数表最大数		默认值1 </summary>
        private string ctstyxhs2 = "1";
        [CategoryAttribute("基本信息"), DescriptionAttribute("CT水体影响函数表最大数		默认值1"), DisplayName("3.水体影响函数表最大数"), ReadOnly(true)]
        public string Ctstyxhs2
        {
            get { return ctstyxhs2; }
            set { ctstyxhs2 = value; }
        }
        /// <summary> CT水体每个影响函数表的最大行数		默认值36 </summary>
        private string ctstyxzdhs3 = "36";
        [CategoryAttribute("基本信息"), DescriptionAttribute("CT水体每个影响函数表的最大行数		默认值36"), DisplayName("4.CT水体每个影响函数表的最大行数"), ReadOnly(true)]
        public string Ctstyxzdhs3
        {
            get { return ctstyxzdhs3; }
            set { ctstyxzdhs3 = value; }
        }
        /// <summary> 解析水体最大数		默认值1 </summary>
        private string jxstzds4 = "1";
        [CategoryAttribute("基本信息"), DescriptionAttribute("解析水体最大数		默认值1 "), DisplayName("5.解析水体最大数"), ReadOnly(true)]
        public string Jxstzds4
        {
            get { return jxstzds4; }
            set { jxstzds4 = value; }
        }
        /// <summary> E100：连接单个解析水体的网格最大数 </summary>
        private string e100wgzds5 = "1";
        [CategoryAttribute("基本信息"), DescriptionAttribute("E100：连接单个解析水体的网格最大数"), DisplayName("6.E100：连接单个解析水体的网格最大数"), ReadOnly(true)]
        public string E100wgzds5
        {
            get { return e100wgzds5; }
            set { e100wgzds5 = value; }
        }
        /// <summary> E300: 解析水体连接数据行最大数		默认值1 </summary>
        private string e300jxstzds6 = "1";
        [CategoryAttribute("基本信息"), DescriptionAttribute(" E300: 解析水体连接数据行最大数		默认值1"), DisplayName("7.E300: 解析水体连接数据行最大数"), ReadOnly(true)]
        public string E300jxstzds6
        {
            get { return e300jxstzds6; }
            set { e300jxstzds6 = value; }
        }
        /// <summary> 水体列表最大数		默认值0 </summary>
        private string stlbzds7 = "0";
        [CategoryAttribute("基本信息"), DescriptionAttribute("水体列表最大数		默认值0"), DisplayName("8.水体列表最大数"), ReadOnly(true)]
        public string Stlbzds7
        {
            get { return stlbzds7; }
            set { stlbzds7 = value; }
        }
        /// <summary> 单个水体列表中解析水体最大个数		默认值0 </summary>
        private string jxstzds8 = "0";
        [CategoryAttribute("基本信息"), DescriptionAttribute("单个水体列表中解析水体最大个数		默认值0"), DisplayName("9.单个水体列表中解析水体最大个数"), ReadOnly(true)]
        public string Jxstzds8
        {
            get { return jxstzds8; }
            set { jxstzds8 = value; }
        }

        string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7} /";
        /// <summary> 转换成字符串 </summary>
        public override string ToString()
        {
            return string.Format(formatStr, szstzdhs0.ToDD(), szstljs1.ToDD(), ctstyxhs2.ToDD(), ctstyxzdhs3.ToDD(), jxstzds4.ToDD(),
                e100wgzds5.ToDD(), e300jxstzds6.ToDD(), stlbzds7.ToDD(), jxstzds8.ToDD());
        }

        /// <summary> 解析字符串 </summary>
        public override  void Build(List<string> newStr)
        {
            this.ID = Guid.NewGuid().ToString();

            for (int i = 0; i < newStr.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        this.szstzdhs0 = newStr[0];
                        break;
                    case 1:
                        this.szstljs1 = newStr[1];
                        break;
                    case 2:
                        this.ctstyxhs2 = newStr[2];
                        break;
                    case 3:
                        this.ctstyxzdhs3 = newStr[3];
                        break;
                    case 4:
                        this.jxstzds4 = newStr[4];
                        break;
                    case 5:
                        this.e100wgzds5 = newStr[5];
                        break;
                    case 6:
                        this.e300jxstzds6 = newStr[6];
                        break;
                    case 7:
                        this.stlbzds7 = newStr[7];
                        break;
                    case 8:
                        this.jxstzds8 = newStr[8];
                        break;

                    default:
                        break;
                }
            }
        }

    }
}
