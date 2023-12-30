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

    Assert.That(xhl.OutputType, Is.EqualTo(GeneratorOutputType.Xhtml));
  }

  [Test]
  public void Generate()
  {
    using var xhl = CreateInstance();

    Assert.That(
      xhl.Generate("using System;"),
      Does.Contain($"xmlns=\"{XmlnsXhtml.NamespaceName}\""),
      "must generate XHTML document"
    );
  }

  [Test]
  public void GenerateXhtmlDocument([Values(true, false)] bool fragment)
  {
    using var xhl = CreateInstance();

    xhl.Fragment = fragment; // this value must be ignored

    var doc = xhl.GenerateXhtmlDocument("using System;");

    Assert.That(xhl.Fragment, Is.EqualTo(fragment), $"{nameof(xhl.Fragment)} must be restored to initial state");

    Assert.That(doc, Is.Not.Null, nameof(doc));
    Assert.That(doc.Root, Is.Not.Null, nameof(doc.Root));
    Assert.That(doc.Root!.Name.Namespace, Is.EqualTo(XmlnsXhtml), nameof(doc.Root.Name.Namespace));
  }

  [Test]
  public void GenerateXhtmlDocument_PreserveWhiteSpaces([Values(true, false)] bool preserveWhitespace)
  {
    using var xhl = CreateInstance();

    xhl.PreserveWhitespace = preserveWhitespace;

    var doc = xhl.GenerateXhtmlDocument("using System;");
    var pre = doc.Descendants(XmlnsXhtml + "pre").First();

    if (preserveWhitespace)
      Assert.That(pre.ToString(), Does.EndWith("\n</pre>"));
    else
      Assert.That(pre.ToString(), Does.EndWith("</span></pre>"));
  }

  [Test]
  public void GenerateXhtmlFragment([Values(true, false)] bool fragment)
  {
    using var xhl = CreateInstance();

    xhl.Fragment = fragment; // this value must be ignored

    var nodes = xhl.GenerateXhtmlFragment("using System;")?.ToArray();

    Assert.That(nodes, Is.Not.Null);
    Assert.That(nodes!, Is.Not.Empty);

    Assert.That(nodes!.Any(static n => n is XElement e && e.Value.StartsWith("using", StringComparison.Ordinal)), Is.True);
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
      Assert.That(pre.ToString(), Does.EndWith("\n</pre>"));
    else
      Assert.That(pre.ToString(), Does.EndWith("</span></pre>"));
  }
}
