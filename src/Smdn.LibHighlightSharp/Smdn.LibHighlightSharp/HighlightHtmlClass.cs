// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
using System;
using System.Collections.Generic;
#if NULL_STATE_STATIC_ANALYSIS_ATTRIBUTES
using System.Diagnostics.CodeAnalysis;
#endif
using System.Reflection;

namespace Smdn.LibHighlightSharp;

public sealed class HighlightHtmlClass : IEquatable<HighlightHtmlClass>, IEquatable<string> {
  private static readonly Version Version3x = new(3, 0);
  private static readonly Version Version4x = new(4, 0);
  private static readonly Version VersionLatest = new(5, 0);

  // XHTML attribute values are case sensitive
  // ref: https://stackoverflow.com/questions/12533926/are-class-names-in-css-selectors-case-sensitive
  private static readonly StringComparer ClassNameEqualityComparer = StringComparer.Ordinal;
  private static readonly StringComparison ClassNameComparison = StringComparison.Ordinal;

  internal const string CommonPrefix = "hl ";

  public static HighlightHtmlClass Highlight { get; } = new("hl", HighlightElementType.Other, Version3x, VersionLatest);

  // The following style names have changed in version 4.
  // - str -> sng
  // - std -> def
  // ref: https://gitlab.com/saalen/highlight/-/blob/master/README_V4_MIGRATION.adoc

  public static HighlightHtmlClass Default => 4 <= VersionInformations.NativeLibraryVersion.Major ? DefaultV4 : DefaultV3;
  public static HighlightHtmlClass DefaultV3 { get; } = new("std", HighlightElementType.Default, Version3x, Version4x);
  public static HighlightHtmlClass DefaultV4 { get; } = new("def", HighlightElementType.Default, Version4x, VersionLatest);

  public static HighlightHtmlClass Strings => 4 <= VersionInformations.NativeLibraryVersion.Major ? StringsV4 : StringsV3;
  public static HighlightHtmlClass StringsV3 { get; } = new("str", HighlightElementType.Strings, Version3x, Version4x);
  public static HighlightHtmlClass StringsV4 { get; } = new("sng", HighlightElementType.Strings, Version4x, VersionLatest);

  public static HighlightHtmlClass Number { get; } = new("num", HighlightElementType.Number, Version3x, VersionLatest);
  public static HighlightHtmlClass SingleLineComment { get; } = new("slc", HighlightElementType.SingleLineComment, Version3x, VersionLatest);
  public static HighlightHtmlClass MultiLineComment { get; } = new("com", HighlightElementType.MultiLineComment, Version3x, VersionLatest);
  public static HighlightHtmlClass EscapedCharacter { get; } = new("esc", HighlightElementType.EscapedCharacter, Version3x, VersionLatest);
  public static HighlightHtmlClass Preprocessor { get; } = new("ppc", HighlightElementType.Preprocessor, Version3x, VersionLatest);
  public static HighlightHtmlClass PreprocessorString { get; } = new("pps", HighlightElementType.PreprocessorString, Version3x, VersionLatest);
  public static HighlightHtmlClass LineNumber { get; } = new("lin", HighlightElementType.LineNumber, Version3x, VersionLatest);
  public static HighlightHtmlClass Operator { get; } = new("opt", HighlightElementType.Operator, Version3x, VersionLatest);
  public static HighlightHtmlClass StringInterpolation { get; } = new("ipl", HighlightElementType.StringInterpolation, Version3x, VersionLatest);

  // IDs are defined up to 6 (6 is used in plugins/cpp_qt.lua from v3.x, langDefs/c.lang from v4.0)
  public static HighlightHtmlClass KeywordA { get; } = new("kwa", HighlightElementType.KeywordA, Version3x, VersionLatest);
  public static HighlightHtmlClass KeywordB { get; } = new("kwb", HighlightElementType.KeywordB, Version3x, VersionLatest);
  public static HighlightHtmlClass KeywordC { get; } = new("kwc", HighlightElementType.KeywordC, Version3x, VersionLatest);
  public static HighlightHtmlClass KeywordD { get; } = new("kwd", HighlightElementType.KeywordD, Version3x, VersionLatest);
  public static HighlightHtmlClass KeywordE { get; } = new("kwe", HighlightElementType.KeywordE, Version3x, VersionLatest);
  public static HighlightHtmlClass KeywordF { get; } = new("kwf", HighlightElementType.KeywordF, Version3x, VersionLatest);

  public static HighlightHtmlClass HoverText { get; } = new("hvr", HighlightElementType.HoverText, Version4x, VersionLatest);
  public static HighlightHtmlClass SyntaxError { get; } = new("err", HighlightElementType.SyntaxError, Version4x, VersionLatest);
  public static HighlightHtmlClass ErrorMessage { get; } = new("erm", HighlightElementType.ErrorMessage, Version4x, VersionLatest);

  private static readonly IReadOnlyDictionary<string, HighlightHtmlClass> ClassNameMap = InitializeClassNameMap();

#pragma warning disable CA1859
  private static IReadOnlyDictionary<string, HighlightHtmlClass> InitializeClassNameMap()
#pragma warning restore CA1859
  {
    var map = new Dictionary<string, HighlightHtmlClass>(ClassNameEqualityComparer);
    var typeOfHighlightHtmlClass = typeof(HighlightHtmlClass);

    foreach (var propertyName in new[] {
      nameof(Highlight),
      nameof(Default),
      nameof(Strings),
      nameof(Number),
      nameof(SingleLineComment),
      nameof(MultiLineComment),
      nameof(EscapedCharacter),
      nameof(Preprocessor),
      nameof(PreprocessorString),
      nameof(LineNumber),
      nameof(Operator),
      nameof(StringInterpolation),
      nameof(KeywordA),
      nameof(KeywordB),
      nameof(KeywordC),
      nameof(KeywordD),
      nameof(KeywordE),
      nameof(KeywordF),
      nameof(HoverText),
      nameof(SyntaxError),
      nameof(ErrorMessage),
    }) {
      var property = typeOfHighlightHtmlClass.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Static);

      if (property is null)
        continue; // should throw exception?

      var propertyValue = property.GetMethod?.Invoke(null, null);

      if (propertyValue is not HighlightHtmlClass value)
        continue; // should throw exception?

      if (
        value.minimumVersionInclusive <= VersionInformations.NativeLibraryVersion &&
        VersionInformations.NativeLibraryVersion < value.maximumVersionExclusive
      ) {
        map[value.ClassName] = value;
      }
    }

    return map;
  }

  public static bool TryParse(
    string className,
#if NULL_STATE_STATIC_ANALYSIS_ATTRIBUTES
    [NotNullWhen(true)]
#endif
    out HighlightHtmlClass? @class
  )
  {
    @class = default;

    if (className is null)
      return false;

    return ClassNameMap.TryGetValue(className, out @class);
  }

  public static bool TryParsePrefixed(
    string prefixedClassName,
#if NULL_STATE_STATIC_ANALYSIS_ATTRIBUTES
    [NotNullWhen(true)]
#endif
    out HighlightHtmlClass? @class
  )
  {
    @class = default;

    if (prefixedClassName is null)
      return false;
    if (!prefixedClassName.StartsWith(CommonPrefix, ClassNameComparison))
      return false;

    var styleName = prefixedClassName.Substring(CommonPrefix.Length).Trim();

    return ClassNameMap.TryGetValue(styleName, out @class);
  }

  /*
   * instance members
   */
  public string ClassName { get; }
  public HighlightElementType ElementType { get; }
  private readonly Version minimumVersionInclusive;
  private readonly Version maximumVersionExclusive;

  internal HighlightHtmlClass(
    string className,
    HighlightElementType elementType,
    Version minimumVersionInclusive,
    Version maximumVersionExclusive
  )
  {
    ClassName = className;
    ElementType = elementType;
    this.minimumVersionInclusive = minimumVersionInclusive;
    this.maximumVersionExclusive = maximumVersionExclusive;
  }

  public override int GetHashCode()
    => ClassNameEqualityComparer.GetHashCode(ClassName);

  public override bool Equals(object? obj)
    => obj switch {
      HighlightHtmlClass @class => Equals(@class),
      string str => Equals(str),
      _ => false,
    };

  public bool Equals(HighlightHtmlClass? other)
  {
    if (other is null)
      return false;
    if (ReferenceEquals(this, other))
      return true;

    return string.Equals(ClassName, other.ClassName, ClassNameComparison);
  }

  public bool Equals(string? other)
  {
    if (other is null)
      return false;

    return string.Equals(ClassName, other, ClassNameComparison);
  }

  public override string ToString()
    => ClassName;
}
