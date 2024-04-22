using Audit.AuditProperties;
using System.Collections.Frozen;

namespace Audit
{
    public static class AuditPropertiesManager
    {
        private readonly static FrozenDictionary<Type, AuditProperty> _definitionMap;
        private static FrozenDictionary<Type, Property[]> _cache;

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

            _cache = new Dictionary<Type, Property[]>().ToFrozenDictionary();
        }

        public static Property[] GetAuditProperties(Type entityType)
        {
            if (_cache.TryGetValue(entityType, out var cacheItem))
            {
                return cacheItem;
            }

            var properties = new List<Property>();

            foreach (var interfaceType in entityType.GetInterfaces())
            {
                var definition = interfaceType.IsGenericType ? interfaceType.GetGenericTypeDefinition() : interfaceType;
                if (_definitionMap.TryGetValue(definition, out var auditProperty) is false)
                {
                    continue;
                }

                properties.Add(auditProperty.Create(entityType));
            }

            Property[] arrayProperties = [.. properties];
            _cache = new Dictionary<Type, Property[]>(_cache)
            {
                [entityType] = arrayProperties
            }.ToFrozenDictionary();

            return arrayProperties;
        }
    }
}
