using System;
using System.Runtime.InteropServices;

using NUnit.Framework;

namespace Smdn.LibHighlightSharp;

[TestFixture]
public partial class HighlightTests {
  [Test]
  public void NativeLibraryVersion()
  {
    TestContext.WriteLine($"{nameof(VersionInformations.NativeLibraryVersion)}: {VersionInformations.NativeLibraryVersion}");

    Assert.That(VersionInformations.NativeLibraryVersion, Is.Not.Null);
    Assert.That(VersionInformations.NativeLibraryVersion.Major, Is.GreaterThanOrEqualTo(0), nameof(VersionInformations.NativeLibraryVersion.Major));
    Assert.That(VersionInformations.NativeLibraryVersion.Minor, Is.GreaterThanOrEqualTo(0), nameof(VersionInformations.NativeLibraryVersion.Minor));
  }

  [Test]
  public void NativeLibraryName()
  {
    TestContext.WriteLine($"{nameof(VersionInformations.NativeLibraryName)}: {VersionInformations.NativeLibraryName}");

    var v = VersionInformations.NativeLibraryVersion;
    var versionSuffix = $"-v{v.Major}_{v.Minor}_0_0";

    Assert.That(VersionInformations.NativeLibraryName, Does.EndWith(versionSuffix));
  }

  [Test]
  public void NativeLibraryFileName()
  {
    if (
      RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
      RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
      RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
    ) {
      Assert.DoesNotThrow(() => Assert.That(VersionInformations.NativeLibraryFileName, Is.Not.Null));
    }
    else {
      Assert.Throws<PlatformNotSupportedException>(() => Assert.That(VersionInformations.NativeLibraryFileName, Is.Not.Null));
      return;
    }

    TestContext.WriteLine($"{nameof(VersionInformations.NativeLibraryFileName)}: {VersionInformations.NativeLibraryFileName}");

    var v = VersionInformations.NativeLibraryVersion;
    var versionSuffix = $"-v{v.Major}_{v.Minor}_0_0";

    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
      Assert.That(VersionInformations.NativeLibraryFileName, Does.StartWith("lib"));
      Assert.That(VersionInformations.NativeLibraryFileName, Does.EndWith(versionSuffix + ".so"));
    }
    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
      Assert.That(VersionInformations.NativeLibraryFileName, Does.StartWith("lib"));
      Assert.That(VersionInformations.NativeLibraryFileName, Does.EndWith(versionSuffix + ".dylib"));
    }
    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
      Assert.That(VersionInformations.NativeLibraryFileName, Does.EndWith(versionSuffix + ".dll"));
    }
  }

  [Test]
  public void BindingsVersion()
  {
    TestContext.WriteLine($"{nameof(VersionInformations.BindingsVersion)}: {VersionInformations.BindingsVersion}");

    Assert.That(VersionInformations.BindingsVersion, Is.Not.Null);
    Assert.That(VersionInformations.BindingsVersion.Major, Is.GreaterThanOrEqualTo(0), nameof(VersionInformations.BindingsVersion.Major));
    Assert.That(VersionInformations.BindingsVersion.Minor, Is.GreaterThanOrEqualTo(0), nameof(VersionInformations.BindingsVersion.Minor));

    Assert.That(VersionInformations.NativeLibraryVersion.Major, Is.EqualTo(VersionInformations.BindingsVersion.Major));
    Assert.That(VersionInformations.NativeLibraryVersion.Minor, Is.EqualTo(VersionInformations.BindingsVersion.Minor));
  }
}
