using OPT.Product.SimalorManager.RegisterKeys.Eclipse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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

        /// <summary> 判断当前关键字的父关键字是否匹配 </summary>
        public static bool IsMatchParent<T>(this BaseKey sender) where T : ParentKey
        {
            var pk = sender.GetParentKey();

            if (pk == null) return false;

            if (pk is T) return true;

            return false;
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

        /// <summary> 递归查找父节点 </summary>
        public static BaseKey FindParentKey(this BaseKey bk, Predicate<BaseKey> match)
        {
            if (match(bk))
            {
                return bk;
            }

            if (bk.ParentKey == null)
            {
                return null;
            }

            return bk.ParentKey.FindParentKey(match);
        }
        
        /// <summary> 按条件截取表格 </summary>
        public static List<List<T>> SpiltSpace<T>(this List<T> regions, Func<T, string> getValue, Predicate<string> matchValue) where T : Item, new()
        {

            List<List<T>> result = new List<List<T>>();

            List<T> outItems = new List<T>();

            result.Add(outItems);

            regions.Count.DoCountWhile(i =>
                {
                    T item = regions[i];

                    if (i == 0)
                    {
                        outItems.Add(item);
                        return;
                    }
                    // Todo ：泛型获取值的规则外挂 对应的列值
                    string v = getValue(item);

                    string old = getValue(regions[i - 1]);

                    if (!matchValue(v) && matchValue(old))
                    {
                        // Todo ：最后一个不匹配的项 
                        T temp = outItems.Last(l => !matchValue(getValue(l)));

                        // Todo ：上下行格式不一样
                        outItems = new List<T>();
                        result.Add(outItems);

                        // Todo ：从不匹配到匹配 多增加一行 
                        outItems.Add(temp);
                    }

                    outItems.Add(item);
                });

            return result;

        }

    }
}
