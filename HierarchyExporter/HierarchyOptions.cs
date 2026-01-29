using System;
using System.Collections.Generic;
using System.Text;

namespace HierarchyExporter;

public sealed class HierarchyOptions
{
    public int? MaxDepth { get; init; }
    public IgnoreRuleSet IgnoreRules { get; init; } = IgnoreRuleSet.Default;
}
