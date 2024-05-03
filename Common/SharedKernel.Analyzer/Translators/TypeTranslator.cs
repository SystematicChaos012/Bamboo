using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SharedKernel.Analyzer.Translators;

/// <summary>
/// 类翻译器
/// </summary>
[DebuggerStepThrough]
public class TypeTranslator
{
    /// <summary>
    /// 生成器上下文
    /// </summary>
    public GeneratorExecutionContext Context { get; }

    /// <summary>
    /// 符号
    /// </summary>
    public INamedTypeSymbol Symbol { get; }

    /// <summary>
    /// 创建类翻译器
    /// </summary>
    /// <param name="context">生成器上下文</param>
    /// <param name="symbol">符号</param>
    public TypeTranslator(GeneratorExecutionContext context, INamedTypeSymbol symbol)
    {
        Context = context;
        Symbol = symbol;
    }

    /// <summary>
    /// 创建类翻译器
    /// </summary>
    /// <param name="context">生成器上下文</param>
    /// <param name="syntax">类语法</param>
    public TypeTranslator(GeneratorExecutionContext context, ClassDeclarationSyntax syntax)
    {
        Context = context;

        // 获取语义模型
        var model = Context.Compilation.GetSemanticModel(syntax.SyntaxTree);

        // 获取符号
        if (model.GetDeclaredSymbol(syntax) is not INamedTypeSymbol symbol)
        {
            throw new InvalidOperationException();
        }

        Symbol = symbol;
    }

    /// <summary>
    /// 获取类名
    /// </summary>
    /// <returns>类名</returns>
    public string GetClassName()
    {
        return Symbol.Name;
    }

    /// <summary>
    /// 获取父类
    /// </summary>
    /// <returns>父类</returns>
    public TypeTranslator? GetBaseClass()
    {
        return Symbol.BaseType == null ? null : new TypeTranslator(Context, Symbol.BaseType);
    }

    /// <summary>
    /// 获取命名空间
    /// </summary>
    /// <returns>命名空间</returns>
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

    /// <summary>
    /// 获取所有引用
    /// </summary>
    public IEnumerable<string> GetUsings()
    {
        return Symbol.DeclaringSyntaxReferences
            .SelectMany(reference =>
            {
                var syntax = reference.GetSyntax();
                while (syntax.Parent != null)
                {
                    syntax = syntax.Parent;
                }

                return syntax
                    .DescendantNodes()
                    .OfType<UsingDirectiveSyntax>()
                    .Select(directiveSyntax =>
                        directiveSyntax.Name!.ToString());
            });
    }

    /// <summary>
    /// 获取字段
    /// </summary>
    public IEnumerable<FieldTranslator> GetFields()
    {
        return Symbol.GetMembers().OfType<IFieldSymbol>().Select(x => new FieldTranslator(Context, x));
    }

    /// <summary>
    /// 获取接口
    /// </summary>
    public IEnumerable<TypeTranslator> GetInterfaces()
    {
        return Symbol.Interfaces.Select(x => new TypeTranslator(Context, x));
    }
}