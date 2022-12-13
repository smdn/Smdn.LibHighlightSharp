// With Highlight version 4.x series, if the CodeGenerator::initTheme() is not called before
// calling CodeGenerator::loadLanguage(), an unmanaged exception described below will be thrown
// on calling CodeGenerator::generate*() and crash.
//
// terminate called after throwing an instance of 'std::out_of_range'
//    what():  vector::_M_range_check: __n (which is 13) >= this->size() (which is 13)
//

using System;
using System.IO;
using Smdn.LibHighlightSharp.Bindings;

var pathToDataDir = Path.Combine(
  Path.GetDirectoryName(Environment.ProcessPath)!,
  "highlight"
);

var pathToLangDefFile = Path.Join(pathToDataDir, "langDefs", "csharp.lang");
var pathToThemeFile = Path.Join(pathToDataDir, "themes", "github.theme");

using var generator = CodeGenerator.getInstance(OutputType.HTML);

// Call CodeGenerator::loadLanguage before calling CodeGenerator::initTheme
generator.loadLanguage(pathToLangDefFile);

generator.initTheme(pathToThemeFile);

// This will throw std::out_of_range
Console.WriteLine(generator.generateString("using System;"));
