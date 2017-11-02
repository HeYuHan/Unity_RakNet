LOCAL_PATH := $(call my-dir) 

include $(CLEAR_VARS) 

LOCAL_CPP_FEATURES := exceptions

LOCAL_MODULE := RakNet 

MY_PREFIX := $(LOCAL_PATH)/../Source

MY_SOURCES := $(wildcard $(MY_PREFIX)/*.cpp)

LOCAL_SRC_FILES := $(MY_SOURCES:$(LOCAL_PATH)/%=%)

$(warning files = $(LOCAL_SRC_FILES))

include $(BUILD_SHARED_LIBRARY)
