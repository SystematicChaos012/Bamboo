using Bamboo.Users.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Identity
{
    public sealed class IdentityUserClaimAlreadyExistsException(IdentityUserClaimId claimId) : DomainException($"声明 Id {claimId} 已存在");
}