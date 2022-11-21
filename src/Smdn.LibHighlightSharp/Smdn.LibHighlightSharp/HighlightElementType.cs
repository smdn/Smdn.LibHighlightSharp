// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
namespace Smdn.LibHighlightSharp;

public enum HighlightElementType {
  Other = 0, // not a highlighted element

  Default,
  Strings,
  Number,
  SingleLineComment,
  MultiLineComment,
  EscapedCharacter,
  Preprocessor,
  PreprocessorString,
  LineNumber,
  Operator,
  StringInterpolation,

  KeywordA,
  KeywordB,
  KeywordC,
  KeywordD,

  HoverText,
  SyntaxError,
  ErrorMessage,
}
