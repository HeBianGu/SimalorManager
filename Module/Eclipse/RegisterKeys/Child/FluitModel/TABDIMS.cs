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
1	饱和度表数	TABDIMS	默认值1
2	PVT表数		默认值1
3	每个饱和度表中饱和度数据的最大数		默认值20/50
4	每个PVT表中压力数据的最大数		默认值20/50
5	FIP分区最大数		默认值1
6	在PVTO、PVCO和PVTG中，Rs数据点的最大数		默认值20
7	在PVTG中，Rv数据点的最大数		默认值20
8	饱和度端点VS深度表的最大数		默认值1
9	油藏条件的EOS分区数		默认值1
10	地面条件的EOS分区最大数		
12	热采分区最大数		默认值1
13	岩石类型个数		
15	K值表的最大数		

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
    /// <summary> 表维数定义 </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Include, IsBigDataKey = true)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class TABDIMS : ConfigerKey
    {
        public TABDIMS(string _name)
            : base(_name)
        {

        }

        /// <summary> 饱和度表数	TABDIMS	默认值1 </summary>
        private int bhdbs0 = 1;
         [DisplayName("1.饱和度表数"), DescriptionAttribute("饱和度表数	TABDIMS	默认值1"), CategoryAttribute("基本信息"), ReadOnly(false)]
        public int Bhdbs0
        {
            get { return bhdbs0; }
            set { bhdbs0 = value; }
        }
        /// <summary> PVT表数		默认值1 </summary>
        private int pvtbs1 = 1;
          [DisplayName("2.PVT表数"), DescriptionAttribute("PVT表数		默认值1"), CategoryAttribute("基本信息"), ReadOnly(false)]
        public int Pvtbs1
        {
            get { return pvtbs1; }
            set { pvtbs1 = value; }
        }
        /// <summary> 每个饱和度表中饱和度数据的最大数		默认值20/50 </summary>
        private string bhdsjzds2 = "20";
          [DisplayName("3.每个饱和度表中饱和度数据的最大数"), DescriptionAttribute("每个饱和度表中饱和度数据的最大数		默认值20/50"), CategoryAttribute("基本信息"), ReadOnly(false)]
        public string Bhdsjzds2
        {
            get { return bhdsjzds2; }
            set { bhdsjzds2 = value; }
        }
        /// <summary> 每个PVT表中压力数据的最大数		默认值20/50 </summary>
        private string bhdzds3 = "20";
          [DisplayName("4.每个PVT表中压力数据的最大数"), DescriptionAttribute("每个PVT表中压力数据的最大数		默认值20/50"), CategoryAttribute("基本信息"), ReadOnly(false)]
        public string Bhdzds3
        {
            get { return bhdzds3; }
            set { bhdzds3 = value; }
        }
        /// <summary> FIP分区最大数		默认值1 </summary>
        private string fipfqzds4 = "1";
          [DisplayName("5.FIP分区最大数"), DescriptionAttribute("FIP分区最大数		默认值1"), CategoryAttribute("基本信息"), ReadOnly(false)]
        public string Fipfqzds4
        {
            get { return fipfqzds4; }
            set { fipfqzds4 = value; }
        }
        /// <summary> 在PVTO、PVCO和PVTG中，Rs数据点的最大数		默认值20 </summary>
        private string oogrvsjdzds5 = "20";
          [DisplayName("6.在PVTO、PVCO和PVTG中，Rs数据点的最大数"), DescriptionAttribute("在PVTO、PVCO和PVTG中，Rs数据点的最大数		默认值20"), CategoryAttribute("基本信息"), ReadOnly(false)]
        public string Oogrvsjdzds5
        {
            get { return oogrvsjdzds5; }
            set { oogrvsjdzds5 = value; }
        }
        /// <summary> 在PVTG中，Rv数据点的最大数		默认值20 </summary>
        private string grvsjdzds6 = "20";
          [DisplayName("7.在PVTG中，Rv数据点的最大数"), DescriptionAttribute("在PVTG中，Rv数据点的最大数		默认值20"), CategoryAttribute("基本信息"), ReadOnly(false)]
        public string Grvsjdzds6
        {
            get { return grvsjdzds6; }
            set { grvsjdzds6 = value; }
        }
        /// <summary> 饱和度端点VS深度表的最大数		默认值1 </summary>
        private string vssdbzds7 = "1";
          [DisplayName("8.饱和度端点VS深度表的最大数"), DescriptionAttribute("饱和度端点VS深度表的最大数		默认值1"), CategoryAttribute("基本信息"), ReadOnly(false)]
        public string Vssdbzds7
        {
            get { return vssdbzds7; }
            set { vssdbzds7 = value; }
        }
        /// <summary> 油藏条件的EOS分区数		默认值1 </summary>
        private string eosfqs8 = "1";
          [DisplayName("9.油藏条件的EOS分区数"), DescriptionAttribute("油藏条件的EOS分区数		默认值1"), CategoryAttribute("基本信息"), ReadOnly(false)]
        public string Eosfqs8
        {
            get { return eosfqs8; }
            set { eosfqs8 = value; }
        }
        /// <summary> 地面条件的EOS分区最大数	 </summary>
        private string eoszds9;
          [DisplayName("10.地面条件的EOS分区最大数"), DescriptionAttribute("地面条件的EOS分区最大数"), CategoryAttribute("基本信息"), ReadOnly(false)]
        public string Eoszds9
        {
            get { return eoszds9; }
            set { eoszds9 = value; }
        }

          /// <summary> 流量分区个数		默认值1 </summary>
          private string ltfqgs10= "1";
          [DisplayName("11.流量分区个数"), DescriptionAttribute("流量分区个数		默认值1"), CategoryAttribute("基本信息"), ReadOnly(false)]
          public string Ltfqgs10
          {
              get { return rcfqzds11; }
              set { rcfqzds11 = value; }
          }

          /// <summary> 热采分区个数		默认值1 </summary>
        private string rcfqzds11 = "1";
          [DisplayName("11.热采分区个数"), DescriptionAttribute("热采分区最大数		默认值1"), CategoryAttribute("基本信息"), ReadOnly(false)]
        public string Rcfqzds11
        {
            get { return rcfqzds11; }
            set { rcfqzds11 = value; }
        }
          /// <summary> 岩石分区个数 </summary>
        private string yslxgs12="1";
          [DisplayName("12.岩石分区个数"), DescriptionAttribute("岩石类型个数"), CategoryAttribute("基本信息"), ReadOnly(false)]
        public string Yslxgs12
        {
            get { return yslxgs12; }
            set { yslxgs12 = value; }
        }
          /// <summary> 压力保持分区个数 </summary>
        private string ylbcfqgs13;
          [DisplayName("13.压力保持分区个数"), DescriptionAttribute("压力保持分区个数"), CategoryAttribute("基本信息"), ReadOnly(false)]
        public string Ylbcfqgs13
        {
            get { return ylbcfqgs13; }
            set { ylbcfqgs13 = value; }
        }
          /// <summary> 相平衡常数K值表中温度的个数</summary>
          private string xphcskzbzwddgs14;
          [DisplayName("13.相平衡常数K值表中温度的个数"), DescriptionAttribute("相平衡常数K值表中温度的个数"), CategoryAttribute("基本信息"), ReadOnly(false)]
          public string Xphcskzbzwddgs14
          {
              get { return xphcskzbzwddgs14; }
              set { xphcskzbzwddgs14 = value; }
          }



          string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13} /";
        /// <summary> 转换成字符串 </summary>
        public override string ToString()
        {
            return string.Format(formatStr, bhdbs0.ToD(), pvtbs1.ToD(), bhdsjzds2.ToDD(), bhdzds3.ToDD(), fipfqzds4.ToDD(),
                oogrvsjdzds5.ToDD(), grvsjdzds6.ToDD(), vssdbzds7.ToDD(), eosfqs8.ToDD(), eoszds9.ToDD(),eoszds9.ToDD(), rcfqzds11.ToDD(),
                yslxgs12.ToDD(), ylbcfqgs13.ToDD(), xphcskzbzwddgs14.ToDD());
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
                        this.bhdbs0 = newStr[0].ToInt();
                        break;
                    case 1:
                        this.pvtbs1 = newStr[1].ToInt();
                        break;
                    case 2:
                        this.bhdsjzds2 = newStr[2];
                        break;
                    case 3:
                        this.bhdzds3 = newStr[3];
                        break;
                    case 4:
                        this.fipfqzds4 = newStr[4];
                        break;
                    case 5:
                        this.oogrvsjdzds5 = newStr[5];
                        break;
                    case 6:
                        this.grvsjdzds6 = newStr[6];
                        break;
                    case 7:
                        this.vssdbzds7 = newStr[7];
                        break;
                    case 8:
                        this.eosfqs8 = newStr[8];
                        break;
                    case 9:
                        this.eoszds9 = newStr[9];
                        break;
                    case 10:
                        this.ltfqgs10 = newStr[10];
                        break;
                    case 11:
                        this.rcfqzds11 = newStr[11];
                        break;
                    case 12:
                        this.yslxgs12 = newStr[12];
                        break;
                    case 13:
                        this.ylbcfqgs13 = newStr[12];
                        break;
                    case 14:
                        this.xphcskzbzwddgs14 = newStr[12];
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
