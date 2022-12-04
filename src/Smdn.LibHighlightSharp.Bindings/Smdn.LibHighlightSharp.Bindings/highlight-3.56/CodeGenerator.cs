//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace Smdn.LibHighlightSharp.Bindings {

public class CodeGenerator : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal CodeGenerator(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(CodeGenerator obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~CodeGenerator() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          highlightPINVOKE.delete_CodeGenerator(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public static CodeGenerator getInstance(OutputType type) {
    global::System.IntPtr cPtr = highlightPINVOKE.CodeGenerator_getInstance((int)type);
    CodeGenerator ret = (cPtr == global::System.IntPtr.Zero) ? null : new CodeGenerator(cPtr, false);
    return ret;
  }

  public static void deleteInstance(CodeGenerator inst) {
    highlightPINVOKE.CodeGenerator_deleteInstance(CodeGenerator.getCPtr(inst));
  }

  public bool initTheme(string themePath) {
    bool ret = highlightPINVOKE.CodeGenerator_initTheme(swigCPtr, themePath);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string getThemeInitError() {
    string ret = highlightPINVOKE.CodeGenerator_getThemeInitError(swigCPtr);
    return ret;
  }

  public string getPluginScriptError() {
    string ret = highlightPINVOKE.CodeGenerator_getPluginScriptError(swigCPtr);
    return ret;
  }

  public bool initIndentationScheme(string indentScheme) {
    bool ret = highlightPINVOKE.CodeGenerator_initIndentationScheme(swigCPtr, indentScheme);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setIndentationOptions(SWIGTYPE_p_std__vectorT_std__string_t options) {
    highlightPINVOKE.CodeGenerator_setIndentationOptions(swigCPtr, SWIGTYPE_p_std__vectorT_std__string_t.getCPtr(options));
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public LoadResult loadLanguage(string langDefPath, bool embedded) {
    LoadResult ret = (LoadResult)highlightPINVOKE.CodeGenerator_loadLanguage__SWIG_0(swigCPtr, langDefPath, embedded);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public LoadResult loadLanguage(string langDefPath) {
    LoadResult ret = (LoadResult)highlightPINVOKE.CodeGenerator_loadLanguage__SWIG_1(swigCPtr, langDefPath);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ParseError generateFile(string inFileName, string outFileName) {
    ParseError ret = (ParseError)highlightPINVOKE.CodeGenerator_generateFile(swigCPtr, inFileName, outFileName);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string generateString(string input) {
    string ret = highlightPINVOKE.CodeGenerator_generateString(swigCPtr, input);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string generateStringFromFile(string inFileName) {
    string ret = highlightPINVOKE.CodeGenerator_generateStringFromFile(swigCPtr, inFileName);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool printExternalStyle(string outFile) {
    bool ret = highlightPINVOKE.CodeGenerator_printExternalStyle(swigCPtr, outFile);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool printPersistentState(string outFile) {
    bool ret = highlightPINVOKE.CodeGenerator_printPersistentState(swigCPtr, outFile);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual bool printIndexFile(SWIGTYPE_p_std__vectorT_std__string_t fileList, string outPath) {
    bool ret = highlightPINVOKE.CodeGenerator_printIndexFile(swigCPtr, SWIGTYPE_p_std__vectorT_std__string_t.getCPtr(fileList), outPath);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setPreformatting(WrapMode lineWrappingStyle, uint lineLength, int numberSpaces) {
    highlightPINVOKE.CodeGenerator_setPreformatting(swigCPtr, (int)lineWrappingStyle, lineLength, numberSpaces);
  }

  public bool styleFound() {
    bool ret = highlightPINVOKE.CodeGenerator_styleFound(swigCPtr);
    return ret;
  }

  public bool formattingDisabled() {
    bool ret = highlightPINVOKE.CodeGenerator_formattingDisabled(swigCPtr);
    return ret;
  }

  public bool formattingIsPossible() {
    bool ret = highlightPINVOKE.CodeGenerator_formattingIsPossible(swigCPtr);
    return ret;
  }

  public void setPrintLineNumbers(bool flag, uint startCnt) {
    highlightPINVOKE.CodeGenerator_setPrintLineNumbers__SWIG_0(swigCPtr, flag, startCnt);
  }

  public void setPrintLineNumbers(bool flag) {
    highlightPINVOKE.CodeGenerator_setPrintLineNumbers__SWIG_1(swigCPtr, flag);
  }

  public bool getPrintLineNumbers() {
    bool ret = highlightPINVOKE.CodeGenerator_getPrintLineNumbers(swigCPtr);
    return ret;
  }

  public void setPrintZeroes(bool flag) {
    highlightPINVOKE.CodeGenerator_setPrintZeroes(swigCPtr, flag);
  }

  public bool getPrintZeroes() {
    bool ret = highlightPINVOKE.CodeGenerator_getPrintZeroes(swigCPtr);
    return ret;
  }

  public void setFragmentCode(bool flag) {
    highlightPINVOKE.CodeGenerator_setFragmentCode(swigCPtr, flag);
  }

  public bool getFragmentCode() {
    bool ret = highlightPINVOKE.CodeGenerator_getFragmentCode(swigCPtr);
    return ret;
  }

  public void setLineNumberWidth(int w) {
    highlightPINVOKE.CodeGenerator_setLineNumberWidth(swigCPtr, w);
  }

  public int getLineNumberWidth() {
    int ret = highlightPINVOKE.CodeGenerator_getLineNumberWidth(swigCPtr);
    return ret;
  }

  public void setValidateInput(bool flag) {
    highlightPINVOKE.CodeGenerator_setValidateInput(swigCPtr, flag);
  }

  public bool getValidateInput() {
    bool ret = highlightPINVOKE.CodeGenerator_getValidateInput(swigCPtr);
    return ret;
  }

  public void setKeepInjections(bool flag) {
    highlightPINVOKE.CodeGenerator_setKeepInjections(swigCPtr, flag);
  }

  public bool getKeepInjections() {
    bool ret = highlightPINVOKE.CodeGenerator_getKeepInjections(swigCPtr);
    return ret;
  }

  public bool requiresTwoPassParsing() {
    bool ret = highlightPINVOKE.CodeGenerator_requiresTwoPassParsing(swigCPtr);
    return ret;
  }

  public void setNumberWrappedLines(bool flag) {
    highlightPINVOKE.CodeGenerator_setNumberWrappedLines(swigCPtr, flag);
  }

  public bool getNumberWrappedLines() {
    bool ret = highlightPINVOKE.CodeGenerator_getNumberWrappedLines(swigCPtr);
    return ret;
  }

  public void setOmitVersionComment(bool flag) {
    highlightPINVOKE.CodeGenerator_setOmitVersionComment(swigCPtr, flag);
  }

  public bool getOmitVersionComment() {
    bool ret = highlightPINVOKE.CodeGenerator_getOmitVersionComment(swigCPtr);
    return ret;
  }

  public void setIsolateTags(bool flag) {
    highlightPINVOKE.CodeGenerator_setIsolateTags(swigCPtr, flag);
  }

  public bool getIsolateTags() {
    bool ret = highlightPINVOKE.CodeGenerator_getIsolateTags(swigCPtr);
    return ret;
  }

  public string getStyleName() {
    string ret = highlightPINVOKE.CodeGenerator_getStyleName(swigCPtr);
    return ret;
  }

  public void setBaseFont(string fontName) {
    highlightPINVOKE.CodeGenerator_setBaseFont(swigCPtr, fontName);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public string getBaseFont() {
    string ret = highlightPINVOKE.CodeGenerator_getBaseFont(swigCPtr);
    return ret;
  }

  public void setBaseFontSize(string fontSize) {
    highlightPINVOKE.CodeGenerator_setBaseFontSize(swigCPtr, fontSize);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public string getBaseFontSize() {
    string ret = highlightPINVOKE.CodeGenerator_getBaseFontSize(swigCPtr);
    return ret;
  }

  public void setIncludeStyle(bool flag) {
    highlightPINVOKE.CodeGenerator_setIncludeStyle(swigCPtr, flag);
  }

  public void disableTrailingNL(int flag) {
    highlightPINVOKE.CodeGenerator_disableTrailingNL(swigCPtr, flag);
  }

  public void setStyleInputPath(string path) {
    highlightPINVOKE.CodeGenerator_setStyleInputPath(swigCPtr, path);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public void setStyleOutputPath(string path) {
    highlightPINVOKE.CodeGenerator_setStyleOutputPath(swigCPtr, path);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public void setEncoding(string encodingName) {
    highlightPINVOKE.CodeGenerator_setEncoding(swigCPtr, encodingName);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public string getStyleInputPath() {
    string ret = highlightPINVOKE.CodeGenerator_getStyleInputPath(swigCPtr);
    return ret;
  }

  public string getStyleOutputPath() {
    string ret = highlightPINVOKE.CodeGenerator_getStyleOutputPath(swigCPtr);
    return ret;
  }

  public void setTitle(string title) {
    highlightPINVOKE.CodeGenerator_setTitle(swigCPtr, title);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public string getTitle() {
    string ret = highlightPINVOKE.CodeGenerator_getTitle(swigCPtr);
    return ret;
  }

  public void setStartingInputLine(uint begin) {
    highlightPINVOKE.CodeGenerator_setStartingInputLine(swigCPtr, begin);
  }

  public void setMaxInputLineCnt(uint cnt) {
    highlightPINVOKE.CodeGenerator_setMaxInputLineCnt(swigCPtr, cnt);
  }

  public void setFilesCnt(uint cnt) {
    highlightPINVOKE.CodeGenerator_setFilesCnt(swigCPtr, cnt);
  }

  public void setKeyWordCase(SWIGTYPE_p_StringTools__KeywordCase keyCase) {
    highlightPINVOKE.CodeGenerator_setKeyWordCase(swigCPtr, SWIGTYPE_p_StringTools__KeywordCase.getCPtr(keyCase));
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public void setEOLDelimiter(char delim) {
    highlightPINVOKE.CodeGenerator_setEOLDelimiter(swigCPtr, delim);
  }

  public void setStartingNestedLang(string langName) {
    highlightPINVOKE.CodeGenerator_setStartingNestedLang(swigCPtr, langName);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public void setPluginParameter(string param) {
    highlightPINVOKE.CodeGenerator_setPluginParameter(swigCPtr, param);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public void resetSyntaxReaders() {
    highlightPINVOKE.CodeGenerator_resetSyntaxReaders(swigCPtr);
  }

  public bool initPluginScript(string script) {
    bool ret = highlightPINVOKE.CodeGenerator_initPluginScript(swigCPtr, script);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool syntaxRequiresTwoPassRun() {
    bool ret = highlightPINVOKE.CodeGenerator_syntaxRequiresTwoPassRun(swigCPtr);
    return ret;
  }

  public void clearPersistentSnippets() {
    highlightPINVOKE.CodeGenerator_clearPersistentSnippets(swigCPtr);
  }

  public string getSyntaxRegexError() {
    string ret = highlightPINVOKE.CodeGenerator_getSyntaxRegexError(swigCPtr);
    return ret;
  }

  public string getSyntaxLuaError() {
    string ret = highlightPINVOKE.CodeGenerator_getSyntaxLuaError(swigCPtr);
    return ret;
  }

  public string getSyntaxDescription() {
    string ret = highlightPINVOKE.CodeGenerator_getSyntaxDescription(swigCPtr);
    return ret;
  }

  public string getSyntaxEncodingHint() {
    string ret = highlightPINVOKE.CodeGenerator_getSyntaxEncodingHint(swigCPtr);
    return ret;
  }

  public string getThemeDescription() {
    string ret = highlightPINVOKE.CodeGenerator_getThemeDescription(swigCPtr);
    return ret;
  }

  public string getSyntaxCatDescription() {
    string ret = highlightPINVOKE.CodeGenerator_getSyntaxCatDescription(swigCPtr);
    return ret;
  }

  public string getThemeCatDescription() {
    string ret = highlightPINVOKE.CodeGenerator_getThemeCatDescription(swigCPtr);
    return ret;
  }

  public SWIGTYPE_p_std__vectorT_std__string_t getPosTestErrors() {
    SWIGTYPE_p_std__vectorT_std__string_t ret = new SWIGTYPE_p_std__vectorT_std__string_t(highlightPINVOKE.CodeGenerator_getPosTestErrors(swigCPtr), true);
    return ret;
  }

  public SyntaxReader getSyntaxReader() {
    global::System.IntPtr cPtr = highlightPINVOKE.CodeGenerator_getSyntaxReader(swigCPtr);
    SyntaxReader ret = (cPtr == global::System.IntPtr.Zero) ? null : new SyntaxReader(cPtr, false);
    return ret;
  }

  public virtual void setHTMLAttachAnchors(bool arg0) {
    highlightPINVOKE.CodeGenerator_setHTMLAttachAnchors(swigCPtr, arg0);
  }

  public virtual void setHTMLOrderedList(bool arg0) {
    highlightPINVOKE.CodeGenerator_setHTMLOrderedList(swigCPtr, arg0);
  }

  public virtual void setHTMLInlineCSS(bool arg0) {
    highlightPINVOKE.CodeGenerator_setHTMLInlineCSS(swigCPtr, arg0);
  }

  public virtual void setHTMLEnclosePreTag(bool arg0) {
    highlightPINVOKE.CodeGenerator_setHTMLEnclosePreTag(swigCPtr, arg0);
  }

  public virtual void setHTMLUseNonBreakingSpace(bool arg0) {
    highlightPINVOKE.CodeGenerator_setHTMLUseNonBreakingSpace(swigCPtr, arg0);
  }

  public virtual void setHTMLAnchorPrefix(string arg0) {
    highlightPINVOKE.CodeGenerator_setHTMLAnchorPrefix(swigCPtr, arg0);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void setHTMLClassName(string arg0) {
    highlightPINVOKE.CodeGenerator_setHTMLClassName(swigCPtr, arg0);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void setLATEXReplaceQuotes(bool arg0) {
    highlightPINVOKE.CodeGenerator_setLATEXReplaceQuotes(swigCPtr, arg0);
  }

  public virtual void setLATEXNoShorthands(bool arg0) {
    highlightPINVOKE.CodeGenerator_setLATEXNoShorthands(swigCPtr, arg0);
  }

  public virtual void setLATEXPrettySymbols(bool arg0) {
    highlightPINVOKE.CodeGenerator_setLATEXPrettySymbols(swigCPtr, arg0);
  }

  public virtual void setLATEXBeamerMode(bool arg0) {
    highlightPINVOKE.CodeGenerator_setLATEXBeamerMode(swigCPtr, arg0);
  }

  public virtual void setRTFPageSize(string arg0) {
    highlightPINVOKE.CodeGenerator_setRTFPageSize(swigCPtr, arg0);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void setRTFCharStyles(bool arg0) {
    highlightPINVOKE.CodeGenerator_setRTFCharStyles(swigCPtr, arg0);
  }

  public virtual void setRTFPageColor(bool arg0) {
    highlightPINVOKE.CodeGenerator_setRTFPageColor(swigCPtr, arg0);
  }

  public virtual void setSVGSize(string arg0, string arg1) {
    highlightPINVOKE.CodeGenerator_setSVGSize(swigCPtr, arg0, arg1);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void setESCTrueColor(bool arg0) {
    highlightPINVOKE.CodeGenerator_setESCTrueColor(swigCPtr, arg0);
  }

  public virtual void setESCCanvasPadding(uint arg0) {
    highlightPINVOKE.CodeGenerator_setESCCanvasPadding(swigCPtr, arg0);
  }

}

}