using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace Smdn.LibHighlightSharp;

[TestFixture]
public partial class HighlightTests {
  [Test]
  public void NativeLibraryVersion()
  {
    TestContext.WriteLine($"{nameof(VersionInformations.NativeLibraryVersion)}: {VersionInformations.NativeLibraryVersion}");

    Assert.IsNotNull(VersionInformations.NativeLibraryVersion);
    Assert.GreaterOrEqual(VersionInformations.NativeLibraryVersion.Major, 0, nameof(VersionInformations.NativeLibraryVersion.Major));
    Assert.GreaterOrEqual(VersionInformations.NativeLibraryVersion.Minor, 0, nameof(VersionInformations.NativeLibraryVersion.Minor));
  }

  [Test]
  public void NativeLibraryName()
  {
    TestContext.WriteLine($"{nameof(VersionInformations.NativeLibraryName)}: {VersionInformations.NativeLibraryName}");

    var v = VersionInformations.NativeLibraryVersion;
    var versionSuffix = $"-v{v.Major}_{v.Minor}_0_0";

    StringAssert.EndsWith(versionSuffix, VersionInformations.NativeLibraryName);
  }

  [Test]
  public void NativeLibraryFileName()
  {
    if (
      RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
      RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
      RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
    ) {
      Assert.DoesNotThrow(() => Assert.IsNotNull(VersionInformations.NativeLibraryFileName));
    }
    else {
      Assert.Throws<PlatformNotSupportedException>(() => Assert.IsNotNull(VersionInformations.NativeLibraryFileName));
      return;
    }

    TestContext.WriteLine($"{nameof(VersionInformations.NativeLibraryFileName)}: {VersionInformations.NativeLibraryFileName}");

    var v = VersionInformations.NativeLibraryVersion;
    var versionSuffix = $"-v{v.Major}_{v.Minor}_0_0";

    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
      StringAssert.StartsWith("lib", VersionInformations.NativeLibraryFileName);
      StringAssert.EndsWith(versionSuffix + ".so", VersionInformations.NativeLibraryFileName);
    }
    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
      StringAssert.StartsWith("lib", VersionInformations.NativeLibraryFileName);
      StringAssert.EndsWith(versionSuffix + ".dylib", VersionInformations.NativeLibraryFileName);
    }
    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
      StringAssert.EndsWith(versionSuffix + ".dll", VersionInformations.NativeLibraryFileName);
    }
  }

  [Test]
  public void BindingsVersion()
  {
    TestContext.WriteLine($"{nameof(VersionInformations.BindingsVersion)}: {VersionInformations.BindingsVersion}");

    Assert.IsNotNull(VersionInformations.BindingsVersion);
    Assert.GreaterOrEqual(VersionInformations.BindingsVersion.Major, 0, nameof(VersionInformations.BindingsVersion.Major));
    Assert.GreaterOrEqual(VersionInformations.BindingsVersion.Minor, 0, nameof(VersionInformations.BindingsVersion.Minor));

    Assert.AreEqual(VersionInformations.BindingsVersion.Major, VersionInformations.NativeLibraryVersion.Major);
    Assert.AreEqual(VersionInformations.BindingsVersion.Minor, VersionInformations.NativeLibraryVersion.Minor);
  }
}
