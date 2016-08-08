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
    /// <summary> 气相PVT </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Include, IsBigDataKey = true)]
    public class PVTG : RegionKey<PVTG.Item>
    {
        public PVTG(string _name)
            : base(_name)
        {

        }
        /// <summary> 获取项字符串集合 </summary>
        protected override void CmdGetWellLines()
        {
            string s = string.Empty;

            this.Lines.Clear();

            for (int i = 0; i < Regions.Count; i++)
            {
                for (int j = 0; j < Regions[i].Count; j++)
                {
                    //  查找下一个关键字 如果溶解气油比为空则不添加结束符
                    if (j < Regions[i].Count - 1)
                    {
                        s = Regions[i][j + 1].ldyl0.ToString();

                        if (string.IsNullOrEmpty(s))
                        {
                            s = string.Empty;
                        }
                        else
                        {
                            s = KeyConfiger.EndFlag;
                        }
                    }
                    else
                    {
                        s = KeyConfiger.EndFlag;
                    }

                    //int index = this.Lines.FindIndex(l => l == Regions[i][j].ID);

                    //if (index >= 0)
                    //{
                    //    //  找到直接插入
                    //    this.Lines[index] = Regions[i][j].ToString() + s;
                    //}
                    //else
                    //{
                        //   没找到直接插入 有可能是新增
                        this.Lines.Add(Regions[i][j].ToString() + s);
                    //}
                }
                //  增加分区标识
                this.Lines.Add(KeyConfiger.EndFlag);
            }
        }

        protected override void CmdGetWellItems()
        {
            this.Regions.Clear();

            string str = string.Empty;

            int regionNum = 1;

            Region pRegion = new Region(1);

            //  解析格式  详细信息参考上面关键字格式
            bool IsDefalt = false;


            for (int i = 0; i < Lines.Count; i++)
            {
                str = Lines[i];

                //  过滤空行和备注行
                if (string.IsNullOrEmpty(str) || str.StartsWith(KeyConfiger.ExcepFlag) || str.StartsWith(KeyConfiger.ExcepFlag2))
                {
                    continue;
                }

                //  读到结束符 增加分区
                if (str.StartsWith("/") && str.EndsWith("/"))
                {
                    this.Regions.Add(pRegion);
                    regionNum++;
                    pRegion = new Region(regionNum);
                    continue;
                }

                List<string> newStr = str.EclExtendToArray();

                if (IsDefalt)
                {
                    newStr.Insert(0, string.Empty);
                }

                if (newStr.Count > 0)
                {
                    Item pitem = new Item();
                    pitem.Build(newStr);
                    //  标记行的ID位置
                    //Lines[i] = pitem.ID;
                    if (pitem != null)
                    {
                        pRegion.Add(pitem);
                    }
                }
                //  开始执行Insert默认值
                IsDefalt = !str.EndsWith("/");
            }
        }

        public class Item: OPT.Product.SimalorManager.ItemNormal
        {
           /// <summary> 露点压力 </summary>
           public string ldyl0;
            /// <summary> 挥发油气比 </summary>
           public string hfyqb1;
           /// <summary> 体积系数 </summary>
           public string tjxs2;
           /// <summary> 粘度</summary>
           public string nd3;

           string formatStr = "{0}{1}{2}{3}";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr, ldyl0.ToD(), hfyqb1.ToString().ToDD(), tjxs2.ToString().ToDD(), nd3.ToDD());//, isUse.ToDD()
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
                            this.ldyl0 = newStr[0];
                            break;
                        case 1:
                            this.hfyqb1 = newStr[1];
                            break;
                        case 2:
                            this.tjxs2 = newStr[2];
                            break;
                        case 3:
                            this.nd3 = newStr[3];
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
                    ldyl0 = this.ldyl0,
                    hfyqb1 = this.hfyqb1,
                    tjxs2 = this.tjxs2,
                    nd3 = this.nd3
                };

                return item;
            }
        }
    }
}
