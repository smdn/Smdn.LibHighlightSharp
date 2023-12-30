// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
using System;
using System.Collections.Generic;

namespace Smdn.LibHighlightSharp;

#pragma warning disable IDE0040
partial class Highlight {
#pragma warning restore IDE0040
  private static readonly IReadOnlyDictionary<GeneratorOutputType, string> GeneratorOutputTypeValueToNameMap
    = new Dictionary<GeneratorOutputType, string>() {
      [GeneratorOutputType.Html] = "HTML",
      [GeneratorOutputType.Xhtml] = "XHTML",
      [GeneratorOutputType.TeX] = "TEX",
      [GeneratorOutputType.LaTeX] = "LATEX",
      [GeneratorOutputType.Rtf] = "RTF",
      [GeneratorOutputType.EscapeSequencesAnsi] = "ESC_ANSI",
      [GeneratorOutputType.EscapeSequencesXterm256] = "ESC_XTERM256",
      [GeneratorOutputType.Svg] = "SVG",
      [GeneratorOutputType.BBCode] = "BBCODE",
      [GeneratorOutputType.Pango] = "PANGO",
      [GeneratorOutputType.Odt] = "ODTFLAT",
      [GeneratorOutputType.EscapeSequencesTrueColor] = "ESC_TRUECOLOR",
    };

  /// <summary>
  /// Converts the value of <see cref="GeneratorOutputType"/> to <see cref="Bindings.OutputType"/>.
  /// </summary>
  /// <remarks>
  /// The value of each constant in OutputType has changed in between Highlight 3.x and 4.x.
  /// Therefore, instead of converting from GeneratorOutputType to OutputType by its values, this method converts them based on the name of the constant.
  /// </remarks>
  internal static Bindings.OutputType TranslateOutputType(GeneratorOutputType outputType)
  {
    // translate by enum filed name
    if (GeneratorOutputTypeValueToNameMap.TryGetValue(outputType, out var name)) {
#if SYSTEM_ENUM_GETVALUES_OF_TENUM && SYSTEM_ENUM_GETNAME_OF_TENUM
      foreach (var value in Enum.GetValues<Bindings.OutputType>()) {
        if (string.Equals(name, Enum.GetName(value), StringComparison.Ordinal))
          return value;
      }
#else
      foreach (var value in Enum.GetValues(typeof(Bindings.OutputType))) {
        if (string.Equals(name, Enum.GetName(typeof(Bindings.OutputType), value), StringComparison.Ordinal))
          return (Bindings.OutputType)value;
      }
#endif
    }

    // translate by its value directly if any names did not match
    return (Bindings.OutputType)outputType;
  }
}
