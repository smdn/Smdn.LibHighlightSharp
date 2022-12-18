using Smdn.LibHighlightSharp;

const int columnWidthName = 16;
const int columnWidthDescription = 48;

Console.WriteLine($"|{"Name", -columnWidthName}|{"Description", -columnWidthDescription}|");
Console.WriteLine($"|{new string('-', columnWidthName)}|{new string('-', columnWidthDescription)}|");

using var hl = new Highlight();

foreach (var syntax in hl.EnumerateSyntaxFilesWithDescription().OrderBy(static s => Path.GetFileName(s.Path))) {
  Console.WriteLine($"|{Path.GetFileNameWithoutExtension(syntax.Path), -columnWidthName}|{syntax.Description, -columnWidthDescription}|");
}
