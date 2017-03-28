#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/10/28 16:33:27

 * 说明：正则表达式扩展封装
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

namespace OPT.Product.SimalorManager.Match
{
    /// <summary> 正则表达式扩展封装 Add By lihaijun 2015.10.28 </summary>
    public static class RegexDao
    {
        ///  <summary> 判断输入的字符串只包含汉字 </summary>
        public static bool IsChineseCh(this string input)
        {
            return IsMatch(@"^[\u4e00-\u9fa5]+$", input);
        }

        ///  <summary> 匹配3位或4位区号的电话号码，其中区号可以用小括号括起来,也可以不用，区号与本地号间可以用连字号或空格间隔，也可以没有间隔 </summary>
        public static bool IsPhone(this string input)
        {
            string pattern = "^\\(0\\d{2}\\)[- ]?\\d{8}$|^0\\d{2}[- ]?\\d{8}$|^\\(0\\d{3}\\)[- ]?\\d{7}$|^0\\d{3}[- ]?\\d{7}$";
            return IsMatch(pattern, input);
        }

        ///  <summary> 判断输入的字符串是否是一个合法的手机号 </summary>
        public static bool IsMobilePhone(this string input)
        {
            return IsMatch(@"^13\\d{9}$", input);
        }

        ///  <summary> 判断输入的字符串只包含数字可以匹配整数和浮点数^-?\d+$|^(-?\d+)(\.\d+)?$ </summary>
        public static bool IsNumber(this string input)
        {
            string pattern = "^-?\\d+$|^(-?\\d+)(\\.\\d+)?$";
            return IsMatch(pattern, input);
        }

        ///  <summary> 匹配非负整数 </summary>
        public static bool IsNotNagtive(this string input)
        {
            return IsMatch(@"^\d+$", input);
        }

        ///  <summary> 匹配正整数 </summary>
        public static bool IsUint(this string input)
        {
            return IsMatch(@"^[0-9]*[1-9][0-9]*$", input);
        }

        ///  <summary> 判断输入的字符串字包含英文字母 </summary>
        public static bool IsEnglisCh(this string input)
        {
            return IsMatch(@"^[A-Za-z]+$", input);
        }

        ///  <summary> 判断输入的字符串是否是一个合法的Email地址 </summary>
        public static bool IsEmail(this string input)
        {
            string pattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            return IsMatch(pattern, input);
        }

        ///  <summary> 判断输入的字符串是否只包含数字和英文字母 </summary>
        public static bool IsNumAndEnCh(this string input)
        {
            return IsMatch(@"^[A-Za-z0-9]+$", input);
        }

        ///  <summary> 判断输入的字符串是否是一个超链接 </summary>
        public static bool IsURL(this string input)
        {
            string pattern = @"^[a-zA-Z]+://(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?$";
            return IsMatch(pattern, input);
        }

        ///  <summary> 判断输入的字符串是否是表示一个IP地址 </summary>
        public static bool IsIPv4(this string input)
        {
            string[] IPs = input.Split('.');

            for (int i = 0; i < IPs.Length; i++)
            {
                if (!IsMatch(@"^\d+$", IPs[i]))
                {
                    return false;
                }
                if (Convert.ToUInt16(IPs[i]) > 255)
                {
                    return false;
                }
            }
            return true;
        }

        ///  <summary> 判断输入的字符串是否是合法的IPV6 地址 </summary>
        public static bool IsIPV6(this string input)
        {
            string pattern = "";
            string temp = input;
            string[] strs = temp.Split(':');
            if (strs.Length > 8)
            {
                return false;
            }
            int count = RegexDao.GetStringCount(input, "::");
            if (count > 1)
            {
                return false;
            }
            else if (count == 0)
            {
                pattern = @"^([\da-f]{1,4}:){7}[\da-f]{1,4}$";
                return IsMatch(pattern, input);
            }
            else
            {
                pattern = @"^([\da-f]{1,4}:){0,5}::([\da-f]{1,4}:){0,5}[\da-f]{1,4}$";
                return IsMatch(pattern, input);
            }
        }

        ///  <summary> 计算字符串的字符长度，一个汉字字符将被计算为两个字符 </summary>
        public static int GetCount(this string input)
        {
            return Regex.Replace(input, @"[\u4e00-\u9fa5/g]", "aa").Length;
        }

        ///  <summary> 调用Regex中IsMatch函数实现一般的正则表达式匹配 </summary>
        public static bool IsMatch(this string pattern, string input)
        {
            if (input == null || input == "") return false;
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        ///  <summary> 从输入字符串中的第一个字符开始，用替换字符串替换指定的正则表达式模式的所有匹配项 </summary>
        public static string Replace(this string pattern, string input, string replacement)
        {
            Regex regex = new Regex(pattern);
            return regex.Replace(input, replacement);
        }

        ///  <summary> 在由正则表达式模式定义的位置拆分输入字符串 </summary>
        public static string[] Split(this string pattern, string input)
        {
            Regex regex = new Regex(pattern);
            return regex.Split(input);
        }

        /* *******************************************************************
         * 1、通过“:”来分割字符串看得到的字符串数组长度是否小于等于8
         * 2、判断输入的IPV6字符串中是否有“::”。
         * 3、如果没有“::”采用 ^([\da-f]{1,4}:){7}[\da-f]{1,4}$ 来判断
         * 4、如果有“::” ，判断"::"是否止出现一次
         * 5、如果出现一次以上 返回false
         * 6、^([\da-f]{1,4}:){0,5}::([\da-f]{1,4}:){0,5}[\da-f]{1,4}$
         * ******************************************************************/

        ///  <summary> 判断字符串compare 在 input字符串中出现的次数 </summary>
        private static int GetStringCount(this string input, string compare)
        {
            int index = input.IndexOf(compare);
            if (index != -1)
            {
                return 1 + GetStringCount(input.Substring(index + compare.Length), compare);
            }
            else
            {
                return 0;
            }

        }

        /// <summary> 检查是否为路径 </summary>
        public static bool CheckPath(this string path)
        {
            string pattern = @"^[a-zA-Z]:(((\\(?! )[^/:*?<>\""|\\]+)+\\?)|(\\)?)\s*$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(path);
        }

        /// <summary> 匹配双字节字符(包括汉字在内)：[^x00-xff] 评注：可以用来计算字符串的长度（一个双字节字符长度计2，ASCII字符计1） </summary>
        public static bool IsDoubleByte(this string input)
        {
            string pattern = @"[^x00-xff]";
            return IsMatch(pattern, input);
        }

        /// <summary> 匹配空白行的正则表达式：ns*r 评注：可以用来删除空白行 </summary>
        public static bool IsEmptyLine(this string input)
        {
            string pattern = @"ns*r";
            return IsMatch(pattern, input);
        }
        /// <summary> 匹配HTML标记的正则表达式：<(S*?)[^>]*>.*?|<.*? /> 　　评注：网上流传的版本太糟糕，上面这个也仅仅能匹配部分，对于复杂的嵌套标记依旧无能为力 </summary>
        public static bool IsHTML(this string input)
        {
            string pattern = @"<(S*?)[^>]*>.*?|<.*? />";
            return IsMatch(pattern, input);
        }

        /// <summary> 匹配首尾空白字符的正则表达式：^s*|s*$   　评注：可以用来删除行首行尾的空白字符(包括空格、制表符、换页符等等)，非常有用的表达式  </summary>
        public static bool IsEmptyHomeEnd(this string input)
        {
            string pattern = @"^s*|s*$";
            return IsMatch(pattern, input);
        }

        ///// <summary> 匹配Email地址的正则表达式：w+([-+.]w+)*@w+([-.]w+)*.w+([-.]w+)*  评注：表单验证时很实用 </summary>
        //public static bool IsEmail(this string input)
        //{
        //    string pattern = @"w+([-+.]w+)*@w+([-.]w+)*.w+([-.]w+)*";
        //    return IsMatch(pattern, input);
        //}

        /// <summary> 匹配网址URL的正则表达式：[a-zA-z]+://[^s]* 评注：网上流传的版本功能很有限，上面这个基本可以满足需求 </summary>
        public static bool IsUrl(this string input)
        {
            string pattern = @"[a-zA-z]+://[^s]*";
            return IsMatch(pattern, input);
        }

        /// <summary> 匹配帐号是否合法(字母开头，允许5-16字节，允许字母数字下划线)：^[a-zA-Z][a-zA-Z0-9_]{4,15}$　评注：表单验证时很实用 </summary>
        public static bool IsACCountVisible(this string input)
        {
            string pattern = @"^[a-zA-Z][a-zA-Z0-9_]{4,15}$";
            return IsMatch(pattern, input);
        }

        /// <summary> 匹配国内电话号码：d{3}-d{8}|d{4}-d{7} 评注：匹配形式如 0511-4405222 或 021-87888822 </summary>
        public static bool IsTelInCon(this string input)
        {
            string pattern = @"d{3}-d{8}|d{4}-d{7}";
            return IsMatch(pattern, input);
        }

        /// <summary> 匹配腾讯QQ号：[1-9][0-9]{4,}  评注：腾讯QQ号从10000开始 </summary>
        public static bool IsQQ(this string input)
        {
            string pattern = @"[1-9][0-9]{4,}";
            return IsMatch(pattern, input);
        }

        /// <summary> 匹配中国邮政编码：[1-9]d{5}(?!d)  评注：中国邮政编码为6位数字 </summary>
        public static bool IsPostalCode(this string input)
        {
            string pattern = @"[1-9]d{5}(?!d)";
            return IsMatch(pattern, input);
        }

        /// <summary> 匹配身份证：d{15}|d{18}　评注：中国的身份证为15位或18位 </summary>
        public static bool IsIDNumber(this string input)
        {
            string pattern = @"d{15}|d{18}";
            return IsMatch(pattern, input);
        }

        /// <summary> 匹配ip地址：d+.d+.d+.d+ 评注：提取ip地址时有用 </summary>
        public static bool IsIP(this string input)
        {
            string pattern = @"d+.d+.d+.d+";
            return IsMatch(pattern, input);
        }

        /// <summary> 匹配正整数 　　^[1-9]d*$ </summary>
        public static bool IsUInt(this string input)
        {
            string pattern = @"^[1-9]d*$";
            return IsMatch(pattern, input);
        }

        /// <summary> ^-[1-9]d*$ 　 //匹配负整数  </summary>
        public static bool IsMInt(this string input)
        {
            string pattern = @"^-[1-9]d*$";
            return IsMatch(pattern, input);
        }

        /// <summary> ^-?[1-9]d*$　　 //匹配整数 </summary>
        public static bool IsInt(this string input)
        {
            string pattern = @"^-?[1-9]d*$";
            return IsMatch(pattern, input);
        }

        /// <summary> ^[1-9]d*|0$　 //匹配非负整数（正整数 + 0） </summary>
        public static bool IsNotMInt(this string input)
        {
            string pattern = @"^[1-9]d*|0$";
            return IsMatch(pattern, input);
        }

        /// <summary> ^-[1-9]d*|0$　　 //匹配非正整数（负整数 + 0） </summary>
        public static bool IsNotUInt(this string input)
        {
            string pattern = @"^-[1-9]d*|0$";
            return IsMatch(pattern, input);
        }

        /// <summary> ^[1-9]d*.d*|0.d*[1-9]d*$　　 //匹配正浮点数 </summary>
        public static bool IsUFloat(this string input)
        {
            string pattern = @"^[1-9]d*.d*|0.d*[1-9]d*$";
            return IsMatch(pattern, input);
        }

        /// <summary> ^-([1-9]d*.d*|0.d*[1-9]d*)$　 //匹配负浮点数 </summary>
        public static bool IsMFloatl(this string input)
        {
            string pattern = @"^-([1-9]d*.d*|0.d*[1-9]d*)$";
            return IsMatch(pattern, input);
        }

        /// <summary> ^-?([1-9]d*.d*|0.d*[1-9]d*|0?.0+|0)$　 //匹配浮点数 </summary>
        public static bool IsFloat(this string input)
        {
            string pattern = @"^-?([1-9]d*.d*|0.d*[1-9]d*|0?.0+|0)$";
            return IsMatch(pattern, input);
        }

        /// <summary> ^[1-9]d*.d*|0.d*[1-9]d*|0?.0+|0$　　 //匹配非负浮点数（正浮点数 + 0） </summary>
        public static bool IsNotMFloat(this string input)
        {
            string pattern = @"^[1-9]d*.d*|0.d*[1-9]d*|0?.0+|0$";
            return IsMatch(pattern, input);
        }

        /// <summary> ^(-([1-9]d*.d*|0.d*[1-9]d*))|0?.0+|0$　　//匹配非正浮点数（负浮点数 + 0） 评注：处理大量数据时有用，具体应用时注意修正 </summary>
        public static bool IsNotUFloat(this string input)
        {
            string pattern = @"^(-([1-9]d*.d*|0.d*[1-9]d*))|0?.0+|0$";
            return IsMatch(pattern, input);
        }

        /// <summary> ^[A-Za-z]+$　　//匹配由26个英文字母组成的字符串 </summary>
        public static bool IsEnglish(this string input)
        {
            string pattern = @"^[A-Za-z]+$";
            return IsMatch(pattern, input);
        }

        /// <summary> ^[A-Za-z]　　//匹配由26个英文字母开始的字符串 </summary>
        public static bool IsStartEnglish(this string input)
        {
            string pattern = @"^[a-zA-Z]";
            return IsMatch(pattern, input);
        }

        /// <summary> ^[A-Z]+$　　//匹配由26个英文字母的大写组成的字符串 </summary>
        public static bool IsEnglishUpper(this string input)
        {
            string pattern = @"^[A-Z]+$";
            return IsMatch(pattern, input);
        }

        /// <summary> ^[a-z]+$　　//匹配由26个英文字母的小写组成的字符串 </summary>
        public static bool IsEnglishLetter(this string input)
        {
            string pattern = @"^[a-z]+$";
            return IsMatch(pattern, input);
        }

        /// <summary> ^[A-Za-z0-9]+$　　//匹配由数字和26个英文字母组成的字符串  </summary>
        public static bool IsNumOrEnglish(this string input)
        {
            string pattern = @"^[A-Za-z0-9]+$";
            return IsMatch(pattern, input);
        }

        /// <summary> ^w+$　　//匹配由数字、26个英文字母或者下划线组成的字符串 </summary>
        public static bool IsNumOrEngOrUndLine(this string input)
        {
            string pattern = @"^w+$";
            return IsMatch(pattern, input);
        }

        /// <summary> 只能输入数字：“^[0-9]*$” </summary>
        public static bool IsNumberOnly(this string input)
        {
            string pattern = @"^[0-9]*$";
            return IsMatch(pattern, input);
        }

        /// <summary> 只能输入n位的数字：“^d{n}$”  </summary>
        public static bool IsLenghNumber(this string input, int length)
        {
            string pattern = @"^d{" + length + "}$";
            return IsMatch(pattern, input);
        }

        /// <summary> 只能输入至少n位数字：“^d{n,}$” </summary>
        public static bool IsLeastLenNum(this string input, int minLenth)
        {
            string pattern = @"^d{" + minLenth + ",}$";
            return IsMatch(pattern, input);
        }

        /// <summary> 只能输入m-n位的数字：“^d{m,n}$” </summary>
        public static bool IsBetweenNumber(this string input, int minLen, int maxLen)
        {
            string pattern = @"^d{" + minLen + "," + maxLen + "}$";
            return IsMatch(pattern, input);
        }

        /// <summary> 只能输入零和非零开头的数字：“^(0|[1-9][0-9]*)$” </summary>
        public static bool IsStartWithZoreNum(this string input)
        {
            string pattern = @"^(0|[1-9][0-9]*)$";
            return IsMatch(pattern, input);
        }

        /// <summary> 只能输入有两位小数的正实数：“^[0-9]+(.[0-9]{2})?$” </summary>
        public static bool IsLenthDecimal(this string input)
        {
            string pattern = @"^[0-9]+(.[0-9]{2})?$";
            return IsMatch(pattern, input);
        }

        /// <summary> 只能输入有1-3位小数的正实数：“^[0-9]+(.[0-9]{1,3})?$” </summary>
        public static bool IsURealNum(this string input)
        {
            string pattern = @"^[0-9]+(.[0-9]{1,3})?$";
            return IsMatch(pattern, input);
        }

        /// <summary> 只能输入非零的正整数：“^+?[1-9][0-9]*$” </summary>
        public static bool IsUIntExceptZore(this string input)
        {
            string pattern = @"^+?[1-9][0-9]*$";
            return IsMatch(pattern, input);
        }

        /// <summary> 只能输入非零的负整数：“^-[1-9][0-9]*$” </summary>
        public static bool IsMIntExceptZore(this string input)
        {
            string pattern = @"^-[1-9][0-9]*$";
            return IsMatch(pattern, input);
        }

        /// <summary> 只能输入长度为3的字符：“^.{3}$” </summary>
        public static bool IsLenghChar(this string input)
        {
            string pattern = @"^.{3}$";
            return IsMatch(pattern, input);
        }

        /// <summary> 验证用户密码:“^[a-zA-Z]w{5,17}$”正确格式为：以字母开头，长度在6-18之间，只能包含字符、数字和下划线 </summary>
        public static bool IsUserPassWord(this string input)
        {
            string pattern = @"^[a-zA-Z]w{5,17}$";
            return IsMatch(pattern, input);
        }

        /// <summary> 验证是否含有^%&'',;=?$"等字符：“[^%&'',;=?$x22]+” </summary>
        public static bool IsComtain(this string input)
        {
            string pattern = @"[^%&'',;=?$x22]+";
            return IsMatch(pattern, input);
        }

        /// <summary> 只能输入汉字：“^[u4e00-u9fa5],{0,}$” </summary>
        public static bool IsChinese(this string input)
        {
            string pattern = @"^[u4e00-u9fa5],{0,}$";
            return IsMatch(pattern, input);
        }

        /// <summary> 验证一年的12个月：“^(0?[1-9]|1[0-2])$”正确格式为：“01”-“09”和“1”“12” </summary>
        public static bool IsMonth(this string input)
        {
            string pattern = @"^(0?[1-9]|1[0-2])$";
            return IsMatch(pattern, input);
        }

        /// <summary> 验证一个月的31天：“^((0?[1-9])|((1|2)[0-9])|30|31)$” 正确格式为：“01”“09”和“1”“31” </summary>
        public static bool IsDay(this string input)
        {
            string pattern = @"^((0?[1-9])|((1|2)[0-9])|30|31)$";
            return IsMatch(pattern, input);
        }

        /// <summary> 匹配中文字符的正则表达式： [u4e00-u9fa5] </summary>
        public static bool IsChineseChar(this string input)
        {
            string pattern = @"[u4e00-u9fa5]";
            return IsMatch(pattern, input);
        }

        /// <summary> 是否是路径 </summary>
        public static bool IsPath(this string path)
        {
            string pattern = @"^[a-zA-Z]:(((\\(?! )[^/:*?<>\""|\\]+)+\\?)|(\\)?)\s*$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(path);
        }

        /// <summary>
        /// 是否是日期的字符串
        /// </summary>
        /// <param name="path">只对这些格式进行验证（"d'MMM'yyyy", "dd'MMM'yyyy"）</param>
        /// <returns></returns>
        public static bool IsDate(this string path)
        {
            string path1 = path.Replace("'", "").Replace(" ", "");
            DateTime parseDate;
            string[] formats = { "dMMMyyyy", "ddMMMyyyy" };
            if (DateTime.TryParseExact(path1, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AllowInnerWhite, out parseDate))
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
