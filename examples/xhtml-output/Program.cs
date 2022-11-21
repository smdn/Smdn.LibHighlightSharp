using System.Xml;
using Smdn.LibHighlightSharp.Xhtml;

using var hl = new XhtmlHighlight();

hl.SetTheme("github");
hl.SetSyntax("csharp");

hl.Title = "Demonstration of XHTML output";
hl.PreserveWhitespace = false;

var highlighted = hl.GenerateXhtmlDocumentFromFile(GetThisFilePath());

var settings = new XmlWriterSettings() {
  Encoding = Console.OutputEncoding,
  Indent = true,
  IndentChars = "  ",
  NewLineChars = Environment.NewLine,
  CloseOutput = false,
};

using var writer = XmlWriter.Create(Console.OpenStandardOutput(), settings);

highlighted.Save(writer);

static string GetThisFilePath([System.Runtime.CompilerServices.CallerFilePath] string file = "") => file;
