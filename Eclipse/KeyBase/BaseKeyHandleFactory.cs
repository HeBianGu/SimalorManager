using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager
{
    class BaseKeyHandleFactory : ServiceFactory<BaseKeyHandleFactory>
    {
        /// <summary> 增加节点方法 </summary>
       public Action<BaseKey,BaseKey> AddNodeHandler = (last,per )=>
            {
                #region - 父关键字规则判断 -
                if (per is ParentKey)
                {
                    //  如果是父关键字 直接放到文件关键字下面
                     last.BaseFile.Key.Add(per);
                     return;
                }

                if(last is ParentKey)
                {
                        //  上节点是父关键字 本节点不是 直接放到父关键字下面
                    last.Add(per);
                    return;
                }
                #endregion

                #region - 逻辑父节点判断 -

                if(last is IRootNode&&!(per is IRootNode) )
                {
                    //  上一关键字是父节点类型 下一关键字不是 直接添加到子节点
                    last.Add(per);
                    return;
                }
                if (last is IRootNode && per is IRootNode)
                {
                    //  上一关键字是父节点类型 下一关键字也是
                    if(last.GetType()==per.GetType())
                    {
                        //  上一关键字和下一个关键字类型相同 直接添加到根节点
                        last.ParentKey.Add(per);
                        return;
                    }

                    if(per.GetType()==last.ParentKey.GetType())
                    {
                        //  上一关键字的父节点和下一个关键字类型相同 直接添加到上一关键字父节点的根节点
                        last.ParentKey.ParentKey.Add(per);
                        return;
                    }

                    last.ParentKey.Add(per);
                    return;
                }

                if (!(last is IRootNode) && per is IRootNode)
                {
                    if (per.GetType() == last.ParentKey.GetType())
                    {
                        //  上一关键字的父节点和下一个关键字类型相同 直接添加到上一关键字父节点的根节点
                        last.ParentKey.ParentKey.Add(per);
                        return;
                    }
                    else
                    {
                        last.ParentKey.Add(per);
                        return;
                    }
                }

                #endregion

                #region - 普通关键字判断 -

                //  不存在关系 直接添加到本节点的根节点
                last.ParentKey.Add(per);
                #endregion
            };

       ///// <summary> 增加子节点方法 </summary>
       //public Action<BaseKey, BaseKey> AddChildPosition = (l, k) =>
       //{
           
       //};


       ///// <summary> 插入到文件的子节点下面 </summary>
       //public Action<BaseKey, BaseKey> AddFileKeyPosition = (l, k) =>
       //{
       //    l.BaseFile.Key.Add(k);
       //};
    }
}
