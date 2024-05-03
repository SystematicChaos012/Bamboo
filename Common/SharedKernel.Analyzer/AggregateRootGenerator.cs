using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SharedKernel.Analyzer.Translators;

namespace SharedKernel.Analyzer;

[Generator]
public class AggregateRootGenerator : ISourceGenerator
{
    /// <summary>
    /// 聚合根标识
    /// </summary>
    private static readonly string[] AggregateRootName = ["SharedKernel.Ddd", "IAggregateRoot"];
    
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new ClassSyntaxContextReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxContextReceiver is not ClassSyntaxContextReceiver receiver)
        {
            return;
        }

        // 遍历所有 Translator
        foreach (var translator in receiver.Syntaxes.Select(syntax => new TypeTranslator(context, syntax)))
        {
            if (translator
                    .GetInterfaces()
                    .Any(x => AggregateRootName.SequenceEqual([x.GetNamespace(), x.GetClassName()])) is false)
            {
                continue;
            }

            ProcessAggregateRootSymbol(context, translator);
        }
    }

    /// <summary>
    /// 处理聚合根符号
    /// </summary>
    private static void ProcessAggregateRootSymbol(GeneratorExecutionContext context, TypeTranslator translator)
    {
        // 查找实体符号
        var entityList = new List<FieldTranslator>();
        var collectionEntityList = new List<FieldTranslator>();
        foreach (var field in translator.GetFields())
        {
            if (field.IsImplDefinitionTypeFrom(EntityGenerator.EntityName[0], EntityGenerator.EntityName[1]))
            {
                entityList.Add(field);
            }
            else if (field.IsImplDefinitionTypeFrom(typeof(ICollection<>)))
            {
                collectionEntityList.Add(field);
            }
        }
        
        // 构建源码
        var source = new StringBuilder();
        
        // usings
        var allNamespaces = translator.GetUsings()
            .Union(["SharedKernel.Ddd.Events"])
            .Union(entityList.Select(x => x.GetNamespace()))
            .Union(collectionEntityList.Select(x => x.GetNamespace()));
        
        foreach (var @namespace in allNamespaces)
        {
            source.AppendLine($"using {@namespace};");
        }
        
        // namespace
        source.AppendLine($"namespace {translator.GetNamespace()};");
        
        // class declare
        source.AppendLine($"partial class {translator.GetClassName()}");
        source.AppendLine("{");
        
        // aggregate root detail
        source.AppendLine("""
                              private int _version;
                              private List<DomainEvent> _domainEvent = [];
                              
                              /// <summary>
                              /// 提交领域事件
                              /// </summary>
                              private void Raise<T>(T domainEvent) where T : DomainEvent
                              {
                                  _version += 1;
                                  _domainEvent.Add(domainEvent);
                                  if (this is IApplier<T> applier)
                                  {
                                      applier.Apply(domainEvent);
                                  }
                              }
                          """);

        // 处理单实体
        foreach (var entity in entityList)
        {
            ProcessEntitySymbol(context, entity, source);
        }

        // class end
        source.AppendLine("}");
        
        context.AddSource($"{translator.GetClassName()}.g.cs", source.ToString());
    }

    /// <summary>
    /// 处理单实体符号
    /// </summary>
    private static void ProcessEntitySymbol(GeneratorExecutionContext context, FieldTranslator translator, StringBuilder source)
    {
        source.AppendLine($$"""
                              public {{translator.Symbol.Type.Name}} {{FieldCastToProperty(translator.Symbol.Name)}}
                              {
                                  get
                                  {
                                      return {{translator.Symbol.Name}};
                                  }
                                  set
                                  {
                                      {{translator.Symbol.Name}}UnsafeAccessor.Raise = Raise;
                                      {{translator.Symbol.Name}} = value;
                                  }
                              }
                          """);
    }

    /// <summary>
    /// 字段转属性
    /// </summary>
    /// <param name="field">字段</param>
    private static string FieldCastToProperty(string field)
    {
        var fieldSpan = field.AsSpan();

        // 第一个字符是否为下划线
        var skipFirstChar = fieldSpan[0].Equals('_');

        // 创建新 Span
        Span<char> newSpan = stackalloc char[skipFirstChar ? field.Length - 1 : field.Length];
        if (skipFirstChar)
        {
            fieldSpan.Slice(1).CopyTo(newSpan);
        }
        else
        {
            fieldSpan.CopyTo(newSpan);
        }

        ref var firstChar = ref newSpan[0];
        firstChar = char.ToUpper(firstChar);

        return new string([..newSpan]);
    }

    /// <summary>
    /// 类型语法上下文接收类
    /// </summary>
    private class ClassSyntaxContextReceiver : ISyntaxContextReceiver
    {
        /// <summary>
        /// 类翻译对象集合
        /// </summary>
        public List<ClassDeclarationSyntax> Syntaxes { get; } = [];

        /// <inheritdoc />
        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if (context.Node is ClassDeclarationSyntax classDeclarationSyntax)
            {
                Syntaxes.Add(classDeclarationSyntax);
            }
        }
    }
}