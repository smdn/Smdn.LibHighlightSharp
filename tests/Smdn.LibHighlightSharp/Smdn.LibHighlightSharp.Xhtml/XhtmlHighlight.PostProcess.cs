// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;

namespace Smdn.LibHighlightSharp.Xhtml;

partial class XhtmlHighlightTests {
  private class EnumerateHighlightedElementsXhtmlHighlight : XhtmlHighlight {
    public static new IEnumerable<(
      XElement HighlightedElement,
      HighlightHtmlClass HighlightClass
    )> EnumerateHighlightedElements(XContainer container)
      => XhtmlHighlight.EnumerateHighlightedElements(container);
  }

  [Test]
  public void EnumerateHighlightedElements()
  {
    var elements = EnumerateHighlightedElementsXhtmlHighlight.EnumerateHighlightedElements(
      new XElement(
        XmlnsXhtml + "pre",
        new XElement(XmlnsXhtml + "p", new XAttribute("class", "hl kwa"), "non-<span> elements must not be enumerated"),
        new XElement(XmlnsXhtml + "span", "<span> elements without class must not be enumerated"),
        new XElement(XmlnsXhtml + "span", new XAttribute("class", "hl"), "<span> elements only with 'hl' class must not be enumerated"),
        new XElement(XmlnsXhtml + "span", new XAttribute("class", "hl unknown"), "highlighted elements with unknown class must not be enumerated"),
        new XElement(XmlnsXhtml + "span", new XAttribute("class", "hl kwa kwb"), "highlighted elements with multiple class must not be enumerated"),
        new XElement(XmlnsXhtml + "span", new XAttribute("class", "hl kwa"), nameof(HighlightHtmlClass.KeywordA)),
        new XElement(XmlnsXhtml + "span", new XAttribute("class", "hl num"), nameof(HighlightHtmlClass.Number)),
        "container element must not be enumerated"
      )
    ).ToList();

    Assert.That(elements.Count, Is.EqualTo(2), nameof(elements.Count));

    Assert.That(elements[0].HighlightedElement.Value, Is.EqualTo(nameof(HighlightHtmlClass.KeywordA)), "#0 HighlightedElement");
    Assert.That(elements[0].HighlightClass.ToString(), Is.EqualTo(HighlightHtmlClass.KeywordA.ToString()), "#0 HighlightClass");

    Assert.That(elements[1].HighlightedElement.Value, Is.EqualTo(nameof(HighlightHtmlClass.Number)), "#1 HighlightedElement");
    Assert.That(elements[1].HighlightClass.ToString(), Is.EqualTo(HighlightHtmlClass.Number.ToString()), "#1 HighlightClass");
  }

  private class ReverseGenerateInputXhtmlHighlight : XhtmlHighlight {
    public string? ReverseGeneratedInput { get; set; } = null;

    public ReverseGenerateInputXhtmlHighlight()
     : base()
    {
    }

    protected override void PostProcessXhtml(XContainer container)
    {
      ReverseGeneratedInput = string.Concat(
        container.DescendantNodes().OfType<XText>().Select(static text => text.Value)
      );

      base.PostProcessXhtml(container);
    }
  }

  [Test]
  public void GenerateXhtmlDocument_PostProcessXhtml()
  {
    using var xhl = new ReverseGenerateInputXhtmlHighlight();

    xhl.SetTheme("github");
    xhl.SetSyntax("csharp");

    const string input = "using System;";

    Assert.That(xhl.ReverseGeneratedInput, Is.Null, "PostProcessXhtml must not be called yet at this time.");

    xhl.GenerateXhtmlDocument(input);

    Assert.That(xhl.ReverseGeneratedInput, Is.Not.Null, "PostProcessXhtml must be called at this time.");
    Assert.That(xhl.ReverseGeneratedInput, Does.Contain(input));
  }

  [Test]
  public void GenerateXhtmlFragment_PostProcessXhtml()
  {
    using var xhl = new ReverseGenerateInputXhtmlHighlight();

    xhl.SetTheme("github");
    xhl.SetSyntax("csharp");

    const string input = "using System;";

    Assert.That(xhl.ReverseGeneratedInput, Is.Null, "PostProcessXhtml must not be called yet at this time.");

    xhl.GenerateXhtmlFragment(input);

    Assert.That(xhl.ReverseGeneratedInput, Is.Not.Null, "PostProcessXhtml must be called at this time.");
    Assert.That(xhl.ReverseGeneratedInput, Does.Contain(input));
  }
}
