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
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary> 限制条件 </summary>
    public class LIMIT : ConfigerKey
    {

        public LIMIT(string _name)
            : base(_name)
        {

            this.EachLineCmdHandler = l =>
            {
                //  截取前后空格判断是否为关键字
                return l.Trim();

            };

        }


        // Todo ：限制类型 
        string xzlx0;

        string clfz1;

        /// <summary> 产率阀值 </summary>
        public string Clfz1
        {
            get { return clfz1; }
            set { clfz1 = value; }
        }

        string jcbl2;

        /// <summary> 减产比率 </summary>
        public string Jcbl2
        {
            get { return jcbl2; }
            set { jcbl2 = value; }
        }

        private LimitType _limitType;
        /// <summary> 限制类型枚举 </summary>
        public LimitType LimitTypeP
        {
            get
            {
                return (LimitType)Enum.Parse(typeof(LimitType), this.xzlx0);
            }
            set
            {
                this.xzlx0 = (value).ToString();
            }
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
                        this.xzlx0 = ((LimitType)Enum.Parse(typeof(LimitType), newStr[0])).ToString();
                        break;
                    case 1:
                        this.clfz1 = newStr[1];
                        break;
                    case 2:
                        this.jcbl2 = newStr[2];
                        break;
                    default:
                        break;
                }
            }


        }

        string formatStr = "{0}{1}{2}";

        public override string ToString()
        {
            return string.Format(formatStr, this.xzlx0.ToSDD(),this.clfz1.ToSDD(),this.jcbl2.ToSDD());
        }

        public override void WriteKeyMethod(System.IO.StreamWriter writer)
        {
            writer.WriteLine();
            //   PERF 19   21   25   25   OPEN   1   0.048   0   0   DZ   0 
            writer.WriteLine(string.Empty.ToD() + string.Empty.ToD() + "LIMIT".ToD() + this.ToString());
        }

        /// <summary> 限制类型 </summary>
        public enum LimitType : int
        {
            
            /// <summary> 含水率 </summary>
            WCUT = 0,
            /// <summary> 气油比 </summary>
            OGR,
            /// <summary> 气液比 </summary>
            GLR,
            /// <summary> 水气比 </summary>
            WGR
        }
    }
}
