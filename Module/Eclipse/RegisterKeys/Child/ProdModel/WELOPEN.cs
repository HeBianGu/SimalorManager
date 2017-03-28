#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/2 10:38:01

 * 说明：


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

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 井定义 </summary>
    public class WELOPEN : ItemsKey<WELOPEN.Item>, IProductEvent
    {
        public WELOPEN(string _name)
            : base(_name)
        {

        }

        public Item GetSingleItem
        {
            get
            {
                if(this.Items.Count==0)
                {
                    Item item= new Item();
                    this.Items.Add(item);
                    return item;
                       
                }
                else
                {
                    return this.Items[0];
                }
            }
        }

        public class Item : OPT.Product.SimalorManager.Item, IProductItem
        {
           /// <summary> 井名 </summary>
           public string jm0 = "新增";
           /// <summary> 操作 有4个枚举参数，即开井（OPEN）, 关层（STOP）, 关井（SHUT）or自动（AUTO） </summary>
           public string jz1 = "OPEN";
           /// <summary> 网格（I）</summary>
           public string i2;
           /// <summary> 网格（J） </summary>
           public string j3;
           /// <summary> 网格（J） </summary>
           public string k4;
           /// <summary> 完井层段编号（开始） </summary>
           public string cksd5;
           /// <summary> 完井层段编号（结束） </summary>
           public string yxltx6;


           string formatStr = "{0}{1}{2}{3}{4}{5}{6}/";

            /// <summary> 转换成字符串 </summary>
            public override string ToString()
            {
                return string.Format(formatStr,
                    jm0.ToEclStr(),
                    jz1.ToEclStr(),
                    this.i2.ToDD(),
                    this.j3.ToDD(),
                    this.k4.ToDD(),
                    this.cksd5.ToDD(),
                    this.yxltx6.ToDD());
            }

            /// <summary> 解析字符串 </summary>
            public override void Build(List<string> newStr)
            {

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
                            this.i2 = newStr[2];
                            break;
                        case 3:
                            this.j3 = newStr[3];
                            break;
                        case 4:
                            this.k4 = newStr[4];
                            break;
                        case 5:
                            this.cksd5 = newStr[5];
                            break;
                        case 6:
                            this.yxltx6 = newStr[6];
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


        public void SetWellName(string wellName)
        {
            this.Items.ForEach(l => l.Name = wellName);
        }
    }


}
