using Audit.AuditProperties;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audit
{
    public record Property(Action<EntityTypeBuilder> Build, Action<AuditContext> Write);
}
