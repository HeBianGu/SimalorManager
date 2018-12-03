#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/11/30 14:41:12
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

namespace HeBianGu.Product.SimalorManager
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
                return str.AnatherName().ToUpper();
            }

            if (temps.Length > 1)
            {
                if (!temps[1].StartsWith(KeyConfiger.ExcepFlag) && !temps[1].StartsWith(KeyConfiger.ExcepFlag1))
                {
                    return temps[0].AnatherName().ToUpper();
                }
            }

            return temps[0].AnatherName().ToUpper();

        }

        /// <summary> 获取关键字别名 </summary>
        static string AnatherName(this string str)
        {
            return KeyConfigerFactroy.Instance.EclipseKeyFactory.AnatherNameConfiger.ContainsKey(str) ? KeyConfigerFactroy.Instance.EclipseKeyFactory.AnatherNameConfiger[str] : str;
        }

        /// <summary> 是否为注册关键字格式 </summary>
        public static bool IsRegisterName(this string str)
        {
            return true;
        }


        /// <summary> 判断是否是过滤的行 </summary> 
        public static bool IsNotExcepLine(this string tempStr)
        {
            if (
                !string.IsNullOrEmpty(tempStr) &&
                !tempStr.StartsWith(KeyConfiger.ExcepFlag) &&
                !tempStr.StartsWith(KeyConfiger.ExcepFlag1)
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary> 判断是否是有效的行 </summary> 
        public static bool IsWorkLine(this string tempStr)
        {
            if (
                !string.IsNullOrEmpty(tempStr) &&
                !tempStr.StartsWith(KeyConfiger.ExcepFlag) &&
                !tempStr.StartsWith(KeyConfiger.ExcepFlag1) &&
                !tempStr.StartsWith(KeyConfiger.ExcepFlag2) &&
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

        /// <summary> 判断是否是结束行 </summary> 
        public static bool IsEndLine(this string tempStr)
        {
            return tempStr.StartsWith(KeyConfiger.EndFlag) && tempStr.EndsWith(KeyConfiger.EndFlag);
        }

        /// <summary> 过滤一行中的备注部分 示例：  19900101D # Use date or time? </summary> 
        public static string ClearLine(this string tempStr)
        {
            ;
            tempStr = func(tempStr, KeyConfiger.ExcepFlag);
            tempStr = func(tempStr, KeyConfiger.ExcepFlag1);
            tempStr = func(tempStr, KeyConfiger.ExcepFlag2);

            return tempStr;

        }

        static Func<string, string, string> func = (l, k) =>
              {
                  if (l.Contains(k))
                  {
                      return l.Split(new string[] { k }, StringSplitOptions.RemoveEmptyEntries)[0];
                  }
                  else
                  {
                      return l;
                  }

              };

    }
}
