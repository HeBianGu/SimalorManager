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
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary> 重启时间标志 </summary>
    public class RESTART : ConfigerKey
    {

        public RESTART(string _name)
            : base(_name)
        {

            this.EachLineCmdHandler = l =>
            {
                //  截取前后空格判断是否为关键字
                return l.Trim();

            };

        }


        string filename;

        /// <summary> 文件名 </summary>
        public string Filename
        {
            get
            {
                return filename;
            }

            set
            {
                filename = value;
            }
        }


        string stepCount;

        /// <summary> 时间步数量 </summary>
        public string StepCount
        {
            get
            {
                return stepCount;
            }

            set
            {
                stepCount = value;
            }
        }

        string formatStr = "{0}{1} /";


        /// <summary> 转换成字符串 </summary>
        public override string ToString()
        {
            return string.Format(formatStr, Filename.ToEclStr(), StepCount.ToDD());
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
                        this.Filename = newStr[0];
                        break;
                    case 1:
                        this.StepCount = newStr[1];
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
