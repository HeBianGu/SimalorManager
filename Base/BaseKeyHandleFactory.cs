using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPT.Product.SimalorManager.Match;

namespace OPT.Product.SimalorManager
{
    class BaseKeyHandleFactory : ServiceFactory<BaseKeyHandleFactory>
    {
        /// <summary> 增加节点方法 </summary>
        public Action<BaseKey, BaseKey> AddNodeHandler = (last, per) =>
             {
                 #region - 父关键字规则判断 -

                 if (per is ParentKey)
                 {
                     //  如果是父关键字 直接放到文件关键字下面
                     last.BaseFile.Key.Add(per);
                     return;
                 }

                 if (last is ParentKey)
                 {
                     //  上节点是父关键字 本节点不是 直接放到父关键字下面
                     last.Add(per);
                     return;
                 }

                 #endregion

                 // 如果是主文件关键字
                 if (last is FileKey)
                 {
                     last.Add(per);
                     return;
                 }

                 // 如果没有父关键字
                 if (last.ParentKey == null)
                 {
                     last.Add(per);
                     return;
                 }


                 #region - 逻辑父节点判断 -

                 if (last is IRootNode && !(per is IRootNode))
                 {
                     //  上一关键字是父节点类型 下一关键字不是 直接添加到子节点
                     last.Add(per);
                     return;
                 }


                 if (last is IRootNode && per is IRootNode)
                 {

                     // 递归查找上级父节点
                     BaseKey parent = last.FindParentKey(l => l.GetType() == per.GetType());

                     if (parent == null)
                     {
                         last.Add(per);
                         return;
                     }
                     else
                     {
                         parent.ParentKey.Add(per);
                         return;
                     }

                     ////  上一关键字是父节点类型 下一关键字也是
                     //if (last.GetType() == per.GetType())
                     //{
                     //    //  上一关键字和下一个关键字类型相同 直接添加到根节点
                     //    last.ParentKey.Add(per);
                     //    return;
                     //}

                     //if (per.GetType() == last.ParentKey.GetType())
                     //{
                     //    //  上一关键字的父节点和下一个关键字类型相同 直接添加到上一关键字父节点的根节点
                     //    last.ParentKey.ParentKey.Add(per);
                     //    return;
                     //}

                     ////last.ParentKey.Add(per);

                     //last.Add(per);
                     //return;
                 }

                 if (!(last is IRootNode) && per is IRootNode)
                 {
                     // 递归查找上级父节点
                     BaseKey parent = last.FindParentKey(l => l.GetType() == per.GetType());

                     if (parent == null)
                     {
                         last.ParentKey.Add(per);
                         return;
                     }
                     else
                     {
                         parent.ParentKey.Add(per);
                         return;
                     }

                     //if (per.GetType() == last.ParentKey.GetType())
                     //{
                     //    //  上一关键字的父节点和下一个关键字类型相同 直接添加到上一关键字父节点的根节点
                     //    last.ParentKey.ParentKey.Add(per);
                     //    return;
                     //}
                     //else
                     //{

                     //}
                 }

                 #endregion

                 #region - 普通关键字判断 -

                 //  不存在关系 直接添加到本节点的根节点
                 last.ParentKey.Add(per);

                 #endregion
             };

        /// <summary> 初始化节点方法 </summary>
        public Func<BaseKey, BaseKey, BaseKey> InitLineHandler = (last, per) =>
        {
            int index = last.Name.Trim().IndexOf(' ');

            if (index < 0) return last;

            string nameValues = last.Name.Substring(index).Trim();

            //  插入第一条 TIME  20100201D   INIT
            last.Lines.Insert(0, nameValues);

            return last;
        };

        /// <summary> 初始化节点方法 </summary>
        public Func<string, string> EachLineCmdHandler = l =>
        {
            //  正常对后空格进行处理，来判断是否是关键字，前空格不是为关键字
            return l.TrimEnd();
        };

        /// <summary> NAME 'G13' 后面有内容也视为关键字NAME </summary>
        public Predicate<string> IsKeyFormat = l =>
            {
                return l.IsKeyFormat();
            };

        /// <summary> NAME 'G13' 后面有内容也视为关键字NAME 完全大写</summary>
        public Predicate<string> IsKeyFormatUpper = l =>
        {
            if (l.StartsWith(" "))
            {
                return false;
            }

            string temp = l.FormatKey();

            return temp.IsEnglishUpper();
        };

        /// <summary> NAME 后面有内容也不视为关键字NAME 是英文 </summary>
        public Predicate<string> IsKeyFormatOnlyEnglish = l =>
        {
            if (l.StartsWith(" "))
            {
                return false;
            }

            return l.IsEnglish();
        };

        /// <summary> NAME 后面有内容也不视为关键字NAME 是英文完全大写 </summary>
        public Predicate<string> IsKeyFormatOnlyEnUpper = l =>
        {
            if (l.StartsWith(" "))
            {
                return false;
            }

            return l.IsEnglish();
        };

        /// <summary> 是否以结束符结束 </summary>
        public Predicate<string> IsKeyFormatOnlyEndChar = l =>
        {
            return l.Trim().Equals(KeyConfiger.EndFlag);
        };

    }
}
