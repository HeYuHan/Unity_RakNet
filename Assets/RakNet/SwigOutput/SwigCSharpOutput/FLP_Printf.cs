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

public class FLP_Printf : FileListProgress {
  private HandleRef swigCPtr;

  internal FLP_Printf(IntPtr cPtr, bool cMemoryOwn) : base(RakNetPINVOKE.CSharp_FLP_Printf_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(FLP_Printf obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~FLP_Printf() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          RakNetPINVOKE.CSharp_delete_FLP_Printf(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public new static FLP_Printf GetInstance() {
    IntPtr cPtr = RakNetPINVOKE.CSharp_FLP_Printf_GetInstance();
    FLP_Printf ret = (cPtr == IntPtr.Zero) ? null : new FLP_Printf(cPtr, false);
    return ret;
  }

  public static void DestroyInstance(FLP_Printf i) {
    RakNetPINVOKE.CSharp_FLP_Printf_DestroyInstance(FLP_Printf.getCPtr(i));
  }

  public FLP_Printf() : this(RakNetPINVOKE.CSharp_new_FLP_Printf(), true) {
  }

  public override void OnAddFilesFromDirectoryStarted(FileList fileList, string dir) {
    RakNetPINVOKE.CSharp_FLP_Printf_OnAddFilesFromDirectoryStarted(swigCPtr, FileList.getCPtr(fileList), dir);
  }

  public override void OnDirectory(FileList fileList, string dir, uint directoriesRemaining) {
    RakNetPINVOKE.CSharp_FLP_Printf_OnDirectory(swigCPtr, FileList.getCPtr(fileList), dir, directoriesRemaining);
  }

  public override void OnFilePushesComplete(SystemAddress systemAddress, ushort setID) {
    RakNetPINVOKE.CSharp_FLP_Printf_OnFilePushesComplete(swigCPtr, SystemAddress.getCPtr(systemAddress), setID);
    if (RakNetPINVOKE.SWIGPendingException.Pending) throw RakNetPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void OnSendAborted(SystemAddress systemAddress) {
    RakNetPINVOKE.CSharp_FLP_Printf_OnSendAborted(swigCPtr, SystemAddress.getCPtr(systemAddress));
    if (RakNetPINVOKE.SWIGPendingException.Pending) throw RakNetPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
