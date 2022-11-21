using System.Xml;
using System.Xml.Linq;
using Smdn.LibHighlightSharp.Xhtml;

using var hl = new XhtmlHighlight();

hl.SetTheme("github");
hl.SetSyntax("csharp");

hl.Title = "Demonstration of XHTML fragment output";
hl.PreserveWhitespace = true;

var xmlnsXhtml = (XNamespace)"http://www.w3.org/1999/xhtml";

var highlighted = new XDocument(
  new XElement(
    xmlnsXhtml + "html",
    new XElement(
      xmlnsXhtml + "body",
      new XElement(
        xmlnsXhtml + "pre",
        hl.GenerateXhtmlFragmentFromFile(GetThisFilePath())
      )
    )
  )
);

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
