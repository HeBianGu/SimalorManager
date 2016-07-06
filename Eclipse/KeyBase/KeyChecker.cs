﻿#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/30 14:41:12
 * 文件名：KeyChecker
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    public static class KeyChecker
    {
        /// <summary> 检查字符串是否是关键字格式 </summary>
        public static bool IsKeyFormat(this string pKeyStr)
        {
            if (pKeyStr.StartsWith(" "))
            {
                return false;
            }

            string temp = pKeyStr.FormatKey();

            return IsStartEnglish(temp);

        }

        /// <summary> ^[A-Za-z]　　//匹配由26个英文字母开始的字符串 </summary>
        public static bool IsStartEnglish(string input)
        {
            return regex.IsMatch(input);
        }

        static Regex regex = new Regex(@"^[a-zA-Z]");

        /// <summary> 获取关键字格式 其中包含别名关键字</summary>
        public static string FormatKey(this string str)
        {
            string[] temps = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (temps == null || temps.Length == 0)
            {
                return str.AnatherName();
            }

            if (temps.Length > 1)
            {
                if (!temps[1].StartsWith(KeyConfiger.ExcepFlag) && !temps[1].StartsWith(KeyConfiger.ExcepFlag1))
                {
                    return str.AnatherName();
                }
            }

            return temps[0].AnatherName();

        }

        /// <summary> 获取关键字别名 </summary>
        static string AnatherName(this string str)
        {
            return KeyConfigerFactroy.AnatherNameConfiger.ContainsKey(str) ? KeyConfigerFactroy.AnatherNameConfiger[str] : str;
        }

        /// <summary> 判断是否是有效的行 </summary> 
        public static bool IsWorkLine(this string tempStr)
        {
            if (
                !string.IsNullOrEmpty(tempStr) &&
                !tempStr.StartsWith(KeyConfiger.ExcepFlag) &&
                !tempStr.StartsWith(KeyConfiger.ExcepFlag1) &&
                !tempStr.StartsWith(KeyConfiger.EndFlag)
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
