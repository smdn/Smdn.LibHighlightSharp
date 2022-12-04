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

public class DataDir : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal DataDir(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(DataDir obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~DataDir() {
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
          highlightPINVOKE.delete_DataDir(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public static string LSB_DATA_DIR {
    set {
      highlightPINVOKE.DataDir_LSB_DATA_DIR_set(value);
      if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = highlightPINVOKE.DataDir_LSB_DATA_DIR_get();
      if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public static string LSB_CFG_DIR {
    set {
      highlightPINVOKE.DataDir_LSB_CFG_DIR_set(value);
      if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = highlightPINVOKE.DataDir_LSB_CFG_DIR_get();
      if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public static string LSB_DOC_DIR {
    set {
      highlightPINVOKE.DataDir_LSB_DOC_DIR_set(value);
      if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = highlightPINVOKE.DataDir_LSB_DOC_DIR_get();
      if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public void initSearchDirectories(string userDefinedDir) {
    highlightPINVOKE.DataDir_initSearchDirectories(swigCPtr, userDefinedDir);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public void searchDataDir(string userDefinedDir) {
    highlightPINVOKE.DataDir_searchDataDir(swigCPtr, userDefinedDir);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public void printConfigPaths() {
    highlightPINVOKE.DataDir_printConfigPaths(swigCPtr);
  }

  public string searchFile(string path) {
    string ret = highlightPINVOKE.DataDir_searchFile(swigCPtr, path);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string getLangPath(string file) {
    string ret = highlightPINVOKE.DataDir_getLangPath__SWIG_0(swigCPtr, file);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string getThemePath() {
    string ret = highlightPINVOKE.DataDir_getThemePath__SWIG_0(swigCPtr);
    return ret;
  }

  public string getLangPath() {
    string ret = highlightPINVOKE.DataDir_getLangPath__SWIG_1(swigCPtr);
    return ret;
  }

  public string getSystemDataPath() {
    string ret = highlightPINVOKE.DataDir_getSystemDataPath(swigCPtr);
    return ret;
  }

  public string getThemePath(string file, bool base16) {
    string ret = highlightPINVOKE.DataDir_getThemePath__SWIG_1(swigCPtr, file, base16);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string getThemePath(string file) {
    string ret = highlightPINVOKE.DataDir_getThemePath__SWIG_2(swigCPtr, file);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string getFiletypesConfPath(string arg0) {
    string ret = highlightPINVOKE.DataDir_getFiletypesConfPath(swigCPtr, arg0);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string getPluginPath(string arg0) {
    string ret = highlightPINVOKE.DataDir_getPluginPath__SWIG_0(swigCPtr, arg0);
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string getPluginPath() {
    string ret = highlightPINVOKE.DataDir_getPluginPath__SWIG_1(swigCPtr);
    return ret;
  }

  public string getI18nDir() {
    string ret = highlightPINVOKE.DataDir_getI18nDir(swigCPtr);
    return ret;
  }

  public string getExtDir() {
    string ret = highlightPINVOKE.DataDir_getExtDir(swigCPtr);
    return ret;
  }

  public string getDocDir() {
    string ret = highlightPINVOKE.DataDir_getDocDir(swigCPtr);
    return ret;
  }

  public DataDir() : this(highlightPINVOKE.new_DataDir(), true) {
  }

}

}
