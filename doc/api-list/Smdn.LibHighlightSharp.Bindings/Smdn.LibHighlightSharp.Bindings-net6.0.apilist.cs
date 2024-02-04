// Smdn.LibHighlightSharp.Bindings.dll (Smdn.LibHighlightSharp.Bindings-4.6.1)
//   Name: Smdn.LibHighlightSharp.Bindings
//   AssemblyVersion: 4.6.1.0
//   InformationalVersion: 4.6.1+146d46b420652a4dffa11df7beed84cbb1b8a3c0
//   TargetFramework: .NETCoreApp,Version=v6.0
//   Configuration: Release
//   Referenced assemblies:
//     System.Runtime, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
//     System.Runtime.InteropServices, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
//     System.Runtime.InteropServices.RuntimeInformation, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
//     System.Threading, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a

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
  public enum LSResult : int {
    CMD_ERROR = 3,
    INIT_BAD_PIPE = 1,
    INIT_BAD_REQUEST = 2,
    INIT_OK = 0,
  }

  public enum LoadResult : int {
    LOAD_FAILED = 1,
    LOAD_FAILED_LUA = 3,
    LOAD_FAILED_REGEX = 2,
    LOAD_OK = 0,
  }

  public enum OutputType : int {
    BBCODE = 9,
    ESC_ANSI = 5,
    ESC_TRUECOLOR = 7,
    ESC_XTERM256 = 6,
    HTML = 0,
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
    DIRECTIVE_END = 19,
    DIRECTIVE_STRING = 7,
    EMBEDDED_CODE_BEGIN = 25,
    EMBEDDED_CODE_END = 26,
    ESC_CHAR = 5,
    ESC_CHAR_END = 18,
    IDENTIFIER_BEGIN = 23,
    IDENTIFIER_END = 24,
    KEYWORD = 13,
    KEYWORD_END = 22,
    LINENUMBER = 8,
    ML_COMMENT = 4,
    ML_COMMENT_END = 17,
    NUMBER = 2,
    NUMBER_END = 15,
    SL_COMMENT = 3,
    SL_COMMENT_END = 16,
    STANDARD = 0,
    STRING = 1,
    STRING_END = 14,
    STRING_INTERPOLATION = 10,
    STRING_INTERPOLATION_END = 21,
    SYMBOL = 9,
    SYMBOL_END = 20,
    SYNTAX_ERROR = 11,
    SYNTAX_ERROR_MSG = 12,
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
    public void clearPersistentSnippets() {}
    public void disableTrailingNL(int flag) {}
    public void exitLanguageServer() {}
    public bool formattingDisabled() {}
    public bool formattingIsPossible() {}
    public ParseError generateFile(string inFileName, string outFileName) {}
    public string generateString(string input) {}
    public string generateStringFromFile(string inFileName) {}
    public byte getAdditionalEOFChar() {}
    public string getBaseFont() {}
    public string getBaseFontSize() {}
    public bool getFragmentCode() {}
    public virtual string getHoverTagClose() {}
    public virtual string getHoverTagOpen(string hoverText) {}
    public bool getIsolateTags() {}
    public bool getKeepInjections() {}
    public int getLineNumberWidth() {}
    public bool getNumberWrappedLines() {}
    public bool getOmitVersionComment() {}
    public string getPluginScriptError() {}
    public SWIGTYPE_p_std__vectorT_std__string_t getPosTestErrors() {}
    public bool getPrintLineNumbers() {}
    public bool getPrintZeroes() {}
    public virtual string getStyleDefinition() {}
    public string getStyleInputPath() {}
    public string getStyleName() {}
    public string getStyleOutputPath() {}
    public string getSyntaxCatDescription() {}
    public string getSyntaxDescription() {}
    public string getSyntaxEncodingHint() {}
    public string getSyntaxLuaError() {}
    public SyntaxReader getSyntaxReader() {}
    public string getSyntaxRegexError() {}
    public string getThemeCatDescription() {}
    public float getThemeContrast() {}
    public string getThemeDescription() {}
    public string getThemeInitError() {}
    public string getTitle() {}
    public bool getValidateInput() {}
    public bool initIndentationScheme(string indentScheme) {}
    public LSResult initLanguageServer(string executable, SWIGTYPE_p_std__vectorT_std__string_t options, string workspace, string syntax, int delay, int logLevel) {}
    public LSResult initLanguageServer(string executable, SWIGTYPE_p_std__vectorT_std__string_t options, string workspace, string syntax, int delay, int logLevel, bool legacy) {}
    public bool initPluginScript(string script) {}
    public bool initTheme(string themePath) {}
    public bool initTheme(string themePath, bool loadSemanticStyles) {}
    public bool isHoverProvider() {}
    public bool isSemanticTokensProvider() {}
    public LoadResult loadLanguage(string langDefPath) {}
    public LoadResult loadLanguage(string langDefPath, bool embedded) {}
    public void lsAddHoverInfo(bool hover) {}
    public bool lsAddSemanticInfo(string fileName, string suffix) {}
    public void lsAddSyntaxErrorInfo(bool error) {}
    public bool lsCloseDocument(string fileName, string suffix) {}
    public bool lsOpenDocument(string fileName, string suffix) {}
    public bool printExternalStyle(string outFile) {}
    public virtual bool printIndexFile(SWIGTYPE_p_std__vectorT_std__string_t fileList, string outPath) {}
    public bool printPersistentState(string outFile) {}
    public string readUserStyleDef() {}
    public bool requiresTwoPassParsing() {}
    public void resetSyntaxReaders() {}
    public void setAdditionalEOFChar() {}
    public void setAdditionalEOFChar(byte eofChar) {}
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
    public void setStyleCaching(bool flag) {}
    public void setStyleInputPath(string path) {}
    public void setStyleOutputPath(string path) {}
    public void setTitle(string title) {}
    public void setValidateInput(bool flag) {}
    public bool styleFound() {}
    public bool syntaxRequiresTwoPassRun() {}
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
    public SWIGTYPE_p_std__mapT_std__string_std__string_std__lessT_std__string_t_t encodingHint { get; set; }

    protected virtual void Dispose(bool disposing) {}
    public void Dispose() {}
    ~DataDir() {}
    public string getDocDir() {}
    public string getEncodingHint(string arg0) {}
    public string getExtDir() {}
    public string getFileSuffix(string fileName) {}
    public string getFiletypesConfPath() {}
    public string getFiletypesConfPath(string path) {}
    public string getI18nDir() {}
    public string getLangPath() {}
    public string getLangPath(string file) {}
    public string getPluginPath() {}
    public string getPluginPath(string arg0) {}
    public SWIGTYPE_p_highlight__LSPProfile getProfile(string profile) {}
    public string getSystemDataPath() {}
    public string getThemePath() {}
    public string getThemePath(string file) {}
    public string getThemePath(string file, bool base16) {}
    public string guessFileType(string suffix, string inputFile) {}
    public string guessFileType(string suffix, string inputFile, bool useUserSuffix) {}
    public string guessFileType(string suffix, string inputFile, bool useUserSuffix, bool forceShebangCheckStdin) {}
    public void initSearchDirectories(string userDefinedDir) {}
    public bool loadFileTypeConfig(string name) {}
    public bool loadLSPConfig(string name) {}
    public void printConfigPaths() {}
    public bool profileExists(string profile) {}
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

  public class SWIGTYPE_p_highlight__LSPProfile {
    protected SWIGTYPE_p_highlight__LSPProfile() {}
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

  public class SWIGTYPE_p_std__vectorT_int_t {
    protected SWIGTYPE_p_std__vectorT_int_t() {}
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
    public void addPersistentKeyword(uint groupID, string kw) {}
    public void addPersistentStateRange(uint groupID, uint column, uint length, uint lineNumber, string fileName) {}
    public void addUserChunk(SWIGTYPE_p_Diluculum__LuaFunction chunk) {}
    public bool allowNestedMLComments() {}
    public bool allowsInnerSection(string langPath) {}
    public bool assertDelimEqualLength() {}
    public void clearPersistentSnippets() {}
    public bool delimiterIsDistinct(int delimID) {}
    public bool delimiterIsRawString(int delimID) {}
    public bool enableReformatting() {}
    public uint generateNewKWClass(int classID) {}
    public uint generateNewKWClass(int classID, string prefix) {}
    public string getCategoryDescription() {}
    public byte getContinuationChar() {}
    public string getCurrentPath() {}
    public SWIGTYPE_p_Diluculum__LuaFunction getDecorateFct() {}
    public SWIGTYPE_p_Diluculum__LuaFunction getDecorateLineBeginFct() {}
    public SWIGTYPE_p_Diluculum__LuaFunction getDecorateLineEndFct() {}
    public string getDescription() {}
    public string getEncodingHint() {}
    public string getFailedRegex() {}
    public string getFooterInjection() {}
    public string getHeaderInjection() {}
    public string getInputFileName() {}
    public SWIGTYPE_p_std__vectorT_std__string_t getKeywordClasses() {}
    public int getKeywordCount() {}
    public int getKeywordListGroup(string s) {}
    public SWIGTYPE_p_std__mapT_std__string_int_std__lessT_std__string_t_t getKeywords() {}
    public string getLuaErrorText() {}
    public SWIGTYPE_p_Diluculum__LuaState getLuaState() {}
    public string getNewPath(string lang) {}
    public int getOpenDelimiterID(string token, State s) {}
    public string getOverrideConfigVal(string name) {}
    public SWIGTYPE_p_std__vectorT_int_t getOverrideStyleAttributes() {}
    public string getPersistentHookConditions() {}
    public SWIGTYPE_p_std__vectorT_std__string_t getPersistentSnippets() {}
    public int getPersistentSnippetsNum() {}
    public byte getRawStringPrefix() {}
    public SWIGTYPE_p_std__vectorT_highlight__RegexElement_p_t getRegexElements() {}
    public SWIGTYPE_p_Diluculum__LuaFunction getValidateStateChangeFct() {}
    public bool highlightingDisabled() {}
    public bool highlightingEnabled() {}
    public bool isIgnoreCase() {}
    public bool isKeyword(string s) {}
    public LoadResult load(string langDefPath, string pluginReadFilePath, OutputType outputType) {}
    public bool matchesOpenDelimiter(string token, State s, int openDelimId) {}
    public bool needsReload(string langDefPath) {}
    public bool requiresParamUpdate() {}
    public bool requiresTwoPassRun() {}
    public void restoreLangEndDelim(string langPath) {}
    public void setInputFileName(string fn) {}
  }

  public class highlight {
    public static readonly string GLOBAL_SR_INSTANCE_NAME = "HL_SRInstance";

    public highlight() {}
  }
}
// API list generated by Smdn.Reflection.ReverseGenerating.ListApi.MSBuild.Tasks v1.3.2.0.
// Smdn.Reflection.ReverseGenerating.ListApi.Core v1.2.0.0 (https://github.com/smdn/Smdn.Reflection.ReverseGenerating)
