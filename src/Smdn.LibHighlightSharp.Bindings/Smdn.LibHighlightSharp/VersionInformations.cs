// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
#nullable enable

using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Smdn.LibHighlightSharp;

public static class VersionInformations {
  public static Version BindingsVersion => Assembly.GetExecutingAssembly().GetName().Version ?? new Version();

  [DllImport(
    Bindings.HighlightConfigurations.DllImportName,
    EntryPoint = nameof(smdn_libhighlightsharp_get_highlight_version)
  )]
#pragma warning disable SA1300, SA1305
  private static unsafe extern int smdn_libhighlightsharp_get_highlight_version(
    int nVersionPart,
    sbyte* lpVersion,
    int nLength
  );
#pragma warning restore SA1300, SA1305

  /// <summary>The version part to retrieve.</summary>
  /// <remarks>
  /// The value should be synchronized with the value required by nVersionPart parameter of <see cref="smdn_libhighlightsharp_get_highlight_version"/>.
  /// See <see href="../../highlight/highlight-version.cpp">../../highlight/highlight-version.cpp</see> for detail.
  /// </remarks>
  private enum VersionPart : int {
    Full = -1,
    Major = 0,
    Minor = 1,
  }

  private static unsafe int GetHighlightVersion(VersionPart part)
  {
    const int length = 8;
    var buffer = stackalloc sbyte[length];

    var len = smdn_libhighlightsharp_get_highlight_version((int)part, buffer, length);

    if (0 < len) {
      var versionString = new string(buffer, 0, Math.Min(len, length));

      if (int.TryParse(versionString, out var version))
        return version;
    }
    else {
      // try to get full version string instead
      len = smdn_libhighlightsharp_get_highlight_version((int)VersionPart.Full, buffer, length);

      var versionString = new string(buffer, 0, Math.Min(len, length));

      if (Version.TryParse(versionString, out var version)) {
        return part switch {
          VersionPart.Major => version.Major,
          VersionPart.Minor => version.Minor,
          _ => 0,
        };
      }
    }

    return 0;
  }

  private static readonly Lazy<Version> nativeLibraryVersion = new(
    () => new Version(
      major: GetHighlightVersion(VersionPart.Major),
      minor: GetHighlightVersion(VersionPart.Minor)
    )
  );

  public static Version NativeLibraryVersion => nativeLibraryVersion.Value;

  public static string NativeLibraryName => Bindings.HighlightConfigurations.DllImportName;

  public static string NativeLibraryFileName {
    get {
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        return $"lib{Bindings.HighlightConfigurations.DllImportName}.so";
      if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        return $"lib{Bindings.HighlightConfigurations.DllImportName}.dylib";
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        return $"{Bindings.HighlightConfigurations.DllImportName}.dll";

      throw new PlatformNotSupportedException();
    }
  }
}
