// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
using System;
using System.Collections.Generic;
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
    var environmentVariables = new Dictionary<string, string>() {
      ["NO_COLOR"] = "NO_COLOR", // disable emitting ANSI color escape codes
    };

    if (0 != Shell.Execute(commandLine, arguments: null, environmentVariables, out var stdout, out var stderr)) {
      TestContext.Out.WriteLine("[command line]");
      TestContext.Out.WriteLine(commandLine);
      TestContext.Out.WriteLine("[stdout]");
      TestContext.Out.WriteLine(stdout);
      TestContext.Out.WriteLine("[stderr]");
      TestContext.Out.WriteLine(stderr);

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

    Assert.That(
      resultSmdnLibHighlightSharpBindings.ValueKind,
      Is.EqualTo(JsonValueKind.Object),
      nameof(resultSmdnLibHighlightSharpBindings)
    );

    var mameSmdnLibHighlightSharpBindings = resultSmdnLibHighlightSharpBindings
      .GetProperty("name")
      .GetString();

    Assert.That(
      mameSmdnLibHighlightSharpBindings,
      Does.StartWith($"Smdn.LibHighlightSharp.Bindings, Version={new Version(TestConstants.ExpectedBindingsVersionString)}"),
      "must be Smdn.LibHighlightSharp.Bindings, Version=<specific-version>"
    );

    /*
     * Smdn.LibHighlightSharp.dll
     */
    var resultSmdnLibHighlightSharp = result
      .RootElement
      .EnumerateArray()
      .FirstOrDefault(static entry => entry.GetProperty("name").GetString()?.StartsWith("Smdn.LibHighlightSharp,", StringComparison.Ordinal) ?? false);

    Assert.That(
      resultSmdnLibHighlightSharp.ValueKind,
      Is.EqualTo(JsonValueKind.Object),
      nameof(resultSmdnLibHighlightSharp)
    );

    var referencedAssemblyNameSmdnLibHighlightSharpBindings = resultSmdnLibHighlightSharp
      .GetProperty("referencedAssemblies")
      .EnumerateArray()
      .FirstOrDefault(static element => element.GetString()?.StartsWith("Smdn.LibHighlightSharp.Bindings,", StringComparison.Ordinal) ?? false);

    Assert.That(
      referencedAssemblyNameSmdnLibHighlightSharpBindings.ValueKind, Is.EqualTo(JsonValueKind.String),
      nameof(referencedAssemblyNameSmdnLibHighlightSharpBindings)
    );

    Assert.That(
      referencedAssemblyNameSmdnLibHighlightSharpBindings.GetString(),
      Does.StartWith($"Smdn.LibHighlightSharp.Bindings, Version={new Version(TestConstants.ExpectedBindingsReferenceVersionString)}"),
      "must be Smdn.LibHighlightSharp.Bindings, Version=<minimum>"
    );
  }
}
