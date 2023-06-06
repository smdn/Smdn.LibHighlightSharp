// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
using System;
using Microsoft.CSharp.RuntimeBinder;

namespace Smdn.LibHighlightSharp;

// ref: The CodeGenerator APIs as of highlight 3.0 beta
//      https://gitlab.com/saalen/highlight/-/blob/3a106f48d988310418c8a00571076eef3f5b28de/highlight/src/include/codegenerator.h

#pragma warning disable IDE0040
partial class Highlight {
#pragma warning restore IDE0040
  /// <summary>
  /// Gets or sets a value indicating whether the CodeGenerator prints the line numbers in output or not.
  /// This value is equivalent to the option '--line-numbers'.
  /// </summary>
  /// <value>
  /// A value that specifies the CodeGenerator to print the line numbers in output or not.
  /// </value>
  public bool PrintLineNumbers {
    get => CodeGenerator.getPrintLineNumbers();
    set => CodeGenerator.setPrintLineNumbers(value, startCnt: 1u); // TODO: set startCnt
  }

  /// <summary>
  /// Gets or sets a value indicating whether the CodeGenerator pads the line numbers with 0's or not.
  /// This value is equivalent to the option '--zeroes'.
  /// </summary>
  /// <value>
  /// A value that specifies the CodeGenerator to pad the line numbers with 0's or not.
  /// </value>
  public bool LineNumberZeroPadding {
    get => CodeGenerator.getPrintZeroes();
    set => CodeGenerator.setPrintZeroes(value);
  }

  /// <summary>
  /// Gets or sets a value indicating whether the CodeGenerator omits the document header/footer or not.
  /// This value is equivalent to the option '--fragment'.
  /// </summary>
  /// <value>
  /// A value that specifies the CodeGenerator to omit the document header/footer or not.
  /// </value>
  public bool Fragment {
    get => CodeGenerator.getFragmentCode();
    set => CodeGenerator.setFragmentCode(value);
  }

  /// <summary>
  /// Gets or sets a value indicating the line number width that the CodeGenerator prints in output.
  /// This value is equivalent to the option '--line-number-length'.
  /// </summary>
  /// <value>
  /// A value that specifies the line number width that the CodeGenerator prints in output.
  /// </value>
  public int LineNumberWidth {
    get => CodeGenerator.getLineNumberWidth();
    set => CodeGenerator.setLineNumberWidth(value);
  }

  public bool ValidateInput {
    get => CodeGenerator.getValidateInput();
    set => CodeGenerator.setValidateInput(value);
  }

  // TODO: dynamic binding
  public bool KeepInjections {
    get => CodeGenerator.getKeepInjections();
    set => CodeGenerator.setKeepInjections(value);
  }

  // TODO: dynamic binding
  public bool IncrementWrappedLineNumber {
    get => CodeGenerator.getNumberWrappedLines();
    set => CodeGenerator.setNumberWrappedLines(value);
  }

  /// <summary>
  /// Gets or sets a value indicating whether the CodeGenerator omits the version info comment or not.
  /// This value is equivalent to the option '--no-version-info'.
  /// </summary>
  /// <value>
  /// A value that specifies the CodeGenerator to omit the version info comment or not.
  /// This property does nothing and always returns <see langword="false"/> if the API is not supported with this version of the native library.
  /// </value>
  public bool OmitVersionComment {
    get {
      dynamic generator = CodeGenerator;

      try {
        return generator.getOmitVersionComment();
      }
      catch (RuntimeBinderException) {
        return false;
      }
    }
    set {
      dynamic generator = CodeGenerator;

      try {
        generator.setOmitVersionComment(value);
      }
      catch (RuntimeBinderException) {
        // ignore
      }
    }
  }

  /// <summary>
  /// Gets or sets a value indicating whether the CodeGenerator outputs each syntax token separately or not.
  /// This value is equivalent to the option '--isolate'.
  /// </summary>
  /// <value>
  /// A value that specifies the CodeGenerator to output each syntax token separately or not.
  /// This property does nothing and always returns <see langword="false"/> if the API is not supported with this version of the native library.
  /// </value>
  public bool IsolateTags {
    get {
      dynamic generator = CodeGenerator;

      try {
        return generator.getIsolateTags();
      }
      catch (RuntimeBinderException) {
        return false;
      }
    }
    set {
      dynamic generator = CodeGenerator;

      try {
        generator.setIsolateTags(value);
      }
      catch (RuntimeBinderException) {
        // ignore
      }
    }
  }

  public string StyleName => CodeGenerator.getStyleName();

  public string BaseFont {
    get => CodeGenerator.getBaseFont();
    set => CodeGenerator.setBaseFont(value ?? throw new ArgumentNullException(nameof(BaseFont)));
  }

  public string BaseFontSize {
    get => CodeGenerator.getBaseFontSize();
    set => CodeGenerator.setBaseFontSize(value ?? throw new ArgumentNullException(nameof(BaseFontSize)));
  }

  public string Title {
    get => CodeGenerator.getTitle();
    set => CodeGenerator.setTitle(value ?? throw new ArgumentNullException(nameof(Title)));
  }

  public string StyleInputPath {
    get => CodeGenerator.getStyleInputPath();
    set => CodeGenerator.setStyleInputPath(value ?? throw new ArgumentNullException(nameof(StyleInputPath)));
  }

  public string StyleOutputPath {
    get => CodeGenerator.getStyleOutputPath();
    set => CodeGenerator.setStyleOutputPath(value ?? throw new ArgumentNullException(nameof(StyleOutputPath)));
  }

  /// <summary>Gets the message of the error that occurred on the lastest call of <see cref="SetTheme(string)"/> or <see cref="SetThemeFromFile(string)"/>.</summary>
  /// <value>An error message string, or <see langword="null"/> if the API is not supported with this version of the native library.</value>
  public string? LastSyntaxError {
    get {
      dynamic generator = CodeGenerator;

      try {
        return generator.getSyntaxLuaError();
      }
      catch (RuntimeBinderException) {
        return null;
      }
    }
  }

  /// <summary>Gets the value of 'Description' described in the currently loaded syntax file.</summary>
  /// <value>A 'Description' value, or <see langword="null"/> if the API is not supported with this version of the native library.</value>
  public string? SyntaxDescription {
    get {
      dynamic generator = CodeGenerator;

      try {
        return generator.getSyntaxDescription();
      }
      catch (RuntimeBinderException) {
        return null;
      }
    }
  }

  /// <summary>Gets the value of 'EncodingHint' described in the currently loaded syntax file.</summary>
  /// <value>A 'EncodingHint' value, or <see langword="null"/> if the API is not supported with this version of the native library.</value>
  public string? SyntaxEncodingHint {
    get {
      dynamic generator = CodeGenerator;

      try {
        return generator.getSyntaxEncodingHint();
      }
      catch (RuntimeBinderException) {
        return null;
      }
    }
  }

  /// <summary>Gets the value of 'Description' described in the currently loaded theme file.</summary>
  /// <value>A 'Description' value, or <see langword="null"/> if the API is not supported with this version of the native library.</value>
  public string? ThemeDescription {
    get {
      dynamic generator = CodeGenerator;

      try {
        return generator.getThemeDescription();
      }
      catch (RuntimeBinderException) {
        return null;
      }
    }
  }

  /// <summary>Gets the value of 'Categories' described in the currently loaded syntax file.</summary>
  /// <value>A 'Categories' value, or <see langword="null"/> if the API is not supported with this version of the native library.</value>
  public string? SyntaxCategoryDescription {
    get {
      dynamic generator = CodeGenerator;

      try {
        return generator.getSyntaxCatDescription();
      }
      catch (RuntimeBinderException) {
        return null;
      }
    }
  }

  /// <summary>Gets the value of 'Categories' described in the currently loaded theme file.</summary>
  /// <value>A 'Categories' value, or <see langword="null"/> if the API is not supported with this version of the native library.</value>
  public string? ThemeCategoryDescription {
    get {
      dynamic generator = CodeGenerator;

      try {
        return generator.getThemeCatDescription();
      }
      catch (RuntimeBinderException) {
        return null;
      }
    }
  }

  public void SetEncoding(string encodingName)
    => CodeGenerator.setEncoding(encodingName ?? throw new ArgumentNullException(nameof(encodingName)));

  public void SetIncludeStyle(bool trueForInclude)
    => CodeGenerator.setIncludeStyle(trueForInclude);

  /// <summary>
  /// Gets or sets a value for an additional EOF char to look for in stream.
  /// </summary>
  /// <value>
  /// A value that specifies the additional EOF char.
  /// <see langword="null"/> indicates that the additional EOF char is not set or that the CodeGenerator does not support setting additional EOF char.
  /// If <see langword="null"/> is set, the additional EOF char is cleared.
  /// </value>
  public char? AdditionalEndOfFileChar {
    get {
      dynamic generator = CodeGenerator;

      try {
        var extraEOFChar = generator.getAdditionalEOFChar();

        return extraEOFChar == DefaultExtraEOFChar
          ? null
          : (char)extraEOFChar;
      }
      catch (RuntimeBinderException) {
        return null;
      }
    }
    set {
      dynamic generator = CodeGenerator;

      try {
        if (value is null)
          generator.setAdditionalEOFChar();
        else if (DefaultExtraEOFChar <= value)
          throw new ArgumentOutOfRangeException(message: "must be in range of 0x00~0xFE", paramName: nameof(AdditionalEndOfFileChar));
        else
          generator.setAdditionalEOFChar((byte)value);
      }
      catch (RuntimeBinderException) {
        // ignore
      }
    }
  }

  private const byte DefaultExtraEOFChar = 0xFF;
}
