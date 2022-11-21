// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
using System.Collections.Generic;
using System.Xml.Linq;

namespace Smdn.LibHighlightSharp.Xhtml;

#pragma warning disable IDE0040
partial class XhtmlHighlight {
#pragma warning restore IDE0040
  protected static IEnumerable<(
    XElement HighlightedElement,
    HighlightHtmlClass HighlightClass
  )> EnumerateHighlightedElements(XContainer container)
  {
    foreach (var attrClass in container.Descendants(ElementNameXhtmlSpan).Attributes(AttributeNameXhtmlClass)) {
      if (attrClass.Parent is null)
        continue;

      if (HighlightHtmlClass.TryParsePrefixed(attrClass.Value, out var highlightClass)) {
#if NULL_STATE_STATIC_ANALYSIS_ATTRIBUTES
        yield return (attrClass.Parent, highlightClass);
#else
        yield return (attrClass.Parent, highlightClass!);
#endif
      }
    }
  }

  protected virtual void PostProcessXhtml(XContainer container)
  {
    // nothing to do in this class
  }
}
