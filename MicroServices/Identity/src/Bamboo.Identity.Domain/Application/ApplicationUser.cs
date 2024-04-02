using Microsoft.AspNetCore.Identity;
using SharedKernel.Domain.Audit;

namespace Bamboo.Application
{
    /// <summary>
    /// 用户
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>, IHasCreationTime, IHasModificationTime, IHasDeletedFlag
    {
    }
}
