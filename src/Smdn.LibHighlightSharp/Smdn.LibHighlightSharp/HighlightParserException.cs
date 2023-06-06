// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
#pragma warning disable CA1032

using System;

namespace Smdn.LibHighlightSharp;

public class HighlightParserException : InvalidOperationException {
  internal static void ThrowIfError(Bindings.ParseError reason)
  {
    if (reason == Bindings.ParseError.PARSE_OK)
      return;

    throw new HighlightParserException($"Highlight internal parser error ({reason})", reason);
  }

  public Bindings.ParseError Reason { get; }

  public HighlightParserException(string message)
    : base(message)
  {
    Reason = default;
  }

  public HighlightParserException(string message, Bindings.ParseError reason)
    : base(message)
  {
    Reason = reason;
  }
}
