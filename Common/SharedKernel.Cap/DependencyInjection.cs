using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharedKernel.Cap
{
    /// <summary>
    /// 依赖注入
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// 添加 Post 服务
        /// </summary>
        public static IServiceCollection AddPosts(this IServiceCollection services, IConfiguration configuration)
        {
        }
    }
}
