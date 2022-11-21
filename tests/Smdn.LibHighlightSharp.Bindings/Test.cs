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
  [Test]
  public void TestNativeLibrary()
  {
    string nativeLibraryFilePath;

    var nativeLibraryFilePathProjectReference = Path.Combine(
      TestContext.CurrentContext.TestDirectory,
      VersionInformations.NativeLibraryFileName
    );

    if (File.Exists(nativeLibraryFilePathProjectReference)) {
      nativeLibraryFilePath = nativeLibraryFilePathProjectReference;
    }
    else {
      var nativeLibraryFilePathPackageReference = Directory.EnumerateFiles(
        Path.Combine(TestContext.CurrentContext.TestDirectory, "runtimes"),
        VersionInformations.NativeLibraryFileName,
        SearchOption.AllDirectories
      ).FirstOrDefault();

      if (nativeLibraryFilePathPackageReference is null) {
        Assert.Fail($"Expected native library file not found ({VersionInformations.NativeLibraryFileName})");
        return;
      }

      nativeLibraryFilePath = nativeLibraryFilePathPackageReference;
    }

#if SYSTEM_RUNTIME_INTEROPSERVICES_NATIVELIBRARY
    var nativeLibraryHandle = IntPtr.Zero;

    Assert.DoesNotThrow(() => nativeLibraryHandle = NativeLibrary.Load(nativeLibraryFilePath)); // maybe alread loaded

    Assert.AreNotEqual(IntPtr.Zero, nativeLibraryHandle);
#endif
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
