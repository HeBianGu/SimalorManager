#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) ********************, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[HeBianGu]   时间：2015/12/2 16:13:27

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
    public static class EclipseKeyExt
    {
        /// <summary> 将 1.456 100968.040  2*    174.400  2* /中*展开 </summary>
        public static List<string> EclExtendToArray(this string str)
        {
            string[] tempStr = str.Split('/');

            var strs = tempStr[0].Split(KeyConfiger.splitKeyWord, StringSplitOptions.RemoveEmptyEntries);

            List<string> newStr = new List<string>();
            string s = string.Empty;
            for (int i = 0; i < strs.Length; i++)
            {
                s = strs[i];
                if (s.EndsWith("*"))
                {
                    int count = s.Trim('*').ToInt();

                    if (count == 0)
                    {
                        newStr.Add("1*");
                    }

                    while (count > 0)
                    {
                        newStr.Add("1*");
                        count--;
                    }
                }
                else
                {
                    newStr.Add(s);
                }
            }

            return newStr;
        }

        /// <summary>  'PROD1' 4 5 1  4 5 1  90 0  800  800  10 10  0.02  'propant 12/18' 'FLOWFUNC0' 'TIME' 3  7* / 对单引号中带空格单独处理 </summary>
        public static List<string> EclExceptSpaceToArray(this string str)
        {
            //  首先截取单引号
            // string ss = "'PROD1' 4 5 1    0.02  'propant 12/18' 'FLOWFUNC0' 'TIME' 3  7* /";

            //string[] tempStr = str.Split('/');

            string[] strs = str.Split('\'');

            List<string> outStr = new List<string>();

            for (int i = 0; i < strs.Length; i++)
            {
                //  偶数列
                if (i % 2 == 0)
                {
                    outStr.AddRange(strs[i].EclExtendToArray());
                }
                else
                {
                    outStr.Add(strs[i]);
                }
            }

            return outStr;
        }


        /// <summary> 486*1.39  /中*展开枚举 </summary>
        public static List<string> EclExtendToPetrelArray(this string str)
        {
            string[] tempStr = str.Split('/');

            var strs = tempStr[0].Split(KeyConfiger.splitKeyWord, StringSplitOptions.RemoveEmptyEntries);

            List<string> newStr = new List<string>();
            string s = string.Empty;
            for (int i = 0; i < strs.Length; i++)
            {
                s = strs[i];
                if (s.Contains("*"))
                {
                    int count = s.Split('*')[0].ToInt();

                    string value = s.Split('*')[1];

                    while (count > 0)
                    {
                        newStr.Add(value);
                        count--;
                    }
                }
                else
                {
                    newStr.Add(s);
                }
            }

            return newStr;
        }

        /// <summary> 出现换行情况自动合并 </summary>
        public static List<string> ReadNewLineArray(this List<string> strs)
        {
            List<string>.Enumerator enumerator = strs.GetEnumerator();

            List<string> outStr = new List<string>();

            StringBuilder sb = new StringBuilder();

            while (enumerator.MoveNext())
            {
                string ss = enumerator.Current.Trim();

                if (!ss.IsWorkLine()) continue;
                //  满足条件
                if ((!ss.StartsWith("/")) && ss.EndsWith("/"))
                {
                    sb.Append(ss);
                    outStr.Add(sb.ToString());
                    sb.Clear();
                }
                else if (ss.StartsWith("/") && ss.EndsWith("/"))
                {
                    sb.Clear();
                    outStr.Add(ss);
                }
                else
                {
                    sb.Append(ss);
                    sb.Append(" ");
                }
            }

            return outStr;
        }

        /// <summary> 按配置信息截取有效信息 </summary>
        public static List<string> EclToArray(this string str)
        {
            string[] tempStr = str.Split('/');

            return tempStr[0].Split(KeyConfiger.splitKeyWord, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

    }
}
