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

public class MessageFilter : PluginInterface2 {
  private HandleRef swigCPtr;

  internal MessageFilter(IntPtr cPtr, bool cMemoryOwn) : base(RakNetPINVOKE.CSharp_MessageFilter_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(MessageFilter obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~MessageFilter() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          RakNetPINVOKE.CSharp_delete_MessageFilter(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public static MessageFilter GetInstance() {
    IntPtr cPtr = RakNetPINVOKE.CSharp_MessageFilter_GetInstance();
    MessageFilter ret = (cPtr == IntPtr.Zero) ? null : new MessageFilter(cPtr, false);
    return ret;
  }

  public static void DestroyInstance(MessageFilter i) {
    RakNetPINVOKE.CSharp_MessageFilter_DestroyInstance(MessageFilter.getCPtr(i));
  }

  public MessageFilter() : this(RakNetPINVOKE.CSharp_new_MessageFilter(), true) {
  }

  public void SetAutoAddNewConnectionsToFilter(int filterSetID) {
    RakNetPINVOKE.CSharp_MessageFilter_SetAutoAddNewConnectionsToFilter(swigCPtr, filterSetID);
  }

  public void SetAllowMessageID(bool allow, int messageIDStart, int messageIDEnd, int filterSetID) {
    RakNetPINVOKE.CSharp_MessageFilter_SetAllowMessageID(swigCPtr, allow, messageIDStart, messageIDEnd, filterSetID);
  }

  public void SetAllowRPC4(bool allow, string uniqueID, int filterSetID) {
    RakNetPINVOKE.CSharp_MessageFilter_SetAllowRPC4(swigCPtr, allow, uniqueID, filterSetID);
  }

  public void SetActionOnDisallowedMessage(bool kickOnDisallowed, bool banOnDisallowed, uint banTimeMS, int filterSetID) {
    RakNetPINVOKE.CSharp_MessageFilter_SetActionOnDisallowedMessage(swigCPtr, kickOnDisallowed, banOnDisallowed, banTimeMS, filterSetID);
  }

  public void SetFilterMaxTime(int allowedTimeMS, bool banOnExceed, uint banTimeMS, int filterSetID) {
    RakNetPINVOKE.CSharp_MessageFilter_SetFilterMaxTime(swigCPtr, allowedTimeMS, banOnExceed, banTimeMS, filterSetID);
  }

  public int GetSystemFilterSet(AddressOrGUID addressOrGUID) {
    int ret = RakNetPINVOKE.CSharp_MessageFilter_GetSystemFilterSet(swigCPtr, AddressOrGUID.getCPtr(addressOrGUID));
    if (RakNetPINVOKE.SWIGPendingException.Pending) throw RakNetPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetSystemFilterSet(AddressOrGUID addressOrGUID, int filterSetID) {
    RakNetPINVOKE.CSharp_MessageFilter_SetSystemFilterSet(swigCPtr, AddressOrGUID.getCPtr(addressOrGUID), filterSetID);
    if (RakNetPINVOKE.SWIGPendingException.Pending) throw RakNetPINVOKE.SWIGPendingException.Retrieve();
  }

  public uint GetSystemCount(int filterSetID) {
    uint ret = RakNetPINVOKE.CSharp_MessageFilter_GetSystemCount(swigCPtr, filterSetID);
    return ret;
  }

  public uint GetFilterSetCount() {
    uint ret = RakNetPINVOKE.CSharp_MessageFilter_GetFilterSetCount(swigCPtr);
    return ret;
  }

  public int GetFilterSetIDByIndex(uint index) {
    int ret = RakNetPINVOKE.CSharp_MessageFilter_GetFilterSetIDByIndex(swigCPtr, index);
    return ret;
  }

  public void DeleteFilterSet(int filterSetID) {
    RakNetPINVOKE.CSharp_MessageFilter_DeleteFilterSet(swigCPtr, filterSetID);
  }

}

}
