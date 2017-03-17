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
1	平衡分区数	EQLDIMS	
2	压力VS深度表的深度段最大数		默认值100/50
3	在RSVD、RVVD、PBVD、PDVD、RTEMPVD、RSWVD中深度数据点的最大数		默认值20/50
4	TVDP表的最大数		默认值1
5	在TVDP中深度数据点的最大数		默认值20/50
	

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
    /// <summary> 平衡表维数定义 </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class REGDIMS : ConfigerKey
    {
        public REGDIMS(string _name)
            : base(_name)
        {

        }

        public string p0;
        public string p1 ;
        public string p2;
        public string p3;
        public string p4;
        public string mcqfqgs5 = "1";
        public string p6;
        public string p7;
        public string p8;
        public string p9;

        string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9} /";
        /// <summary> 转换成字符串 </summary>
        public override string ToString()
        {
            return string.Format(formatStr, p0.ToDD(),p1.ToDD(), p2.ToDD(), p3.ToDD(), p4.ToDD(), mcqfqgs5.ToDD(), p6.ToDD(), p7.ToDD(), p8.ToDD(), p9.ToDD());
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
                        this.p0 = newStr[0];
                        break;
                    case 1:
                        this.p1 = newStr[1];
                        break;
                    case 2:
                        this.p2 = newStr[2];
                        break;
                    case 3:
                        this.p3 = newStr[3];
                        break;
                    case 4:
                        this.p4 = newStr[4];
                        break;
                    case 5:
                        this.mcqfqgs5 = newStr[5];
                        break;
                    case 6:
                        this.p6 = newStr[6];
                        break;
                    case 7:
                        this.p7 = newStr[7];
                        break;
                    case 8:
                        this.p8 = newStr[8];
                        break;
                    case 9:
                        this.p9 = newStr[9];
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
