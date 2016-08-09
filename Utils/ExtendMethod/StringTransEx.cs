using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    public static class StringTransEx
    {
        /// <summary> 转换double </summary>
        public static double ToDouble(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            if (str.EndsWith("*"))
            {
                return 0;
            }
            return double.Parse(str);
        }

        /// <summary> 转换int </summary>
        public static int ToInt(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            if (str.EndsWith("*"))
            {
                return 0;
            }
            return int.Parse(str);
        }

        /// <summary> 转换bool</summary>
        public static bool ToBool(this string str)
        {
            switch (str)
            {
                case "1":
                    return true;
                case "0":
                    return false;
                case "true":
                    return true;
                case "false":
                    return false;
                default:
                    throw new ArgumentException();
            }
        }


        /// <summary> 转换成默认数据 </summary>
        public static string ToDD(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return " *".PadRight(KeyConfiger.ItemLenght);
            }
            else
            {
                return " " + str.PadRight(KeyConfiger.ItemLenght);
            }
        }

        /// <summary> 转换成默认数据 </summary>
        public static string ToD(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return " ".PadRight(KeyConfiger.ItemLenght + 1);
            }
            else
            {
                return " " + str.PadRight(KeyConfiger.ItemLenght);
            }
        }

        /// <summary> 转换成默认数据 </summary>
        public static string ToD(this int intstr)
        {
            string str = intstr.ToString();
            if (string.IsNullOrEmpty(str))
            {
                return " ".PadRight(KeyConfiger.ItemLenght + 1);
            }
            else
            {
                return " " + str.PadRight(KeyConfiger.ItemLenght);
            }
        }
        /// <summary> 指示指定的字符串是 null 还是 System.String.Empty 字符串 </summary>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);

        }
        /// <summary> 字符串在两边加单引号 </summary>
        public static string ToEclStr(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return " 1*".PadRight(KeyConfiger.ItemLenght);
            }
            else if (str.EndsWith("*"))
            {
                return " " + str.PadRight(KeyConfiger.ItemLenght + 1);
            }
            else
            {
                return (" \'" + str.Trim('\'') + "\'").PadRight(KeyConfiger.ItemLenght);
            }
        }

        /// <summary> 将1*转换成空值 </summary>
        public static string ToEclDefaultStr(this string str)
        {
            if (str.Trim() == "1*")
            {
                return string.Empty;
            }
            else
            {
                return str;
            }
        }

    }
}
