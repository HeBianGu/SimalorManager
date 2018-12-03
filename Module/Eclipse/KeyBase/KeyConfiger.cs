#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/3 10:08:15
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
    public class KeyConfiger
    {
        /// <summary> 关键字结束标识 </summary>
        public const string EndFlag = "/";

        /// <summary> 项字符间隔 </summary>
        public const int ItemLenght = 4;

        /// <summary> 表格间隔 </summary>
        public const int TableLenght = 4;

        /// <summary> 表格最大列数 </summary>
        public const int MaxColCount = 6;


        /// <summary> Eclipse默认值 </summary>
        public const string EclipseDefalt = "*";

        /// <summary> SimON默认值 </summary>
        public const string SimONDefalt = "NA";

        /// <summary> 注释标识 </summary>
        /// 
        public const string ExcepFlag = "--";

        /// <summary> 注释标识 </summary>
        public const string ExcepFlag1 = "==";

        /// <summary> 注释标识 </summary>
        public const string ExcepFlag2 = "#";

        public const string exceptionFormat = "err ：Method {0}  line {1}";

        /// <summary> 拆分数据的间隔符 </summary>
        public static char[] splitKeyWord = new char[] { '\t', ' ', '\'', '/', ',' };

        /// <summary> 换行+结束标识 </summary>
        public static string NewLineEndFlag = Environment.NewLine + KeyConfiger.EndFlag;

        /// <summary> 换行+结束标识 </summary>
        public static string NewLine = Environment.NewLine;

        /// <summary> 时间格式 </summary>
        public static string TimeFormat = "yyyy-MM-dd";

        /// <summary> 当网格维数大于此阀值值启用硬盘读存储 </summary>
        public static long bingDataSize = 100 * 100 * 100;
        
        /// <summary> SimON编码格式 </summary>
        public static Encoding SimONEncoder = Encoding.Default;// new System.Text.UTF8Encoding(false);

        /// <summary> 间隔 </summary>
        //public static string Space = " ";

        /// <summary> Include头文件 {0} 3106_SCAL.INC {1} C:\Users\Laobb\Desktop\SimON示例\projectTreeNode\断层案例\断层案例.dat {2} 2014-09-25 20:31:34</summary>
        public const string IncludeFileDetial =
@"# SimCore Simulation File (DATA) Data Section Version {5}
# FileName:    {0}
# File:        {1}
# FromFile:    {3}
# Created on:  {2}
# Keys Contain:{4}     
";

        public const string MainFileDetial =
@"# SimCore Simulation File (DATA) Data Section Version {3}
# FileName:    {0}
# File:        {1}
# Created on:  {2}  
# MachineName: {4}
";


        /// <summary> 表格镜像文件文件夹名称 </summary>
        public const string tableMapCache = "tableMapCache";


        /// <summary> 老版本Well文件格式 </summary>
        public const string oldWellLocationName = "Location";


        /// <summary> SimON文件扩展名 </summary>
        public const string SimONExtend = ".DATA";

        public const string HistroyFileName = "_HistoryProduction.DAT";

    }
}
