// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using NUnit.Framework;

using Smdn.OperatingSystem;
using Smdn.LibHighlightSharp;

[TestFixture]
public class AssemblyMetadataTests {
  [Test]
  public void TestReferencedBindingsVersion()
  {
    var pathToAssemblySmdnLibHighlight = typeof(Highlight).Assembly.Location;
    var pathToAssemblySmdnLibHighlightBindings = Path.Combine(
      Path.GetDirectoryName(pathToAssemblySmdnLibHighlight)!,
      "Smdn.LibHighlightSharp.Bindings.dll"
    );
    var pathToTestRootDirectory = Path.Combine(
      TestContext.CurrentContext.TestDirectory,
      "..",
      "..",
      "..",
      ".."
    );
    var pathToToolProject = Path.GetFullPath(
      Path.Combine(
        pathToTestRootDirectory,
        "tools",
        "ListReferencedAssembly",
        "ListReferencedAssembly.csproj"
      )
    );

    var commandLine = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
      ? $"dotnet run --project \"{pathToToolProject}\" -- \"{pathToAssemblySmdnLibHighlight}\" \"{pathToAssemblySmdnLibHighlightBindings}\""
      : $"dotnet run --project '{pathToToolProject}' -- '{pathToAssemblySmdnLibHighlight}' '{pathToAssemblySmdnLibHighlightBindings}'";

    if (0 != Shell.Execute(commandLine, out var stdout, out var stderr)) {
      TestContext.WriteLine("[command line]");
      TestContext.WriteLine(commandLine);
      TestContext.WriteLine("[stdout]");
      TestContext.WriteLine(stdout);
      TestContext.WriteLine("[stderr]");
      TestContext.WriteLine(stderr);

      Assert.Fail("failed to run tool");
    }

    var result = JsonDocument.Parse(stdout);

    /*
     * Smdn.LibHighlightSharp.Bindings.dll
     */
    var resultSmdnLibHighlightSharpBindings = result
      .RootElement
      .EnumerateArray()
      .FirstOrDefault(static entry => entry.GetProperty("name").GetString()?.StartsWith("Smdn.LibHighlightSharp.Bindings,", StringComparison.Ordinal) ?? false);

    Assert.AreEqual(
      JsonValueKind.Object,
      resultSmdnLibHighlightSharpBindings.ValueKind,
      nameof(resultSmdnLibHighlightSharpBindings)
    );

    var mameSmdnLibHighlightSharpBindings = resultSmdnLibHighlightSharpBindings
      .GetProperty("name")
      .GetString();

    StringAssert.StartsWith(
      $"Smdn.LibHighlightSharp.Bindings, Version={new Version(TestConstants.ExpectedBindingsVersionString)}",
      mameSmdnLibHighlightSharpBindings,
      "must be Smdn.LibHighlightSharp.Bindings, Version=<specific-version>"
    );

    /*
     * Smdn.LibHighlightSharp.dll
     */
    var resultSmdnLibHighlightSharp = result
      .RootElement
      .EnumerateArray()
      .FirstOrDefault(static entry => entry.GetProperty("name").GetString()?.StartsWith("Smdn.LibHighlightSharp,", StringComparison.Ordinal) ?? false);

    Assert.AreEqual(
      JsonValueKind.Object,
      resultSmdnLibHighlightSharp.ValueKind,
      nameof(resultSmdnLibHighlightSharp)
    );

    var referencedAssemblyNameSmdnLibHighlightSharpBindings = resultSmdnLibHighlightSharp
      .GetProperty("referencedAssemblies")
      .EnumerateArray()
      .FirstOrDefault(static element => element.GetString()?.StartsWith("Smdn.LibHighlightSharp.Bindings,", StringComparison.Ordinal) ?? false);

    Assert.AreEqual(
      JsonValueKind.String,
      referencedAssemblyNameSmdnLibHighlightSharpBindings.ValueKind,
      nameof(referencedAssemblyNameSmdnLibHighlightSharpBindings)
    );

    StringAssert.StartsWith(
      $"Smdn.LibHighlightSharp.Bindings, Version={new Version(TestConstants.ExpectedBindingsReferenceVersionString)}",
      referencedAssemblyNameSmdnLibHighlightSharpBindings.GetString(),
      "must be Smdn.LibHighlightSharp.Bindings, Version=<minimum>"
    );
  }
}
