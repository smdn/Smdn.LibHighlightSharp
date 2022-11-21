// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
#nullable enable

using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Smdn.LibHighlightSharp;

public class VersionInformations {
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

  private static int GetHighlightVersionMajor() => GetHighlightVersion(0);
  private static int GetHighlightVersionMinor() => GetHighlightVersion(1);

  private static unsafe int GetHighlightVersion(int part)
  {
    const int length = 8;
    var buffer = stackalloc sbyte[length];

    var len = smdn_libhighlightsharp_get_highlight_version(part, buffer, length);

    if (0 < len) {
      var versionString = new string(buffer, 0, Math.Min(len, length));

      if (int.TryParse(versionString, out var version))
        return version;
    }
    else {
      // try to get full version string instead
      len = smdn_libhighlightsharp_get_highlight_version(-1, buffer, length);

      var versionString = new string(buffer, 0, Math.Min(len, length));

      if (Version.TryParse(versionString, out var version)) {
        return part switch {
          0 => version.Major,
          1 => version.Minor,
          _ => 0,
        };
      }
    }

    return 0;
  }

  private static readonly Lazy<Version> nativeLibraryVersion = new(
    () => new Version(
      major: GetHighlightVersionMajor(),
      minor: GetHighlightVersionMinor()
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
