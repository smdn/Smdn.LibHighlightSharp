using System.IO;
using System.Text;
using Smdn.LibHighlightSharp;

// Creates an instance that generates code highlighted as a HTML document.
using var hl = new Highlight(GeneratorOutputType.Html);

// Sets 'github' to the theme.
hl.SetTheme("github");

// Sets 'csharp' to the language/syntax of input code.
hl.SetSyntax("csharp");

// Sets other options
hl.Title = "Hello, world!";
hl.SetIncludeStyle(true);
hl.SetEncoding("UTF-8");

// Generates the highlighted code from string.
var input = @"using System;
Console.WriteLine(""Hello, world!"");";

File.WriteAllText("HelloWorld.html", hl.Generate(input), Encoding.UTF8);
