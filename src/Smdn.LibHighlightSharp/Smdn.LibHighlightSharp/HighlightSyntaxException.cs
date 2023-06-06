// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
#pragma warning disable CA1032

using System;

namespace Smdn.LibHighlightSharp;

public class HighlightSyntaxException : InvalidOperationException {
  internal static void ThrowIfError(string pathToLangFile, Bindings.LoadResult reason)
  {
    if (reason == Bindings.LoadResult.LOAD_OK)
      return;

    throw new HighlightSyntaxException(pathToLangFile, reason);
  }

  public string? LangFilePath { get; }
  public Bindings.LoadResult Reason { get; }

  public HighlightSyntaxException(string message)
    : base(message)
  {
    LangFilePath = default;
    Reason = default;
  }

  public HighlightSyntaxException(
    string langFilePath,
    Bindings.LoadResult reason
  )
    : base($"Highlight language definition error ({nameof(LangFilePath)}: '{langFilePath}', {nameof(Reason)}: {reason})")
  {
    LangFilePath = langFilePath;
    Reason = reason;
  }
}
