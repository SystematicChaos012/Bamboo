namespace Audit.Common
{
    /// <summary>
    /// 审计帮助类
    /// </summary>
    internal static class AuditHelper
    {
        /// <summary>
        /// 将字符串转换为指定类型的值
        /// </summary>
        public static object Parse(Type type, string value)
        {
            return type.GetMethod("Parse", [typeof(string)])!.Invoke(null, [value])!;
        }

        /// <summary>
        /// 获取创建人审计的 Nullable 类型
        /// </summary>
        public static Type GetNullableTypeOfGenericArgument(Type genericType, Type definition, int index)
        {
            var type = ReflectionHelper.GetInterface(genericType, definition);
            if (type == null)
            {
                ArgumentNullException.ThrowIfNull(type, nameof(type));
            }

            var isNullable = false;
            var arg = type.GetGenericArguments()[index];
            if ((arg.IsGenericType ? arg.GetGenericTypeDefinition() : arg).Equals(typeof(Nullable<>)))
            {
                isNullable = true;
            }

            return isNullable ? arg : typeof(Nullable<>).MakeGenericType(arg);
        }

        /// <summary>
        /// 获取创建人审计的 Nullable 类型
        /// </summary>
        public static Type GetNullableTypeOfGenericArgument<T>(Type definition, int index) where T : class
        {
            return GetNullableTypeOfGenericArgument(typeof(T), definition, index);
        }
    }
}
