#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2016/12/21 16:06:04
 * 文件名：MAPAXES
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

namespace OPT.Product.SimalorManager.RegisterKeys.Eclipse
{
    
    /// <summary> 相对坐标 </summary>
    public class MAPUNITS : MergeConfiger
    {
        public MAPUNITS(string _name)
            : base(_name)
        {

        }


        string unitType = null;
        /// <summary> 单位类型 </summary>
        public string UnitType
        {
            get { return unitType; }
            set { unitType = value; }
        }

        private string mapType = null;
        /// <summary> 相对坐标 </summary>
        public string MapType
        {
            get { return mapType; }
            set { mapType = value; }
        }

        
        /// <summary> 是否使用相对坐标 </summary>
        public bool IsUseMap
        {
            get
            {
                return string.IsNullOrEmpty(mapType);
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
                        //this.xLocation = newStr[0];
                        break;
                    case 1:
                        //this.yLocation = newStr[1];
                        break;
                    case 2:
                        this.unitType = newStr[2];
                        break;
                    case 3:
                        this.mapType = newStr[3];
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
