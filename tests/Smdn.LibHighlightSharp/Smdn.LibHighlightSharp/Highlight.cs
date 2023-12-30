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
  [OneTimeSetUp]
  public void OneTimeSetUp()
  {
    TestContext.Progress.WriteLine($"{typeof(Highlight).Assembly.GetName()}");
    TestContext.Progress.WriteLine($"{typeof(VersionInformations).Assembly.GetName()}");
    TestContext.Progress.WriteLine($"{nameof(VersionInformations.NativeLibraryVersion)}: {VersionInformations.NativeLibraryVersion}");
  }

  [Test]
  public void Ctor()
  {
    Highlight hl = null;

    Assert.DoesNotThrow(() => hl = new Highlight());
    Assert.That(hl, Is.Not.Null);
    Assert.That(hl.OutputType, Is.EqualTo(GeneratorOutputType.Html), nameof(Highlight.OutputType));
    Assert.DoesNotThrow(() => hl.Dispose());
  }

  [TestCase(GeneratorOutputType.Html)]
  [TestCase(GeneratorOutputType.Xhtml)]
  public void Ctor_WithGeneratorOutputType(GeneratorOutputType outputType)
  {
    Highlight hl = null;

    Assert.DoesNotThrow(() => hl = new Highlight(outputType: outputType));
    Assert.That(hl, Is.Not.Null);
    Assert.That(hl.OutputType, Is.EqualTo(outputType), nameof(Highlight.OutputType));
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

    Assert.That(dataDir.OwnsSwigCMem, Is.EqualTo(!shouldDisposeDataDir));
  }

  [Test]
  public void Ctor_WithDataDirForEach_ShouldDisposeDataDir([Values(true, false)] bool shouldDisposeDataDir)
  {
    using var dataDirForSyntaxes = new DataDirEx();
    using var dataDirForThemes = new DataDirEx();
    using var hl = new Highlight(dataDirForSyntaxes: dataDirForSyntaxes, dataDirForThemes: dataDirForThemes, shouldDisposeDataDir: shouldDisposeDataDir);

    Assert.DoesNotThrow(() => hl.Dispose());

    Assert.That(dataDirForSyntaxes.OwnsSwigCMem, Is.EqualTo(!shouldDisposeDataDir));
    Assert.That(dataDirForThemes.OwnsSwigCMem, Is.EqualTo(!shouldDisposeDataDir));
  }

  [TestCase("")]
  [TestCase("/usr/local/share/highlight")]
  [TestCase("/usr/local/share/highlight/")]
  public void Ctor_WithDataDir(string dataDir)
  {
    Highlight hl = null;

    Assert.DoesNotThrow(() => hl = new Highlight(dataDir: dataDir));
    Assert.That(hl, Is.Not.Null);
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
    Assert.That(hl, Is.Not.Null);
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
  public void GeneratorInformationalVersion()
  {
    Assert.That(Highlight.GeneratorInformationalVersion, Is.Not.Null);
    Assert.That(Highlight.GeneratorInformationalVersion, Does.Contain(VersionInformations.NativeLibraryVersion.ToString()));
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

  private static System.Collections.IEnumerable YieldTestCases_GeneratorInformationalVersion_MustLoadThemeAndSyntaxFromPathIndependentFromDataDirs()
  {
    yield return new object[] { Highlight.CreateDefaultDataDir()!, CreateNonExistentPathDataDir() };
    yield return new object[] { CreateNonExistentPathDataDir(), Highlight.CreateDefaultDataDir()! };
    yield return new object[] { CreateNonExistentPathDataDir(), CreateNonExistentPathDataDir() };
  }

  [TestCaseSource(nameof(YieldTestCases_GeneratorInformationalVersion_MustLoadThemeAndSyntaxFromPathIndependentFromDataDirs))]
  [Ignore("cannot test")]
  public void GeneratorInformationalVersion_MustLoadThemeAndSyntaxFromPathIndependentFromDataDirs(
    Bindings.DataDir dataDirForSyntaxes,
    Bindings.DataDir dataDirForThemes
  )
    => Assert.That(Highlight.GeneratorInformationalVersion, Is.Not.Null);

  [Test]
  public void GeneratorInformationalVersion_FailedToLoadThemeOrSyntax()
  {
    var testAction = () => Assert.That(Highlight.GeneratorInformationalVersion, Is.Empty);

    // HACK: force reset backing field to null
    var backingField = typeof(Highlight).GetField("generatorInformationalVersion", BindingFlags.Static | BindingFlags.NonPublic);

    backingField.SetValue(null, null);

    try {
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        GeneratorInformationalVersion_FailedToLoadThemeOrSyntax_Windows(testAction);
      else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        GeneratorInformationalVersion_FailedToLoadThemeOrSyntax_Unix(testAction);
      else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        GeneratorInformationalVersion_FailedToLoadThemeOrSyntax_Unix(testAction);
      else
        Assert.Ignore($"undefined platform: {RuntimeInformation.OSDescription}");
    }
    finally {
      backingField.SetValue(null, null);
    }
  }

  private void GeneratorInformationalVersion_FailedToLoadThemeOrSyntax_Unix(Action testAction)
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

  private void GeneratorInformationalVersion_FailedToLoadThemeOrSyntax_Windows(Action testAction)
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
