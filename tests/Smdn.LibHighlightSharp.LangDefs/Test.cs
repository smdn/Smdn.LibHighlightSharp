using System;
using System.IO;
using NUnit.Framework;

[TestFixture]
public class Test {
  [Test]
  public void FileTypesConfFileMustBeDeployedToOutputPath()
    => FileAssert.Exists(Path.Combine(TestContext.CurrentContext.TestDirectory, "highlight", "filetypes.conf"));

  [Test]
  public void LangDefFilesMustBeDeployedToOutputPath()
  {
    var langDefsDirectory = Path.Combine(TestContext.CurrentContext.TestDirectory, "highlight", "langDefs");

    FileAssert.Exists(Path.Combine(langDefsDirectory, "c.lang"));

    TestContext.WriteLine(
      "Total {0} syntax files are deployed",
      Directory.GetFiles(langDefsDirectory, "*.lang", SearchOption.TopDirectoryOnly).Length
    );
  }
}
