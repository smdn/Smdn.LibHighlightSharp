using System;
using System.IO;

Console.WriteLine("Syntax files copied to the output directory:");

var highlightSyntaxDir = Path.Combine(Path.GetDirectoryName(Environment.ProcessPath)!, "highlight", "langDefs");

foreach (var syntax in Directory.EnumerateFiles(highlightSyntaxDir, "*.lang", SearchOption.AllDirectories)) {
  Console.WriteLine(syntax);
}
