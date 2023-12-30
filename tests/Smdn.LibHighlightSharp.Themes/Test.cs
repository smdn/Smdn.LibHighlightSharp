using System.IO;

using NUnit.Framework;

[TestFixture]
public class Test {
  [Test]
  public void ThemeFilesMustBeDeployedToOutputPath()
  {
    var themesDirectory = Path.Combine(TestContext.CurrentContext.TestDirectory, "highlight", "themes");

    FileAssert.Exists(Path.Combine(themesDirectory, "edit-vim.theme"));

    TestContext.WriteLine(
      "Total {0} theme files are deployed",
      Directory.GetFiles(themesDirectory, "*.theme", SearchOption.TopDirectoryOnly).Length
    );
  }
}
