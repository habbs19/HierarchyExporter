using System;
using System.Collections.Generic;
using System.Text;

namespace HierarchyExporter;

public sealed class FolderHierarchyExporter
{
    public string Export(string rootPath, HierarchyOptions options)
    {
        var sb = new StringBuilder();
        WriteDirectory(sb, rootPath, options, "", 0, true);
        return sb.ToString();
    }

    private void WriteDirectory(
        StringBuilder sb,
        string path,
        HierarchyOptions options,
        string indent,
        int depth,
        bool isLast)
    {
        if (options.MaxDepth.HasValue && depth > options.MaxDepth.Value)
            return;

        var name = Path.GetFileName(path);
        if (options.IgnoreRules.ShouldIgnoreDirectory(name))
            return;

        sb.AppendLine($"{indent}{(isLast ? "└── " : "├── ")}{name}/");

        var childIndent = indent + (isLast ? "    " : "│   ");

        string[] directories;
        string[] files;

        try
        {
            directories = Directory.GetDirectories(path);
            files = Directory.GetFiles(path);
        }
        catch (UnauthorizedAccessException)
        {
            sb.AppendLine($"{childIndent}[Access Denied]");
            return;
        }

        for (int i = 0; i < directories.Length; i++)
        {
            WriteDirectory(
                sb,
                directories[i],
                options,
                childIndent,
                depth + 1,
                isLast: i == directories.Length - 1 && files.Length == 0);
        }

        for (int i = 0; i < files.Length; i++)
        {
            var fileName = Path.GetFileName(files[i]);
            if (options.IgnoreRules.ShouldIgnoreFile(fileName))
                continue;

            sb.AppendLine($"{childIndent}{(i == files.Length - 1 ? "└── " : "├── ")}{fileName}");
        }
    }
}