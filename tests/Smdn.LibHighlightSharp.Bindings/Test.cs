using System;
using System.IO;
using System.Linq;
using System.Reflection;
#if SYSTEM_RUNTIME_INTEROPSERVICES_NATIVELIBRARY
using System.Runtime.InteropServices;
#endif
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

    var nativeLibraryFilePathPackageReference = Directory.EnumerateFiles(
      Path.Combine(TestContext.CurrentContext.TestDirectory, "runtimes"),
      VersionInformations.NativeLibraryFileName,
      SearchOption.AllDirectories
    ).FirstOrDefault();

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

#if SYSTEM_RUNTIME_INTEROPSERVICES_NATIVELIBRARY
    var nativeLibraryHandle = IntPtr.Zero;

    Assert.DoesNotThrow(() => nativeLibraryHandle = NativeLibrary.Load(nativeLibraryFilePath)); // maybe alread loaded

    Assert.AreNotEqual(IntPtr.Zero, nativeLibraryHandle);
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

    Assert.IsNotNull(win32VersionResource);
    Assert.AreEqual(
      win32VersionResource.ProductMajorPart,
      VersionInformations.NativeLibraryVersion.Major,
      nameof(win32VersionResource.ProductMajorPart)
    );
    Assert.AreEqual(
      win32VersionResource.ProductMinorPart,
      VersionInformations.NativeLibraryVersion.Minor,
      nameof(win32VersionResource.ProductMinorPart)
    );
    Assert.AreEqual(
      win32VersionResource.ProductName,
      $"Highlight v{VersionInformations.NativeLibraryVersion.Major}.{VersionInformations.NativeLibraryVersion.Minor}",
      nameof(win32VersionResource.ProductName)
    );
    Assert.AreEqual(
      win32VersionResource.FileMajorPart,
      VersionInformations.NativeLibraryVersion.Major,
      nameof(win32VersionResource.FileMajorPart)
    );
    Assert.AreEqual(
      win32VersionResource.FileMinorPart,
      VersionInformations.NativeLibraryVersion.Minor,
      nameof(win32VersionResource.ProductMinorPart)
    );
    Assert.AreEqual(
      win32VersionResource.InternalName,
      VersionInformations.NativeLibraryName,
      nameof(win32VersionResource.InternalName)
    );
    Assert.AreEqual(
      win32VersionResource.OriginalFilename,
      VersionInformations.NativeLibraryFileName,
      nameof(win32VersionResource.OriginalFilename)
    );
    Assert.AreEqual(
      win32VersionResource.LegalTrademarks,
      "GNU General Public License v3.0",
      nameof(win32VersionResource.LegalTrademarks)
    );
    StringAssert.Contains("smdn", win32VersionResource.LegalCopyright, nameof(win32VersionResource.LegalCopyright));
    StringAssert.Contains("smdn.jp", win32VersionResource.CompanyName, nameof(win32VersionResource.CompanyName));

    TestContext.WriteLine($"{nameof(win32VersionResource.Comments)}: {win32VersionResource.Comments}");
  }

  [Test]
  public void CreateCodeGenerator()
  {
    CodeGenerator? generator = null;

    try {
      Assert.DoesNotThrow(() => generator = CodeGenerator.getInstance(OutputType.HTML));
      Assert.IsNotNull(generator);
      Assert.DoesNotThrow(() => generator!.getFragmentCode());
      Assert.DoesNotThrow(() => generator!.Dispose());
    }
    finally {
      if (generator is not null)
        generator.Dispose();
    }
  }
}
