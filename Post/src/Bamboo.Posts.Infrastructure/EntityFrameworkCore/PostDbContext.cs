using Microsoft.EntityFrameworkCore;

namespace Bamboo.EntityFrameworkCore
{
    /// <summary>
    /// Post 数据上下文
    /// </summary>
    public sealed class PostDbContext(DbContextOptions<PostDbContext> options) : DbContext(options)
    {

    }
}
