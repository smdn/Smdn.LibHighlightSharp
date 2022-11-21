// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
using System;
#if NULL_STATE_STATIC_ANALYSIS_ATTRIBUTES
using System.Diagnostics.CodeAnalysis;
#endif
using System.IO;
using Microsoft.CSharp.RuntimeBinder;

namespace Smdn.LibHighlightSharp;

#pragma warning disable IDE0040
partial class Highlight {
#pragma warning restore IDE0040
  /*
   * filetypes.conf
   */
  private const string DefaultFileTypesConfigFileNameWithoutExtension = "filetypes";

  public bool TryLoadFileTypesConfig()
  {
    dynamic dataDir = DataDir;

    try {
      return dataDir.loadFileTypeConfig(DefaultFileTypesConfigFileNameWithoutExtension);
    }
    catch (RuntimeBinderException) {
      return false;
    }
  }

  public void LoadFileTypesConfig(string fileTypesConfPath)
  {
    if (fileTypesConfPath is null)
      throw new ArgumentNullException(nameof(fileTypesConfPath));

    ThrowIfVersionNotSupported(
      feature: "DataDir::loadFileTypeConfig()",
      minimumVersion: MinimumVersionSupportingLoadFileTypesConfig
    );

    // DataDir::loadFileTypeConfig attempt to read filetype config with the filename appending .conf extension internally,
    // so the file extension should be removed before calling it.
    var fileTypesConfPathWithoutExtension =
#if SYSTEM_IO_PATH_JOIN
      Path.Join(
#else
      Path.Combine(
#endif
#pragma warning disable SA1114
        Path.GetDirectoryName(fileTypesConfPath),
        Path.GetFileNameWithoutExtension(fileTypesConfPath)
      );
#pragma warning restore SA1114

    dynamic dataDir = DataDir;

    if (!dataDir.loadFileTypeConfig(fileTypesConfPathWithoutExtension))
      throw new InvalidOperationException($"failed to load filetypes.conf from file '{fileTypesConfPath}'");
  }

  public string GuessFileType(string inputFilePath)
  {
    if (inputFilePath is null)
      throw new ArgumentNullException(nameof(inputFilePath));
    if (!File.Exists(inputFilePath))
      throw new FileNotFoundException("specified file does not exist", fileName: inputFilePath);

    ThrowIfVersionNotSupported(
      feature: "DataDir::guessFileType()",
      minimumVersion: MinimumVersionSupportingGuessFileType
    );

    var suffix = Path.GetExtension(inputFilePath);

#if SYSTEM_STRING_STARTSWITH_CHAR
    if (suffix.StartsWith('.'))
#else
    if (suffix.StartsWith(".", StringComparison.Ordinal))
#endif
      suffix = suffix.Substring(1);

    dynamic dataDir = DataDir;

    return dataDir.guessFileType(
      suffix: suffix,
      inputFile: inputFilePath,
      useUserSuffix: false,
      forceShebangCheckStdin: false
    );
  }

  /*
   * langDefs/*.lang
   */
  private const string SyntaxFileExtension = ".lang";

  private string GetSyntaxFilePathFromName(string name, string paramName)
    => DataDirForSyntaxes.getLangPath(
      file: (name ?? throw new ArgumentNullException(paramName)) + SyntaxFileExtension
    );

  public bool TryFindSyntaxFile(
    string name,
#if NULL_STATE_STATIC_ANALYSIS_ATTRIBUTES
    [NotNullWhen(true)]
#endif
    out string? syntaxFilePath
  )
  {
    syntaxFilePath = null;

    if (string.IsNullOrEmpty(name))
      return false;

    syntaxFilePath = GetSyntaxFilePathFromName(name, nameof(name));

    return File.Exists(syntaxFilePath);
  }

  /*
   * themes/*.theme
   */
  private const string ThemeFileExtension = ".theme";

  private string GetThemeFilePathFromName(string name, bool base16, string paramName)
  {
    var file = (name ?? throw new ArgumentNullException(paramName)) + ThemeFileExtension;

    ThrowIfDisposed();

    if (base16) {
      ThrowIfVersionNotSupported(
        feature: "Base16 themes",
        minimumVersion: MinimumVersionSupportingBase16Themes
      );

      dynamic dataDir = DataDirForThemes;

      return dataDir.getThemePath(file: file, base16: true);
    }

    return DataDirForThemes.getThemePath(file: file);
  }

  public bool TryFindThemeFile(
    string name,
#if NULL_STATE_STATIC_ANALYSIS_ATTRIBUTES
    [NotNullWhen(true)]
#endif
    out string? themeFilePath
  )
  {
    themeFilePath = null;

    if (string.IsNullOrEmpty(name))
      return false;

    themeFilePath = GetThemeFilePathFromName(name, base16: false, nameof(name));

    return File.Exists(themeFilePath);
  }

  public bool TryFindThemeBase16File(
    string name,
#if NULL_STATE_STATIC_ANALYSIS_ATTRIBUTES
    [NotNullWhen(true)]
#endif
    out string? themeFilePath
  )
  {
    themeFilePath = null;

    if (string.IsNullOrEmpty(name))
      return false;
    if (VersionInformations.NativeLibraryVersion < MinimumVersionSupportingBase16Themes)
      return false;

    themeFilePath = GetThemeFilePathFromName(name, base16: true, nameof(name));

    return File.Exists(themeFilePath);
  }
}
