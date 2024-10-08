//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.2.0
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace Smdn.LibHighlightSharp.Bindings {

public class SyntaxReader : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal SyntaxReader(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(SyntaxReader obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(SyntaxReader obj) {
    if (obj != null) {
      if (!obj.swigCMemOwn)
        throw new global::System.ApplicationException("Cannot release ownership as memory is not owned");
      global::System.Runtime.InteropServices.HandleRef ptr = obj.swigCPtr;
      obj.swigCMemOwn = false;
      obj.Dispose();
      return ptr;
    } else {
      return new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
    }
  }

  ~SyntaxReader() {
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
          highlightPINVOKE.delete_SyntaxReader(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public SyntaxReader() : this(highlightPINVOKE.new_SyntaxReader(), true) {
  }

  public LoadResult load(string langDefPath, string pluginReadFilePath, OutputType outputType) {
    LoadResult ret = (LoadResult)highlightPINVOKE.SyntaxReader_load(swigCPtr, langDefPath, pluginReadFilePath, (int)outputType);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool needsReload(string langDefPath) {
    bool ret = highlightPINVOKE.SyntaxReader_needsReload(swigCPtr, langDefPath);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string getFailedRegex() {
    string ret = highlightPINVOKE.SyntaxReader_getFailedRegex(swigCPtr);
    return ret;
  }

  public string getLuaErrorText() {
    string ret = highlightPINVOKE.SyntaxReader_getLuaErrorText(swigCPtr);
    return ret;
  }

  public byte getRawStringPrefix() {
    byte ret = highlightPINVOKE.SyntaxReader_getRawStringPrefix(swigCPtr);
    return ret;
  }

  public byte getContinuationChar() {
    byte ret = highlightPINVOKE.SyntaxReader_getContinuationChar(swigCPtr);
    return ret;
  }

  public bool highlightingEnabled() {
    bool ret = highlightPINVOKE.SyntaxReader_highlightingEnabled(swigCPtr);
    return ret;
  }

  public bool isIgnoreCase() {
    bool ret = highlightPINVOKE.SyntaxReader_isIgnoreCase(swigCPtr);
    return ret;
  }

  public bool isKeyword(string s) {
    bool ret = highlightPINVOKE.SyntaxReader_isKeyword(swigCPtr, s);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int getKeywordListGroup(string s) {
    int ret = highlightPINVOKE.SyntaxReader_getKeywordListGroup(swigCPtr, s);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool allowNestedMLComments() {
    bool ret = highlightPINVOKE.SyntaxReader_allowNestedMLComments(swigCPtr);
    return ret;
  }

  public bool highlightingDisabled() {
    bool ret = highlightPINVOKE.SyntaxReader_highlightingDisabled(swigCPtr);
    return ret;
  }

  public bool enableReformatting() {
    bool ret = highlightPINVOKE.SyntaxReader_enableReformatting(swigCPtr);
    return ret;
  }

  public bool assertDelimEqualLength() {
    bool ret = highlightPINVOKE.SyntaxReader_assertDelimEqualLength(swigCPtr);
    return ret;
  }

  public SWIGTYPE_p_std__mapT_std__string_int_std__lessT_std__string_t_t getKeywords() {
    SWIGTYPE_p_std__mapT_std__string_int_std__lessT_std__string_t_t ret = new SWIGTYPE_p_std__mapT_std__string_int_std__lessT_std__string_t_t(highlightPINVOKE.SyntaxReader_getKeywords(swigCPtr), false);
    return ret;
  }

  public SWIGTYPE_p_std__vectorT_std__string_t getKeywordClasses() {
    SWIGTYPE_p_std__vectorT_std__string_t ret = new SWIGTYPE_p_std__vectorT_std__string_t(highlightPINVOKE.SyntaxReader_getKeywordClasses(swigCPtr), false);
    return ret;
  }

  public SWIGTYPE_p_std__vectorT_highlight__RegexElement_p_t getRegexElements() {
    SWIGTYPE_p_std__vectorT_highlight__RegexElement_p_t ret = new SWIGTYPE_p_std__vectorT_highlight__RegexElement_p_t(highlightPINVOKE.SyntaxReader_getRegexElements(swigCPtr), false);
    return ret;
  }

  public SWIGTYPE_p_std__vectorT_std__string_t getPersistentSnippets() {
    SWIGTYPE_p_std__vectorT_std__string_t ret = new SWIGTYPE_p_std__vectorT_std__string_t(highlightPINVOKE.SyntaxReader_getPersistentSnippets(swigCPtr), false);
    return ret;
  }

  public int getPersistentSnippetsNum() {
    int ret = highlightPINVOKE.SyntaxReader_getPersistentSnippetsNum(swigCPtr);
    return ret;
  }

  public SWIGTYPE_p_std__vectorT_int_t getOverrideStyleAttributes() {
    SWIGTYPE_p_std__vectorT_int_t ret = new SWIGTYPE_p_std__vectorT_int_t(highlightPINVOKE.SyntaxReader_getOverrideStyleAttributes(swigCPtr), false);
    return ret;
  }

  public string getDescription() {
    string ret = highlightPINVOKE.SyntaxReader_getDescription(swigCPtr);
    return ret;
  }

  public string getCategoryDescription() {
    string ret = highlightPINVOKE.SyntaxReader_getCategoryDescription(swigCPtr);
    return ret;
  }

  public string getHeaderInjection() {
    string ret = highlightPINVOKE.SyntaxReader_getHeaderInjection(swigCPtr);
    return ret;
  }

  public string getFooterInjection() {
    string ret = highlightPINVOKE.SyntaxReader_getFooterInjection(swigCPtr);
    return ret;
  }

  public bool delimiterIsDistinct(int delimID) {
    bool ret = highlightPINVOKE.SyntaxReader_delimiterIsDistinct(swigCPtr, delimID);
    return ret;
  }

  public bool delimiterIsRawString(int delimID) {
    bool ret = highlightPINVOKE.SyntaxReader_delimiterIsRawString(swigCPtr, delimID);
    return ret;
  }

  public int getOpenDelimiterID(string token, State s) {
    int ret = highlightPINVOKE.SyntaxReader_getOpenDelimiterID(swigCPtr, token, (int)s);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool matchesOpenDelimiter(string token, State s, int openDelimId) {
    bool ret = highlightPINVOKE.SyntaxReader_matchesOpenDelimiter(swigCPtr, token, (int)s, openDelimId);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void restoreLangEndDelim(string langPath) {
    highlightPINVOKE.SyntaxReader_restoreLangEndDelim(swigCPtr, langPath);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public bool allowsInnerSection(string langPath) {
    bool ret = highlightPINVOKE.SyntaxReader_allowsInnerSection(swigCPtr, langPath);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool requiresTwoPassRun() {
    bool ret = highlightPINVOKE.SyntaxReader_requiresTwoPassRun(swigCPtr);
    return ret;
  }

  public bool requiresParamUpdate() {
    bool ret = highlightPINVOKE.SyntaxReader_requiresParamUpdate(swigCPtr);
    return ret;
  }

  public string getPersistentHookConditions() {
    string ret = highlightPINVOKE.SyntaxReader_getPersistentHookConditions(swigCPtr);
    return ret;
  }

  public void clearPersistentSnippets() {
    highlightPINVOKE.SyntaxReader_clearPersistentSnippets(swigCPtr);
  }

  public string getNewPath(string lang) {
    string ret = highlightPINVOKE.SyntaxReader_getNewPath(swigCPtr, lang);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string getCurrentPath() {
    string ret = highlightPINVOKE.SyntaxReader_getCurrentPath(swigCPtr);
    return ret;
  }

  public string getEncodingHint() {
    string ret = highlightPINVOKE.SyntaxReader_getEncodingHint(swigCPtr);
    return ret;
  }

  public string getOverrideConfigVal(string name) {
    string ret = highlightPINVOKE.SyntaxReader_getOverrideConfigVal(swigCPtr, name);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_Diluculum__LuaFunction getValidateStateChangeFct() {
    global::System.IntPtr cPtr = highlightPINVOKE.SyntaxReader_getValidateStateChangeFct(swigCPtr);
    SWIGTYPE_p_Diluculum__LuaFunction ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_Diluculum__LuaFunction(cPtr, false);
    return ret;
  }

  public SWIGTYPE_p_Diluculum__LuaFunction getDecorateFct() {
    global::System.IntPtr cPtr = highlightPINVOKE.SyntaxReader_getDecorateFct(swigCPtr);
    SWIGTYPE_p_Diluculum__LuaFunction ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_Diluculum__LuaFunction(cPtr, false);
    return ret;
  }

  public SWIGTYPE_p_Diluculum__LuaFunction getDecorateLineBeginFct() {
    global::System.IntPtr cPtr = highlightPINVOKE.SyntaxReader_getDecorateLineBeginFct(swigCPtr);
    SWIGTYPE_p_Diluculum__LuaFunction ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_Diluculum__LuaFunction(cPtr, false);
    return ret;
  }

  public SWIGTYPE_p_Diluculum__LuaFunction getDecorateLineEndFct() {
    global::System.IntPtr cPtr = highlightPINVOKE.SyntaxReader_getDecorateLineEndFct(swigCPtr);
    SWIGTYPE_p_Diluculum__LuaFunction ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_Diluculum__LuaFunction(cPtr, false);
    return ret;
  }

  public SWIGTYPE_p_Diluculum__LuaState getLuaState() {
    global::System.IntPtr cPtr = highlightPINVOKE.SyntaxReader_getLuaState(swigCPtr);
    SWIGTYPE_p_Diluculum__LuaState ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_Diluculum__LuaState(cPtr, false);
    return ret;
  }

  public void addUserChunk(SWIGTYPE_p_Diluculum__LuaFunction chunk) {
    highlightPINVOKE.SyntaxReader_addUserChunk(swigCPtr, SWIGTYPE_p_Diluculum__LuaFunction.getCPtr(chunk));
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public void setInputFileName(string fn) {
    highlightPINVOKE.SyntaxReader_setInputFileName(swigCPtr, fn);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public string getInputFileName() {
    string ret = highlightPINVOKE.SyntaxReader_getInputFileName(swigCPtr);
    return ret;
  }

  public void addPersistentKeyword(uint groupID, string kw) {
    highlightPINVOKE.SyntaxReader_addPersistentKeyword(swigCPtr, groupID, kw);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public void addPersistentStateRange(uint groupID, uint column, uint length, uint lineNumber, string fileName) {
    highlightPINVOKE.SyntaxReader_addPersistentStateRange(swigCPtr, groupID, column, length, lineNumber, fileName);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void initLuaState(SWIGTYPE_p_Diluculum__LuaState ls, string langDefPath, string pluginReadFilePath, OutputType outputType) {
    highlightPINVOKE.SyntaxReader_initLuaState__SWIG_0(SWIGTYPE_p_Diluculum__LuaState.getCPtr(ls), langDefPath, pluginReadFilePath, (int)outputType);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void initLuaState(SWIGTYPE_p_Diluculum__LuaState ls, string langDefPath, string pluginReadFilePath) {
    highlightPINVOKE.SyntaxReader_initLuaState__SWIG_1(SWIGTYPE_p_Diluculum__LuaState.getCPtr(ls), langDefPath, pluginReadFilePath);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public uint generateNewKWClass(int classID, string prefix) {
    uint ret = highlightPINVOKE.SyntaxReader_generateNewKWClass__SWIG_0(swigCPtr, classID, prefix);
    return ret;
  }

  public uint generateNewKWClass(int classID) {
    uint ret = highlightPINVOKE.SyntaxReader_generateNewKWClass__SWIG_1(swigCPtr, classID);
    return ret;
  }

  public int getKeywordCount() {
    int ret = highlightPINVOKE.SyntaxReader_getKeywordCount(swigCPtr);
    return ret;
  }

}

}
