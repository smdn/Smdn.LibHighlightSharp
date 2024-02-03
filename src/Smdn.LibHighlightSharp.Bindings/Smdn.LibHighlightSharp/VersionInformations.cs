// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
#nullable enable

using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Smdn.LibHighlightSharp;

public static partial class VersionInformations {
  public static Version BindingsVersion => Assembly.GetExecutingAssembly().GetName().Version ?? new Version();

#if SYSTEM_RUNTIME_INTEROPSERVICES_LIBRARYIMPORTATTRIBUTE
  [LibraryImport(
    Bindings.HighlightConfigurations.DllImportName,
    EntryPoint = nameof(smdn_libhighlightsharp_get_highlight_version)
  )]
  private static unsafe partial
#else
  [DllImport(
    Bindings.HighlightConfigurations.DllImportName,
    EntryPoint = nameof(smdn_libhighlightsharp_get_highlight_version)
  )]
  private static unsafe extern
#endif
#pragma warning disable SA1300, SA1305
  int smdn_libhighlightsharp_get_highlight_version(
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
    static bool TryGetHighlightVersionPart(VersionPart part, out int version)
    {
      version = 0;

      try {
        const int BufferLength = 4; // "nnn\0".Length
        var buffer = stackalloc sbyte[BufferLength];
        var len = smdn_libhighlightsharp_get_highlight_version((int)part, buffer, BufferLength);

        if (len <= 0)
          return false;

        var versionString = new string(buffer, 0, Math.Min(len, BufferLength));

        return int.TryParse(versionString, out version);
      }
      catch (EntryPointNotFoundException) {
        return false; // just ignore exception
      }
    }

    static bool TryGetHighlightFullVersion(out Version? version)
    {
      version = default;

      try {
        const int BufferLength = 7; // "xx.xxx\0".Length
        var buffer = stackalloc sbyte[BufferLength];
        var len = smdn_libhighlightsharp_get_highlight_version((int)VersionPart.Full, buffer, BufferLength);

        if (len <= 0)
          return false;

        var versionString = new string(buffer, 0, Math.Min(len, BufferLength));

        return Version.TryParse(versionString, out version);
      }
      catch (EntryPointNotFoundException) {
        return false; // just ignore exception
      }
    }

    // get major/minor part of version string
    if (TryGetHighlightVersionPart(part, out var versionPart))
      return versionPart;

    // fallback: try to get full version string instead and split into major/minor part
    if (TryGetHighlightFullVersion(out var version) && version is not null) {
      return part switch {
        VersionPart.Major => version.Major,
        VersionPart.Minor => version.Minor,
        _ => 0,
      };
    }

    return 0; // failed
  }

  private static readonly Lazy<Version> LazyNativeLibraryVersion = new(
    () => new Version(
      major: GetHighlightVersion(VersionPart.Major),
      minor: GetHighlightVersion(VersionPart.Minor)
    )
  );

  public static Version NativeLibraryVersion => LazyNativeLibraryVersion.Value;

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
