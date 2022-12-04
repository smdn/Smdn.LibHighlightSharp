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

public class RegexElement : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal RegexElement(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(RegexElement obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~RegexElement() {
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
          highlightPINVOKE.delete_RegexElement(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public RegexElement() : this(highlightPINVOKE.new_RegexElement__SWIG_0(), true) {
  }

  public RegexElement(State oState, State eState, string rePattern, uint cID, int group, string name) : this(highlightPINVOKE.new_RegexElement__SWIG_1((int)oState, (int)eState, rePattern, cID, group, name), true) {
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public RegexElement(State oState, State eState, string rePattern, uint cID, int group) : this(highlightPINVOKE.new_RegexElement__SWIG_2((int)oState, (int)eState, rePattern, cID, group), true) {
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public RegexElement(State oState, State eState, string rePattern, uint cID) : this(highlightPINVOKE.new_RegexElement__SWIG_3((int)oState, (int)eState, rePattern, cID), true) {
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public RegexElement(State oState, State eState, string rePattern) : this(highlightPINVOKE.new_RegexElement__SWIG_4((int)oState, (int)eState, rePattern), true) {
    if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
  }

  public State open {
    set {
      highlightPINVOKE.RegexElement_open_set(swigCPtr, (int)value);
    } 
    get {
      State ret = (State)highlightPINVOKE.RegexElement_open_get(swigCPtr);
      return ret;
    } 
  }

  public State end {
    set {
      highlightPINVOKE.RegexElement_end_set(swigCPtr, (int)value);
    } 
    get {
      State ret = (State)highlightPINVOKE.RegexElement_end_get(swigCPtr);
      return ret;
    } 
  }

  public SWIGTYPE_p_boost__xpressive__sregex rex {
    set {
      highlightPINVOKE.RegexElement_rex_set(swigCPtr, SWIGTYPE_p_boost__xpressive__sregex.getCPtr(value));
      if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      SWIGTYPE_p_boost__xpressive__sregex ret = new SWIGTYPE_p_boost__xpressive__sregex(highlightPINVOKE.RegexElement_rex_get(swigCPtr), true);
      if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public uint kwClass {
    set {
      highlightPINVOKE.RegexElement_kwClass_set(swigCPtr, value);
    } 
    get {
      uint ret = highlightPINVOKE.RegexElement_kwClass_get(swigCPtr);
      return ret;
    } 
  }

  public int capturingGroup {
    set {
      highlightPINVOKE.RegexElement_capturingGroup_set(swigCPtr, value);
    } 
    get {
      int ret = highlightPINVOKE.RegexElement_capturingGroup_get(swigCPtr);
      return ret;
    } 
  }

  public string langName {
    set {
      highlightPINVOKE.RegexElement_langName_set(swigCPtr, value);
      if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = highlightPINVOKE.RegexElement_langName_get(swigCPtr);
      if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string pattern {
    set {
      highlightPINVOKE.RegexElement_pattern_set(swigCPtr, value);
      if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = highlightPINVOKE.RegexElement_pattern_get(swigCPtr);
      if (highlightPINVOKE.SWIGPendingException.Pending) throw highlightPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public static int instanceCnt {
    set {
      highlightPINVOKE.RegexElement_instanceCnt_set(value);
    } 
    get {
      int ret = highlightPINVOKE.RegexElement_instanceCnt_get();
      return ret;
    } 
  }

  public int instanceId {
    set {
      highlightPINVOKE.RegexElement_instanceId_set(swigCPtr, value);
    } 
    get {
      int ret = highlightPINVOKE.RegexElement_instanceId_get(swigCPtr);
      return ret;
    } 
  }

}

}