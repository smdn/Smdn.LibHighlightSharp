// Smdn.LibHighlightSharp.dll (Smdn.LibHighlightSharp-1.0.0-preview3)
//   Name: Smdn.LibHighlightSharp
//   AssemblyVersion: 1.0.0.0
//   InformationalVersion: 1.0.0-preview3+ec85410b835bb13a16872c9d1482d3e56deabaec
//   TargetFramework: .NETStandard,Version=v2.1
//   Configuration: Release
#nullable enable annotations

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Smdn.LibHighlightSharp;
using Smdn.LibHighlightSharp.Bindings;

namespace Smdn.LibHighlightSharp {
  public enum GeneratorOutputType : int {
    BBCode = 8,
    EscapeSequencesAnsi = 5,
    EscapeSequencesTrueColor = 11,
    EscapeSequencesXterm256 = 6,
    Html = 0,
    LaTeX = 3,
    Odt = 10,
    Pango = 9,
    Rtf = 4,
    Svg = 7,
    TeX = 2,
    Xhtml = 1,
  }

  public enum HighlightElementType : int {
    Default = 1,
    ErrorMessage = 18,
    EscapedCharacter = 6,
    HoverText = 16,
    KeywordA = 12,
    KeywordB = 13,
    KeywordC = 14,
    KeywordD = 15,
    LineNumber = 9,
    MultiLineComment = 5,
    Number = 3,
    Operator = 10,
    Other = 0,
    Preprocessor = 7,
    PreprocessorString = 8,
    SingleLineComment = 4,
    StringInterpolation = 11,
    Strings = 2,
    SyntaxError = 17,
  }

  public class Highlight : IDisposable {
    public static readonly Version MinimumVersionSupportingBase16Themes; // = "3.44"
    public static readonly Version MinimumVersionSupportingGuessFileType; // = "3.51"
    public static readonly Version MinimumVersionSupportingLoadFileTypesConfig; // = "3.51"

    public static DataDir? CreateDefaultDataDir() {}

    public Highlight(DataDir dataDir, GeneratorOutputType outputType = GeneratorOutputType.Html, bool shouldDisposeDataDir = false) {}
    public Highlight(DataDir dataDirForSyntaxes, DataDir dataDirForThemes, GeneratorOutputType outputType = GeneratorOutputType.Html, bool shouldDisposeDataDir = false) {}
    public Highlight(GeneratorOutputType outputType = GeneratorOutputType.Html) {}
    public Highlight(string dataDir, GeneratorOutputType outputType = GeneratorOutputType.Html) {}
    public Highlight(string dataDirForSyntaxes, string dataDirForThemes, GeneratorOutputType outputType = GeneratorOutputType.Html) {}

    public string BaseFont { get; set; }
    public string BaseFontSize { get; set; }
    public bool Fragment { get; set; }
    public string? GeneratorVersionString { get; }
    public bool IncrementWrappedLineNumber { get; set; }
    public bool IsolateTags { get; set; }
    public bool KeepInjections { get; set; }
    public string? LastSyntaxError { get; }
    public int LineNumberWidth { get; set; }
    public bool LineNumberZeroPadding { get; set; }
    public bool OmitVersionComment { get; set; }
    public GeneratorOutputType OutputType { get; }
    public bool PrintLineNumbers { get; set; }
    public string StyleInputPath { get; set; }
    public string StyleName { get; }
    public string StyleOutputPath { get; set; }
    public string? SyntaxCategoryDescription { get; }
    public string? SyntaxDescription { get; }
    public string? SyntaxEncodingHint { get; }
    public string? ThemeCategoryDescription { get; }
    public string? ThemeDescription { get; }
    public string Title { get; set; }
    public bool ValidateInput { get; set; }

    protected virtual void Dispose(bool disposing) {}
    public void Dispose() {}
    public string Generate(string input) {}
    public void Generate(string inputPath, string outputPath) {}
    public string GenerateFromFile(string path) {}
    public string GuessFileType(string inputFilePath) {}
    public void LoadFileTypesConfig(string fileTypesConfPath) {}
    public void SetEncoding(string encodingName) {}
    public void SetIncludeStyle(bool trueForInclude) {}
    public void SetSyntax(string name) {}
    public void SetSyntaxFromFile(string pathToLangFile) {}
    public void SetTheme(string name) {}
    public void SetThemeBase16(string name) {}
    public void SetThemeFromFile(string pathToThemeFile) {}
    public bool TryFindSyntaxFile(string name, [NotNullWhen(true)] out string? syntaxFilePath) {}
    public bool TryFindThemeBase16File(string name, [NotNullWhen(true)] out string? themeFilePath) {}
    public bool TryFindThemeFile(string name, [NotNullWhen(true)] out string? themeFilePath) {}
    public bool TryLoadFileTypesConfig() {}
  }

  public sealed class HighlightHtmlClass :
    IEquatable<HighlightHtmlClass>,
    IEquatable<string>
  {
    public static HighlightHtmlClass Default { get; }
    public static HighlightHtmlClass DefaultV3 { get; }
    public static HighlightHtmlClass DefaultV4 { get; }
    public static HighlightHtmlClass ErrorMessage { get; }
    public static HighlightHtmlClass EscapedCharacter { get; }
    public static HighlightHtmlClass Highlight { get; }
    public static HighlightHtmlClass HoverText { get; }
    public static HighlightHtmlClass KeywordA { get; }
    public static HighlightHtmlClass KeywordB { get; }
    public static HighlightHtmlClass KeywordC { get; }
    public static HighlightHtmlClass KeywordD { get; }
    public static HighlightHtmlClass LineNumber { get; }
    public static HighlightHtmlClass MultiLineComment { get; }
    public static HighlightHtmlClass Number { get; }
    public static HighlightHtmlClass Operator { get; }
    public static HighlightHtmlClass Preprocessor { get; }
    public static HighlightHtmlClass PreprocessorString { get; }
    public static HighlightHtmlClass SingleLineComment { get; }
    public static HighlightHtmlClass StringInterpolation { get; }
    public static HighlightHtmlClass Strings { get; }
    public static HighlightHtmlClass StringsV3 { get; }
    public static HighlightHtmlClass StringsV4 { get; }
    public static HighlightHtmlClass SyntaxError { get; }

    public static bool TryParse(string className, [NotNullWhen(true)] out HighlightHtmlClass? @class) {}
    public static bool TryParsePrefixed(string prefixedClassName, [NotNullWhen(true)] out HighlightHtmlClass? @class) {}

    public string ClassName { get; }
    public HighlightElementType ElementType { get; }

    public bool Equals(HighlightHtmlClass? other) {}
    public bool Equals(string? other) {}
    public override bool Equals(object? obj) {}
    public override int GetHashCode() {}
    public override string ToString() {}
  }

  public class HighlightParserException : InvalidOperationException {
    public HighlightParserException(string message) {}
    public HighlightParserException(string message, ParseError reason) {}

    public ParseError Reason { get; }
  }

  public class HighlightSyntaxException : InvalidOperationException {
    public HighlightSyntaxException(string langFilePath, LoadResult reason) {}
    public HighlightSyntaxException(string message) {}

    public string? LangFilePath { get; }
    public LoadResult Reason { get; }
  }

  public class HighlightThemeException : InvalidOperationException {
    public HighlightThemeException(string message) {}
    public HighlightThemeException(string themeFilePath, string? reason) {}

    public string? Reason { get; }
    public string? ThemeFilePath { get; }
  }
}

namespace Smdn.LibHighlightSharp.Xhtml {
  public class XhtmlHighlight : Highlight {
    protected static IEnumerable<(XElement HighlightedElement, HighlightHtmlClass HighlightClass)> EnumerateHighlightedElements(XContainer container) {}

    public XhtmlHighlight() {}
    public XhtmlHighlight(DataDir dataDir, bool shouldDisposeDataDir = false) {}
    public XhtmlHighlight(DataDir dataDirForSyntaxes, DataDir dataDirForThemes, bool shouldDisposeDataDir = false) {}
    public XhtmlHighlight(string dataDir) {}
    public XhtmlHighlight(string dataDirForSyntaxes, string dataDirForThemes) {}

    public bool PreserveWhitespace { get; set; }

    public XDocument GenerateXhtmlDocument(string input) {}
    public XDocument GenerateXhtmlDocumentFromFile(string path) {}
    public IEnumerable<XNode> GenerateXhtmlFragment(string input) {}
    public IEnumerable<XNode> GenerateXhtmlFragmentFromFile(string path) {}
    protected virtual void PostProcessXhtml(XContainer container) {}
  }
}