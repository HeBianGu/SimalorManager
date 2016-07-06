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

namespace OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child
{
    /// <summary> 水相PVT </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Include, IsBigDataKey = true)]
    public class PVTW : RegionKey<PVTW.Item>
    {
        public PVTW(string _name)
            : base(_name)
        {

        }

        public class Item: OPT.Product.SimalorManager.ItemNormal
        {
           /// <summary> 参考压力 </summary>
           public double ckyl0;
           /// <summary> 水体积系数 </summary>
           public double stjxs1;
           /// <summary> 水压缩系数 </summary>
           public double syxxs2;
            /// <summary> 水粘度 </summary>
           public double snd3;
           /// <summary> 水粘度压缩系数 </summary>
           public double sndysxs4;



           string formatStr = "{0}{1}{2}{3}{4} " ;

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, ckyl0.ToString().ToDD(), stjxs1.ToString().ToDD(), syxxs2.ToString().ToDD(), snd3.ToString().ToDD(), sndysxs4.ToString().ToDD());
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
                            this.ckyl0 = newStr[0].ToDouble();
                            break;
                        case 1:
                            this.stjxs1 = newStr[1].ToDouble();
                            break;
                        case 2:
                            this.syxxs2 = newStr[2].ToDouble();
                            break;
                        case 3:
                            this.snd3 = newStr[3].ToDouble();
                            break;
                        case 4:
                            this.sndysxs4 = newStr[4].ToDouble();
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
                    ckyl0=this.ckyl0,
                    stjxs1=this.stjxs1,
                    syxxs2=this.syxxs2,
                    snd3=this.snd3,
                    sndysxs4=this.sndysxs4
                };

                return item;
            }
        }
    }
}
