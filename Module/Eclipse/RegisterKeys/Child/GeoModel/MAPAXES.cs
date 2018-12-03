#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2016/12/21 16:06:04

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

namespace HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse
{
    
    /// <summary> 相对坐标 </summary>
    public class MAPAXES : MergeConfiger
    {
        public MAPAXES(string _name)
            : base(_name)
        {

        }


        string xLocation = null;
        /// <summary> x方向参考位置 </summary>
        public string XLocation
        {
            get { return xLocation; }
            set { xLocation = value; }
        }

        private string yLocation = null;
        /// <summary> y方向参考位置 </summary>
        public string YLocation
        {
            get { return yLocation; }
            set { yLocation = value; }
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
                        //this.xLocation = newStr[0];
                        break;
                    case 1:
                        //this.yLocation = newStr[1];
                        break;
                    case 2:
                        this.xLocation = newStr[2];
                        break;
                    case 3:
                        this.yLocation = newStr[3];
                        break;
                    case 4:
                        //this.jxstzds4 = newStr[4];
                        break;
                    case 5:
                        //this.e100wgzds5 = newStr[5];
                        break;
                    case 6:
                        //this.e300jxstzds6 = newStr[6];
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
