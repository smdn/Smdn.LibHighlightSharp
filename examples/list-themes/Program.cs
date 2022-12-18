using Smdn.LibHighlightSharp;

const int columnWidthName = 30;
const int columnWidthDescription = 48;

using var hl = new Highlight();

var allThemesWithDescription = hl.EnumerateThemeFilesWithDescription();
var base16ThemesWithDescription = allThemesWithDescription.Where(static theme => Path.GetDirectoryName(theme.Path)!.EndsWith("base16"));

Console.WriteLine($"|{"Name", -columnWidthName}|{"Description", -columnWidthDescription}|");
Console.WriteLine($"|{new string('-', columnWidthName)}|{new string('-', columnWidthDescription)}|");

foreach (var theme in allThemesWithDescription.Except(base16ThemesWithDescription).OrderBy(static s => Path.GetFileName(s.Path))) {
  Console.WriteLine($"|{Path.GetFileNameWithoutExtension(theme.Path), -columnWidthName}|{theme.Description, -columnWidthDescription}|");
}

Console.WriteLine();
Console.WriteLine($"|{"Name (Base16)", -columnWidthName}|{"Description", -columnWidthDescription}|");
Console.WriteLine($"|{new string('-', columnWidthName)}|{new string('-', columnWidthDescription)}|");

foreach (var theme in base16ThemesWithDescription.OrderBy(static s => Path.GetFileName(s.Path))) {
  Console.WriteLine($"|{Path.GetFileNameWithoutExtension(theme.Path), -columnWidthName}|{theme.Description, -columnWidthDescription}|");
}
