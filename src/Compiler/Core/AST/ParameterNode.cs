﻿using Compiler.CodeGenerator;
using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class ParameterNode : INode
{
    public Dictionary<string, TypeManager.DataType> Parameters { get; } = new();

    public ParameterNode()
    {
    }

    public override TranslationResult Translate(TranslationContext ctx)
    {
        throw new NotImplementedException();
    }
}