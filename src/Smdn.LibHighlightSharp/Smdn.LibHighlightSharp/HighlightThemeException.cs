// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
#pragma warning disable CA1032

using System;

namespace Smdn.LibHighlightSharp;

public class HighlightThemeException : InvalidOperationException {
  public string? ThemeFilePath { get; }
  public string? Reason { get; }

  public HighlightThemeException(string message)
    : base(message)
  {
    ThemeFilePath = default;
    Reason = default;
  }

  public HighlightThemeException(
    string themeFilePath,
    string? reason
  )
    : base($"Failed to initialize theme ({nameof(ThemeFilePath)}: '{themeFilePath}', {nameof(Reason)}: '{reason}')")
  {
    ThemeFilePath = themeFilePath;
    Reason = reason;
  }
}
