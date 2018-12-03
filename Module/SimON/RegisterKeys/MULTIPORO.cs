#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/1 13:39:53

 * 说明：
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Product.SimalorManager.Eclipse.FileInfos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace HeBianGu.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary> 孔隙类型  </summary>
    public class MULTIPORO : ConfigerKey
    {
        public MULTIPORO(string _name)
            : base(_name)
        {

        }

        private string nl0 = "1";

        //public string NL0
        //{
        //    get { return nl0; }
        //    set { nl0 = value; }
        //}

        private string nwp1 = "1";

        //public string NWP1
        //{
        //    get { return nwp1; }
        //    set { nwp1 = value; }
        //}


        private string np2 = "1";

        //public string NP2
        //{
        //    get { return np2; }
        //    set { np2 = value; }
        //}


        private DoubleType _doubleType;
        /// <summary> 孔隙类型 </summary>
        public DoubleType DoubleType
        {
            get
            {

                // HTodo  ：双孔单渗  2     1    1 或2     2    1
                if (this.nl0 == "2" && this.np2 == "1")
                {
                    _doubleType = DoubleType.SKDSMX;
                }

                // HTodo  ： 双孔双渗 2     1    2 或2     2    2  
                else if (this.nl0 == "2" && this.np2 == "2")
                {
                    _doubleType = DoubleType.SKSSMX;
                }

                // HTodo  ：单孔单渗 
                else
                {
                    _doubleType = DoubleType.DKJZMX;
                }


                return _doubleType;
            }
            set
            {

                // HTodo  ：双孔单渗 默认2     1    1  
                if (value == DoubleType.SKDSMX)
                {
                    this.nl0 = "2";
                    this.nwp1 = "1";
                    this.np2 = "1";
                }

                // HTodo  ：双孔双渗默认2     1    2  
               else  if (value == DoubleType.SKSSMX)
                {
                    this.nl0 = "2";
                    this.nwp1 = "1";
                    this.np2 = "2";
                }
                else
                {
                    this.nl0 = "1";
                    this.nwp1 = "1";
                    this.np2 = "1";
                }

                _doubleType = value;
            }
        }


        string formatStr = "{0}{1}{2}";

        public override string ToString()
        {
            return string.Format(formatStr, nl0.ToSDD(), nwp1.ToSDD(), np2.ToSDD());
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
                        this.nl0 = newStr[0];
                        break;
                    case 1:
                        this.nwp1 = newStr[1];
                        break;
                    case 2:
                        this.np2 = newStr[2];
                        break;
                    default:
                        break;
                }
            }


            // HTodo  ：将解析的孔隙类型增加到主文件中 
            if(this.BaseFile!=null)
            {
                this.BaseFile.DoubleType = this.DoubleType;
            }
        }

    }


}
