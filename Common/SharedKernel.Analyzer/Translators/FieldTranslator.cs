using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace SharedKernel.Analyzer.Translators;

/// <summary>
/// 字段翻译器
/// </summary>
[DebuggerStepThrough]
public class FieldTranslator
{
    /// <summary>
    /// 生成器上下文
    /// </summary>
    public GeneratorExecutionContext Context { get; }
    
    /// <summary>
    /// 字段符号
    /// </summary>
    public IFieldSymbol Symbol { get; }

    /// <summary>
    /// 创建字段翻译器
    /// </summary>
    /// <param name="context">生成器上下文</param>
    /// <param name="symbol">符号</param>
    public FieldTranslator(GeneratorExecutionContext context, IFieldSymbol symbol)
    {
        Context = context;
        Symbol = symbol;
    }

    /// <summary>
    /// 是否实现自
    /// </summary>
    /// <param name="definitionType">定义类型</param>
    public bool IsImplDefinitionTypeFrom(Type definitionType)
    {
        if (Symbol.Type is not INamedTypeSymbol namedTypeSymbol)
        {
            return false;
        }

        if (definitionType.FullName == null)
        {
            return false;
        }

        var definitionTypeSymbol = Context.Compilation.GetTypeByMetadataName(definitionType.FullName);

        return namedTypeSymbol.AllInterfaces.Any(x => SymbolEqualityComparer.Default.Equals(x.OriginalDefinition, definitionTypeSymbol));
    }

    /// <summary>
    /// 是否实现自
    /// </summary>
    /// <param name="namespace">命名空间</param>
    /// <param name="typeName">类型名称</param>
    public bool IsImplDefinitionTypeFrom(string @namespace, string typeName)
    {
        if (Symbol.Type is not INamedTypeSymbol namedTypeSymbol)
        {
            return false;
        }

        return namedTypeSymbol.AllInterfaces.Select(x =>
        {
            var translator = new TypeTranslator(Context, x);
            return new[] { translator.GetNamespace(), translator.GetClassName() };
        }).Any(x => x.SequenceEqual([@namespace, typeName]));
    }

    /// <summary>
    /// 获取命名空间
    /// </summary>
    public string GetNamespace()
    {
        var namespaceSymbol = Symbol.ContainingNamespace;
        var buffer = new List<string>();

        while (namespaceSymbol is not null and { ContainingNamespace: not null })
        {
            buffer.Add(namespaceSymbol.Name);
            namespaceSymbol = namespaceSymbol.ContainingNamespace;
        }

        return string.Join(".", ((IEnumerable<string>)buffer).Reverse());
    }
}