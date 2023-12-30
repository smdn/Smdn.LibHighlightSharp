using NUnit.Framework;

namespace Smdn.LibHighlightSharp.Bindings;

[TestFixture]
public class CodeGeneratorTests {
  [Test]
  public void getInstance()
  {
    CodeGenerator? gen = null;

    Assert.DoesNotThrow(() => gen = CodeGenerator.getInstance(OutputType.HTML));
    Assert.That(gen, Is.Not.Null);
    Assert.DoesNotThrow(() => CodeGenerator.deleteInstance(gen!));
    Assert.DoesNotThrow(() => gen!.Dispose());
  }
}
