// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
using System;

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

      Assert.That(hl.PrintLineNumbers, Is.False);
    });
  }

  [Test]
  public void LineNumberZeroPadding()
  {
    using var hl = new Highlight();

    Assert.DoesNotThrow(() => {
      hl.LineNumberZeroPadding = true;
      hl.LineNumberZeroPadding = false;

      Assert.That(hl.LineNumberZeroPadding, Is.False);
    });
  }

  [Test]
  public void Fragment()
  {
    using var hl = new Highlight();

    Assert.DoesNotThrow(() => {
      hl.Fragment = true;
      hl.Fragment = false;

      Assert.That(hl.Fragment, Is.False);
    });
  }

  [Test]
  public void LineNumberWidth()
  {
    using var hl = new Highlight();

    Assert.DoesNotThrow(() => {
      hl.LineNumberWidth = 1;
      hl.LineNumberWidth = 2;

      Assert.That(hl.LineNumberWidth, Is.EqualTo(2));
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
      () => Assert.That(hl.OmitVersionComment, Is.False),
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
      () => Assert.That(hl.IsolateTags, Is.False),
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

  [Test]
  public void AdditionalEndOfFileChar_Default()
  {
    using var hl = new Highlight();

    if (new Version(4, 7) <= VersionInformations.NativeLibraryVersion)
      Assert.That(hl.AdditionalEndOfFileChar, Is.Null, nameof(hl.AdditionalEndOfFileChar));
  }

  [TestCase('\x00')] // NULL
  [TestCase('\x04')] // CTRL-D / EOT (end of transmission)
  [TestCase('\x1A')] // CTRL-Z / SUB (substitute)
  [TestCase('\xFE')] // maximum
  public void AdditionalEndOfFileChar(char eof)
  {
    using var hl = new Highlight();

    Assert.DoesNotThrow(() => hl.AdditionalEndOfFileChar = eof);

    if (new Version(4, 6) <= VersionInformations.NativeLibraryVersion)
      Assert.That(hl.AdditionalEndOfFileChar, Is.EqualTo(eof), nameof(hl.AdditionalEndOfFileChar));
    else
      Assert.That(hl.AdditionalEndOfFileChar, Is.Null, nameof(hl.AdditionalEndOfFileChar));
  }

  [TestCase('\xFF')]
  [TestCase('\u0100')]
  [TestCase('\uFFFF')]
  public void AdditionalEndOfFileChar_OutOfRange(char eof)
  {
    using var hl = new Highlight();
    const char InitialEofChar = '\x00';

    Assert.DoesNotThrow(() => hl.AdditionalEndOfFileChar = InitialEofChar);

    Assert.Throws<ArgumentOutOfRangeException>(() => hl.AdditionalEndOfFileChar = eof);

    if (new Version(4, 6) <= VersionInformations.NativeLibraryVersion)
      Assert.That(hl.AdditionalEndOfFileChar, Is.EqualTo(InitialEofChar));
    else
      Assert.That(hl.AdditionalEndOfFileChar, Is.Null, nameof(hl.AdditionalEndOfFileChar));
  }

  [Test]
  public void AdditionalEndOfFileChar_SetNull()
  {
    using var hl = new Highlight();
    const char InitialEofChar = '\x00';

    Assert.DoesNotThrow(() => hl.AdditionalEndOfFileChar = InitialEofChar);

    if (new Version(4, 6) <= VersionInformations.NativeLibraryVersion)
      Assert.That(hl.AdditionalEndOfFileChar, Is.EqualTo(InitialEofChar));
    else
      Assert.That(hl.AdditionalEndOfFileChar, Is.Null, nameof(hl.AdditionalEndOfFileChar));

    Assert.DoesNotThrow(() => hl.AdditionalEndOfFileChar = null);

    Assert.That(hl.AdditionalEndOfFileChar, Is.Null, nameof(hl.AdditionalEndOfFileChar));
  }
}
