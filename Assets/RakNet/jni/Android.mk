LOCAL_PATH := $(call my-dir) 

include $(CLEAR_VARS) 

LOCAL_MODULE := RakNet 

MY_PREFIX := $(LOCAL_PATH)/../Source

MY_SOURCES := $(wildcard $(MY_PREFIX)*.cpp) 

LOCAL_SRC_FILES += $(MY_SOURCES:$(MY_PREFIX)%=RakNetSources/%) 
LOCAL_MODULE_PATH := ../../Plugins/Android

include $(BUILD_SHARED_LIBRARY)
