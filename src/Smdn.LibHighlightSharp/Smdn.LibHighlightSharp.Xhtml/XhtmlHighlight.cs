// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Smdn.LibHighlightSharp.Xhtml;

public partial class XhtmlHighlight : Highlight {
  private static readonly XNamespace XmlnsXhtml = (XNamespace)"http://www.w3.org/1999/xhtml";
  private static readonly XName ElementNameXhtmlSpan = XmlnsXhtml + "span";
  private static readonly XName AttributeNameXhtmlClass = "class";

  public bool PreserveWhitespace { get; set; } = true;

  public XhtmlHighlight()
    : base(GeneratorOutputType.Xhtml)
  {
  }

  public XhtmlHighlight(string dataDir)
    : base(
      dataDir: dataDir,
      outputType: GeneratorOutputType.Xhtml
    )
  {
  }

  public XhtmlHighlight(
    Bindings.DataDir dataDir,
    bool shouldDisposeDataDir = false
  )
    : base(
      dataDir,
      outputType: GeneratorOutputType.Xhtml,
      shouldDisposeDataDir: shouldDisposeDataDir
    )
  {
  }

  public XhtmlHighlight(
    string dataDirForSyntaxes,
    string dataDirForThemes
  )
    : base(
      dataDirForSyntaxes: dataDirForSyntaxes,
      dataDirForThemes: dataDirForThemes,
      outputType: GeneratorOutputType.Xhtml
    )
  {
  }

  public XhtmlHighlight(
    Bindings.DataDir dataDirForSyntaxes,
    Bindings.DataDir dataDirForThemes,
    bool shouldDisposeDataDir = false
  )
    : base(
      dataDirForSyntaxes: dataDirForSyntaxes,
      dataDirForThemes: dataDirForThemes,
      outputType: GeneratorOutputType.Xhtml,
      shouldDisposeDataDir: shouldDisposeDataDir
    )
  {
  }

  public IEnumerable<XNode> GenerateXhtmlFragment(
    string input
  )
    => GenerateXhtmlFragment(
      input ?? throw new ArgumentNullException(nameof(input)),
      fromFile: false
    );

  public IEnumerable<XNode> GenerateXhtmlFragmentFromFile(
    string path
  )
    => GenerateXhtmlFragment(
      path ?? throw new ArgumentNullException(nameof(path)),
      fromFile: true
    );

  private IEnumerable<XNode> GenerateXhtmlFragment(
    string inputOrPath,
    bool fromFile
  )
  {
    var encoding = Encoding.UTF8;
    var initialFragmentState = Fragment;

    try {
      Fragment = true;
      SetEncoding(encoding.WebName); // TODO: revert encoding after generating

      // Output size is estimated to be x3.0 of the input size here.
      var inputSize = fromFile ? new FileInfo(inputOrPath).Length : inputOrPath.Length;
      var estimatedOutputSize = Math.Max(16L, (long)(inputSize * 3.0));

      using var generatedFragmentStream = new MemoryStream(
        capacity: (int)Math.Min(int.MaxValue, estimatedOutputSize)
      );

      using (var generatedFragmentWriter = new StreamWriter(generatedFragmentStream, Encoding.UTF8, bufferSize: -1, leaveOpen: true)) {
        generatedFragmentWriter.WriteLine($@"<?xml version=""1.0"" encoding=""{encoding.WebName}""?>");
        generatedFragmentWriter.WriteLine($@"<body xml:space=""{(PreserveWhitespace ? "preserve" : "default")}"" xmlns=""{XmlnsXhtml.NamespaceName}"">");
        generatedFragmentWriter.WriteLine(
          fromFile
            ? GenerateFromFile(path: inputOrPath)
            : Generate(input: inputOrPath)
        );
        generatedFragmentWriter.WriteLine("</body>");
        generatedFragmentWriter.Close();
      }

      generatedFragmentStream.Position = 0L;

      var settings = new XmlReaderSettings() {
        ConformanceLevel = ConformanceLevel.Fragment,
        DtdProcessing = DtdProcessing.Ignore,
        IgnoreComments = false,
        IgnoreProcessingInstructions = false,
        IgnoreWhitespace = !PreserveWhitespace,
        ValidationType = ValidationType.None,
      };

      var root = XElement.Load(
        XmlReader.Create(generatedFragmentStream, settings),
        PreserveWhitespace
          ? LoadOptions.PreserveWhitespace
          : LoadOptions.None
      );

      PostProcessXhtml(root);

      return root.Nodes();
    }
    finally {
      Fragment = initialFragmentState;
    }
  }

  public XDocument GenerateXhtmlDocument(
    string input
  )
    => GenerateXhtmlDocument(
      input ?? throw new ArgumentNullException(nameof(input)),
      fromFile: false
    );

  public XDocument GenerateXhtmlDocumentFromFile(
    string path
  )
    => GenerateXhtmlDocument(
      path ?? throw new ArgumentNullException(nameof(path)),
      fromFile: true
    );

  private XDocument GenerateXhtmlDocument(
    string inputOrPath,
    bool fromFile
  )
  {
    var initialFragmentState = Fragment;

    try {
      Fragment = false;

      var doc = XDocument.Load(
        new StringReader(
          fromFile
            ? GenerateFromFile(path: inputOrPath)
            : Generate(input: inputOrPath)
        ),
        PreserveWhitespace
          ? LoadOptions.PreserveWhitespace
          : LoadOptions.None
      );

      if (doc.Root is not null)
        PostProcessXhtml(doc.Root);

      return doc;
    }
    finally {
      Fragment = initialFragmentState;
    }
  }
}
