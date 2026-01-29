namespace HierarchyExporter;

internal static class Program
{
    static int Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  FolderHierarchy <rootPath> [outputFile]");
            return 1;
        }

        var rootPath = args[0];
        var outputFile = args.Length > 1
            ? args[1]
            : "folder-structure.txt";

        var options = new HierarchyOptions
        {
            MaxDepth = null,
            IgnoreRules = IgnoreRuleSet.Default
                .AddExtension(".log")
                .AddFile("appsettings.Development.json")
        };

        var exporter = new FolderHierarchyExporter();
        var result = exporter.Export(rootPath, options);

        File.WriteAllText(outputFile, result);
        Console.WriteLine($"Hierarchy written to {outputFile}");

        return 0;
    }

}
