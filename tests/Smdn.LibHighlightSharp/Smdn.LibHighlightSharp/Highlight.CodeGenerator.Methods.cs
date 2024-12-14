// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
using System;
using System.IO;

using NUnit.Framework;
using NUnit.Framework.Legacy;

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
    Assert.That(hl.SyntaxDescription, Is.Not.Null);
    Assert.That(hl.SyntaxDescription, Is.Not.Empty);
    Assert.That(hl.SyntaxDescription, Is.EqualTo("C#"));

    hl.Dispose();

    Assert.Throws<ObjectDisposedException>(() => hl.SetSyntax("csharp"));
  }

  [Test]
  public void SetSyntaxFromFile()
  {
    var syntaxFilePath = Path.Combine(GetDataDirPath(), "langDefs", "csharp.lang");
    using var hl = new Highlight();

    Assert.DoesNotThrow(() => hl.SetSyntaxFromFile(syntaxFilePath));
    Assert.That(hl.SyntaxDescription, Is.Not.Null);
    Assert.That(hl.SyntaxDescription, Is.Not.Empty);
    Assert.That(hl.SyntaxDescription, Is.EqualTo("C#"));

    hl.Dispose();

    Assert.Throws<ObjectDisposedException>(() => hl.SetSyntaxFromFile(syntaxFilePath));
  }

  [Test]
  public void SetSyntax_ArgumentNull([Values] bool fromFile)
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
  public void SetSyntax_NonExistent([Values] bool fromFile)
  {
    const string SyntaxName = "non-existent-syntax";
    using var hl = new Highlight();

    var ex = Assert.Throws<HighlightSyntaxException>(() => {
      if (fromFile)
        hl.SetSyntaxFromFile(SyntaxName);
      else
        hl.SetSyntax(SyntaxName);
    })!;

    Assert.That(ex.LangFilePath, Does.Contain(SyntaxName + (fromFile ? string.Empty : ".lang")));
    Assert.That(ex.Reason, Is.Not.EqualTo(Bindings.LoadResult.LOAD_OK));
  }

  [Test]
  public void SetTheme()
  {
    using var hl = new Highlight();

    Assert.DoesNotThrow(() => hl.SetTheme("github"));

    if (hl.ThemeDescription is not null)
      Assert.That(hl.ThemeDescription, Is.Not.Empty);

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
      Assert.That(hl.ThemeDescription, Is.Not.Empty);

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
    Assert.That(hl.ThemeDescription, Is.Not.Null);
    Assert.That(hl.ThemeDescription, Is.Not.Empty);
  }

  [Test]
  public void SetThemeBase16()
  {
    using var hl = new Highlight();

    void Action() => hl.SetThemeBase16("default-light");

    if (Highlight.MinimumVersionSupportingBase16Themes <= VersionInformations.NativeLibraryVersion) {
      Assert.DoesNotThrow(Action);
      Assert.That(hl.ThemeDescription, Is.Not.Null);
      Assert.That(hl.ThemeDescription, Is.Not.Empty);
    }
    else {
      Assert.Throws<NotSupportedException>(Action);
    }

    hl.Dispose();

    Assert.Throws<ObjectDisposedException>(() => hl.SetThemeBase16("default-light"));
  }

  [Test]
  public void SetTheme_ArgumentNull([Values] bool fromFile)
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
  public void SetTheme_NonExistent([Values] bool fromFile)
  {
    const string ThemeName = "non-existent-theme";
    using var hl = new Highlight();

    var ex = Assert.Throws<HighlightThemeException>(() => {
      if (fromFile)
        hl.SetThemeFromFile(ThemeName);
      else
        hl.SetTheme(ThemeName);
    })!;

    Assert.That(ex.ThemeFilePath, Does.Contain(ThemeName + (fromFile ? string.Empty : ".theme")));
    Assert.That(ex.Reason, Is.Not.Empty);
  }

  [Test]
  public void SetThemeBase16_NonExistent()
  {
    const string ThemeName = "non-existent-theme";
    using var hl = new Highlight();

    void Action() => hl.SetThemeBase16(ThemeName);

    if (Highlight.MinimumVersionSupportingBase16Themes <= VersionInformations.NativeLibraryVersion) {
      var ex = Assert.Throws<HighlightThemeException>(Action)!;

      Assert.That(
        ex.ThemeFilePath
, Does.Contain(Path.Join("base16", ThemeName + ".theme")));
      Assert.That(ex.Reason, Is.Not.Empty);
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
    Assert.That(generated, Is.Not.Null);
    Assert.That(generated, Is.Not.Empty);
    Assert.That(generated, Does.Contain(">using</"));
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
    const string OutputFilePath = $"generated-{nameof(Generate_FromFileToFile)}.html";

    using var hl = new Highlight(outputType: GeneratorOutputType.Html);

    hl.SetTheme("github");
    hl.SetSyntax("csharp");

    try {
      Assert.DoesNotThrow(() => hl.Generate(inputPath: GetThisFilePath(), outputPath: OutputFilePath));

      FileAssert.Exists(OutputFilePath);

      var generated = File.ReadAllText(OutputFilePath);

      Assert.That(generated, Is.Not.Empty);
      Assert.That(generated, Does.Contain(">" + nameof(Generate_FromFileToFile) + "</"));
    }
    finally {
      File.Delete(OutputFilePath);
    }
  }

  [Test]
  public void Generate_FromFileToFile_InvalidCallingOrder()
  {
    const string OutputFilePath = $"generated-{nameof(Generate_FromFileToFile_InvalidCallingOrder)}.html";

    using var hl = new Highlight(outputType: GeneratorOutputType.Html);

    hl.SetSyntax("csharp");
    hl.SetTheme("github");

    void Action() => hl.Generate(inputPath: GetThisFilePath(), outputPath: OutputFilePath);

    try {
      if (4 <= VersionInformations.NativeLibraryVersion.Major)
        Assert.Throws<InvalidOperationException>(Action);
      else
        Assert.DoesNotThrow(Action);
    }
    finally {
      File.Delete(OutputFilePath);
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
    Assert.That(generated, Is.Not.Null);
    Assert.That(generated, Is.Not.Empty);
    Assert.That(generated, Does.Contain(">" + nameof(GenerateFromFile) + "</"));
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
    Assert.That(generated, Is.Not.Null);
    Assert.That(generated, Is.Not.Empty);
    Assert.That(generated, Does.Contain("<svg"));
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
    Assert.That(generated, Is.Not.Null);
    Assert.That(generated, Is.Not.Empty);
    Assert.That(generated, Does.Contain("\x1b[38;2;"));
  }

  [Test]
  public void Generate_AdditionalEndOfFileChar()
  {
    if (VersionInformations.NativeLibraryVersion < new Version(4, 6)) {
      Assert.Ignore($"not supported: {nameof(Highlight.AdditionalEndOfFileChar)}");
      return;
    }

    using var hl = new Highlight(outputType: GeneratorOutputType.Html);

    hl.SetTheme("github");
    hl.SetSyntax("csharp");
    hl.AdditionalEndOfFileChar = '\x04'; // CTRL-D / EOT (end of transmission)

    string? generated = null;

    Assert.DoesNotThrow(
      () => generated = hl.Generate(input: @$"using System;
{hl.AdditionalEndOfFileChar}
Console.WriteLine(""Hello, world!"")"
      )
    );
    Assert.That(generated, Is.Not.Null);
    Assert.That(generated, Is.Not.Empty);
    Assert.That(generated, Does.Contain(">using</"));
    Assert.That(generated, Does.Not.Contain("Hello, world!"));
  }
}
