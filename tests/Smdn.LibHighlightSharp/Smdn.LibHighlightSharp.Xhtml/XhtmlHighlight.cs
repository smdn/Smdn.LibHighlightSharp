// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
using System;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;

namespace Smdn.LibHighlightSharp.Xhtml;

[TestFixture]
public partial class XhtmlHighlightTests {
  private static readonly XNamespace XmlnsXhtml = (XNamespace)"http://www.w3.org/1999/xhtml";

  private static XhtmlHighlight CreateInstance()
  {
    var hl = new XhtmlHighlight();

    hl.SetTheme("github");
    hl.SetSyntax("csharp");

    return hl;
  }

  [Test]
  public void Ctor()
  {
    using var xhl = new XhtmlHighlight();

    Assert.AreEqual(GeneratorOutputType.Xhtml, xhl.OutputType);
  }

  [Test]
  public void Generate()
  {
    using var xhl = CreateInstance();

    StringAssert.Contains(
      $"xmlns=\"{XmlnsXhtml.NamespaceName}\"",
      xhl.Generate("using System;"),
      "must generate XHTML document"
    );
  }

  [Test]
  public void GenerateXhtmlDocument([Values(true, false)] bool fragment)
  {
    using var xhl = CreateInstance();

    xhl.Fragment = fragment; // this value must be ignored

    var doc = xhl.GenerateXhtmlDocument("using System;");

    Assert.AreEqual(fragment, xhl.Fragment, $"{nameof(xhl.Fragment)} must be restored to initial state");

    Assert.IsNotNull(doc, nameof(doc));
    Assert.IsNotNull(doc.Root, nameof(doc.Root));
    Assert.AreEqual(XmlnsXhtml, doc.Root!.Name.Namespace, nameof(doc.Root.Name.Namespace));
  }

  [Test]
  public void GenerateXhtmlDocument_PreserveWhiteSpaces([Values(true, false)] bool preserveWhitespace)
  {
    using var xhl = CreateInstance();

    xhl.PreserveWhitespace = preserveWhitespace;

    var doc = xhl.GenerateXhtmlDocument("using System;");
    var pre = doc.Descendants(XmlnsXhtml + "pre").First();

    if (preserveWhitespace)
      StringAssert.EndsWith("\n</pre>", pre.ToString());
    else
      StringAssert.EndsWith("</span></pre>", pre.ToString());
  }

  [Test]
  public void GenerateXhtmlFragment([Values(true, false)] bool fragment)
  {
    using var xhl = CreateInstance();

    xhl.Fragment = fragment; // this value must be ignored

    var nodes = xhl.GenerateXhtmlFragment("using System;")?.ToArray();

    Assert.IsNotNull(nodes);
    Assert.IsNotEmpty(nodes!);

    Assert.IsTrue(nodes!.Any(static n => n is XElement e && e.Value.StartsWith("using", StringComparison.Ordinal)));
  }

  [Test]
  public void GenerateXhtmlFragment_PreserveWhiteSpaces([Values(true, false)] bool preserveWhitespace)
  {
    using var xhl = CreateInstance();

    xhl.PreserveWhitespace = preserveWhitespace;

    var pre = new XElement(
      XmlnsXhtml + "pre",
      xhl.GenerateXhtmlFragment("using System;")
    );

    if (preserveWhitespace)
      StringAssert.EndsWith("\n</pre>", pre.ToString());
    else
      StringAssert.EndsWith("</span></pre>", pre.ToString());
  }
}
