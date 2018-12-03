#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/18 11:28:21
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager
{
    /// <summary> 只有名称没有内容的关键字 </summary>
    public abstract class SingleKey : BaseKey, OutPutBindKey
    {
        public SingleKey(string _name)
            : base(_name)
        {

        }

        public override void WriteKey(System.IO.StreamWriter writer)
        {
            //  是否输出
            if (isCheck)
            {
                writer.WriteLine();
                writer.WriteLine(this.Name);
                base.WriteKey(writer);
            }
        }

        public bool isCheck = true;

        public object IsCheck
        {
            get
            {
                return isCheck;
            }
            set
            {
                //  是否等于1
                bool r = false ;

                if (value==null)
                {
                    isCheck = false;

                    return;
                }
                if (bool.TryParse(value.ToString(), out r))
                {
                    isCheck = r;
                }
                else
                {
                    isCheck = r;
                }
            }
        }

        public string OutName
        {
            get
            {
               return this.Name;
            }
            set
            {
                this.Name=value;
            }
        }


        public string OutType
        {
            get
            {
                if(this.GetType().Name.StartsWith("F")&&this.GetType().Name.EndsWith("H"))
                {
                    return "油田历史输出项";
                }
                else if(this.GetType().Name.StartsWith("F")&&!this.GetType().Name.EndsWith("H"))
                {
                    return "油田指标输出项";
                }
                else
                {
                    return "其他指标项";
                }
              
            }
        }


        public string Description
        {
            get { return this.TitleStr; }
        }
    }
}
