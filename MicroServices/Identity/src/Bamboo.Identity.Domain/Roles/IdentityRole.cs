using SharedKernel.Domain;

namespace Bamboo.Roles
{
    /// <summary>
    /// 角色信息
    /// </summary>
    public class IdentityRole : AggregateRoot<Guid>
    {
        /// <summary>
        /// Gets or sets the primary key for this role.
        /// </summary>
        public override Guid Id { get; protected set; }

        /// <summary>
        /// Gets or sets the name for this role.
        /// </summary>
        public string? Name { get; private set; }

        /// <summary>
        /// Gets or sets the normalized name for this role.
        /// </summary>
        public string? NormalizedName { get; private set; }

        /// <summary>
        /// A random value that should change whenever a role is persisted to the store
        /// </summary>
        public string? ConcurrencyStamp { get; private set; }
    }
}
