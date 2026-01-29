using System;
using System.Collections.Generic;
using System.Text;

namespace HierarchyExporter;

public sealed class IgnoreRuleSet
{
    private readonly List<IgnoreRule> _rules = new();

    public static IgnoreRuleSet Default => new IgnoreRuleSet()
        .AddDirectory("bin")
        .AddDirectory(".vs")
        .AddDirectory(".github")
        .AddDirectory(".vscode")
        .AddDirectory("obj")
        .AddDirectory(".git")
        .AddDirectory("node_modules")
        .AddExtension(".dll")
        .AddExtension(".pdb");


    public IgnoreRuleSet AddDirectory(string name)
    {
        _rules.Add(new IgnoreRule(name, IgnoreRuleType.Directory));
        return this;
    }

    public IgnoreRuleSet AddFile(string name)
    {
        _rules.Add(new IgnoreRule(name, IgnoreRuleType.FileName));
        return this;
    }

    public IgnoreRuleSet AddExtension(string extension)
    {
        _rules.Add(new IgnoreRule(extension, IgnoreRuleType.Extension));
        return this;
    }

    public bool ShouldIgnoreDirectory(string directoryName) =>
        _rules.Any(r => r.MatchesDirectory(directoryName));

    public bool ShouldIgnoreFile(string fileName) =>
        _rules.Any(r => r.MatchesFile(fileName));
}