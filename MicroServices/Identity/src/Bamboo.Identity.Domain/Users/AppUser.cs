using Audit;
using SharedKernel.Domain;

namespace Bamboo.Identity
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class AppUser 
        : AggregateRoot<Guid>
        , IConcurrencyStamp, ICreationTime, ICreator<Guid>, IModificationTime, IModifier<Guid>, IDeletionTime, IDeleter<Guid>, ILogicalDeletion
    {
        /// <summary>
        /// Used by EF Core
        /// </summary>
        private AppUser() { }
    }
}
