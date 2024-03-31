using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace SharedKernel.Exceptions
{
    /// <summary>
    /// 异常处理类
    /// </summary>
    public static class ThrowHelper
    {
        public static bool ThrowWhenNull<T>([NotNullWhen(true)]T? obj, [CallerArgumentExpression("obj")] string parameterName = "")
        {
            if (obj == null)
            {
                throw new ArgumentNullException($"argument {parameterName} be null");
            }

            return true;
        }
    }
}
