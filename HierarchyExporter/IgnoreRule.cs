using System;
using System.Collections.Generic;
using System.Text;

namespace HierarchyExporter;

public sealed class IgnoreRule
{
    public string Value { get; }
    public IgnoreRuleType Type { get; }

    public IgnoreRule(string value, IgnoreRuleType type)
    {
        Value = value;
        Type = type;
    }

    public bool MatchesDirectory(string directoryName) =>
        Type == IgnoreRuleType.Directory &&
        directoryName.Equals(Value, StringComparison.OrdinalIgnoreCase);

    public bool MatchesFile(string fileName)
    {
        return Type switch
        {
            IgnoreRuleType.FileName =>
                fileName.Equals(Value, StringComparison.OrdinalIgnoreCase),

            IgnoreRuleType.Extension =>
                fileName.EndsWith(Value, StringComparison.OrdinalIgnoreCase),

            _ => false
        };
    }
}

public enum IgnoreRuleType
{
    Directory,
    FileName,
    Extension
}