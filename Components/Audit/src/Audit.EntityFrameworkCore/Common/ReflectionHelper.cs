namespace Audit.Common
{
    /// <summary>
    /// 反射帮助类
    /// </summary>
    internal static class ReflectionHelper
    {
        /// <summary>
        /// 获取接口
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="definitionType">定义类型</param>
        /// <returns>接口</returns>
        public static Type? GetInterface(Type type, Type definitionType)
        {
            ArgumentNullException.ThrowIfNull(type);

            return type.GetInterfaces().Where(x => (x.IsGenericType ? x.GetGenericTypeDefinition() : x).Equals(definitionType)).FirstOrDefault();
        }
    }
}
