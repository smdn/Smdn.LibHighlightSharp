// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
using NUnit.Framework;

namespace Smdn.LibHighlightSharp;

partial class HighlightTests {
  private static void EmptyMethodAcceptsNullableString(string? s) { }

  [Test]
  public void PrintLineNumbers()
  {
    using var hl = new Highlight();

    Assert.DoesNotThrow(() => {
      hl.PrintLineNumbers = true;
      hl.PrintLineNumbers = false;

      Assert.IsNotNull(hl.PrintLineNumbers);
    });
  }

  [Test]
  public void LineNumberZeroPadding()
  {
    using var hl = new Highlight();

    Assert.DoesNotThrow(() => {
      hl.LineNumberZeroPadding = true;
      hl.LineNumberZeroPadding = false;

      Assert.IsNotNull(hl.LineNumberZeroPadding);
    });
  }

  [Test]
  public void Fragment()
  {
    using var hl = new Highlight();

    Assert.DoesNotThrow(() => {
      hl.Fragment = true;
      hl.Fragment = false;

      Assert.IsNotNull(hl.Fragment);
    });
  }

  [Test]
  public void LineNumberWidth()
  {
    using var hl = new Highlight();

    Assert.DoesNotThrow(() => {
      hl.LineNumberWidth = 1;
      hl.LineNumberWidth = 2;

      Assert.IsNotNull(hl.LineNumberWidth);
    });
  }

  [Test]
  public void OmitVersionComment()
  {
    using var hl = new Highlight();

    Assert.DoesNotThrow(
      () => {
        hl.OmitVersionComment = true;
        hl.OmitVersionComment = false;
      },
      $"set_{nameof(Highlight.OmitVersionComment)}"
    );
    Assert.DoesNotThrow(
      () => Assert.IsNotNull(hl.OmitVersionComment),
      $"get_{nameof(Highlight.OmitVersionComment)}"
    );
  }


  [Test]
  public void IsolateTags()
  {
    using var hl = new Highlight();

    Assert.DoesNotThrow(
      () => {
        hl.IsolateTags = true;
        hl.IsolateTags = false;
      },
      $"set_{nameof(Highlight.IsolateTags)}"
    );
    Assert.DoesNotThrow(
      () => Assert.IsNotNull(hl.IsolateTags),
      $"get_{nameof(Highlight.IsolateTags)}"
    );
  }

  [Test]
  public void LastSyntaxError()
  {
    using var hl = new Highlight();

    Assert.DoesNotThrow(
      () => EmptyMethodAcceptsNullableString(hl.LastSyntaxError),
      $"get_{nameof(Highlight.LastSyntaxError)}"
    );
  }

  [Test]
  public void SyntaxDescription()
  {
    using var hl = new Highlight();

    hl.SetSyntax("csharp");

    Assert.DoesNotThrow(
      () => EmptyMethodAcceptsNullableString(hl.SyntaxDescription),
      $"get_{nameof(Highlight.SyntaxDescription)}"
    );
  }

  [Test]
  public void SyntaxEncodingHint()
  {
    using var hl = new Highlight();

    hl.SetSyntax("csharp");

    Assert.DoesNotThrow(
      () => EmptyMethodAcceptsNullableString(hl.SyntaxEncodingHint),
      $"get_{nameof(Highlight.SyntaxEncodingHint)}"
    );
  }

  [Test]
  public void ThemeDescription()
  {
    using var hl = new Highlight();

    Assert.DoesNotThrow(
      () => EmptyMethodAcceptsNullableString(hl.ThemeDescription),
      $"get_{nameof(Highlight.ThemeDescription)}"
    );
  }

  [Test]
  public void SyntaxCategoryDescription()
  {
    using var hl = new Highlight();

    hl.SetSyntax("csharp");

    Assert.DoesNotThrow(
      () => EmptyMethodAcceptsNullableString(hl.SyntaxCategoryDescription),
      $"get_{nameof(Highlight.SyntaxCategoryDescription)}"
    );
  }

  [Test]
  public void ThemeCategoryDescription()
  {
    using var hl = new Highlight();

    Assert.DoesNotThrow(
      () => EmptyMethodAcceptsNullableString(hl.ThemeCategoryDescription),
      $"get_{nameof(Highlight.ThemeCategoryDescription)}"
    );
  }
}
