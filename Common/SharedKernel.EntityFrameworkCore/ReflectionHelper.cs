using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bamboo
{
    /// <summary>
    /// 反射帮助类
    /// </summary>
    internal static class ReflectionHelper
    {
        /// <summary>
        /// 获取泛型类型中指定序号的类型
        /// </summary>
        public static Type? GetGenericArgumentType(Type type, int sequenceNumber)
        {
            var args = type.GetGenericArguments();

            if (args.Length > sequenceNumber || sequenceNumber < 0)
            {
                return null;
            }

            return args[sequenceNumber - 1];
        }

        /// <summary>
        /// 从接口静态属性中获取值
        /// </summary>
        public static TValue GetValueFromInterfaceStaticProperty<TValue>(Type interfaceType, string propertyName)
        {
            var property = interfaceType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty) 
                ?? throw new InvalidOperationException($"invalid property in '{interfaceType.Name}' type");

            var getter = property.GetMethod!;

            if (getter.ReturnType != typeof(TValue))
            {
                throw new InvalidOperationException($"invalid return type of '{propertyName}' property in '{interfaceType.Name}' type");
            }

            return (TValue)property.GetMethod!.Invoke(null, [])!;
        }
    }
}
