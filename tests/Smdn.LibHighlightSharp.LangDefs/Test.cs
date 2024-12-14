// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: MIT
using System.IO;

using NUnit.Framework;
using NUnit.Framework.Legacy;

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

    TestContext.Out.WriteLine(
      "Total {0} syntax files are deployed",
      Directory.GetFiles(langDefsDirectory, "*.lang", SearchOption.TopDirectoryOnly).Length
    );
  }
}
