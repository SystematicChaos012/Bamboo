using Microsoft.AspNetCore.Identity;
using SharedKernel.Domain.Audit;

namespace Bamboo.Application
{
    /// <summary>
    /// 角色
    /// </summary>
    public class ApplicationRole : IdentityRole<Guid>, IHasCreationTime, IHasModificationTime, IHasDeletedFlag
    {
    }
}
