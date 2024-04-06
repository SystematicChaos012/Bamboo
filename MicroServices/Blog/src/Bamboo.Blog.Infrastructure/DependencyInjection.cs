using Bamboo.EntityFrameworkCore;
using Bamboo.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.UnitOfWork;

namespace Bamboo
{
    /// <summary>
    /// 依赖注入
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// 添加 Post 服务
        /// </summary>
        public static IServiceCollection AddBlogService(this IServiceCollection services, IConfiguration configuration)
        {
            // Mediator
            services.AddMediatR(options => 
            {
                options.RegisterServicesFromAssemblies([
                        typeof(Post).Assembly,
                        typeof(DependencyInjection).Assembly
                    ]);
            });

            // 添加 BlogDbContext
            services.AddDbContext<BlogDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("Blog")), ServiceLifetime.Scoped);

            // 添加 UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
