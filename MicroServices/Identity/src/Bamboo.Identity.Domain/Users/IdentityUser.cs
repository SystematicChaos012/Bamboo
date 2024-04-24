using SharedKernel.Domain;

namespace Bamboo.Identity
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class IdentityUser : AggregateRoot<Guid>
    {
        /// <summary>
        /// Used by EF Core
        /// </summary>
        private IdentityUser() { }
    }
}
