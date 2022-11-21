// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
#pragma warning disable CS8600, CS8602

using System;
using System.IO;
using System.Reflection;
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

  [Test]
  public void GeneratorVersionString_FailedToLoadLangDef()
  {
    using var hl = new Highlight(
      dataDirForSyntaxes: CreateNonExistentPathDataDir(),
      dataDirForThemes: Highlight.CreateDefaultDataDir()!,
      shouldDisposeDataDir: true
    );

    Assert.IsEmpty(hl.GeneratorVersionString);
  }

  [Test]
  public void GeneratorVersionString_FailedToLoadTheme()
  {
    using var hl = new Highlight(
      dataDirForSyntaxes: Highlight.CreateDefaultDataDir()!,
      dataDirForThemes: CreateNonExistentPathDataDir(),
      shouldDisposeDataDir: true
    );

    Assert.IsEmpty(hl.GeneratorVersionString);
  }
}
