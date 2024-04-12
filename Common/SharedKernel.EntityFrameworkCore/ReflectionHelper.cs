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

        // 获取或创建一个通过反射获取的静态方法编译的委托
        public static Func<T, TReturn> GetOrCreatePropertyGetter<T, TReturn>(Type type, string propertyName, BindingFlags bindingFlags)
        {
            var property = type.GetProperty(propertyName, bindingFlags) 
                ?? throw new ArgumentException($"Property {propertyName} not found in {type.FullName}");

            var method = property.GetGetMethod() 
                ?? throw new ArgumentException($"Property {propertyName} does not have a getter in {type.FullName}");

            return (Func<T, TReturn>)method.CreateDelegate(typeof(Func<T, TReturn>));
        }
    }
}
