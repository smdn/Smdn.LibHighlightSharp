using Smdn.LibHighlightSharp;

// Creates an instance that generates code highlighted with the ANSI escape sequences.
using var hl = new Highlight(GeneratorOutputType.EscapeSequencesAnsi);

// Gets the source file path to be highlighted.
var sourceFilePath = GetSourceFilePath();

// This code block is intended to demonstrate the language elements to be highlighted, such as directives and string interpolations.
#if SHOW_HEADER || true
Console.WriteLine($"This exmaple outputs the '{sourceFilePath}' highlighted with '{Highlight.GeneratorInformationalVersion}'.");
Console.WriteLine(new string('-', 120));
#endif

// Sets the line number format
hl.PrintLineNumbers = true;
hl.LineNumberWidth = 3;
hl.LineNumberZeroPadding = true;

hl.SetTheme("github"); // Sets 'github' to the theme.
hl.SetSyntax("csharp"); // Sets 'csharp' to the language/syntax of input code.

// Generates the highlighted code.
// If you are running on a terminal that supports ANSI escape sequences, you will see the highlighted code.
Console.WriteLine(hl.GenerateFromFile(sourceFilePath));

/// <returns>
/// If this method is called with no arguments, the path to this file is returned.
/// Otherwise, the passed arguments is returned as is.
/// </returns>
static string GetSourceFilePath([System.Runtime.CompilerServices.CallerFilePath] string file = "") => file;
