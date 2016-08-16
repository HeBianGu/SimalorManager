using OPT.Product.SimalorManager.RegisterKeys.Eclipse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager
{
    /// <summary> 关键字服务 </summary>
    public static class BaseKeyService
    {
        /// <summary> 递归查找指定节点的八大关键字 </summary>
        public static ParentKey GetParentKey(this BaseKey bk)
        {
            if (bk.ParentKey != null)
            {
                if (bk.ParentKey is ParentKey)
                {
                    return bk.ParentKey as ParentKey;
                }
                else
                {
                    return bk.ParentKey.GetParentKey();
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary> 查找所有到 指定条件时结束 </summary>
        public static List<T> FindAllEndOfMatch<T>(this BaseKey bk, Predicate<BaseKey> match, Predicate<BaseKey> endOfmatch) where T : class
        {
            List<T> findKeys = new List<T>();

            bk.GetKeys<T>(ref findKeys, bk, match, endOfmatch);

            return findKeys;
        }

        /// <summary> 删除指定关键字同级所有关键字 isContainThis是否删除本节点 </summary>
        public static void ClearChildAfter(this BaseKey bk, bool isContainThis = true)
        {
            if (bk.ParentKey == null) return;

            int index = isContainThis ? bk.ParentKey.Keys.IndexOf(bk) + 1 : bk.ParentKey.Keys.IndexOf(bk);

            bk.ParentKey.RemoveRange(index);
        }

        /// <summary> 删除指定关键字后面所有关键字 </summary>
        public static void ClearAllAfter(this BaseKey bk, bool isContainThis = true)
        {
            if (bk.ParentKey == null) return;

            bk.ClearChildAfter(isContainThis);

            //  递归清理
            bk.ParentKey.ClearAllAfter();

        }

        /// <summary> 清理父节点只保留INCLUDE </summary>
        public static void ClearParentKey(this ParentKey parentKey)
        {
            parentKey.DeleteAll<BaseKey>(l => !(l is INCLUDE || l is ParentKey));
        }
    }
}
