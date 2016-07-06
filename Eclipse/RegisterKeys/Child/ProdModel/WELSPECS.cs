#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/2 10:38:01
 * 文件名：START
 * 说明：
 * ROCK
             0            11
/
 * 
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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child
{
    /// <summary> 井定义 </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Include, IsBigDataKey = true)]
    public class WELSPECS : ItemsKey<WELSPECS.Item>, IWelspecslDefine
    {
        public WELSPECS(string _name)
            : base(_name)
        {

        }

        public Item GetSingleItem
        {
            get
            {
                if (this.Items.Count == 0)
                {
                    Item item = new Item();
                    this.Items.Add(item);
                    return item;

                }
                else
                {
                    return this.Items[0];
                }
            }
        }

        public class Item : OPT.Product.SimalorManager.Item
        {

            /// <summary> 井类型 </summary>
            public WellType WELSPECSType
            {
                get;
                set;
            }

            /// <summary> 井名 </summary>
            public string jm0 = "新增";
            /// <summary> 井组 </summary>
            public string jz1;
            /// <summary> 网格i </summary>
            public string i3;
            /// <summary> 网格j </summary>
            public string j4;
            /// <summary> 参考深度 </summary>
            public string cksd5;
            /// <summary> 优先流体相 </summary>
            public string yxltx6 = "OIL";
            /// <summary> 泄流半径 </summary>
            public string xlbj7;
            /// <summary> 流动方程 </summary>
            public string ldfc8 = "STD";
            /// <summary> 自动关井 STOP SHUT </summary>
            public string zdgj9 = "SHUT";
            /// <summary> 窜流 </summary>
            public string cl10 = "YES";
            /// <summary> PVT表编号 </summary>
            public string pvtbh11;
            /// <summary> 密度计算方法 </summary>
            public string mdjsff12 = "SEG";
            /// <summary> 储量分区编号 </summary>
            public string clfqbh13;
            /// <summary> 井模型 </summary>
            public string jmx14;

            string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}/";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, jm0.ToEclStr(), jz1.ToEclStr(), i3.ToDD(), j4.ToDD(), cksd5.ToDD(), yxltx6.ToEclStr(), xlbj7.ToDD(), ldfc8.ToEclStr(), zdgj9.ToEclStr(), cl10.ToEclStr(), pvtbh11.ToDD(), mdjsff12.ToEclStr(), clfqbh13.ToDD(), jmx14.ToDD());
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
                            this.jm0 = newStr[0];
                            break;
                        case 1:
                            this.jz1 = newStr[1];
                            break;
                        case 2:
                            this.i3 = newStr[2];
                            break;
                        case 3:
                            this.j4 = newStr[3];
                            break;
                        case 4:
                            this.cksd5 = newStr[4];
                            break;
                        case 5:
                            this.yxltx6 = newStr[5];
                            break;
                        case 6:
                            this.xlbj7 = newStr[6];
                            break;
                        case 7:
                            this.ldfc8 = newStr[7];
                            break;
                        case 8:
                            this.zdgj9 = newStr[8];
                            break;
                        case 9:
                            this.cl10 = newStr[9];
                            break;
                        case 10:
                            this.pvtbh11 = newStr[10];
                            break;
                        case 11:
                            this.mdjsff12 = newStr[11];
                            break;
                        case 12:
                            this.clfqbh13 = newStr[12];
                            break;
                        case 13:
                            this.jmx14 = newStr[13];
                            break;
                        default:
                            break;
                    }
                }
            }



            public string Name
            {
                get
                {
                    return jm0;
                }
                set
                {
                    jm0 = value;
                }
            }
        }



        public IEnumerable<WELSPECS.Item> GetAllItems()
        {
            return this.Items;
        }




        public WellType GetWellType()
        {
          return  this.GetSingleItem().WELSPECSType;
        }
    }

    /// <summary> 井类型 </summary>
    public enum WellType
    {
        [DescriptionAttribute("生产井")]
        WCONPROD = 0,
        [DescriptionAttribute("注入井")]
        WCONINJE = 1
    }

    /// <summary> 局部网格加密井定义 </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Include, IsBigDataKey = true)]
    public class WELSPECL : ItemsKey<WELSPECL.Item>, IWelspecslDefine
    {
        public WELSPECL(string _name)
            : base(_name)
        {

        }

        public Item GetSingleItem
        {
            get
            {
                if (this.Items.Count == 0)
                {
                    Item item = new Item();
                    this.Items.Add(item);
                    return item;

                }
                else
                {
                    return this.Items[0];
                }
            }
        }

        public class Item : WELSPECS.Item
        {

            /// <summary> 局部加密网格名 </summary>
            public string jbwgjmm;

            string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}/";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, jm0.ToEclStr(), jz1.ToEclStr(), jbwgjmm.ToEclStr(), i3.ToDD(), j4.ToDD(), cksd5.ToDD(), yxltx6.ToEclStr(), xlbj7.ToDD(), ldfc8.ToEclStr(), zdgj9.ToEclStr(), cl10.ToEclStr(), pvtbh11.ToDD(), mdjsff12.ToEclStr(), clfqbh13.ToDD(), jmx14.ToDD());
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
                            this.jm0 = newStr[0];
                            break;
                        case 1:
                            this.jz1 = newStr[1];
                            break;
                        case 2:
                            this.jbwgjmm = newStr[2];
                            break;
                        case 3:
                            this.i3 = newStr[3];
                            break;
                        case 4:
                            this.j4 = newStr[4];
                            break;
                        case 5:
                            this.cksd5 = newStr[5];
                            break;
                        case 6:
                            this.yxltx6 = newStr[6];
                            break;
                        case 7:
                            this.xlbj7 = newStr[7];
                            break;
                        case 8:
                            this.ldfc8 = newStr[9];
                            break;
                        case 9:
                            this.zdgj9 = newStr[9];
                            break;
                        case 10:
                            this.cl10 = newStr[10];
                            break;
                        case 11:
                            this.pvtbh11 = newStr[11];
                            break;
                        case 12:
                            this.mdjsff12 = newStr[12];
                            break;
                        case 13:
                            this.clfqbh13 = newStr[13];
                            break;
                        case 14:
                            this.jmx14 = newStr[14];
                            break;
                        default:
                            break;
                    }
                }
            }



            public string Name
            {
                get
                {
                    return jm0;
                }
                set
                {
                    jm0 = value;
                }
            }
        }

        public IEnumerable<WELSPECS.Item> GetAllItems()
        {
            return this.Items; 
        }
        public WellType GetWellType()
        {
            return this.GetSingleItem().WELSPECSType;
        }
    }


    /// <summary> 井定义接口 用于提取井定义 </summary>
    public interface IWelspecslDefine
    {
        /// <summary> IEnumerable支持协变 </summary>
        IEnumerable<WELSPECS.Item> GetAllItems();

        WellType GetWellType();

    }


}
