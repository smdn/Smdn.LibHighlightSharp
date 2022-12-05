using System;
using System.IO;

Console.WriteLine("Theme files copied to the output directory:");

var highlightThemeDir = Path.Combine(Path.GetDirectoryName(Environment.ProcessPath)!, "highlight", "themes");

foreach (var theme in Directory.EnumerateFiles(highlightThemeDir, "*.theme", SearchOption.AllDirectories)) {
  Console.WriteLine(theme);
}
