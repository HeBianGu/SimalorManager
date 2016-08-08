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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 裂缝数据 </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Include)]
    public class WFRACP : ItemsKey<WFRACP.Item>,IProductEvent
    {
        public WFRACP(string _name)
            : base(_name)
        {

        }


        public void SetWellName(string wellName)
        {
            this.Items.ForEach(l => l.Name = wellName);
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

        protected override void CmdGetWellItems()
        {

            ClearItem();

            string str = string.Empty;

            for (int i = 0; i < Lines.Count; i++)
            {
                str = Lines[i];
                //  读到结束符不继续读取
                if (str.StartsWith("/") && str.EndsWith("/"))
                {
                    break;
                }

                //  不为空的行
                if (str.IsWorkLine())
                {
                    List<string> newStr = str.EclExceptSpaceToArray();

                    if (newStr.Count > 0)
                    {
                        WFRACP.Item pitem = new WFRACP.Item();
                        pitem.Build(newStr);
                        //  标记行的ID位置
                        //Lines[i] = pitem.ID;
                        if (pitem != null)
                        {
                            Items.Add(pitem);
                        }
                    }
                }

            }
        }

        public class Item : OPT.Product.SimalorManager.Item,IProductItem
        {

            /// <summary> 井名 </summary>
            public string jm0 ;

            /// <summary> 网格i1 </summary>
            public string wgiy1;

            /// <summary> 网格j1 </summary>
            public string wgjy2;

            /// <summary> 网格k1 </summary>
            public string wgky3;

            /// <summary> 网格i2 </summary>
            public string wgie4;

            /// <summary> 网格j2 </summary>
            public string wgje5;

            /// <summary> 网格k2 </summary>
            public string wgke6;

            /// <summary> 方位角 </summary>
            public string fwj7;

            /// <summary> 倾角 </summary>
            public string qj8;

            /// <summary> 裂缝左半长L1 </summary>
            public string lfzbc9;

            /// <summary> 裂缝右半长L2 </summary>
            public string lfybc10;

            /// <summary> 裂缝上缝高H1 </summary>
            public string sfg11;

            /// <summary> 裂缝下缝高H2 </summary>
            public string xfg12;

            /// <summary> 裂缝宽度 </summary>
            public string kd13;
            
            /// <summary> 支撑剂名 </summary>
            public string zcjm14;

            /// <summary> 流动函数 </summary>
            public string ldhs15;

            /// <summary> 相或者时间 </summary>
            public string xhzsj16="OIL";

            /// <summary> 裂缝产能乘子 </summary>
            public string cncz17;

            /// <summary> X1 </summary>
            public string xy18;

            /// <summary> Y1 </summary>
            public string yy19;

            /// <summary> Z1 </summary>
            public string zy20;

            /// <summary> X2 </summary>
            public string xe21;

            /// <summary> Y2 </summary>
            public string ye22;

            /// <summary> Z2 </summary>
            public string ze23;

            /// <summary> 支撑剂体积 </summary>
            public string zcjtj24;

            string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}/";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, jm0.ToEclStr(), this.wgiy1.ToDD(), this.wgjy2.ToDD(),
                    this.wgky3.ToDD(), this.wgie4.ToDD(), this.wgje5.ToDD(), this.wgke6.ToDD(),
                    this.fwj7.ToDD(), this.qj8.ToDD(), this.lfzbc9.ToDD(), this.lfybc10.ToDD(),
                    this.sfg11.ToDD(), this.xfg12.ToDD(), this.kd13.ToDD(), this.zcjm14.ToEclStr(), this.ldhs15.ToEclStr(),
                     this.xhzsj16.ToEclStr(), this.cncz17.ToDD(), this.xy18.ToDD(), this.yy19.ToDD(), this.zy20.ToDD(),
                 this.xe21.ToDD(), this.ye22.ToDD(), this.ze23.ToDD(), this.zcjtj24.ToDD());
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
                            this.wgiy1 = newStr[1];
                            break;
                        case 2:
                            this.wgjy2 = newStr[2];
                            break;
                        case 3:
                            this.wgky3 = newStr[3];
                            break;
                        case 4:
                            this.wgie4 = newStr[4];
                            break;
                        case 5:
                            this.wgje5 = newStr[5];
                            break;
                        case 6:
                            this.wgke6 = newStr[6];
                            break;
                        case 7:
                            this.fwj7 = newStr[7];
                            break;
                        case 8:
                            this.qj8 = newStr[8];
                            break;
                        case 9:
                            this.lfzbc9 = newStr[9];
                            break;
                        case 10:
                            this.lfybc10 = newStr[10];
                            break;
                        case 11:
                            this.sfg11 = newStr[11];
                            break;
                        case 12:
                            this.xfg12 = newStr[12];
                            break;
                        case 13:
                            this.kd13 = newStr[13];
                            break;
                        case 14:
                            this.zcjm14 = newStr[14];
                            break;
                        case 15:
                            this.ldhs15 = newStr[15];
                            break;
                        case 16:
                            this.xhzsj16 = newStr[16];
                            break;
                        case 17:
                            this.cncz17 = newStr[17];
                            break;
                        case 18:
                            this.xy18 = newStr[18];
                            break;
                        case 19:
                            this.yy19 = newStr[19];
                            break;
                        case 20:
                            this.zy20 = newStr[20];
                            break;
                        case 21:
                            this.xe21 = newStr[21];
                            break;
                        case 22:
                            this.ye22 = newStr[22];
                            break;
                        case 23:
                            this.ze23 = newStr[23];
                            break;
                        case 24:
                            this.zcjtj24 = newStr[24];
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
                    return this.jm0;
                }
                set
                {
                    this.jm0=value;
                }
            }
        }
    }
}
