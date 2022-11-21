// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
using System;

namespace Smdn.LibHighlightSharp;

#pragma warning disable IDE0040
partial class Highlight {
#pragma warning restore IDE0040
  /*
   * `.theme` methods
   */
  public void SetTheme(string name)
    => SetThemeFromFile(
      GetThemeFilePathFromName(name, base16: false, nameof(name))
    );

  public void SetThemeBase16(string name)
    => SetThemeFromFile(
      GetThemeFilePathFromName(name, base16: true, nameof(name))
    );

  public void SetThemeFromFile(string pathToThemeFile)
  {
    if (pathToThemeFile is null)
      throw new ArgumentNullException(nameof(pathToThemeFile));

    if (!CodeGenerator.initTheme(pathToThemeFile))
      throw new HighlightThemeException(pathToThemeFile, CodeGenerator.getThemeInitError());

    initThemeCalled = true;
  }

  /*
   * `.lang` methods
   */
  public void SetSyntax(string name)
    => SetSyntaxFromFile(
      GetSyntaxFilePathFromName(name, nameof(name))
    );

  public void SetSyntaxFromFile(string pathToLangFile)
  {
    if (!initThemeCalled)
      loadLanguageBeforeInitTheme = true;

    HighlightSyntaxException.ThrowIfError(
      pathToLangFile ?? throw new ArgumentNullException(nameof(pathToLangFile)),
      CodeGenerator.loadLanguage(pathToLangFile)
    );
  }

  /*
   * `generate` methods
   */
  public string Generate(string input)
  {
    ThrowIfDisposed();
    ThrowIfInvalidCallingOrderBeforeGenerate();

    return CodeGenerator.generateString(input ?? throw new ArgumentNullException(nameof(input)));
  }

  public string GenerateFromFile(string path)
  {
    ThrowIfDisposed();
    ThrowIfInvalidCallingOrderBeforeGenerate();

    return CodeGenerator.generateStringFromFile(path ?? throw new ArgumentNullException(nameof(path)));
  }

  public void Generate(string inputPath, string outputPath)
  {
    ThrowIfDisposed();
    ThrowIfInvalidCallingOrderBeforeGenerate();

    HighlightParserException.ThrowIfError(
      CodeGenerator.generateFile(
        inputPath ?? throw new ArgumentNullException(nameof(inputPath)),
        outputPath ?? throw new ArgumentNullException(nameof(inputPath))
      )
    );
  }
}
