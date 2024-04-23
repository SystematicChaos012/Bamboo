using Audit.AuditProperties;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audit
{
    public record AuditProperty(Action<EntityTypeBuilder> Build, Action<AuditPropertyContext> Write);
}
