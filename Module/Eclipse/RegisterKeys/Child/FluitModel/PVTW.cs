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
using OPT.Product.SimalorManager.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 水相PVT </summary>
    public class PVTW : RegionKey<PVTW.Item>
    {
        public PVTW(string _name)
            : base(_name)
        {

        }

        public class Item : OPT.Product.SimalorManager.ItemNormal
        {
            /// <summary> 参考压力 </summary>
            public string ckyl0;
            /// <summary> 水体积系数 </summary>
            public string stjxs1;
            /// <summary> 水压缩系数 </summary> 
            public string syxxs2;
            /// <summary> 水粘度 </summary>
            public string snd3;
            /// <summary> 水粘度压缩系数 </summary>
            public string sndysxs4;


            string formatStr = "{0}{1}{2}{3}{4} ";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                if (EngineConfigerService.Instance.SaveKeyTypeLock == SimKeyType.Eclipse)
                {
                    return string.Format(formatStr, ckyl0.ToSaveLockDD(), stjxs1.ToSaveLockDD(), syxxs2.ToSaveLockDD(), snd3.ToSaveLockDD(), sndysxs4.ToSaveLockDD());

                }
                else if (EngineConfigerService.Instance.SaveKeyTypeLock == SimKeyType.SimON)
                {

                    // HTodo  ：水相PVT，导入Eclipse关键字1*或界面为空时，后台保存文件的PVTW关键字默认值设置，水体积系数1.0（公英制单位都是），压缩系数0.00004（公制，1/bar）及0.000003（英制，1/psi），粘度0.5（公英制单位都是），粘度压缩系数0（公英制单位都是）。ROCK类似处理见下 
                    string stjxs1_temp = null;
                    string syxxs2_temp = null;
                    string snd3_temp = null;
                    string sndysxs4_temp = null;

                    //ckyl0  = string.IsNullOrEmpty(ckyl0) ?"1" : ckyl0;
                    stjxs1_temp = stjxs1.ToSDD().Contains(KeyConfiger.SimONDefalt) ? "1" : stjxs1;

                    if (EngineConfigerService.Instance.Unittype == UnitType.METRIC)
                    {
                        syxxs2_temp = syxxs2.ToSDD().Contains(KeyConfiger.SimONDefalt) ? "0.00004" : syxxs2;
                    }
                    else
                    {
                        syxxs2_temp = syxxs2.ToSDD().Contains(KeyConfiger.SimONDefalt) ? "0.00003" : syxxs2;
                    }
                    snd3_temp = snd3.ToSDD().Contains(KeyConfiger.SimONDefalt) ? "0.5" : snd3;
                    sndysxs4_temp = sndysxs4.ToSDD().Contains(KeyConfiger.SimONDefalt) ? "0" : sndysxs4;

                    return string.Format(formatStr, ckyl0.ToSaveLockDD(), stjxs1_temp.ToSaveLockDD(), syxxs2_temp.ToSaveLockDD(), snd3_temp.ToSaveLockDD(), sndysxs4_temp.ToSaveLockDD());

                }
                else
                {
                    return string.Format(formatStr, ckyl0.ToSaveLockDD(), stjxs1.ToSaveLockDD(), syxxs2.ToSaveLockDD(), snd3.ToSaveLockDD(), sndysxs4.ToSaveLockDD());

                }
            }



            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

                for (int i = 0; i < newStr.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.ckyl0 = newStr[0];
                            break;
                        case 1:
                            this.stjxs1 = newStr[1];
                            break;
                        case 2:
                            this.syxxs2 = newStr[2];
                            break;
                        case 3:
                            this.snd3 = newStr[3];
                            break;
                        case 4:
                            this.sndysxs4 = newStr[4];
                            break;
                        default:
                            break;
                    }
                }
            }



            public override object Clone()
            {
                Item item = new Item()
                {
                    ckyl0 = this.ckyl0,
                    stjxs1 = this.stjxs1,
                    syxxs2 = this.syxxs2,
                    snd3 = this.snd3,
                    sndysxs4 = this.sndysxs4
                };

                return item;
            }
        }
    }
}
