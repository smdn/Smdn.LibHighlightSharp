// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: MIT
using NUnit.Framework;

namespace Smdn.LibHighlightSharp.Bindings;

[TestFixture]
public class CodeGeneratorTests {
  [Test]
#pragma warning disable IDE1006
  public void getInstance()
#pragma warning restore IDE1006
  {
    CodeGenerator? gen = null;

    Assert.That(() => gen = CodeGenerator.getInstance(OutputType.HTML), Throws.Nothing);
    Assert.That(gen, Is.Not.Null);
    Assert.That(() => CodeGenerator.deleteInstance(gen!), Throws.Nothing);
    Assert.That(() => gen!.Dispose(), Throws.Nothing);
  }
}
