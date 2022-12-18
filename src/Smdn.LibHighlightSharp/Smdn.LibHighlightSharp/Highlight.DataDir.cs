// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
using System;
using System.Collections.Generic;
#if NULL_STATE_STATIC_ANALYSIS_ATTRIBUTES
using System.Diagnostics.CodeAnalysis;
#endif
using System.IO;
using System.Linq;
using Microsoft.CSharp.RuntimeBinder;
using Smdn.LibHighlightSharp.Bindings;

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
  private const string SyntaxDirectoryName = "langDefs";

  private string GetSyntaxFilePathFromName(string name, string paramName)
  {
    var syntaxFileName = (name ?? throw new ArgumentNullException(paramName)) + SyntaxFileExtension;
    var syntaxFilePath = DataDirForSyntaxes.getLangPath(file: syntaxFileName);

    if (
      UserDefinedDataDirPathForSyntaxes is not null &&
      !(Path.IsPathRooted(syntaxFilePath) && File.Exists(syntaxFilePath))
    ) {
      // Fallback path
      //   highlight (< 3.40) does not support HIGHLIGHT_DATADIR on Windows.
      //   ref: https://github.com/andre-simon/highlight/issues/24
      var syntaxFilePathUnderUserDefinedDataDir = Path.Combine(
        UserDefinedDataDirPathForSyntaxes,
        DataDirForSyntaxes.getLangPath(file: syntaxFileName)
      );

      if (File.Exists(syntaxFilePathUnderUserDefinedDataDir))
        syntaxFilePath = syntaxFilePathUnderUserDefinedDataDir;
    }

    return syntaxFilePath;
  }

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

  public IEnumerable<string> EnumerateSyntaxFiles()
  {
    var syntaxDir = DataDirForSyntaxes.getLangPath(string.Empty);

    if (UserDefinedDataDirPathForSyntaxes is not null && !Path.IsPathRooted(syntaxDir)) {
      // Fallback path
      //   highlight (< 3.40) does not support HIGHLIGHT_DATADIR on Windows.
      //   ref: https://github.com/andre-simon/highlight/issues/24
      syntaxDir = Path.Combine(UserDefinedDataDirPathForSyntaxes, SyntaxDirectoryName);
    }

    if (!Directory.Exists(syntaxDir))
      return Enumerable.Empty<string>();

    return Directory.EnumerateFiles(
      syntaxDir,
      "*" + SyntaxFileExtension,
      SearchOption.TopDirectoryOnly
    );
  }

  public IEnumerable<(
    string Path,
    string? Description
  )> EnumerateSyntaxFilesWithDescription()
  {
    foreach (var syntaxFile in EnumerateSyntaxFiles()) {
      using var reader = new SyntaxReader();

      var canGetDescription = LoadResult.LOAD_OK == reader.load(syntaxFile, string.Empty, Bindings.OutputType.HTML);

      yield return (Path: syntaxFile, Description: canGetDescription ? reader.getDescription() : null);
    }
  }

  /*
   * themes/*.theme
   */
  private const string ThemeFileExtension = ".theme";
  private const string ThemeDirectoryName = "themes";

  private string GetThemeFilePathFromName(string name, bool base16, string paramName)
  {
    var themeFileName = (name ?? throw new ArgumentNullException(paramName)) + ThemeFileExtension;

    ThrowIfDisposed();

    if (base16) {
      ThrowIfVersionNotSupported(
        feature: "Base16 themes",
        minimumVersion: MinimumVersionSupportingBase16Themes
      );

      dynamic dataDir = DataDirForThemes;

      return dataDir.getThemePath(file: themeFileName, base16: true);
    }

    var themeFilePath = DataDirForThemes.getThemePath(file: themeFileName);

    if (
      UserDefinedDataDirPathForThemes is not null &&
      !(Path.IsPathRooted(themeFilePath) && File.Exists(themeFilePath))
    ) {
      // Fallback path
      //   highlight (< 3.40) does not support HIGHLIGHT_DATADIR on Windows.
      //   ref: https://github.com/andre-simon/highlight/issues/24
      var themeFilePathUnderUserDefinedDataDir = Path.Combine(
        UserDefinedDataDirPathForThemes,
        DataDirForThemes.getThemePath(file: themeFileName)
      );

      if (File.Exists(themeFilePathUnderUserDefinedDataDir))
        themeFilePath = themeFilePathUnderUserDefinedDataDir;
    }

    return themeFilePath;
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

  public IEnumerable<string> EnumerateThemeFiles()
  {
    var themeDir = DataDirForThemes.getThemePath(string.Empty);

    if (UserDefinedDataDirPathForThemes is not null && !Path.IsPathRooted(themeDir)) {
      // Fallback path
      //   highlight (< 3.40) does not support HIGHLIGHT_DATADIR on Windows.
      //   ref: https://github.com/andre-simon/highlight/issues/24
      themeDir = Path.Combine(UserDefinedDataDirPathForThemes, ThemeDirectoryName);
    }

    if (!Directory.Exists(themeDir))
      return Enumerable.Empty<string>();

    return Directory.EnumerateFiles(
      themeDir,
      "*" + ThemeFileExtension,
      SearchOption.AllDirectories
    );
  }

  public IEnumerable<(
    string Path,
    string? Description
  )> EnumerateThemeFilesWithDescription()
  {
    using var hl = Clone();

    foreach (var themeFile in EnumerateThemeFiles()) {
      string? themeDescription = null;

      try {
        hl.SetThemeFromFile(themeFile);

        themeDescription = hl.ThemeDescription;
      }
      catch (HighlightThemeException) {
        // ignore
      }

      yield return (Path: themeFile, Description: themeDescription);
    }
  }
}
