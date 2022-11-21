using Smdn.LibHighlightSharp;

Console.WriteLine($"{nameof(VersionInformations.NativeLibraryVersion)}: {VersionInformations.NativeLibraryVersion}");
Console.WriteLine($"{nameof(VersionInformations.NativeLibraryFileName)}: {VersionInformations.NativeLibraryFileName}");
Console.WriteLine($"{nameof(VersionInformations.BindingsVersion)}: {VersionInformations.BindingsVersion}");

using var hl = new Highlight();

Console.WriteLine($"{nameof(hl.GeneratorVersionString)}: {hl.GeneratorVersionString}");
