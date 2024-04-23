using Audit.AuditProperties;
using System.Collections.Frozen;

namespace Audit
{
    public static class AuditPropertiesManager
    {
        private readonly static FrozenDictionary<Type, AuditPropertyCreator> _definitionMap;
        private static FrozenDictionary<Type, AuditProperty[]> _cache;

        static AuditPropertiesManager()
        {
            _definitionMap = new Dictionary<Type, AuditPropertyCreator>()
            {
                { typeof(ICreationTime), new CreationTimeAuditPropertyCreator() },
                { typeof(ICreator<>), new CreatorAuditPropertyCreator() },
                { typeof(IModificationTime), new ModificationTimeAuditPropertyCreator() },
                { typeof(IModifier<>), new ModifierAuditPropertyCreator() },
                { typeof(IDeletionTime), new DeletionTimeAuditPropertyCreator() },
                { typeof(IDeleter<>), new DeleterAuditPropertyCreator() },
                { typeof(ILogicalDeletion), new LogicalDeletionAuditPropertyCreator() },
                { typeof(IConcurrencyStamp), new ConcurrencyStampAuditPropertyCreator() }
            }.ToFrozenDictionary();

            _cache = new Dictionary<Type, AuditProperty[]>().ToFrozenDictionary();
        }

        public static AuditProperty[] GetAuditProperties(Type entityType)
        {
            if (_cache.TryGetValue(entityType, out var cacheItem))
            {
                return cacheItem;
            }

            var properties = new List<AuditProperty>();

            foreach (var interfaceType in entityType.GetInterfaces())
            {
                var definition = interfaceType.IsGenericType ? interfaceType.GetGenericTypeDefinition() : interfaceType;
                if (_definitionMap.TryGetValue(definition, out var auditProperty) is false)
                {
                    continue;
                }

                properties.Add(auditProperty.Create(entityType));
            }

            AuditProperty[] arrayProperties = [.. properties];
            _cache = new Dictionary<Type, AuditProperty[]>(_cache)
            {
                [entityType] = arrayProperties
            }.ToFrozenDictionary();

            return arrayProperties;
        }
    }
}
