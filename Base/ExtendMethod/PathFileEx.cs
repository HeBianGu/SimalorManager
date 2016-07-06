using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    /// <summary> 公用字符串文件扩展方法 --   Add By lihaijun 2015.09.30 11：43 </summary>
    public static class PathFileEx
    {

        // 摘要: 
        //     更改路径字符串的扩展名。
        //
        // 参数: 
        //   path:
        //     要修改的路径信息。 该路径不能包含在 System.IO.Path.GetInvalidPathChars() 中定义的任何字符。
        //
        //   extension:
        //     新的扩展名（有或没有前导句点）。 指定 null 以从 path 移除现有扩展名。
        //
        // 返回结果: 
        //     已修改的路径信息。 在基于 Windows 的桌面平台上，如果 path 是 null 或空字符串 ("")，则返回的路径信息是未修改的。 如果
        //     extension 是 null，则返回的字符串包含指定的路径，其扩展名已移除。 如果 path 不具有扩展名，并且 extension 不是 null，则返回的路径字符串包含
        //     extension，它追加到 path 的结尾。
        //
        // 异常: 
        //   System.ArgumentException:
        //     path 包含 System.IO.Path.GetInvalidPathChars() 中已定义的一个或多个无效字符。
        public static string ChangeExtensionEx(this string path, string extension)
        {
            return Path.ChangeExtension(path, extension);
        }
        //
        // 摘要: 
        //     将字符串数组组合成一个路径。
        //
        // 参数: 
        //   paths:
        //     由路径的各部分构成的数组。
        //
        // 返回结果: 
        //     组合后的路径。
        //
        // 异常: 
        //   System.ArgumentException:
        //     数组中的一个字符串包含 System.IO.Path.GetInvalidPathChars() 中定义的一个或多个无效字符。
        //
        //   System.ArgumentNullException:
        //     数组中的一个字符串为 null。
        public static string CombineEx(this string[] paths)
        {
            return Path.Combine(paths);
        }

        //
        // 摘要: 
        //     返回指定路径字符串的目录信息。
        //
        // 参数: 
        //   path:
        //     文件或目录的路径。
        //
        // 返回结果: 
        //     path 的目录信息，如果 path 表示根目录或为 null，则该目录信息为 null。 如果 path 没有包含目录信息，则返回 System.String.Empty。
        //
        // 异常: 
        //   System.ArgumentException:
        //     path 参数包含无效字符、为空、或仅包含空白。
        //
        //   System.IO.PathTooLongException:
        //     path 参数的长度超过系统定义的最大长度。
        public static string GetDirectoryName(this string path)
        {
            return Path.GetDirectoryName(path);
        }
        //
        // 摘要: 
        //     返回指定的路径字符串的扩展名。
        //
        // 参数: 
        //   path:
        //     从其获取扩展名的路径字符串。
        //
        // 返回结果: 
        //     指定的路径的扩展名（包含句点“.”）、null 或 System.String.Empty。 如果 path 为 null，则 System.IO.Path.GetExtension(System.String)
        //     返回 null。 如果 path 不具有扩展名信息，则 System.IO.Path.GetExtension(System.String) 返回
        //     System.String.Empty。
        //
        // 异常: 
        //   System.ArgumentException:
        //     path 包含 System.IO.Path.GetInvalidPathChars() 中已定义的一个或多个无效字符。
        public static string GetExtension(this string path)
        {
            return Path.GetExtension(path);
        }
        //
        // 摘要: 
        //     返回指定路径字符串的文件名和扩展名。
        //
        // 参数: 
        //   path:
        //     从其获取文件名和扩展名的路径字符串。
        //
        // 返回结果: 
        //     path 中最后的目录字符后的字符。 如果 path 的最后一个字符是目录或卷分隔符，则此方法返回 System.String.Empty。 如果
        //     path 为 null，则此方法返回 null。
        //
        // 异常: 
        //   System.ArgumentException:
        //     path 包含 System.IO.Path.GetInvalidPathChars() 中已定义的一个或多个无效字符。
        public static string GetFileName(this string path)
        {
            return Path.GetFileName(path);
        }
        //
        // 摘要: 
        //     返回不具有扩展名的指定路径字符串的文件名。
        //
        // 参数: 
        //   path:
        //     文件的路径。
        //
        // 返回结果: 
        //     System.IO.Path.GetFileName(System.String) 返回的字符串，但不包括最后的句点 (.) 以及之后的所有字符。
        //
        // 异常: 
        //   System.ArgumentException:
        //     path 包含 System.IO.Path.GetInvalidPathChars() 中已定义的一个或多个无效字符。
        public static string GetFileNameWithoutExtension(this string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }
        //
        // 摘要: 
        //     返回指定路径字符串的绝对路径。
        //
        // 参数: 
        //   path:
        //     要为其获取绝对路径信息的文件或目录。
        //
        // 返回结果: 
        //     path 的完全限定的位置，例如“C:\MyFile.txt”。
        //
        // 异常: 
        //   System.ArgumentException:
        //     path 是一个零长度字符串，仅包含空白或者包含 System.IO.Path.GetInvalidPathChars() 中已定义一个或多个无效字符。
        //     - 或 - 系统未能检索绝对路径。
        //
        //   System.Security.SecurityException:
        //     调用方没有所需的权限。
        //
        //   System.ArgumentNullException:
        //     path 为 null。
        //
        //   System.NotSupportedException:
        //     path 包含一个冒号（“:”），此冒号不是卷标识符（如，“c:\”）的一部分。
        //
        //   System.IO.PathTooLongException:
        //     指定的路径、文件名或者两者都超出了系统定义的最大长度。 例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260
        //     个字符。
        public static string GetFullPath(this string path)
        {
            return Path.GetFullPath(path);
        }
        //
        // 摘要: 
        //     获取指定路径的根目录信息。
        //
        // 参数: 
        //   path:
        //     从其获取根目录信息的路径。
        //
        // 返回结果: 
        //     path 的根目录，例如“C:\”；如果 path 为 null，则为 null；如果 path 不包含根目录信息，则为空字符串。
        //
        // 异常: 
        //   System.ArgumentException:
        //     path 包含 System.IO.Path.GetInvalidPathChars() 中已定义的一个或多个无效字符。 - 或 - System.String.Empty
        //     被传递到 path。
        public static string GetPathRoot(this  string path)
        {
            return Path.GetPathRoot(path);
        }
        //
        // 摘要: 
        //     返回随机文件夹名或文件名。
        //
        // 返回结果: 
        //     随机文件夹名或文件名。
        public static string GetRandomFileName()
        {
            return Path.GetRandomFileName();
        }
        //
        // 摘要: 
        //     确定路径是否包括文件扩展名。
        //
        // 参数: 
        //   path:
        //     用于搜索扩展名的路径。
        //
        // 返回结果: 
        //     如果路径中最后的目录分隔符（\\ 或 /）或卷分隔符 (:) 之后的字符包括句点 (.)，并且后面跟有一个或多个字符，则为 true；否则为 false。
        //
        // 异常: 
        //   System.ArgumentException:
        //     path 包含 System.IO.Path.GetInvalidPathChars() 中已定义的一个或多个无效字符。
        public static bool HasExtension(this  string path)
        {
            return Path.HasExtension(path);
        }
        //
        // 摘要: 
        //     获取指示指定的路径字符串是否包含根的值。
        //
        // 参数: 
        //   path:
        //     要测试的路径。
        //
        // 返回结果: 
        //     true（如果 path 包含根）；否则为 false。
        //
        // 异常: 
        //   System.ArgumentException:
        //     path 包含 System.IO.Path.GetInvalidPathChars() 中已定义的一个或多个无效字符。
        public static bool IsPathRooted(this  string path)
        {
            return Path.IsPathRooted(path);
        }
        /// <summary> 根据文件路径获取文件 C:\WorkArea\1\3T1-Mat.data </summary>
        /// <param name="path"> 参考文件 C:\WorkArea\1\3T1.data  </param>
        /// <returns>新文件全路径 C:\WorkArea\5\3T1-Mat.data</returns>
        public static string GetFileFullPathEx(this string path, string fileName)
        {
            return Path.GetDirectoryName(path) + "\\" + Path.GetFileName(fileName);
        }

        /// <summary> 文件夹+文件 文件夹结尾带不带\\都可以 文件是不是全路径都行 </summary>
        public static string AppendFileEx(this string dic, string fileName)
        {
            return dic.EndsWith("\\")
                ? dic + Path.GetFileName(fileName)
                : dic + "\\" + Path.GetFileName(fileName);

        }

        /// <summary> 文件名扩展 3T1_100 </summary>
        /// <param name="fileName">3T1_100</param>
        /// <param name="code">'-'</param>
        /// <param name="index">"MAT"</param>
        /// <returns>3T1_100-MAT</returns>
        public static string ExFileName(this string fileName, string code, string index)
        {
            string start = fileName.Split(new string[] { code }, StringSplitOptions.RemoveEmptyEntries)[0];
            return start + code + index;
        }
    }
}
