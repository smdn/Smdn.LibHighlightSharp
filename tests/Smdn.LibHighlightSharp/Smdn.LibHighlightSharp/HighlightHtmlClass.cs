// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
using System.Collections;

using NUnit.Framework;

namespace Smdn.LibHighlightSharp;

[TestFixture]
public class HighlightHtmlClassTests {
  private static IEnumerable YieldTestCases_ToString()
  {
    yield return new object[] { HighlightHtmlClass.Highlight, "hl" };
    yield return new object[] { HighlightHtmlClass.DefaultV3, "std" };
    yield return new object[] { HighlightHtmlClass.DefaultV4, "def" };
    yield return new object[] { HighlightHtmlClass.StringsV3, "str" };
    yield return new object[] { HighlightHtmlClass.StringsV4, "sng" };
    yield return new object[] { HighlightHtmlClass.Number, "num" };
    yield return new object[] { HighlightHtmlClass.SingleLineComment, "slc" };
    yield return new object[] { HighlightHtmlClass.MultiLineComment, "com" };
    yield return new object[] { HighlightHtmlClass.EscapedCharacter, "esc" };
    yield return new object[] { HighlightHtmlClass.Preprocessor, "ppc" };
    yield return new object[] { HighlightHtmlClass.PreprocessorString, "pps" };
    yield return new object[] { HighlightHtmlClass.LineNumber, "lin" };
    yield return new object[] { HighlightHtmlClass.Operator, "opt" };
    yield return new object[] { HighlightHtmlClass.StringInterpolation, "ipl" };
    yield return new object[] { HighlightHtmlClass.KeywordA, "kwa" };
    yield return new object[] { HighlightHtmlClass.KeywordB, "kwb" };
    yield return new object[] { HighlightHtmlClass.KeywordC, "kwc" };
    yield return new object[] { HighlightHtmlClass.KeywordD, "kwd" };
    yield return new object[] { HighlightHtmlClass.KeywordE, "kwe" };
    yield return new object[] { HighlightHtmlClass.KeywordF, "kwf" };
    yield return new object[] { HighlightHtmlClass.HoverText, "hvr" };
    yield return new object[] { HighlightHtmlClass.SyntaxError, "err" };
    yield return new object[] { HighlightHtmlClass.ErrorMessage, "erm" };
  }

  [TestCaseSource(nameof(YieldTestCases_ToString))]
  public void ToString(HighlightHtmlClass @class, string expected)
    => Assert.That(@class.ToString(), Is.EqualTo(expected));

  [Test]
  public void ToString_Default()
  {
    if (4 <= VersionInformations.NativeLibraryVersion.Major)
      Assert.That(HighlightHtmlClass.Default.ToString(), Is.EqualTo("def"));
    else
      Assert.That(HighlightHtmlClass.Default.ToString(), Is.EqualTo("std"));
  }

  [Test]
  public void ToString_Strings()
  {
    if (4 <= VersionInformations.NativeLibraryVersion.Major)
      Assert.That(HighlightHtmlClass.Strings.ToString(), Is.EqualTo("sng"));
    else
      Assert.That(HighlightHtmlClass.Strings.ToString(), Is.EqualTo("str"));
  }

  private static IEnumerable YieldTestCases_TryParse()
  {
    yield return new object?[] { null, false, null };
    yield return new object?[] { string.Empty, false, null };
    yield return new object?[] { "non-existent", false, null };
    yield return new object?[] { "HL", false, null }; // case sensitive

    yield return new object?[] { "hl", true, HighlightHtmlClass.Highlight };
    yield return new object?[] { "num", true, HighlightHtmlClass.Number };
    yield return new object?[] { "slc", true, HighlightHtmlClass.SingleLineComment };
    yield return new object?[] { "com", true, HighlightHtmlClass.MultiLineComment };
    yield return new object?[] { "esc", true, HighlightHtmlClass.EscapedCharacter };
    yield return new object?[] { "ppc", true, HighlightHtmlClass.Preprocessor };
    yield return new object?[] { "pps", true, HighlightHtmlClass.PreprocessorString };
    yield return new object?[] { "lin", true, HighlightHtmlClass.LineNumber };
    yield return new object?[] { "opt", true, HighlightHtmlClass.Operator };
    yield return new object?[] { "ipl", true, HighlightHtmlClass.StringInterpolation };
    yield return new object?[] { "kwa", true, HighlightHtmlClass.KeywordA };
    yield return new object?[] { "kwb", true, HighlightHtmlClass.KeywordB };
    yield return new object?[] { "kwc", true, HighlightHtmlClass.KeywordC };
    yield return new object?[] { "kwd", true, HighlightHtmlClass.KeywordD };
    yield return new object?[] { "kwe", true, HighlightHtmlClass.KeywordE };
    yield return new object?[] { "kwf", true, HighlightHtmlClass.KeywordF };
  }

  [TestCaseSource(nameof(YieldTestCases_TryParse))]
  public void TryParse(string className, bool expectedResult, HighlightHtmlClass expectedParseResult)
  {
    Assert.That(HighlightHtmlClass.TryParse(className, out var @class), Is.EqualTo(expectedResult));
    Assert.That(@class, Is.EqualTo(expectedParseResult));
    Assert.That(ReferenceEquals(expectedParseResult, @class), Is.True);
  }

  private static IEnumerable YieldTestCases_TryParse_V4()
  {
    yield return new object?[] { "hvr", HighlightHtmlClass.HoverText };
    yield return new object?[] { "err", HighlightHtmlClass.SyntaxError };
    yield return new object?[] { "erm", HighlightHtmlClass.ErrorMessage };
  }

  [TestCaseSource(nameof(YieldTestCases_TryParse_V4))]
  public void TryParse_V4(string className, HighlightHtmlClass expectedParseResult)
  {
    var isVersion4x = 4 <= VersionInformations.NativeLibraryVersion.Major;

    Assert.That(HighlightHtmlClass.TryParse(className, out var @class), Is.EqualTo(isVersion4x));

    if (isVersion4x) {
      Assert.That(@class, Is.EqualTo(expectedParseResult));
      Assert.That(ReferenceEquals(expectedParseResult, @class), Is.True);
    }
  }

  [TestCase]
  public void TryParse_Default()
  {
    var isVersion4x = 4 <= VersionInformations.NativeLibraryVersion.Major;

    Assert.That(HighlightHtmlClass.TryParse("def", out var classDef), Is.EqualTo(isVersion4x));
    Assert.That(HighlightHtmlClass.TryParse("std", out var classStd), Is.EqualTo(!isVersion4x));

    if (isVersion4x) {
      Assert.That(HighlightHtmlClass.DefaultV4, Is.EqualTo(classDef));
      Assert.That(ReferenceEquals(HighlightHtmlClass.DefaultV4, classDef), Is.True);
    }
    else {
      Assert.That(HighlightHtmlClass.DefaultV3, Is.EqualTo(classStd));
      Assert.That(ReferenceEquals(HighlightHtmlClass.DefaultV3, classStd), Is.True);
    }
  }

  [TestCase]
  public void TryParse_Strings()
  {
    var isVersion4x = 4 <= VersionInformations.NativeLibraryVersion.Major;

    Assert.That(HighlightHtmlClass.TryParse("sng", out var classSng), Is.EqualTo(isVersion4x));
    Assert.That(HighlightHtmlClass.TryParse("str", out var classStr), Is.EqualTo(!isVersion4x));

    if (isVersion4x) {
      Assert.That(HighlightHtmlClass.StringsV4, Is.EqualTo(classSng));
      Assert.That(ReferenceEquals(HighlightHtmlClass.StringsV4, classSng), Is.True);
    }
    else {
      Assert.That(HighlightHtmlClass.StringsV3, Is.EqualTo(classStr));
      Assert.That(ReferenceEquals(HighlightHtmlClass.StringsV3, classStr), Is.True);
    }
  }

  private static IEnumerable YieldTestCases_TryParsePrefixed()
  {
    yield return new object?[] { null, false, null };
    yield return new object?[] { string.Empty, false, null };
    yield return new object?[] { "non-existent", false, null };
    yield return new object?[] { "hl", false, null };
    yield return new object?[] { "hl ", false, null };
    yield return new object?[] { " hl", false, null };
    yield return new object?[] { "num", false, null };

    yield return new object?[] { "hl hl", true, HighlightHtmlClass.Highlight }; // XXX
    yield return new object?[] { "hl  num", true, HighlightHtmlClass.Number };
    yield return new object?[] { "hl num ", true, HighlightHtmlClass.Number };
    yield return new object?[] { "hl non-existent", false, null };
    yield return new object?[] { "hl num esc", false, null };
    yield return new object?[] { "HL num", false, null }; // case sensitive
    yield return new object?[] { "hl NUM", false, null }; // case sensitive

    yield return new object?[] { "hl num", true, HighlightHtmlClass.Number };
    yield return new object?[] { "hl slc", true, HighlightHtmlClass.SingleLineComment };
    yield return new object?[] { "hl com", true, HighlightHtmlClass.MultiLineComment };
    yield return new object?[] { "hl esc", true, HighlightHtmlClass.EscapedCharacter };
    yield return new object?[] { "hl ppc", true, HighlightHtmlClass.Preprocessor };
    yield return new object?[] { "hl pps", true, HighlightHtmlClass.PreprocessorString };
    yield return new object?[] { "hl lin", true, HighlightHtmlClass.LineNumber };
    yield return new object?[] { "hl opt", true, HighlightHtmlClass.Operator };
    yield return new object?[] { "hl ipl", true, HighlightHtmlClass.StringInterpolation };
    yield return new object?[] { "hl kwa", true, HighlightHtmlClass.KeywordA };
    yield return new object?[] { "hl kwb", true, HighlightHtmlClass.KeywordB };
    yield return new object?[] { "hl kwc", true, HighlightHtmlClass.KeywordC };
    yield return new object?[] { "hl kwd", true, HighlightHtmlClass.KeywordD };
    yield return new object?[] { "hl kwe", true, HighlightHtmlClass.KeywordE };
    yield return new object?[] { "hl kwf", true, HighlightHtmlClass.KeywordF };

    var isVersion4x = 4 <= VersionInformations.NativeLibraryVersion.Major;

    yield return new object?[] { "hl def", isVersion4x, isVersion4x ? HighlightHtmlClass.DefaultV4 : null };
    yield return new object?[] { "hl std", !isVersion4x, isVersion4x ? null : HighlightHtmlClass.DefaultV3 };

    yield return new object?[] { "hl sng", isVersion4x, isVersion4x ? HighlightHtmlClass.StringsV4 : null };
    yield return new object?[] { "hl str", !isVersion4x, isVersion4x ? null : HighlightHtmlClass.StringsV3 };

    yield return new object?[] { "hl hvr", isVersion4x, isVersion4x ? HighlightHtmlClass.HoverText : null };
    yield return new object?[] { "hl err", isVersion4x, isVersion4x ? HighlightHtmlClass.SyntaxError : null };
    yield return new object?[] { "hl erm", isVersion4x, isVersion4x ? HighlightHtmlClass.ErrorMessage : null };
  }

  [TestCaseSource(nameof(YieldTestCases_TryParsePrefixed))]
  public void TryParsePrefixed(string className, bool expectedResult, HighlightHtmlClass expectedParseResult)
  {
    Assert.That(HighlightHtmlClass.TryParsePrefixed(className, out var @class), Is.EqualTo(expectedResult));
    Assert.That(@class, Is.EqualTo(expectedParseResult));

    if (expectedResult)
      Assert.That(ReferenceEquals(expectedParseResult, @class), Is.True);
  }

  [Test]
  public void Equals_OfObject()
  {
    Assert.That(HighlightHtmlClass.Highlight.Equals((object?)null), Is.False);
    Assert.That(HighlightHtmlClass.Highlight!.Equals((object?)1), Is.False);
    Assert.That(HighlightHtmlClass.Highlight.Equals((object?)"HL"), Is.False, "case sensitive");
    Assert.That(HighlightHtmlClass.Highlight.Equals((object?)"hl"), Is.True);
    Assert.That(HighlightHtmlClass.Highlight.Equals((object?)HighlightHtmlClass.Highlight), Is.True);
  }

  [Test]
  public void Equals_OfHighlightHtmlClass()
  {
    Assert.That(HighlightHtmlClass.Highlight.Equals((HighlightHtmlClass?)null), Is.False);
    Assert.That(HighlightHtmlClass.Highlight!.Equals(HighlightHtmlClass.Default), Is.False);
    Assert.That(HighlightHtmlClass.Highlight.Equals(HighlightHtmlClass.Highlight), Is.True);
  }

  [Test]
  public void Equals_OfString()
  {
    Assert.That(HighlightHtmlClass.Highlight.Equals((string?)null), Is.False);
    Assert.That(HighlightHtmlClass.Highlight!.Equals("HL"), Is.False, "case sensitive");
    Assert.That(HighlightHtmlClass.Highlight.Equals("hl"), Is.True);
  }
}
