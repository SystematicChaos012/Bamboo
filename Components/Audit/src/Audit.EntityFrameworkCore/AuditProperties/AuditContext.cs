﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace Audit.AuditProperties
{
    /// <summary>
    /// 审计上下文
    /// </summary>
    public record class AuditContext(IServiceProvider ServiceProvider, EntityState EntityState, EntityEntry EntityEntry);
}