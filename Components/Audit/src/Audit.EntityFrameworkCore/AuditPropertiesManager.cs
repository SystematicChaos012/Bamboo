using Audit.AuditProperties;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Frozen;

namespace Audit
{
    public static class AuditPropertiesManager
    {
        private readonly static FrozenDictionary<Type, AuditProperty> _definitionMap;

        static AuditPropertiesManager()
        {
            _definitionMap = new Dictionary<Type, AuditProperty>()
            {
                { typeof(ICreationTime), new CreationTimeAuditProperty() },
                { typeof(ICreator<>), new CreatorAuditProperty() },
                { typeof(IModificationTime), new ModificationTimeAuditProperty() },
                { typeof(IModifier<>), new ModifierAuditProperty() },
                { typeof(IDeletionTime), new DeletionTimeAuditProperty() },
                { typeof(IDeleter<>), new DeleterAuditProperty() },
                { typeof(ILogicalDeletion), new LogicalDeletionAuditProperty() },
                { typeof(IConcurrencyStamp), new ConcurrencyStampAuditProperty() }
            }.ToFrozenDictionary();
        }

        public static IEnumerable<(Action<EntityTypeBuilder> Builder, Action<AuditContext> Writer)> GetAuditProperties(Type entityType)
        {
            foreach (var interfaceType in entityType.GetInterfaces())
            {
                var definition = interfaceType.IsGenericType ? interfaceType.GetGenericTypeDefinition() : interfaceType;
                if (_definitionMap.TryGetValue(definition, out var auditProperty) is false)
                {
                    continue;
                }

                yield return auditProperty.Create(entityType);
            }
        }
    }
}
