// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
#if NETCOREAPP3_0_OR_GREATER || NET5_0_OR_GREATER
#define SYSTEM_IO_PATH_ENDSINDIRECTORYSEPARATOR // System.IO.Path.EndsInDirectorySeparator
#endif

using System;
using System.IO;
using System.Reflection;

namespace Smdn.LibHighlightSharp;

public partial class Highlight : IDisposable {
  private const GeneratorOutputType DefaultGeneratorOutputType = GeneratorOutputType.Html;

  private Bindings.CodeGenerator? codeGenerator;
  private Bindings.CodeGenerator CodeGenerator => codeGenerator ?? throw new ObjectDisposedException(GetType().FullName);

  private Bindings.DataDir? dataDirForSyntaxes;
  private Bindings.DataDir DataDirForSyntaxes => dataDirForSyntaxes ?? throw new ObjectDisposedException(GetType().FullName);

  private Bindings.DataDir? dataDirForThemes;
  private Bindings.DataDir DataDirForThemes => dataDirForThemes ?? throw new ObjectDisposedException(GetType().FullName);

  private Bindings.DataDir DataDir => dataDirForThemes ?? dataDirForSyntaxes ?? throw new ObjectDisposedException(GetType().FullName);

  public GeneratorOutputType OutputType { get; }

  private bool initThemeCalled;
  private bool loadLanguageBeforeInitTheme;
  private readonly bool shouldDisposeDataDir;

  private static string GetDefaultDataDirPath()
  {
    // Smdn.LibHighlightSharp.LangDefs and Smdn.LibHighlightSharp.Themes deploy assets to the directory '$(OutputPath)$(Highlight_DataDeploymentBasePath)'.
    // So set this directory as the default DataDir.
    var assmLocation = Assembly.GetEntryAssembly()?.Location ?? Assembly.GetExecutingAssembly()?.Location;

    if (assmLocation is null)
      return string.Empty;

    var assmDirectory = Path.GetDirectoryName(assmLocation);

    if (assmDirectory is null)
      return string.Empty;

    var defaultDataDirPath = Path.Combine(
      assmDirectory,
      Bindings.HighlightConfigurations.DataDirRelativePath
    );

    return defaultDataDirPath;
  }

  public static Bindings.DataDir? CreateDefaultDataDir()
  {
    var path = GetDefaultDataDirPath();

    return path.Length == 0
      ? null
      : CreateDataDirFromPath(path, nameof(path));
  }

  private static Bindings.DataDir CreateDataDirFromPath(string path, string paramName)
  {
    if (path is null)
      throw new ArgumentNullException(paramName);

    if (0 < path.Length) {
      var pathEndsInDirectorySeparator =
  #if SYSTEM_IO_PATH_ENDSINDIRECTORYSEPARATOR
        Path.EndsInDirectorySeparator(path);
  #elif SYSTEM_STRING_ENDSWITH_CHAR
        path.EndsWith(Path.DirectorySeparatorChar) || path.EndsWith(Path.AltDirectorySeparatorChar);
  #else
        path[path.Length - 1] == Path.DirectorySeparatorChar || path[path.Length - 1] == Path.AltDirectorySeparatorChar;
  #endif

      if (!pathEndsInDirectorySeparator)
        path += Path.DirectorySeparatorChar;
    }

    var dataDir = new Bindings.DataDir();

    dataDir.initSearchDirectories(path); // here, userDefinedDir can be empty

    return dataDir;
  }

  public Highlight(GeneratorOutputType outputType = DefaultGeneratorOutputType)
    : this(GetDefaultDataDirPath(), outputType)
  {
  }

  public Highlight(string dataDir, GeneratorOutputType outputType = DefaultGeneratorOutputType)
    : this(CreateDataDirFromPath(dataDir, nameof(dataDir)), outputType)
  {
  }

  public Highlight(
    Bindings.DataDir dataDir,
    GeneratorOutputType outputType = DefaultGeneratorOutputType,
    bool shouldDisposeDataDir = false
  )
    : this(
      dataDirForSyntaxes: dataDir ?? throw new ArgumentNullException(nameof(dataDir)),
      dataDirForThemes: dataDir,
      outputType,
      shouldDisposeDataDir: shouldDisposeDataDir
    )
  {
  }

  public Highlight(
    string dataDirForSyntaxes,
    string dataDirForThemes,
    GeneratorOutputType outputType = DefaultGeneratorOutputType
  )
    : this(
      CreateDataDirFromPath(dataDirForSyntaxes, nameof(dataDirForSyntaxes)),
      CreateDataDirFromPath(dataDirForThemes, nameof(dataDirForThemes)),
      outputType,
      shouldDisposeDataDir: true
    )
  {
  }

  public Highlight(
    Bindings.DataDir dataDirForSyntaxes,
    Bindings.DataDir dataDirForThemes,
    GeneratorOutputType outputType = DefaultGeneratorOutputType,
    bool shouldDisposeDataDir = false
  )
  {
    codeGenerator = Bindings.CodeGenerator.getInstance(TranslateOutputType(outputType));
    this.dataDirForSyntaxes = dataDirForSyntaxes ?? throw new ArgumentNullException(nameof(dataDirForSyntaxes));
    this.dataDirForThemes = dataDirForThemes ?? throw new ArgumentNullException(nameof(dataDirForThemes));
    OutputType = outputType;
    this.shouldDisposeDataDir = shouldDisposeDataDir;
  }

  /// <summary>create a new instance with DataDir inherited.</summary>
  private Highlight Create(GeneratorOutputType outputType)
    => new(
      dataDirForSyntaxes: DataDirForSyntaxes,
      dataDirForThemes: DataDirForThemes,
      outputType: outputType,
      shouldDisposeDataDir: false // do not dispose data dirs since this method creates a new instance with data dir of this instance.
    );

  public void Dispose()
  {
    Dispose(disposing: true);
    GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing)
  {
    if (codeGenerator is not null) {
      Bindings.CodeGenerator.deleteInstance(codeGenerator);
      codeGenerator.Dispose();
      codeGenerator = null;
    }

    if (shouldDisposeDataDir) {
      dataDirForSyntaxes?.Dispose();
      dataDirForSyntaxes = null;

      dataDirForThemes?.Dispose();
      dataDirForThemes = null;
    }
  }

  private void ThrowIfDisposed()
  {
    if (codeGenerator is null)
      throw new ObjectDisposedException(GetType().FullName);
  }

  private void ThrowIfInvalidCallingOrderBeforeGenerate()
  {
    // With Highlight version 4.0 series, if the CodeGenerator::initTheme() is not called before calling CodeGenerator::loadLanguage(),
    // an unmanaged exception will be thrown on calling CodeGenerator::generate*() and crash.
    // To forbid such call order, we throw a managed exception here.
    //
    // The exception to be thrown by Highlight 4.x:
    //    terminate called after throwing an instance of 'std::out_of_range'
    //      what():  vector::_M_range_check: __n (which is 13) >= this->size() (which is 13)
    if (4 <= VersionInformations.NativeLibraryVersion.Major && loadLanguageBeforeInitTheme)
      throw new InvalidOperationException("Invalid calling order. Set the theme first and set the syntax next to it to avoid being thrown an unmanaged exception.");
  }
}
