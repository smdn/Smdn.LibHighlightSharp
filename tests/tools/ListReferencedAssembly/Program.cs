// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: MIT
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Reflection;

class Program {
  public static void Main(string[] args)
  {
    Console.WriteLine("[");

    var first = true;

    foreach (var assemblyPath in args) {
      if (first) {
        first = false;
      }
      else {
        Console.WriteLine(",");
      }

      ListReferencedAssembly(assemblyPath);
    }

    Console.WriteLine();

    Console.WriteLine("]");
  }

  static void ListReferencedAssembly(string assemblyPath)
  {
    var assemblyPaths = new List<string>(
      Directory.GetFiles(RuntimeEnvironment.GetRuntimeDirectory(), "*.dll")
    );

    assemblyPaths.Add(assemblyPath);

    var resolver = new PathAssemblyResolver(assemblyPaths);

    using var mlc = new MetadataLoadContext(resolver);

    var assm = mlc.LoadFromAssemblyPath(assemblyPath);

    Console.WriteLine($@"  {{");
    Console.WriteLine($@"    ""name"": ""{assm.GetName()}"",");
    Console.WriteLine($@"    ""path"": ""{assemblyPath.Replace("\\", "\\\\")}"",");
    Console.WriteLine($@"    ""referencedAssemblies"": [");

    var first = true;

    foreach (var referencedAssemblyName in assm.GetReferencedAssemblies()) {
      if (first) {
        first = false;
      }
      else {
        Console.WriteLine(",");
      }

      Console.Write($@"      ""{referencedAssemblyName}""");
    }

    Console.WriteLine();

    Console.WriteLine($@"    ]");
    Console.Write($@"  }}");
  }
}
