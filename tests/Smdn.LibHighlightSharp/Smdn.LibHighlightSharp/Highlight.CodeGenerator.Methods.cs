// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
using System;
using System.IO;
using NUnit.Framework;

namespace Smdn.LibHighlightSharp;

partial class HighlightTests {
  private static string GetThisFilePath([System.Runtime.CompilerServices.CallerFilePath] string file = "") => file;

  private string GetDataDirPath()
    => Path.Combine(TestContext.CurrentContext.TestDirectory, "highlight");

  [Test]
  public void SetSyntax()
  {
    using var hl = new Highlight();

    Assert.DoesNotThrow(() => hl.SetSyntax("csharp"));
    Assert.IsNotNull(hl.SyntaxDescription);
    Assert.IsNotEmpty(hl.SyntaxDescription);
    Assert.AreEqual("C#", hl.SyntaxDescription);

    hl.Dispose();

    Assert.Throws<ObjectDisposedException>(() => hl.SetSyntax("csharp"));
  }

  [Test]
  public void SetSyntaxFromFile()
  {
    var syntaxFilePath = Path.Combine(GetDataDirPath(), "langDefs", "csharp.lang");
    using var hl = new Highlight();

    Assert.DoesNotThrow(() => hl.SetSyntaxFromFile(syntaxFilePath));
    Assert.IsNotNull(hl.SyntaxDescription);
    Assert.IsNotEmpty(hl.SyntaxDescription);
    Assert.AreEqual("C#", hl.SyntaxDescription);

    hl.Dispose();

    Assert.Throws<ObjectDisposedException>(() => hl.SetSyntaxFromFile(syntaxFilePath));
  }

  [Test]
  public void SetSyntax_ArgumentNull([Values(true, false)] bool fromFile)
  {
    using var hl = new Highlight();

    Assert.Throws<ArgumentNullException>(() => {
      if (fromFile)
        hl.SetSyntax(name: null!);
      else
        hl.SetSyntaxFromFile(pathToLangFile: null!);
    });
  }

  [Test]
  public void SetSyntax_NonExistent([Values(true, false)] bool fromFile)
  {
    const string syntaxName = "non-existent-syntax";
    using var hl = new Highlight();

    var ex = Assert.Throws<HighlightSyntaxException>(() => {
      if (fromFile)
        hl.SetSyntaxFromFile(syntaxName);
      else
        hl.SetSyntax(syntaxName);
    })!;

    StringAssert.Contains(syntaxName + (fromFile ? string.Empty : ".lang"), ex.LangFilePath);
    Assert.AreNotEqual(Bindings.LoadResult.LOAD_OK, ex.Reason);
  }

  [Test]
  public void SetTheme()
  {
    using var hl = new Highlight();

    Assert.DoesNotThrow(() => hl.SetTheme("github"));

    if (hl.ThemeDescription is not null)
      Assert.IsNotEmpty(hl.ThemeDescription);

    hl.Dispose();

    Assert.Throws<ObjectDisposedException>(() => hl.SetTheme("github"));
  }

  [Test]
  public void SetThemeFromFile()
  {
    var themeFilePath = Path.Combine(GetDataDirPath(), "themes", "github.theme");
    using var hl = new Highlight();

    Assert.DoesNotThrow(() => hl.SetThemeFromFile(themeFilePath));

    if (hl.ThemeDescription is not null)
      Assert.IsNotEmpty(hl.ThemeDescription);

    hl.Dispose();

    Assert.Throws<ObjectDisposedException>(() => hl.SetThemeFromFile(themeFilePath));
  }

  [Test]
  public void SetThemeFromFile_Base16()
  {
    if (VersionInformations.NativeLibraryVersion < Highlight.MinimumVersionSupportingBase16Themes) {
      Assert.Ignore($"not supported: {nameof(Highlight.MinimumVersionSupportingBase16Themes)}");
      return;
    }

    var themeFilePath = Path.Combine(GetDataDirPath(), "themes", "base16", "default-light.theme");
    using var hl = new Highlight();

    Assert.DoesNotThrow(() => hl.SetThemeFromFile(themeFilePath));
    Assert.IsNotNull(hl.ThemeDescription);
    Assert.IsNotEmpty(hl.ThemeDescription);
  }

  [Test]
  public void SetThemeBase16()
  {
    using var hl = new Highlight();

    void Action() => hl.SetThemeBase16("default-light");

    if (Highlight.MinimumVersionSupportingBase16Themes <= VersionInformations.NativeLibraryVersion) {
      Assert.DoesNotThrow(Action);
      Assert.IsNotNull(hl.ThemeDescription);
      Assert.IsNotEmpty(hl.ThemeDescription);
    }
    else {
      Assert.Throws<NotSupportedException>(Action);
    }

    hl.Dispose();

    Assert.Throws<ObjectDisposedException>(() => hl.SetThemeBase16("default-light"));
  }

  [Test]
  public void SetTheme_ArgumentNull([Values(true, false)] bool fromFile)
  {
    using var hl = new Highlight();

    Assert.Throws<ArgumentNullException>(() => {
      if (fromFile)
        hl.SetTheme(name: null!);
      else
        hl.SetThemeFromFile(pathToThemeFile: null!);
    });
  }

  [Test]
  public void SetThemeBase16_ArgumentNull()
  {
    using var hl = new Highlight();

    Assert.Throws<ArgumentNullException>(() => hl.SetThemeBase16(name: null!));
  }

  [Test]
  public void SetTheme_NonExistent([Values(true, false)] bool fromFile)
  {
    const string themeName = "non-existent-theme";
    using var hl = new Highlight();

    var ex = Assert.Throws<HighlightThemeException>(() => {
      if (fromFile)
        hl.SetThemeFromFile(themeName);
      else
        hl.SetTheme(themeName);
    })!;

    StringAssert.Contains(themeName + (fromFile ? string.Empty : ".theme"), ex.ThemeFilePath);
    Assert.IsNotEmpty(ex.Reason);
  }

  [Test]
  public void SetThemeBase16_NonExistent()
  {
    const string themeName = "non-existent-theme";
    using var hl = new Highlight();

    void Action() => hl.SetThemeBase16(themeName);

    if (Highlight.MinimumVersionSupportingBase16Themes <= VersionInformations.NativeLibraryVersion) {
      var ex = Assert.Throws<HighlightThemeException>(Action)!;

      StringAssert.Contains(
        Path.Join("base16", themeName + ".theme"),
        ex.ThemeFilePath
      );
      Assert.IsNotEmpty(ex.Reason);
    }
    else {
      Assert.Throws<NotSupportedException>(Action);
    }
  }

  [Test]
  public void Generate()
  {
    using var hl = new Highlight(outputType: GeneratorOutputType.Html);

    hl.SetTheme("github");
    hl.SetSyntax("csharp");

    string? generated = null;

    Assert.DoesNotThrow(() => generated = hl.Generate(input: "using System;"));
    Assert.IsNotNull(generated);
    Assert.IsNotEmpty(generated);
    StringAssert.Contains(">using</", generated);
  }

  [Test]
  public void Generate_InvalidCallingOrder()
  {
    using var hl = new Highlight(outputType: GeneratorOutputType.Html);

    hl.SetSyntax("csharp");
    hl.SetTheme("github");

    void Action() => hl.Generate(input: "using System;");

    if (4 <= VersionInformations.NativeLibraryVersion.Major)
      Assert.Throws<InvalidOperationException>(Action);
    else
      Assert.DoesNotThrow(Action);
  }

  [Test]
  public void Generate_FromFileToFile()
  {
    const string outputFilePath = $"generated-{nameof(Generate_FromFileToFile)}.html";

    using var hl = new Highlight(outputType: GeneratorOutputType.Html);

    hl.SetTheme("github");
    hl.SetSyntax("csharp");

    try {
      Assert.DoesNotThrow(() => hl.Generate(inputPath: GetThisFilePath(), outputPath: outputFilePath));

      FileAssert.Exists(outputFilePath);

      var generated = File.ReadAllText(outputFilePath);

      Assert.IsNotEmpty(generated);
      StringAssert.Contains(">" + nameof(Generate_FromFileToFile) + "</", generated);
    }
    finally {
      File.Delete(outputFilePath);
    }
  }

  [Test]
  public void Generate_FromFileToFile_InvalidCallingOrder()
  {
    const string outputFilePath = $"generated-{nameof(Generate_FromFileToFile_InvalidCallingOrder)}.html";

    using var hl = new Highlight(outputType: GeneratorOutputType.Html);

    hl.SetSyntax("csharp");
    hl.SetTheme("github");

    void Action() => hl.Generate(inputPath: GetThisFilePath(), outputPath: outputFilePath);

    try {
      if (4 <= VersionInformations.NativeLibraryVersion.Major)
        Assert.Throws<InvalidOperationException>(Action);
      else
        Assert.DoesNotThrow(Action);
    }
    finally {
      File.Delete(outputFilePath);
    }
  }

  [Test]
  public void GenerateFromFile()
  {
    using var hl = new Highlight(outputType: GeneratorOutputType.Html);

    hl.SetTheme("github");
    hl.SetSyntax("csharp");

    string? generated = null;

    Assert.DoesNotThrow(() => generated = hl.GenerateFromFile(path: GetThisFilePath()));
    Assert.IsNotNull(generated);
    Assert.IsNotEmpty(generated);
    StringAssert.Contains(">" + nameof(GenerateFromFile) + "</", generated);
  }

  [Test]
  public void GenerateFromFile_InvalidCallingOrder()
  {
    using var hl = new Highlight(outputType: GeneratorOutputType.Html);

    hl.SetSyntax("csharp");
    hl.SetTheme("github");

    void Action() => hl.GenerateFromFile(path: GetThisFilePath());

    if (4 <= VersionInformations.NativeLibraryVersion.Major)
      Assert.Throws<InvalidOperationException>(Action);
    else
      Assert.DoesNotThrow(Action);
  }

  [Test]
  public void Generate_ArgumentNull()
  {
    using var hl = new Highlight();

    Assert.Throws<ArgumentNullException>(() => hl.Generate(input: null!));
  }

  [Test]
  public void Generate_FromFileToFile_ArgumentNull()
  {
    using var hl = new Highlight();

    Assert.Throws<ArgumentNullException>(() => hl.Generate(inputPath: null!, outputPath: string.Empty), "inputPath");
    Assert.Throws<ArgumentNullException>(() => hl.Generate(inputPath: string.Empty, outputPath: null!), "outputPath");
  }

  [Test]
  public void GenerateFromFile_ArgumentNull()
  {
    using var hl = new Highlight();

    Assert.Throws<ArgumentNullException>(() => hl.GenerateFromFile(path: null!));
  }

  [Test]
  public void Generate_BreakingChangesOnOutputTypeValue_Svg()
  {
    using var hl = new Highlight(outputType: GeneratorOutputType.Svg);

    hl.SetTheme("github");
    hl.SetSyntax("csharp");

    string? generated = null;

    Assert.DoesNotThrow(() => generated = hl.Generate(input: "using System;"));
    Assert.IsNotNull(generated);
    Assert.IsNotEmpty(generated);
    StringAssert.Contains("<svg", generated);
  }

  [Test]
  public void Generate_BreakingChangesOnOutputTypeValue_TrueColor()
  {
    using var hl = new Highlight(outputType: GeneratorOutputType.EscapeSequencesTrueColor);

    hl.SetTheme("github");
    hl.SetSyntax("csharp");

    if (
      new Version(3, 43) <= VersionInformations.NativeLibraryVersion &&
      new Version(3, 44) >= VersionInformations.NativeLibraryVersion
     ) {
      Assert.Ignore("Disabled test case: see https://github.com/smdn/Smdn.LibHighlightSharp/issues/26 for detail.");
      return;
    }

    string? generated = null;

    Assert.DoesNotThrow(() => generated = hl.Generate(input: "using System;"));
    Assert.IsNotNull(generated);
    Assert.IsNotEmpty(generated);
    StringAssert.Contains("\x1b[38;2;", generated);
  }
}
