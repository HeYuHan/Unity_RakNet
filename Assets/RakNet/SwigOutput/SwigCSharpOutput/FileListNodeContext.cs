/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.9
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace RakNet {

using System;
using System.Runtime.InteropServices;

public class FileListNodeContext : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal FileListNodeContext(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(FileListNodeContext obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~FileListNodeContext() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          RakNetPINVOKE.CSharp_delete_FileListNodeContext(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public FileListNodeContext() : this(RakNetPINVOKE.CSharp_new_FileListNodeContext__SWIG_0(), true) {
  }

  public FileListNodeContext(byte o, uint f1, uint f2, uint f3) : this(RakNetPINVOKE.CSharp_new_FileListNodeContext__SWIG_1(o, f1, f2, f3), true) {
  }

  public byte op {
    set {
      RakNetPINVOKE.CSharp_FileListNodeContext_op_set(swigCPtr, value);
    } 
    get {
      byte ret = RakNetPINVOKE.CSharp_FileListNodeContext_op_get(swigCPtr);
      return ret;
    } 
  }

  public uint flnc_extraData1 {
    set {
      RakNetPINVOKE.CSharp_FileListNodeContext_flnc_extraData1_set(swigCPtr, value);
    } 
    get {
      uint ret = RakNetPINVOKE.CSharp_FileListNodeContext_flnc_extraData1_get(swigCPtr);
      return ret;
    } 
  }

  public uint flnc_extraData2 {
    set {
      RakNetPINVOKE.CSharp_FileListNodeContext_flnc_extraData2_set(swigCPtr, value);
    } 
    get {
      uint ret = RakNetPINVOKE.CSharp_FileListNodeContext_flnc_extraData2_get(swigCPtr);
      return ret;
    } 
  }

  public uint flnc_extraData3 {
    set {
      RakNetPINVOKE.CSharp_FileListNodeContext_flnc_extraData3_set(swigCPtr, value);
    } 
    get {
      uint ret = RakNetPINVOKE.CSharp_FileListNodeContext_flnc_extraData3_get(swigCPtr);
      return ret;
    } 
  }

  public SWIGTYPE_p_void dataPtr {
    set {
      RakNetPINVOKE.CSharp_FileListNodeContext_dataPtr_set(swigCPtr, SWIGTYPE_p_void.getCPtr(value));
    } 
    get {
      IntPtr cPtr = RakNetPINVOKE.CSharp_FileListNodeContext_dataPtr_get(swigCPtr);
      SWIGTYPE_p_void ret = (cPtr == IntPtr.Zero) ? null : new SWIGTYPE_p_void(cPtr, false);
      return ret;
    } 
  }

  public uint dataLength {
    set {
      RakNetPINVOKE.CSharp_FileListNodeContext_dataLength_set(swigCPtr, value);
    } 
    get {
      uint ret = RakNetPINVOKE.CSharp_FileListNodeContext_dataLength_get(swigCPtr);
      return ret;
    } 
  }

}

}
