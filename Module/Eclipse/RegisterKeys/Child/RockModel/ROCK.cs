#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/2 10:38:01

 * 说明：


/
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Product.SimalorManager.Base.AttributeEx;
using HeBianGu.Product.SimalorManager.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 岩石特性 </summary>
    public class ROCK : RegionKey<ROCK.Item>
    {
        public ROCK(string _name)
            : base(_name)
        {

        }

        public class Item: HeBianGu.Product.SimalorManager.ItemNormal
        {
            /// <summary> 参考压力 </summary>
            public string ckyl;
            /// <summary> 压缩系数 </summary>
            public string ysxs;

           string formatStr = "{0}{1}";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                if (EngineConfigerService.Instance.SaveKeyTypeLock == SimKeyType.Eclipse)
                {
                    return string.Format(formatStr, ckyl.ToSaveLockDD(), ysxs.ToSaveLockDD());
                }
                else if (EngineConfigerService.Instance.SaveKeyTypeLock == SimKeyType.SimON)
                {
                    // HTodo  ：因为内核目前不支持NA作为这些参数的默认值且水相PVT不同油田数据很接近，以上默认值参考了Eclipse。岩石压缩性质ROCK 关键字也做类似处理，参考压力1.0132（公制，bar）及14.7（英制，psi），岩石压缩系数0（公英制单位都是）。
 
                    string ckyl_temp = null;
                    string ysxs_temp = null;

                    if (EngineConfigerService.Instance.Unittype==UnitType.METRIC)
                    {
                        ckyl_temp = ckyl.ToSDD().Contains(KeyConfiger.SimONDefalt) ? "1.0132" : ckyl;
                    }
                    else
                    {
                        ckyl_temp = ckyl.ToSDD().Contains(KeyConfiger.SimONDefalt) ? "14.7" : ckyl;
                    }

                    ysxs_temp = ysxs.ToSDD().Contains(KeyConfiger.SimONDefalt) ? "0" : ysxs;

                    return string.Format(formatStr, ckyl_temp.ToSaveLockDD(), ysxs_temp.ToSaveLockDD());
                }
                else
                {
                    return string.Format(formatStr, ckyl.ToSaveLockDD(), ysxs.ToSaveLockDD());
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
                            this.ckyl = newStr[0];
                            break;
                        case 1:
                            this.ysxs = newStr[1];
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
                    ckyl = this.ckyl,
                    ysxs = this.ysxs
                };
                return item;
            }
        }
    }
}
