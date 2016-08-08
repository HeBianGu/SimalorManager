#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/3 10:08:15
 * 文件名：KeyConfiger
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

namespace OPT.Product.SimalorManager
{
    public class KeyConfiger
    {
        /// <summary> 关键字结束标识 </summary>
        public const string EndFlag = "/";

        /// <summary> 项字符间隔 </summary>
        public const int ItemLenght = 8;

        /// <summary> 表格间隔 </summary>
        public const int TableLenght = 8;

        /// <summary> 表格最大列数 </summary>
        public const int MaxColCount = 6;

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
    }
}
