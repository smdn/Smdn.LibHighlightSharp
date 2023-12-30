// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
using System;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Smdn.LibHighlightSharp;

partial class HighlightTests {
  private const string customFileTypesConfFileName = "test-file-types.conf";
  private const string nonDefaultExtensionFileTypesConfFileName = "test-file-types-with-non-default-extension.types";
  private const string csharpSourceWithExtensionFileName = "testfile_csharp.cs";
  private const string csharpSourceWithNonDefaultExtensionFileName = "testfile_csharp.cs11";
  private const string csharpSourceWithoutExtensionFileName = "testfile_csharp";
  private const string csharpScriptWithoutExtensionFileName = "testfile_csharp_script";

  [SetUp]
  public void SetUpTestFiles()
  {
    var utf8nobom = new UTF8Encoding(false);

    File.WriteAllText(
      path: Path.Combine(TestContext.CurrentContext.WorkDirectory, customFileTypesConfFileName),
      contents: @"
FileMapping = {
  { Lang=""csharp"", Extensions={ ""cs"", ""cs11"" } },
  { Lang=""csharp"", Shebang=[[^#!\s*(/usr)?(/local)?/bin/(env\s+)?csharp]] }
}",
      encoding: utf8nobom
    );
    File.WriteAllText(
      path: Path.Combine(TestContext.CurrentContext.WorkDirectory, nonDefaultExtensionFileTypesConfFileName),
      contents: "FileMapping = {}",
      encoding: utf8nobom
    );

    File.WriteAllText(
      path: Path.Combine(TestContext.CurrentContext.WorkDirectory, csharpSourceWithExtensionFileName),
      contents: "using System;",
      encoding: utf8nobom
    );
    File.WriteAllText(
      path: Path.Combine(TestContext.CurrentContext.WorkDirectory, csharpSourceWithNonDefaultExtensionFileName),
      contents: "using System;",
      encoding: utf8nobom
    );
    File.WriteAllText(
      path: Path.Combine(TestContext.CurrentContext.WorkDirectory, csharpSourceWithoutExtensionFileName),
      contents: "using System;",
      encoding: utf8nobom
    );
    File.WriteAllText(
      path: Path.Combine(TestContext.CurrentContext.WorkDirectory, csharpScriptWithoutExtensionFileName),
      contents: @"#!/usr/bin/env csharp
using System;",
      encoding: utf8nobom
    );
  }

  [Test]
  public void LoadFileTypesConfig()
  {
    using var hl = new Highlight();

    void Action() => hl.LoadFileTypesConfig(
      Path.Combine(TestContext.CurrentContext.WorkDirectory, customFileTypesConfFileName)
    );

    if (Highlight.MinimumVersionSupportingLoadFileTypesConfig <= VersionInformations.NativeLibraryVersion)
      Assert.DoesNotThrow(Action);
    else
      Assert.Throws<NotSupportedException>(Action);
  }

  [Test]
  public void LoadFileTypesConfig_FileWithNonDefaultExtensionCannotBeLoaded()
  {
    using var hl = new Highlight();

    void Action() => hl.LoadFileTypesConfig(
      Path.Combine(TestContext.CurrentContext.WorkDirectory, nonDefaultExtensionFileTypesConfFileName)
    );

    if (Highlight.MinimumVersionSupportingLoadFileTypesConfig <= VersionInformations.NativeLibraryVersion)
      Assert.Throws<InvalidOperationException>(Action);
    else
      Assert.Throws<NotSupportedException>(Action);
  }

  [Test]
  public void GuessFileType()
  {
    using var hl = new Highlight();

    string? fileType = null;

    void Action() => fileType = hl.GuessFileType(Path.Combine(TestContext.CurrentContext.WorkDirectory, csharpSourceWithExtensionFileName));

    if (Highlight.MinimumVersionSupportingGuessFileType <= VersionInformations.NativeLibraryVersion) {
      Assert.DoesNotThrow(Action);

      // file extension must be returned in this case
      Assert.IsNotNull(fileType);
      Assert.AreEqual("cs", fileType!);
    }
    else {
      Assert.Throws<NotSupportedException>(Action);
    }
  }

  [TestCase(csharpSourceWithExtensionFileName, "cs")]
  [TestCase(csharpSourceWithNonDefaultExtensionFileName, "cs11")]
  [TestCase(csharpSourceWithoutExtensionFileName, "")]
  [TestCase(csharpScriptWithoutExtensionFileName, "")]
  public void GuessFileType_FileTypesConfigNotLoaded(string file, string expected)
  {
    if (VersionInformations.NativeLibraryVersion <= Highlight.MinimumVersionSupportingGuessFileType) {
      Assert.Ignore($"not supported: {nameof(Highlight.MinimumVersionSupportingGuessFileType)}");
      return;
    }

    using var hl = new Highlight();

    // file extension must be returned in this case
    Assert.AreEqual(
      expected,
      hl.GuessFileType(Path.Combine(TestContext.CurrentContext.WorkDirectory, file)),
      file
    );
  }

  private static System.Collections.IEnumerable YieldTestCases_GuessFileType_DefaultFileTypesConfigLoaded()
  {
    yield return new object[] { csharpSourceWithExtensionFileName, "csharp" }; // proper file type must be returned in this case
    yield return new object[] { csharpSourceWithNonDefaultExtensionFileName, "cs11" }; // file extension must be returned in this case
    yield return new object[] { csharpSourceWithoutExtensionFileName, "" }; // file extension must be returned in this case
    yield return new object[] { csharpScriptWithoutExtensionFileName, 4 <= VersionInformations.NativeLibraryVersion.Major ? "shellscript" : "sh" }; // file type must be misdetected as 'sh'
  }

  [TestCaseSource(nameof(YieldTestCases_GuessFileType_DefaultFileTypesConfigLoaded))]
  public void GuessFileType_DefaultFileTypesConfigLoaded(string file, string expected)
  {
    if (VersionInformations.NativeLibraryVersion <= Highlight.MinimumVersionSupportingGuessFileType) {
      Assert.Ignore($"not supported: {nameof(Highlight.MinimumVersionSupportingGuessFileType)}");
      return;
    }
    if (VersionInformations.NativeLibraryVersion <= Highlight.MinimumVersionSupportingLoadFileTypesConfig) {
      Assert.Ignore($"not supported: {nameof(Highlight.MinimumVersionSupportingLoadFileTypesConfig)}");
      return;
    }

    using var hl = new Highlight();

    Assert.IsTrue(hl.TryLoadFileTypesConfig(), nameof(hl.TryLoadFileTypesConfig));

    Assert.AreEqual(
      expected,
      hl.GuessFileType(Path.Combine(TestContext.CurrentContext.WorkDirectory, file)),
      file
    );
  }

  [TestCase(csharpSourceWithExtensionFileName, "csharp")]
  [TestCase(csharpSourceWithNonDefaultExtensionFileName, "csharp")]
  [TestCase(csharpSourceWithoutExtensionFileName, "")]
  [TestCase(csharpScriptWithoutExtensionFileName, "csharp")]
  public void GuessFileType_CustomFileTypesConfigLoaded(string file, string expected)
  {
    if (VersionInformations.NativeLibraryVersion <= Highlight.MinimumVersionSupportingGuessFileType) {
      Assert.Ignore($"not supported: {nameof(Highlight.MinimumVersionSupportingGuessFileType)}");
      return;
    }
    if (VersionInformations.NativeLibraryVersion <= Highlight.MinimumVersionSupportingLoadFileTypesConfig) {
      Assert.Ignore($"not supported: {nameof(Highlight.MinimumVersionSupportingLoadFileTypesConfig)}");
      return;
    }

    using var hl = new Highlight();

    hl.LoadFileTypesConfig(Path.Combine(TestContext.CurrentContext.WorkDirectory, customFileTypesConfFileName));

    // guessed file type must be returned in this case
    Assert.AreEqual(
      expected,
      hl.GuessFileType(Path.Combine(TestContext.CurrentContext.WorkDirectory, file)),
      file
    );
  }

  [TestCase("csharp", true)]
  [TestCase("non-existent-syntax", false)]
  [TestCase("", false)]
  [TestCase(null, false)]
  public void TryFindSyntaxFile(string name, bool expected)
  {
    using var hl = new Highlight();

    Assert.AreEqual(hl.TryFindSyntaxFile(name, out var syntaxFilePath), expected);

    if (expected) {
      Assert.IsNotNull(syntaxFilePath);
      FileAssert.Exists(syntaxFilePath);
    }
  }

  [TestCase("github", true)]
  [TestCase("non-existent-theme", false)]
  [TestCase("", false)]
  [TestCase(null, false)]
  public void TryFindThemeFile(string name, bool expected)
  {
    using var hl = new Highlight();

    Assert.AreEqual(hl.TryFindThemeFile(name, out var themeFilePath), expected);

    if (expected) {
      Assert.IsNotNull(themeFilePath);
      FileAssert.Exists(themeFilePath);
    }
  }

  [TestCase("github", true)]
  [TestCase("non-existent-theme", false)]
  [TestCase("", false)]
  [TestCase(null, false)]
  public void TryFindThemeBase16File(string name, bool expected)
  {
    using var hl = new Highlight();
    var ret = false;
    string? themeFilePath = null;

    Assert.DoesNotThrow(() => ret = hl.TryFindThemeBase16File(name, out themeFilePath));

    if (Highlight.MinimumVersionSupportingBase16Themes <= VersionInformations.NativeLibraryVersion) {
      Assert.AreEqual(ret, expected);

      if (expected) {
        Assert.IsNotNull(themeFilePath);
        FileAssert.Exists(themeFilePath);
      }
    }
    else {
      Assert.IsFalse(ret);
    }
  }

  [Test]
  public void EnumerateSyntaxFiles()
  {
    using var hl = new Highlight();

    var syntaxFilesEnumerable = hl.EnumerateSyntaxFiles();

    Assert.IsNotNull(syntaxFilesEnumerable, nameof(syntaxFilesEnumerable));

    var syntaxFiles = syntaxFilesEnumerable.ToList();

    CollectionAssert.IsNotEmpty(syntaxFiles, nameof(syntaxFiles));
    Assert.IsNotNull(syntaxFiles.FirstOrDefault(static syntaxFile => Path.GetFileNameWithoutExtension(syntaxFile) == "csharp"), $"{nameof(syntaxFiles)} contains 'csharp'");
    Assert.IsTrue(syntaxFiles.All(static syntaxFile => Path.GetExtension(syntaxFile) == ".lang"), $"all of {nameof(syntaxFiles)} must have extension '.lang'");
  }

  [Test]
  public void EnumerateSyntaxFiles_NonExistentSyntaxDir()
  {
    using var hl = new Highlight(dataDirForSyntaxes: "non-existent-syntax-dir", dataDirForThemes: string.Empty);

    var syntaxFilesEnumerable = hl.EnumerateSyntaxFiles();

    Assert.IsNotNull(syntaxFilesEnumerable, nameof(syntaxFilesEnumerable));

    var syntaxFiles = syntaxFilesEnumerable.ToList();

    CollectionAssert.IsEmpty(syntaxFiles, nameof(syntaxFiles));
  }

  [Test]
  public void EnumerateSyntaxFilesWithDescription()
  {
    using var hl = new Highlight();

    var syntaxFilesWithDescriptionEnumerable = hl.EnumerateSyntaxFilesWithDescription();

    Assert.IsNotNull(syntaxFilesWithDescriptionEnumerable, nameof(syntaxFilesWithDescriptionEnumerable));

    var syntaxFilesWithDescription = syntaxFilesWithDescriptionEnumerable.ToList();

    CollectionAssert.IsNotEmpty(syntaxFilesWithDescription, nameof(syntaxFilesWithDescription));

    var csharp = syntaxFilesWithDescription.FirstOrDefault(static syntax => syntax.Description == "C#");

    Assert.IsNotNull(csharp.Path, "csharp.lang path");
    Assert.AreEqual("csharp", Path.GetFileNameWithoutExtension(csharp.Path), "csharp.lang file name");
    Assert.AreEqual(".lang", Path.GetExtension(csharp.Path), "csharp.lang extension");

    Assert.IsNotNull(csharp.Description, "csharp.lang description");
  }

  [Test]
  public void EnumerateSyntaxFilesWithDescription_NonExistentSyntaxDir()
  {
    using var hl = new Highlight(dataDirForSyntaxes: "non-existent-syntax-dir", dataDirForThemes: string.Empty);

    var syntaxFilesWithDescriptionEnumerable = hl.EnumerateSyntaxFilesWithDescription();

    Assert.IsNotNull(syntaxFilesWithDescriptionEnumerable, nameof(syntaxFilesWithDescriptionEnumerable));

    var syntaxFilesWithDescription = syntaxFilesWithDescriptionEnumerable.ToList();

    CollectionAssert.IsEmpty(syntaxFilesWithDescription, nameof(syntaxFilesWithDescription));
  }

  [Test]
  public void EnumerateThemeFiles()
  {
    using var hl = new Highlight();

    var themeFilesEnumerable = hl.EnumerateThemeFiles();

    Assert.IsNotNull(themeFilesEnumerable, nameof(themeFilesEnumerable));

    var themeFiles = themeFilesEnumerable.ToList();

    CollectionAssert.IsNotEmpty(themeFiles, nameof(themeFiles));
    Assert.IsNotNull(themeFiles.FirstOrDefault(static themeFile => Path.GetFileNameWithoutExtension(themeFile) == "github"), $"{nameof(themeFiles)} contains 'github'");
    Assert.IsTrue(themeFiles.All(static themeFile => Path.GetExtension(themeFile) == ".theme"), $"all of {nameof(themeFiles)} must have extension '.theme'");
  }

  [Test]
  public void EnumerateThemeFiles_NonExistentThemeDir()
  {
    using var hl = new Highlight(dataDirForThemes: "non-existent-theme-dir", dataDirForSyntaxes: string.Empty);

    var themeFilesEnumerable = hl.EnumerateSyntaxFiles();

    Assert.IsNotNull(themeFilesEnumerable, nameof(themeFilesEnumerable));

    var themeFiles = themeFilesEnumerable.ToList();

    CollectionAssert.IsEmpty(themeFiles, nameof(themeFiles));
  }

  [Test]
  public void EnumerateThemeFilesWithDescription()
  {
    using var hl = new Highlight();

    var themeFilesWithDescriptionEnumerable = hl.EnumerateThemeFilesWithDescription();

    Assert.IsNotNull(themeFilesWithDescriptionEnumerable, nameof(themeFilesWithDescriptionEnumerable));

    var themeFilesWithDescription = themeFilesWithDescriptionEnumerable.ToList();

    CollectionAssert.IsNotEmpty(themeFilesWithDescription, nameof(themeFilesWithDescription));

    if (VersionInformations.NativeLibraryVersion < new Version(3, 44)) {
      Assert.Ignore("cannot get theme description");
      return;
    }

    var github = themeFilesWithDescription.FirstOrDefault(static theme => theme.Description?.StartsWith("Github", StringComparison.Ordinal) ?? false);

    Assert.IsNotNull(github.Path, "github.theme path");
    Assert.AreEqual("github", Path.GetFileNameWithoutExtension(github.Path), "github.theme file name");
    Assert.AreEqual(".theme", Path.GetExtension(github.Path), "github.theme extension");

    Assert.IsNotNull(github.Description, "github.theme description");
  }

  [Test]
  public void EnumerateThemeFilesWithDescription_NonExistentThemeDir()
  {
    using var hl = new Highlight(dataDirForThemes: "non-existent-theme-dir", dataDirForSyntaxes: string.Empty);

    var themeFilesWithDescriptionEnumerable = hl.EnumerateThemeFilesWithDescription();

    Assert.IsNotNull(themeFilesWithDescriptionEnumerable, nameof(themeFilesWithDescriptionEnumerable));

    var themeFilesWithDescription = themeFilesWithDescriptionEnumerable.ToList();

    CollectionAssert.IsEmpty(themeFilesWithDescription, nameof(themeFilesWithDescription));
  }
}
