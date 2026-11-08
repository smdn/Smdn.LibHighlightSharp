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

    Assert.That(() => hl = new Highlight(), Throws.Nothing);
    Assert.That(hl, Is.Not.Null);
    Assert.That(hl.OutputType, Is.EqualTo(GeneratorOutputType.Html), nameof(Highlight.OutputType));
    Assert.That(() => hl.Dispose(), Throws.Nothing);
  }

  [TestCase(GeneratorOutputType.Html)]
  [TestCase(GeneratorOutputType.Xhtml)]
  public void Ctor_WithGeneratorOutputType(GeneratorOutputType outputType)
  {
    Highlight hl = null;

    Assert.That(() => hl = new Highlight(outputType: outputType), Throws.Nothing);
    Assert.That(hl, Is.Not.Null);
    Assert.That(hl.OutputType, Is.EqualTo(outputType), nameof(Highlight.OutputType));
    Assert.That(() => hl.Dispose(), Throws.Nothing);
  }

  [Test]
  public void Ctor_WithDataDir_NullString()
    => Assert.That(() => new Highlight(dataDir: (string)null!), Throws.ArgumentNullException);

  [Test]
  public void Ctor_WithDataDir_NullDataDir()
    => Assert.That(() => new Highlight(dataDir: (Bindings.DataDir)null!), Throws.ArgumentNullException);

  [TestCase(null, "")]
  [TestCase("", null)]
  public void Ctor_WithDataDirForEach_NullString(string? dataDirForSyntaxes, string? dataDirForThemes)
    => Assert.That(() => new Highlight(dataDirForSyntaxes: dataDirForSyntaxes!, dataDirForThemes: dataDirForThemes!), Throws.ArgumentNullException);

  private static System.Collections.IEnumerable YieldTestCases_NullableDataDirPairs()
  {
    yield return new object?[] { null, new Bindings.DataDir() };
    yield return new object?[] { new Bindings.DataDir(), null };
  }

  [TestCaseSource(nameof(YieldTestCases_NullableDataDirPairs))]
  public void Ctor_WithDataDirForEach_NullDataDir(Bindings.DataDir? dataDirForSyntaxes, Bindings.DataDir? dataDirForThemes)
    => Assert.That(() => new Highlight(dataDirForSyntaxes: dataDirForSyntaxes!, dataDirForThemes: dataDirForThemes!, shouldDisposeDataDir: true), Throws.ArgumentNullException);

  private class DataDirEx : Bindings.DataDir {
    public bool OwnsSwigCMem => base.swigCMemOwn;

    public DataDirEx()
      : base()
    {
    }
  }

  [Test]
  public void Ctor_WithDataDir_ShouldDisposeDataDir([Values] bool shouldDisposeDataDir)
  {
    using var dataDir = new DataDirEx();
    using var hl = new Highlight(dataDir: dataDir, shouldDisposeDataDir: shouldDisposeDataDir);

    Assert.That(hl.Dispose, Throws.Nothing);

    Assert.That(dataDir.OwnsSwigCMem, Is.EqualTo(!shouldDisposeDataDir));
  }

  [Test]
  public void Ctor_WithDataDirForEach_ShouldDisposeDataDir([Values] bool shouldDisposeDataDir)
  {
    using var dataDirForSyntaxes = new DataDirEx();
    using var dataDirForThemes = new DataDirEx();
    using var hl = new Highlight(dataDirForSyntaxes: dataDirForSyntaxes, dataDirForThemes: dataDirForThemes, shouldDisposeDataDir: shouldDisposeDataDir);

    Assert.That(hl.Dispose, Throws.Nothing);

    Assert.That(dataDirForSyntaxes.OwnsSwigCMem, Is.EqualTo(!shouldDisposeDataDir));
    Assert.That(dataDirForThemes.OwnsSwigCMem, Is.EqualTo(!shouldDisposeDataDir));
  }

  [TestCase("")]
  [TestCase("/usr/local/share/highlight")]
  [TestCase("/usr/local/share/highlight/")]
  public void Ctor_WithDataDir(string dataDir)
  {
    Highlight hl = null;

    Assert.That(() => hl = new Highlight(dataDir: dataDir), Throws.Nothing);
    Assert.That(hl, Is.Not.Null);
    Assert.That(() => hl.Dispose(), Throws.Nothing);
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

    Assert.That(
      () => hl = new Highlight(dataDirForSyntaxes: dataDirForSyntaxes, dataDirForThemes: dataDirForThemes),
      Throws.Nothing
    );
    Assert.That(hl, Is.Not.Null);
    Assert.That(() => hl.Dispose(), Throws.Nothing);
  }

  [Test]
  public void Dispose()
  {
    var hl = new Highlight();

    Assert.That(() => hl.Title = string.Empty, Throws.Nothing);
    Assert.That(() => hl.SetTheme("github"), Throws.Nothing);
    Assert.That(() => hl.SetSyntax("csharp"), Throws.Nothing);
    Assert.That(() => hl.Generate("using System;"), Throws.Nothing);

    Assert.That(hl.Dispose, Throws.Nothing, "dispose #1");
    Assert.That(hl.Dispose, Throws.Nothing, "dispose #2");

    Assert.That(() => hl.Title = string.Empty, Throws.TypeOf<ObjectDisposedException>());
    Assert.That(() => hl.SetSyntax("csharp"), Throws.TypeOf<ObjectDisposedException>());
    Assert.That(() => hl.SetTheme("github"), Throws.TypeOf<ObjectDisposedException>());
    Assert.That(() => hl.Generate("using System;"), Throws.TypeOf<ObjectDisposedException>());
    Assert.That(() => hl.Generate("non-existent.cs", "null-output.cs"), Throws.TypeOf<ObjectDisposedException>());
    Assert.That(() => hl.GenerateFromFile("non-existent.cs"), Throws.TypeOf<ObjectDisposedException>());
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
    static void TestAction()
      => Assert.That(Highlight.GeneratorInformationalVersion, Is.Empty);

    var prop = typeof(Highlight)
      .GetProperty(nameof(Highlight.GeneratorInformationalVersion), BindingFlags.Static | BindingFlags.Public);

    // HACK: force reset backing field to null
    // Due to the changes in af3a85e7739c82f6620e3bfcabf950040212e98d, the backing
    // field has been removed and replaced with a private setter, so retrieve either
    // one and use it in the test.
    var backingField = typeof(Highlight).GetField("generatorInformationalVersion", BindingFlags.Static | BindingFlags.NonPublic);
    var setter = backingField is null
      ? typeof(Highlight)
        .GetProperty(nameof(Highlight.GeneratorInformationalVersion), BindingFlags.Static | BindingFlags.Public)
        .GetSetMethod(nonPublic: true) ?? throw new InvalidOperationException("failed to get the set accessor")
      : null;

    try {
      if (backingField is { } f)
        f.SetValue(null, null);
      else
        setter.Invoke(obj: null, parameters: [null]);

      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        GeneratorInformationalVersion_FailedToLoadThemeOrSyntax_Windows(TestAction);
      else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        GeneratorInformationalVersion_FailedToLoadThemeOrSyntax_Unix(TestAction);
      else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        GeneratorInformationalVersion_FailedToLoadThemeOrSyntax_Unix(TestAction);
      else
        Assert.Ignore($"undefined platform: {RuntimeInformation.OSDescription}");
    }
    finally {
      if (backingField is { } f)
        f.SetValue(null, null);
      else
        setter.Invoke(obj: null, parameters: [null]);
    }
  }

  private void GeneratorInformationalVersion_FailedToLoadThemeOrSyntax_Unix(Action testAction)
  {
    const string EnvVarNameTMPDIR = "TMPDIR";
    var envvarTMPDIR = Environment.GetEnvironmentVariable(EnvVarNameTMPDIR, EnvironmentVariableTarget.Process);

    try {
      var nonexistentDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ".non-existent-temp-directory"
      );

      if (Directory.Exists(nonexistentDirectory)) {
        Assert.Fail($"directory already exists ({nonexistentDirectory})");
        return;
      }

      Environment.SetEnvironmentVariable(EnvVarNameTMPDIR, nonexistentDirectory, EnvironmentVariableTarget.Process);

      testAction();
    }
    finally {
      Environment.SetEnvironmentVariable(EnvVarNameTMPDIR, envvarTMPDIR, EnvironmentVariableTarget.Process);
    }
  }

  private void GeneratorInformationalVersion_FailedToLoadThemeOrSyntax_Windows(Action testAction)
  {
    const string EnvVarNameTMP = "TMP";
    var envvarTMP = Environment.GetEnvironmentVariable(EnvVarNameTMP, EnvironmentVariableTarget.Process);

    try {
      var nonexistentDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ".non-existent-temp-directory"
      );

      if (Directory.Exists(nonexistentDirectory)) {
        Assert.Fail($"directory already exists ({nonexistentDirectory})");
        return;
      }

      Environment.SetEnvironmentVariable(EnvVarNameTMP, nonexistentDirectory, EnvironmentVariableTarget.Process);

      testAction();
    }
    finally {
      Environment.SetEnvironmentVariable(EnvVarNameTMP, envvarTMP, EnvironmentVariableTarget.Process);
    }
  }
}
