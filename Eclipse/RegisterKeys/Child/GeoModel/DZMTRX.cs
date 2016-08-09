#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/2 10:38:01
 * 文件名：START
 * 说明：
SIGMA
 1 /
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 基质网格垂向长度 </summary>
    public class DZMTRX : ConfigerKey
    {
        public DZMTRX(string _name)
            : base(_name)
        {

        }

        private string jzwgcxcd = "1";

        /// <summary> 基质网格垂向长度 </summary>
        public string Jzwgcxcd
        {
            get { return jzwgcxcd; }
            set { jzwgcxcd = value; }
        }

        string formatStr = "{0} /";

        public override string ToString()
        {
            return string.Format(formatStr, jzwgcxcd);
        }

        public override void WriteKey(System.IO.StreamWriter writer)
        {
            this.Lines.Insert(0, string.Format(formatStr, jzwgcxcd.ToD()));
            base.WriteKey(writer);
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
                        this.jzwgcxcd = newStr[0];
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
