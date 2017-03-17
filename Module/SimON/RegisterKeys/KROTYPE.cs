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
using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 三相相渗模型   </summary>
    public class KROTYPE : ConfigerKey
    {
        public KROTYPE(string _name)
            : base(_name)
        {
            //this.BuilderHandler += (l, k) =>
            //    {
            //        this.Date = DatesKeyService.Instance.GetSimONDateTime(this.Lines[0].Trim().Substring(0, 8));
            //        return this;
            //    };
        }

        private string type = "SEGR";
        /// <summary> 类型 </summary>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }


        string formatStr = @"# 0=SEGREGATION, 1=STONE1, 2=STONE2
 {0}";

        /// <summary> 转换成字符串 </summary>
        public override string ToString()
        {
            return string.Format(formatStr, type);
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
                        this.type = newStr[0];
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
