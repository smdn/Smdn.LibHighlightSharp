// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
#pragma warning disable CS8600, CS8602

using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace Smdn.LibHighlightSharp;

[TestFixture]
public partial class HighlightTests {
  [Test]
  public void Ctor()
  {
    Highlight hl = null;

    Assert.DoesNotThrow(() => hl = new Highlight());
    Assert.IsNotNull(hl);
    Assert.AreEqual(GeneratorOutputType.Html, hl.OutputType, nameof(Highlight.OutputType));
    Assert.DoesNotThrow(() => hl.Dispose());
  }

  [TestCase(GeneratorOutputType.Html)]
  [TestCase(GeneratorOutputType.Xhtml)]
  public void Ctor_WithGeneratorOutputType(GeneratorOutputType outputType)
  {
    Highlight hl = null;

    Assert.DoesNotThrow(() => hl = new Highlight(outputType: outputType));
    Assert.IsNotNull(hl);
    Assert.AreEqual(outputType, hl.OutputType, nameof(Highlight.OutputType));
    Assert.DoesNotThrow(() => hl.Dispose());
  }

  [Test]
  public void Ctor_WithDataDir_NullString()
    => Assert.Throws<ArgumentNullException>(() => new Highlight(dataDir: (string)null!));

  [Test]
  public void Ctor_WithDataDir_NullDataDir()
    => Assert.Throws<ArgumentNullException>(() => new Highlight(dataDir: (Bindings.DataDir)null!));

  [TestCase(null, "")]
  [TestCase("", null)]
  public void Ctor_WithDataDirForEach_NullString(string? dataDirForSyntaxes, string? dataDirForThemes)
    => Assert.Throws<ArgumentNullException>(() => new Highlight(dataDirForSyntaxes: dataDirForSyntaxes!, dataDirForThemes: dataDirForThemes!));

  private static System.Collections.IEnumerable YieldTestCases_NullableDataDirPairs()
  {
    yield return new object?[] { null, new Bindings.DataDir() };
    yield return new object?[] { new Bindings.DataDir(), null };
  }

  [TestCaseSource(nameof(YieldTestCases_NullableDataDirPairs))]
  public void Ctor_WithDataDirForEach_NullDataDir(Bindings.DataDir? dataDirForSyntaxes, Bindings.DataDir? dataDirForThemes)
    => Assert.Throws<ArgumentNullException>(() => new Highlight(dataDirForSyntaxes: dataDirForSyntaxes!, dataDirForThemes: dataDirForThemes!, shouldDisposeDataDir: true));

  private class DataDirEx : Bindings.DataDir {
    public bool OwnsSwigCMem => base.swigCMemOwn;

    public DataDirEx()
      : base()
    {
    }
  }

  [Test]
  public void Ctor_WithDataDir_ShouldDisposeDataDir([Values(true, false)] bool shouldDisposeDataDir)
  {
    using var dataDir = new DataDirEx();
    using var hl = new Highlight(dataDir: dataDir, shouldDisposeDataDir: shouldDisposeDataDir);

    Assert.DoesNotThrow(() => hl.Dispose());

    Assert.AreEqual(!shouldDisposeDataDir, dataDir.OwnsSwigCMem);
  }

  [Test]
  public void Ctor_WithDataDirForEach_ShouldDisposeDataDir([Values(true, false)] bool shouldDisposeDataDir)
  {
    using var dataDirForSyntaxes = new DataDirEx();
    using var dataDirForThemes = new DataDirEx();
    using var hl = new Highlight(dataDirForSyntaxes: dataDirForSyntaxes, dataDirForThemes: dataDirForThemes, shouldDisposeDataDir: shouldDisposeDataDir);

    Assert.DoesNotThrow(() => hl.Dispose());

    Assert.AreEqual(!shouldDisposeDataDir, dataDirForSyntaxes.OwnsSwigCMem);
    Assert.AreEqual(!shouldDisposeDataDir, dataDirForThemes.OwnsSwigCMem);
  }

  [TestCase("")]
  [TestCase("/usr/local/share/highlight")]
  [TestCase("/usr/local/share/highlight/")]
  public void Ctor_WithDataDir(string dataDir)
  {
    Highlight hl = null;

    Assert.DoesNotThrow(() => hl = new Highlight(dataDir: dataDir));
    Assert.IsNotNull(hl);
    Assert.DoesNotThrow(() => hl.Dispose());
  }

  [TestCase("", "")]
  [TestCase("", "/usr/local/share/highlight")]
  [TestCase("", "/usr/local/share/highlight/")]
  [TestCase("/usr/local/share/highlight", "")]
  [TestCase("/usr/local/share/highlight/", "")]
  [TestCase("/usr/local/share/highlight/", "/usr/local/share/highlight/")]
  public void Ctor_WithDataDirForEach(string dataDirForSyntaxes, string dataDirForThemes)
  {
    Highlight hl = null;

    Assert.DoesNotThrow(() => hl = new Highlight(dataDirForSyntaxes: dataDirForSyntaxes, dataDirForThemes: dataDirForThemes));
    Assert.IsNotNull(hl);
    Assert.DoesNotThrow(() => hl.Dispose());
  }

  [Test]
  public void Dispose()
  {
    var hl = new Highlight();

    Assert.DoesNotThrow(() => hl.Title = string.Empty);
    Assert.DoesNotThrow(() => hl.SetTheme("github"));
    Assert.DoesNotThrow(() => hl.SetSyntax("csharp"));
    Assert.DoesNotThrow(() => hl.Generate("using System;"));

    Assert.DoesNotThrow(() => hl.Dispose(), "dispose #1");
    Assert.DoesNotThrow(() => hl.Dispose(), "dispose #2");

    Assert.Throws<ObjectDisposedException>(() => hl.Title = string.Empty);
    Assert.Throws<ObjectDisposedException>(() => hl.SetSyntax("csharp"));
    Assert.Throws<ObjectDisposedException>(() => hl.SetTheme("github"));
    Assert.Throws<ObjectDisposedException>(() => hl.Generate("using System;"));
    Assert.Throws<ObjectDisposedException>(() => hl.Generate("non-existent.cs", "null-output.cs"));
    Assert.Throws<ObjectDisposedException>(() => hl.GenerateFromFile("non-existent.cs"));
  }

  [Test]
  public void GeneratorVersionString()
  {
    using var hl = new Highlight();

    Assert.IsNotNull(hl.GeneratorVersionString);
    StringAssert.Contains(VersionInformations.NativeLibraryVersion.ToString(), hl.GeneratorVersionString);

    // data dirs must not be disposed by calling GeneratorVersionString
    Assert.DoesNotThrow(() => hl.SetSyntax("csharp"));
    Assert.DoesNotThrow(() => hl.SetTheme("github"));
  }

  private static Bindings.DataDir CreateNonExistentPathDataDir()
  {
    var dataDir = new Bindings.DataDir();

    dataDir.initSearchDirectories(
      Path.Combine(
        Path.GetDirectoryName(Assembly.GetEntryAssembly().Location!)!,
        "non-existent"
      )
    );

    return dataDir;
  }

  private static System.Collections.IEnumerable YieldTestCases_GeneratorVersionString_MustLoadThemeAndSyntaxFromPathIndependentFromDataDirs()
  {
    yield return new object[] { Highlight.CreateDefaultDataDir()!, CreateNonExistentPathDataDir() };
    yield return new object[] { CreateNonExistentPathDataDir(), Highlight.CreateDefaultDataDir()! };
    yield return new object[] { CreateNonExistentPathDataDir(), CreateNonExistentPathDataDir() };
  }

  [TestCaseSource(nameof(YieldTestCases_GeneratorVersionString_MustLoadThemeAndSyntaxFromPathIndependentFromDataDirs))]
  public void GeneratorVersionString_MustLoadThemeAndSyntaxFromPathIndependentFromDataDirs(
    Bindings.DataDir dataDirForSyntaxes,
    Bindings.DataDir dataDirForThemes
  )
  {
    using var hl = new Highlight(
      dataDirForSyntaxes: dataDirForSyntaxes,
      dataDirForThemes: dataDirForThemes,
      shouldDisposeDataDir: true
    );

    Assert.IsNotNull(hl.GeneratorVersionString);
    StringAssert.Contains(VersionInformations.NativeLibraryVersion.ToString(), hl.GeneratorVersionString);
  }

  [Test]
  public void GeneratorVersionString_FailedToLoadThemeOrSyntax()
  {
    using var hl = new Highlight();

    var testAction = () => Assert.IsEmpty(hl.GeneratorVersionString);

    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
      GeneratorVersionString_FailedToLoadThemeOrSyntax_Windows(testAction);
    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
      GeneratorVersionString_FailedToLoadThemeOrSyntax_Unix(testAction);
    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
      GeneratorVersionString_FailedToLoadThemeOrSyntax_Unix(testAction);
    else
      Assert.Ignore($"undefined platform: {RuntimeInformation.RuntimeIdentifier}");
  }

  private void GeneratorVersionString_FailedToLoadThemeOrSyntax_Unix(Action testAction)
  {
    const string envvarNameTMPDIR = "TMPDIR";
    var envvarTMPDIR = Environment.GetEnvironmentVariable(envvarNameTMPDIR, EnvironmentVariableTarget.Process);

    try {
      var nonexistentDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ".non-existent-temp-directory"
      );

      if (Directory.Exists(nonexistentDirectory)) {
        Assert.Fail($"directory already exists ({nonexistentDirectory})");
        return;
      }

      Environment.SetEnvironmentVariable(envvarNameTMPDIR, nonexistentDirectory, EnvironmentVariableTarget.Process);

      testAction();
    }
    finally {
      Environment.SetEnvironmentVariable(envvarNameTMPDIR, envvarTMPDIR, EnvironmentVariableTarget.Process);
    }
  }

  private void GeneratorVersionString_FailedToLoadThemeOrSyntax_Windows(Action testAction)
  {
    const string envvarNameTMP = "TMP";
    var envvarTMP = Environment.GetEnvironmentVariable(envvarNameTMP, EnvironmentVariableTarget.Process);

    try {
      var nonexistentDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ".non-existent-temp-directory"
      );

      if (Directory.Exists(nonexistentDirectory)) {
        Assert.Fail($"directory already exists ({nonexistentDirectory})");
        return;
      }

      Environment.SetEnvironmentVariable(envvarNameTMP, nonexistentDirectory, EnvironmentVariableTarget.Process);

      testAction();
    }
    finally {
      Environment.SetEnvironmentVariable(envvarNameTMP, envvarTMP, EnvironmentVariableTarget.Process);
    }
  }
}
