using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SharedKernel.Analyzer.Translators;

namespace SharedKernel.Analyzer;

[Generator]
public class EntityGenerator : ISourceGenerator
{
    /// <summary>
    /// 实体名称
    /// </summary>
    public static readonly string[] EntityName = ["SharedKernel.Ddd", "IAggregateRoot"];
    
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
                    .Any(x => EntityName.SequenceEqual([x.GetNamespace(), x.GetClassName()])) is false)
            {
                continue;
            }

            ProcessEntitySymbol(context, translator);
        }
    }

    /// <summary>
    /// 处理实体符号
    /// </summary>
    private void ProcessEntitySymbol(GeneratorExecutionContext context, TypeTranslator translator)
    {
        // 构建源码
        var source = new StringBuilder();
        
        // usings
        var allNamespace = translator.GetUsings().Union(["SharedKernel.Ddd.Events"]);

        foreach (var @namespace in allNamespace)
        {
            source.AppendLine($"using {@namespace};");
        }
        
        source.AppendLine($$"""
                          partial class {{translator.GetClassName()}}
                          {
                              private object _root;
                          }
                          """);

        context.AddSource($"{translator.GetClassName()}.g.cs", source.ToString());
    }

    class ClassSyntaxContextReceiver : ISyntaxContextReceiver
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