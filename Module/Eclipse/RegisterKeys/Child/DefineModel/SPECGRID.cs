#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/2 10:38:01

 * 说明：
SPECGRID
10 15 4 2 ‘F’
/

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

namespace HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 模型维数 </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SPECGRID : ConfigerKey
    {

        public SPECGRID(string _name)
            : base(_name)
        {

        }

        private int x = 1;
        [CategoryAttribute("维数定义"), DescriptionAttribute("X方向维数"), DisplayName("X方向维数")]
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        private int y = 1;
        [CategoryAttribute("维数定义"), DescriptionAttribute("Y方向维数"), DisplayName("Y方向维数")]
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        private int z = 1;
        [CategoryAttribute("维数定义"), DescriptionAttribute("Z方向维数"), DisplayName("Z方向维数")]
        public int Z
        {
            get { return z; }
            set { z = value; }
        }

        /// <summary> 油藏类型 </summary>
        public string yclx;

        /// <summary> 坐标系统 </summary>
        public string zblx;

        string formatStr = "{0}{1}{2}{3}{4} /";

        public override string ToString()
        {
            return string.Format(formatStr, X.ToD(), Y.ToD(), Z.ToD(), yclx.ToD(), zblx.ToEclStr());
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
                        this.x = newStr[0].ToInt();
                        break;
                    case 1:
                        this.y = newStr[1].ToInt();
                        break;
                    case 2:
                        this.z = newStr[2].ToInt();
                        break;
                    case 3:
                        this.yclx = newStr[3];
                        break;
                    case 4:
                        this.zblx = newStr[4];
                        break;
                    default:
                        break;
                }
            }

            //  构造全网格范围
            RegionParam tempRegion = new RegionParam();
            tempRegion.XFrom = 1;
            tempRegion.XTo = this.x;
            tempRegion.YFrom = 1;
            tempRegion.YTo = this.y;
            tempRegion.ZFrom = 1;
            tempRegion.ZTo = this.z;
            //this.BaseFile.TempRegion = tempRegion;

            this.BaseFile.X = this.x;
            this.BaseFile.Y = this.y;
            this.BaseFile.Z = this.z;
        }

    }
}
