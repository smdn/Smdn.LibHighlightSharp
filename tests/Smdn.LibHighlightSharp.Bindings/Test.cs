// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: MIT
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

using NUnit.Framework;

namespace Smdn.LibHighlightSharp.Bindings;

[TestFixture]
public class SwigBindingTests {
  private static bool TryFindNativeLibraryFile(out string? path)
  {
    path = default;

    var nativeLibraryFilePathProjectReference = Path.Combine(
      TestContext.CurrentContext.TestDirectory,
      VersionInformations.NativeLibraryFileName
    );

    if (File.Exists(nativeLibraryFilePathProjectReference)) {
      path = nativeLibraryFilePathProjectReference;
      return true;
    }

    var nativeLibraryFilePathCandidatesPackageReference = Directory.EnumerateFiles(
      Path.Combine(TestContext.CurrentContext.TestDirectory, "runtimes"),
      VersionInformations.NativeLibraryFileName,
      SearchOption.AllDirectories
    );

#if SYSTEM_RUNTIME_INTEROPSERVICES_RUNTIMEINFORMATION_RUNTIMEIDENTIFIER
    var nativeLibraryFilePathForCurrentRuntime = nativeLibraryFilePathCandidatesPackageReference
      .FirstOrDefault(static path => path.Contains(RuntimeInformation.RuntimeIdentifier + Path.DirectorySeparatorChar, StringComparison.Ordinal));

    var nativeLibraryFilePathPackageReference =
      nativeLibraryFilePathForCurrentRuntime ?? nativeLibraryFilePathCandidatesPackageReference.FirstOrDefault();
#else
    var nativeLibraryFilePathPackageReference = nativeLibraryFilePathCandidatesPackageReference.FirstOrDefault();
#endif

    if (nativeLibraryFilePathPackageReference is null)
      return false;

    if (File.Exists(nativeLibraryFilePathPackageReference)) {
      path = nativeLibraryFilePathPackageReference;
      return true;
    }

    return false;
  }

  [Test]
  public void TestNativeLibrary()
  {
    if (!TryFindNativeLibraryFile(out var nativeLibraryFilePath) || nativeLibraryFilePath is null) {
      Assert.Fail($"Expected native library file not found ({VersionInformations.NativeLibraryFileName})");
      return;
    }

#if SYSTEM_RUNTIME_INTEROPSERVICES_NATIVELIBRARY && SYSTEM_RUNTIME_INTEROPSERVICES_RUNTIMEINFORMATION_RUNTIMEIDENTIFIER
    var nativeLibraryHandle = IntPtr.Zero;

    Assert.DoesNotThrow(() => nativeLibraryHandle = NativeLibrary.Load(nativeLibraryFilePath)); // maybe already loaded

    Assert.That(nativeLibraryHandle, Is.Not.EqualTo(IntPtr.Zero));
#endif
  }

  [Test]
  public void TestNativeLibraryFileVersionInfo()
  {
    if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
      Assert.Ignore("not a test target platform");
      return;
    }

    if (!TryFindNativeLibraryFile(out var nativeLibraryFilePath) || nativeLibraryFilePath is null) {
      Assert.Fail($"Expected native library file not found ({VersionInformations.NativeLibraryFileName})");
      return;
    }

    var win32VersionResource = System.Diagnostics.FileVersionInfo.GetVersionInfo(nativeLibraryFilePath);

    Assert.That(win32VersionResource, Is.Not.Null);
    Assert.That(
      VersionInformations.NativeLibraryVersion.Major,
      Is.EqualTo(win32VersionResource.ProductMajorPart),
      nameof(win32VersionResource.ProductMajorPart)
    );
    Assert.That(
      VersionInformations.NativeLibraryVersion.Minor,
      Is.EqualTo(win32VersionResource.ProductMinorPart),
      nameof(win32VersionResource.ProductMinorPart)
    );
    Assert.That(
      $"Highlight v{VersionInformations.NativeLibraryVersion.Major}.{VersionInformations.NativeLibraryVersion.Minor}",
      Is.EqualTo(win32VersionResource.ProductName),
      nameof(win32VersionResource.ProductName)
    );
    Assert.That(
      VersionInformations.NativeLibraryVersion.Major,
      Is.EqualTo(win32VersionResource.FileMajorPart),
      nameof(win32VersionResource.FileMajorPart)
    );
    Assert.That(
      VersionInformations.NativeLibraryVersion.Minor,
      Is.EqualTo(win32VersionResource.FileMinorPart),
      nameof(win32VersionResource.ProductMinorPart)
    );
    Assert.That(
      VersionInformations.NativeLibraryName,
      Is.EqualTo(win32VersionResource.InternalName),
      nameof(win32VersionResource.InternalName)
    );
    Assert.That(
      VersionInformations.NativeLibraryFileName,
      Is.EqualTo(win32VersionResource.OriginalFilename),
      nameof(win32VersionResource.OriginalFilename)
    );
    Assert.That(
      win32VersionResource.LegalTrademarks,
      Is.EqualTo("GNU General Public License v3.0"),
      nameof(win32VersionResource.LegalTrademarks)
    );
    Assert.That(win32VersionResource.LegalCopyright, Does.Contain("smdn"), nameof(win32VersionResource.LegalCopyright));
    Assert.That(win32VersionResource.CompanyName, Does.Contain("smdn.jp"), nameof(win32VersionResource.CompanyName));

    TestContext.Out.WriteLine($"{nameof(win32VersionResource.Comments)}: {win32VersionResource.Comments}");
  }

  [Test]
  public void CreateCodeGenerator()
  {
    CodeGenerator? generator = null;

    try {
      Assert.DoesNotThrow(() => generator = CodeGenerator.getInstance(OutputType.HTML));
      Assert.That(generator, Is.Not.Null);
      Assert.DoesNotThrow(() => generator!.getFragmentCode());
      Assert.DoesNotThrow(() => generator!.Dispose());
    }
    finally {
      if (generator is not null)
        generator.Dispose();
    }
  }
}
