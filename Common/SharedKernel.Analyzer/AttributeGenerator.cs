using System;
using Microsoft.CodeAnalysis;

namespace SharedKernel.Analyzer;

[Generator]
public class AttributeGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
    }

    public void Execute(GeneratorExecutionContext context)
    {
        context.AddSource("AggregateRootAttribute.cs", 
            """
            using System;

            namespace SharedKernel.Ddd;

            [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
            public class AggregateRootAttribute : Attribute { }
            """);
        
        context.AddSource("EntityAttribute.cs", 
            """
            using System;

            namespace SharedKernel.Ddd;

            [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
            public class EntityAttribute : Attribute { }
            """);
        
        context.AddSource("OwnsOneAttribute.cs", 
            """
            using System;

            namespace SharedKernel.Ddd;

            [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
            public class OwnsOneAttribute : Attribute 
            {
                /// <summary>
                /// 实体类型
                /// </summary>
                public Type EntityType { get; }
                
                /// <summary>
                /// 名称
                /// </summary>
                public string Name { get; set; }
            
                public OwnsOneAttribute(Type entityType, string name)
                {
                    EntityType = entityType;
                    Name = name;
                }
            }
            """);
        
        context.AddSource("OwnsManyAttribute.cs", 
            """
            using System;

            namespace SharedKernel.Ddd;

            [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
            public class OwnsManyAttribute : Attribute 
            {
                /// <summary>
                /// 实体类型
                /// </summary>
                public Type EntityType { get; }
                
                /// <summary>
                /// 名称
                /// </summary>
                public string Name { get; }
            
                public OwnsManyAttribute(Type entityType, string name)
                {
                    EntityType = entityType;
                    Name = name;
                }
            }
            """);
    }
}