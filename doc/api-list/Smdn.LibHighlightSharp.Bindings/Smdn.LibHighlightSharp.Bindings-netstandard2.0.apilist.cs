// Smdn.LibHighlightSharp.Bindings.dll (Smdn.LibHighlightSharp.Bindings-3.51.0)
//   Name: Smdn.LibHighlightSharp.Bindings
//   AssemblyVersion: 3.51.0.0
//   InformationalVersion: 3.51.0+e16b6978304ea97cceb7b86f67e2a0e541516e53
//   TargetFramework: .NETStandard,Version=v2.0
//   Configuration: Release

using System;
using Smdn.LibHighlightSharp.Bindings;

namespace Smdn.LibHighlightSharp {
  public static class VersionInformations {
    public static Version BindingsVersion { get; }
    public static string NativeLibraryFileName { get; }
    public static string NativeLibraryName { get; }
    public static Version NativeLibraryVersion { get; }
  }
}

namespace Smdn.LibHighlightSharp.Bindings {
  public enum LoadResult : int {
    LOAD_FAILED = 1,
    LOAD_FAILED_LUA = 3,
    LOAD_FAILED_REGEX = 2,
    LOAD_OK = 0,
  }

  public enum OutputType : int {
    BBCODE = 9,
    ESC_ANSI = 5,
    ESC_TRUECOLOR = 12,
    ESC_XTERM256 = 6,
    HTML = 0,
    HTML32_UNUSED = 7,
    LATEX = 3,
    ODTFLAT = 11,
    PANGO = 10,
    RTF = 4,
    SVG = 8,
    TEX = 2,
    XHTML = 1,
  }

  public enum ParseError : int {
    BAD_BINARY = 8,
    BAD_INPUT = 1,
    BAD_OUTPUT = 2,
    BAD_STYLE = 4,
    PARSE_OK = 0,
  }

  public enum State : int {
    DIRECTIVE = 6,
    DIRECTIVE_END = 17,
    DIRECTIVE_STRING = 7,
    EMBEDDED_CODE_BEGIN = 23,
    EMBEDDED_CODE_END = 24,
    ESC_CHAR = 5,
    ESC_CHAR_END = 16,
    IDENTIFIER_BEGIN = 21,
    IDENTIFIER_END = 22,
    KEYWORD = 11,
    KEYWORD_END = 20,
    LINENUMBER = 8,
    ML_COMMENT = 4,
    ML_COMMENT_END = 15,
    NUMBER = 2,
    NUMBER_END = 13,
    SL_COMMENT = 3,
    SL_COMMENT_END = 14,
    STANDARD = 0,
    STRING = 1,
    STRING_END = 12,
    STRING_INTERPOLATION = 10,
    STRING_INTERPOLATION_END = 19,
    SYMBOL = 9,
    SYMBOL_END = 18,
    _EOF = 103,
    _EOL = 102,
    _REJECT = 101,
    _TESTPOS = 105,
    _UNKNOWN = 100,
    _WS = 104,
  }

  public enum WrapMode : int {
    WRAP_DEFAULT = 2,
    WRAP_DISABLED = 0,
    WRAP_SIMPLE = 1,
  }

  public class CodeGenerator : IDisposable {
    public static void deleteInstance(CodeGenerator inst) {}
    public static CodeGenerator getInstance(OutputType type) {}

    protected bool swigCMemOwn;

    protected virtual void Dispose(bool disposing) {}
    public void Dispose() {}
    ~CodeGenerator() {}
    public void disableTrailingNL(bool flag) {}
    public bool formattingDisabled() {}
    public bool formattingIsPossible() {}
    public ParseError generateFile(string inFileName, string outFileName) {}
    public string generateString(string input) {}
    public string generateStringFromFile(string inFileName) {}
    public string getBaseFont() {}
    public string getBaseFontSize() {}
    public bool getFragmentCode() {}
    public bool getIsolateTags() {}
    public bool getKeepInjections() {}
    public int getLineNumberWidth() {}
    public bool getNumberWrappedLines() {}
    public bool getOmitVersionComment() {}
    public string getPluginScriptError() {}
    public SWIGTYPE_p_std__vectorT_std__string_t getPosTestErrors() {}
    public bool getPrintLineNumbers() {}
    public bool getPrintZeroes() {}
    public string getStyleInputPath() {}
    public string getStyleName() {}
    public string getStyleOutputPath() {}
    public string getSyntaxCatDescription() {}
    public string getSyntaxDescription() {}
    public string getSyntaxLuaError() {}
    public SyntaxReader getSyntaxReader() {}
    public string getSyntaxRegexError() {}
    public string getThemeCatDescription() {}
    public string getThemeDescription() {}
    public string getThemeInitError() {}
    public string getTitle() {}
    public bool getValidateInput() {}
    public bool initIndentationScheme(string indentScheme) {}
    public bool initPluginScript(string script) {}
    public bool initTheme(string themePath) {}
    public LoadResult loadLanguage(string langDefPath) {}
    public LoadResult loadLanguage(string langDefPath, bool embedded) {}
    public bool printExternalStyle(string outFile) {}
    public virtual bool printIndexFile(SWIGTYPE_p_std__vectorT_std__string_t fileList, string outPath) {}
    public void setBaseFont(string fontName) {}
    public void setBaseFontSize(string fontSize) {}
    public void setEOLDelimiter(char delim) {}
    public virtual void setESCCanvasPadding(uint arg0) {}
    public virtual void setESCTrueColor(bool arg0) {}
    public void setEncoding(string encodingName) {}
    public void setFilesCnt(uint cnt) {}
    public void setFragmentCode(bool flag) {}
    public virtual void setHTMLAnchorPrefix(string arg0) {}
    public virtual void setHTMLAttachAnchors(bool arg0) {}
    public virtual void setHTMLClassName(string arg0) {}
    public virtual void setHTMLEnclosePreTag(bool arg0) {}
    public virtual void setHTMLInlineCSS(bool arg0) {}
    public virtual void setHTMLOrderedList(bool arg0) {}
    public virtual void setHTMLUseNonBreakingSpace(bool arg0) {}
    public void setIncludeStyle(bool flag) {}
    public void setIndentationOptions(SWIGTYPE_p_std__vectorT_std__string_t options) {}
    public void setIsolateTags(bool flag) {}
    public void setKeepInjections(bool flag) {}
    public void setKeyWordCase(SWIGTYPE_p_StringTools__KeywordCase keyCase) {}
    public virtual void setLATEXBeamerMode(bool arg0) {}
    public virtual void setLATEXNoShorthands(bool arg0) {}
    public virtual void setLATEXPrettySymbols(bool arg0) {}
    public virtual void setLATEXReplaceQuotes(bool arg0) {}
    public void setLineNumberWidth(int w) {}
    public void setMaxInputLineCnt(uint cnt) {}
    public void setNumberWrappedLines(bool flag) {}
    public void setOmitVersionComment(bool flag) {}
    public void setPluginParameter(string param) {}
    public void setPreformatting(WrapMode lineWrappingStyle, uint lineLength, int numberSpaces) {}
    public void setPrintLineNumbers(bool flag) {}
    public void setPrintLineNumbers(bool flag, uint startCnt) {}
    public void setPrintZeroes(bool flag) {}
    public virtual void setRTFCharStyles(bool arg0) {}
    public virtual void setRTFPageColor(bool arg0) {}
    public virtual void setRTFPageSize(string arg0) {}
    public virtual void setSVGSize(string arg0, string arg1) {}
    public void setStartingInputLine(uint begin) {}
    public void setStartingNestedLang(string langName) {}
    public void setStyleInputPath(string path) {}
    public void setStyleOutputPath(string path) {}
    public void setTitle(string title) {}
    public void setValidateInput(bool flag) {}
    public bool styleFound() {}
  }

  public class DataDir : IDisposable {
    public static string LSB_CFG_DIR { get; set; }
    public static string LSB_DATA_DIR { get; set; }
    public static string LSB_DOC_DIR { get; set; }

    protected bool swigCMemOwn;

    public DataDir() {}

    public SWIGTYPE_p_std__mapT_std__string_std__string_std__lessT_std__string_t_t assocByExtension { get; set; }
    public SWIGTYPE_p_std__mapT_std__string_std__string_std__lessT_std__string_t_t assocByFilename { get; set; }
    public SWIGTYPE_p_std__mapT_std__string_std__string_std__lessT_std__string_t_t assocByShebang { get; set; }

    protected virtual void Dispose(bool disposing) {}
    public void Dispose() {}
    ~DataDir() {}
    public string getDocDir() {}
    public string getExtDir() {}
    public string getFileSuffix(string fileName) {}
    public string getFiletypesConfPath() {}
    public string getFiletypesConfPath(string path) {}
    public string getI18nDir() {}
    public string getLangPath() {}
    public string getLangPath(string file) {}
    public string getPluginPath() {}
    public string getPluginPath(string arg0) {}
    public string getSystemDataPath() {}
    public string getThemePath() {}
    public string getThemePath(string file) {}
    public string getThemePath(string file, bool base16) {}
    public string guessFileType(string suffix, string inputFile) {}
    public string guessFileType(string suffix, string inputFile, bool useUserSuffix) {}
    public string guessFileType(string suffix, string inputFile, bool useUserSuffix, bool forceShebangCheckStdin) {}
    public void initSearchDirectories(string userDefinedDir) {}
    public bool loadFileTypeConfig(string name) {}
    public void printConfigPaths() {}
    public void searchDataDir(string userDefinedDir) {}
  }

  public class ReGroup : IDisposable {
    protected bool swigCMemOwn;

    public ReGroup() {}
    public ReGroup(ReGroup other) {}
    public ReGroup(State s, uint l, uint c, string n) {}

    public uint kwClass { get; set; }
    public uint length { get; set; }
    public string name { get; set; }
    public State state { get; set; }

    protected virtual void Dispose(bool disposing) {}
    public void Dispose() {}
    ~ReGroup() {}
  }

  public class RegexElement : IDisposable {
    public static int instanceCnt { get; set; }

    protected bool swigCMemOwn;

    public RegexElement() {}
    public RegexElement(State oState, State eState, string rePattern) {}
    public RegexElement(State oState, State eState, string rePattern, uint cID) {}
    public RegexElement(State oState, State eState, string rePattern, uint cID, int group) {}
    public RegexElement(State oState, State eState, string rePattern, uint cID, int group, string name) {}

    public int capturingGroup { get; set; }
    public State end { get; set; }
    public int instanceId { get; set; }
    public uint kwClass { get; set; }
    public string langName { get; set; }
    public State open { get; set; }
    public string pattern { get; set; }
    public SWIGTYPE_p_boost__xpressive__sregex rex { get; set; }

    protected virtual void Dispose(bool disposing) {}
    public void Dispose() {}
    ~RegexElement() {}
  }

  public class SWIGTYPE_p_Diluculum__LuaFunction {
    protected SWIGTYPE_p_Diluculum__LuaFunction() {}
  }

  public class SWIGTYPE_p_Diluculum__LuaState {
    protected SWIGTYPE_p_Diluculum__LuaState() {}
  }

  public class SWIGTYPE_p_StringTools__KeywordCase {
    protected SWIGTYPE_p_StringTools__KeywordCase() {}
  }

  public class SWIGTYPE_p_boost__xpressive__sregex {
    protected SWIGTYPE_p_boost__xpressive__sregex() {}
  }

  public class SWIGTYPE_p_std__mapT_std__string_int_std__lessT_std__string_t_t {
    protected SWIGTYPE_p_std__mapT_std__string_int_std__lessT_std__string_t_t() {}
  }

  public class SWIGTYPE_p_std__mapT_std__string_std__string_std__lessT_std__string_t_t {
    protected SWIGTYPE_p_std__mapT_std__string_std__string_std__lessT_std__string_t_t() {}
  }

  public class SWIGTYPE_p_std__vectorT_highlight__RegexElement_p_t {
    protected SWIGTYPE_p_std__vectorT_highlight__RegexElement_p_t() {}
  }

  public class SWIGTYPE_p_std__vectorT_std__string_t {
    protected SWIGTYPE_p_std__vectorT_std__string_t() {}
  }

  public class SyntaxReader : IDisposable {
    public static void initLuaState(SWIGTYPE_p_Diluculum__LuaState ls, string langDefPath, string pluginReadFilePath) {}
    public static void initLuaState(SWIGTYPE_p_Diluculum__LuaState ls, string langDefPath, string pluginReadFilePath, OutputType outputType) {}

    protected bool swigCMemOwn;

    public SyntaxReader() {}

    protected virtual void Dispose(bool disposing) {}
    public void Dispose() {}
    ~SyntaxReader() {}
    public void addUserChunk(SWIGTYPE_p_Diluculum__LuaFunction chunk) {}
    public bool allowNestedMLComments() {}
    public bool allowsInnerSection(string langPath) {}
    public bool assertDelimEqualLength() {}
    public bool delimiterIsDistinct(int delimID) {}
    public bool delimiterIsRawString(int delimID) {}
    public bool enableReformatting() {}
    public string getCategoryDescription() {}
    public byte getContinuationChar() {}
    public string getCurrentPath() {}
    public SWIGTYPE_p_Diluculum__LuaFunction getDecorateFct() {}
    public SWIGTYPE_p_Diluculum__LuaFunction getDecorateLineBeginFct() {}
    public SWIGTYPE_p_Diluculum__LuaFunction getDecorateLineEndFct() {}
    public string getDescription() {}
    public string getFailedRegex() {}
    public string getFooterInjection() {}
    public string getHeaderInjection() {}
    public SWIGTYPE_p_std__vectorT_std__string_t getKeywordClasses() {}
    public SWIGTYPE_p_std__mapT_std__string_int_std__lessT_std__string_t_t getKeywords() {}
    public string getLuaErrorText() {}
    public SWIGTYPE_p_Diluculum__LuaState getLuaState() {}
    public string getNewPath(string lang) {}
    public int getOpenDelimiterID(string token, State s) {}
    public string getOverrideConfigVal(string name) {}
    public byte getRawStringPrefix() {}
    public SWIGTYPE_p_std__vectorT_highlight__RegexElement_p_t getRegexElements() {}
    public SWIGTYPE_p_Diluculum__LuaFunction getValidateStateChangeFct() {}
    public bool highlightingDisabled() {}
    public bool highlightingEnabled() {}
    public bool isIgnoreCase() {}
    public int isKeyword(string s) {}
    public LoadResult load(string langDefPath, string pluginReadFilePath, OutputType outputType) {}
    public bool matchesOpenDelimiter(string token, State s, int openDelimId) {}
    public bool needsReload(string langDefPath) {}
    public void restoreLangEndDelim(string langPath) {}
  }

  public class highlight {
    public static readonly string GLOBAL_SR_INSTANCE_NAME = "HL_SRInstance";

    public highlight() {}
  }
}
