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

    Assert.That(() => hl.SetSyntax("csharp"), Throws.Nothing);
    Assert.That(hl.SyntaxDescription, Is.Not.Null);
    Assert.That(hl.SyntaxDescription, Is.Not.Empty);
    Assert.That(hl.SyntaxDescription, Is.EqualTo("C#"));

    hl.Dispose();

    Assert.That(() => hl.SetSyntax("csharp"), Throws.TypeOf<ObjectDisposedException>());
  }

  [Test]
  public void SetSyntaxFromFile()
  {
    var syntaxFilePath = Path.Combine(GetDataDirPath(), "langDefs", "csharp.lang");
    using var hl = new Highlight();

    Assert.That(() => hl.SetSyntaxFromFile(syntaxFilePath), Throws.Nothing);
    Assert.That(hl.SyntaxDescription, Is.Not.Null);
    Assert.That(hl.SyntaxDescription, Is.Not.Empty);
    Assert.That(hl.SyntaxDescription, Is.EqualTo("C#"));

    hl.Dispose();

    Assert.That(() => hl.SetSyntaxFromFile(syntaxFilePath), Throws.TypeOf<ObjectDisposedException>());
  }

  [Test]
  public void SetSyntax_ArgumentNull([Values] bool fromFile)
  {
    using var hl = new Highlight();

    Assert.That(
      () => {
        if (fromFile)
          hl.SetSyntax(name: null!);
        else
          hl.SetSyntaxFromFile(pathToLangFile: null!);
      },
      Throws.ArgumentNullException
    );
  }

  [Test]
  public void SetSyntax_NonExistent([Values] bool fromFile)
  {
    const string SyntaxName = "non-existent-syntax";
    using var hl = new Highlight();

    Assert.That(
      () => {
        if (fromFile)
          hl.SetSyntaxFromFile(SyntaxName);
        else
          hl.SetSyntax(SyntaxName);
      },
      Throws
        .TypeOf<HighlightSyntaxException>()
        .With
        .Property(nameof(HighlightSyntaxException.LangFilePath)).Contain(SyntaxName + (fromFile ? string.Empty : ".lang"))
        .And
        .Property(nameof(HighlightSyntaxException.Reason)).Not.EqualTo(Bindings.LoadResult.LOAD_OK)
    );
  }

  [Test]
  public void SetTheme()
  {
    using var hl = new Highlight();

    Assert.That(() => hl.SetTheme("github"), Throws.Nothing);

    if (hl.ThemeDescription is not null)
      Assert.That(hl.ThemeDescription, Is.Not.Empty);

    hl.Dispose();

    Assert.That(() => hl.SetTheme("github"), Throws.TypeOf<ObjectDisposedException>());
  }

  [Test]
  public void SetThemeFromFile()
  {
    var themeFilePath = Path.Combine(GetDataDirPath(), "themes", "github.theme");
    using var hl = new Highlight();

    Assert.That(() => hl.SetThemeFromFile(themeFilePath), Throws.Nothing);

    if (hl.ThemeDescription is not null)
      Assert.That(hl.ThemeDescription, Is.Not.Empty);

    hl.Dispose();

    Assert.That(() => hl.SetThemeFromFile(themeFilePath), Throws.TypeOf<ObjectDisposedException>());
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

    Assert.That(() => hl.SetThemeFromFile(themeFilePath), Throws.Nothing);
    Assert.That(hl.ThemeDescription, Is.Not.Null);
    Assert.That(hl.ThemeDescription, Is.Not.Empty);
  }

  [Test]
  public void SetThemeBase16()
  {
    using var hl = new Highlight();

    void Action() => hl.SetThemeBase16("default-light");

    if (Highlight.MinimumVersionSupportingBase16Themes <= VersionInformations.NativeLibraryVersion) {
      Assert.That(Action, Throws.Nothing);
      Assert.That(hl.ThemeDescription, Is.Not.Null);
      Assert.That(hl.ThemeDescription, Is.Not.Empty);
    }
    else {
      Assert.That(Action, Throws.TypeOf<NotSupportedException>());
    }

    hl.Dispose();

    Assert.That(() => hl.SetThemeBase16("default-light"), Throws.TypeOf<ObjectDisposedException>());
  }

  [Test]
  public void SetTheme_ArgumentNull([Values] bool fromFile)
  {
    using var hl = new Highlight();

    Assert.That(
      () => {
        if (fromFile)
          hl.SetTheme(name: null!);
        else
          hl.SetThemeFromFile(pathToThemeFile: null!);
      },
      Throws.ArgumentNullException
    );
  }

  [Test]
  public void SetThemeBase16_ArgumentNull()
  {
    using var hl = new Highlight();

    Assert.That(() => hl.SetThemeBase16(name: null!), Throws.ArgumentNullException);
  }

  [Test]
  public void SetTheme_NonExistent([Values] bool fromFile)
  {
    const string ThemeName = "non-existent-theme";
    using var hl = new Highlight();

    Assert.That(
      () => {
        if (fromFile)
          hl.SetThemeFromFile(ThemeName);
        else
          hl.SetTheme(ThemeName);
      },
      Throws
        .TypeOf<HighlightThemeException>()
        .With
        .Property(nameof(HighlightThemeException.ThemeFilePath)).Contain(ThemeName + (fromFile ? string.Empty : ".theme"))
        .And
        .Property(nameof(HighlightThemeException.Reason)).Not.Empty
    );
  }

  [Test]
  public void SetThemeBase16_NonExistent()
  {
    const string ThemeName = "non-existent-theme";
    using var hl = new Highlight();

    void Action() => hl.SetThemeBase16(ThemeName);

    Assert.That(
      Action,
      Highlight.MinimumVersionSupportingBase16Themes <= VersionInformations.NativeLibraryVersion
        ? Throws
          .TypeOf<HighlightThemeException>()
          .With
          .Property(nameof(HighlightThemeException.ThemeFilePath)).Contain(Path.Join("base16", ThemeName + ".theme"))
          .And
          .Property(nameof(HighlightThemeException.Reason)).Not.Empty
        : Throws.TypeOf<NotSupportedException>()
    );
  }

  [Test]
  public void Generate()
  {
    using var hl = new Highlight(outputType: GeneratorOutputType.Html);

    hl.SetTheme("github");
    hl.SetSyntax("csharp");

    string? generated = null;

    Assert.That(() => generated = hl.Generate(input: "using System;"), Throws.Nothing);
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
      Assert.That(Action, Throws.InvalidOperationException);
    else
      Assert.That(Action, Throws.Nothing);
  }

  [Test]
  public void Generate_FromFileToFile()
  {
    const string OutputFilePath = $"generated-{nameof(Generate_FromFileToFile)}.html";

    using var hl = new Highlight(outputType: GeneratorOutputType.Html);

    hl.SetTheme("github");
    hl.SetSyntax("csharp");

    try {
      Assert.That(
        () => hl.Generate(inputPath: GetThisFilePath(), outputPath: OutputFilePath),
        Throws.Nothing
      );

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
        Assert.That(Action, Throws.InvalidOperationException);
      else
        Assert.That(Action, Throws.Nothing);
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

    Assert.That(() => generated = hl.GenerateFromFile(path: GetThisFilePath()), Throws.Nothing);
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
      Assert.That(Action, Throws.InvalidOperationException);
    else
      Assert.That(Action, Throws.Nothing);
  }

  [Test]
  public void Generate_ArgumentNull()
  {
    using var hl = new Highlight();

    Assert.That(() => hl.Generate(input: null!), Throws.ArgumentNullException);
  }

  [Test]
  public void Generate_FromFileToFile_ArgumentNull()
  {
    using var hl = new Highlight();

    Assert.That(() => hl.Generate(inputPath: null!, outputPath: string.Empty), Throws.ArgumentNullException, "inputPath");
    Assert.That(() => hl.Generate(inputPath: string.Empty, outputPath: null!), Throws.ArgumentNullException, "outputPath");
  }

  [Test]
  public void GenerateFromFile_ArgumentNull()
  {
    using var hl = new Highlight();

    Assert.That(() => hl.GenerateFromFile(path: null!), Throws.ArgumentNullException);
  }

  [Test]
  public void Generate_BreakingChangesOnOutputTypeValue_Svg()
  {
    using var hl = new Highlight(outputType: GeneratorOutputType.Svg);

    hl.SetTheme("github");
    hl.SetSyntax("csharp");

    string? generated = null;

    Assert.That(() => generated = hl.Generate(input: "using System;"), Throws.Nothing);
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

    Assert.That(() => generated = hl.Generate(input: "using System;"), Throws.Nothing);
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

    Assert.That(
      () => generated = hl.Generate(input: @$"using System;
{hl.AdditionalEndOfFileChar}
Console.WriteLine(""Hello, world!"")"
      ),
      Throws.Nothing
    );
    Assert.That(generated, Is.Not.Null);
    Assert.That(generated, Is.Not.Empty);
    Assert.That(generated, Does.Contain(">using</"));
    Assert.That(generated, Does.Not.Contain("Hello, world!"));
  }
}
