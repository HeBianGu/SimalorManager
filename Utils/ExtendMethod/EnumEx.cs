using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager.Uril
{
    /// <summary> 公用枚举扩展类  --   Add by lihaijun 2015.09.30 11：42 </summary>
    public static class EnumUril
    {
        /// <summary>
        /// 获得枚举字段的特性(Attribute)，该属性不允许多次定义。
        /// </summary>
        /// <typeparam name="T">特性类型。</typeparam>
        /// <param name="value">一个枚举的实例对象。</param>
        /// <returns>枚举字段的扩展属性。如果不存在则返回 <c>null</c> 。</returns>
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            return Attribute.GetCustomAttribute(field, typeof(T)) as T;
        }
    }
}
