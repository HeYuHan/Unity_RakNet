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

public class UDPProxyClient : PluginInterface2 {
  private HandleRef swigCPtr;

  internal UDPProxyClient(IntPtr cPtr, bool cMemoryOwn) : base(RakNetPINVOKE.CSharp_UDPProxyClient_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(UDPProxyClient obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~UDPProxyClient() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          RakNetPINVOKE.CSharp_delete_UDPProxyClient(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public static UDPProxyClient GetInstance() {
    IntPtr cPtr = RakNetPINVOKE.CSharp_UDPProxyClient_GetInstance();
    UDPProxyClient ret = (cPtr == IntPtr.Zero) ? null : new UDPProxyClient(cPtr, false);
    return ret;
  }

  public static void DestroyInstance(UDPProxyClient i) {
    RakNetPINVOKE.CSharp_UDPProxyClient_DestroyInstance(UDPProxyClient.getCPtr(i));
  }

  public UDPProxyClient() : this(RakNetPINVOKE.CSharp_new_UDPProxyClient(), true) {
  }

  public void SetResultHandler(UDPProxyClientResultHandler rh) {
    RakNetPINVOKE.CSharp_UDPProxyClient_SetResultHandler(swigCPtr, UDPProxyClientResultHandler.getCPtr(rh));
  }

  public bool RequestForwarding(SystemAddress proxyCoordinator, SystemAddress sourceAddress, SystemAddress targetAddressAsSeenFromCoordinator, uint timeoutOnNoDataMS, BitStream serverSelectionBitstream) {
    bool ret = RakNetPINVOKE.CSharp_UDPProxyClient_RequestForwarding__SWIG_0(swigCPtr, SystemAddress.getCPtr(proxyCoordinator), SystemAddress.getCPtr(sourceAddress), SystemAddress.getCPtr(targetAddressAsSeenFromCoordinator), timeoutOnNoDataMS, BitStream.getCPtr(serverSelectionBitstream));
    if (RakNetPINVOKE.SWIGPendingException.Pending) throw RakNetPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool RequestForwarding(SystemAddress proxyCoordinator, SystemAddress sourceAddress, SystemAddress targetAddressAsSeenFromCoordinator, uint timeoutOnNoDataMS) {
    bool ret = RakNetPINVOKE.CSharp_UDPProxyClient_RequestForwarding__SWIG_1(swigCPtr, SystemAddress.getCPtr(proxyCoordinator), SystemAddress.getCPtr(sourceAddress), SystemAddress.getCPtr(targetAddressAsSeenFromCoordinator), timeoutOnNoDataMS);
    if (RakNetPINVOKE.SWIGPendingException.Pending) throw RakNetPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool RequestForwarding(SystemAddress proxyCoordinator, SystemAddress sourceAddress, RakNetGUID targetGuid, uint timeoutOnNoDataMS, BitStream serverSelectionBitstream) {
    bool ret = RakNetPINVOKE.CSharp_UDPProxyClient_RequestForwarding__SWIG_2(swigCPtr, SystemAddress.getCPtr(proxyCoordinator), SystemAddress.getCPtr(sourceAddress), RakNetGUID.getCPtr(targetGuid), timeoutOnNoDataMS, BitStream.getCPtr(serverSelectionBitstream));
    if (RakNetPINVOKE.SWIGPendingException.Pending) throw RakNetPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool RequestForwarding(SystemAddress proxyCoordinator, SystemAddress sourceAddress, RakNetGUID targetGuid, uint timeoutOnNoDataMS) {
    bool ret = RakNetPINVOKE.CSharp_UDPProxyClient_RequestForwarding__SWIG_3(swigCPtr, SystemAddress.getCPtr(proxyCoordinator), SystemAddress.getCPtr(sourceAddress), RakNetGUID.getCPtr(targetGuid), timeoutOnNoDataMS);
    if (RakNetPINVOKE.SWIGPendingException.Pending) throw RakNetPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
