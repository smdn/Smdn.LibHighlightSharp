using Smdn.LibHighlightSharp;

if (VersionInformations.NativeLibraryVersion < Highlight.MinimumVersionSupportingGuessFileType) {
  Console.Error.WriteLine("GuessFileType is not supported with this version.");
  return;
}

using var hl = new Highlight();

// Attempts to read filetypes.conf from the default search path
if (!hl.TryLoadFileTypesConfig()) {
  // Reads filetypes.conf from the specified path
  hl.LoadFileTypesConfig("/path/to/your/filetypes.conf");
}

// Guesses the file type of test files
var testFilesDirectory = Path.Join(Path.GetDirectoryName(Environment.ProcessPath)!, "testfiles");

foreach (var file in Directory.EnumerateFiles(testFilesDirectory)) {
  var fileType = hl.GuessFileType(file);

  hl.TryFindSyntaxFile(fileType, out var syntaxFilePath);

  Console.WriteLine("{0,-32}: {1,-10} ({2})", Path.GetFileName(file), fileType, syntaxFilePath);
}
