# SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
# SPDX-License-Identifier: MIT
THIS_FILE := $(lastword $(MAKEFILE_LIST))

include config.mk

.DEFAULT_GOAL := native-binaries

CFLAGS_COMMON = -g -O2 -fPIC

libhighlight-native:
	$(MAKE) clean-libhighlight -f libhighlight.mk
	$(MAKE) clean-wrapper -f $(THIS_FILE)
	$(MAKE) libhighlight -f libhighlight.mk

$(WRAPPER_SRC):
	$(MAKE) $(WRAPPER_SRC) -f bindings.mk

$(WRAPPER_OBJ): $(WRAPPER_SRC)
	mkdir -p $(dir $@)
	$(CXX) $(CFLAGS) -c $< $(LUA_CFLAGS) -I$(HIGHLIGHT_INCLUDE_DIR) -o $@

$(WRAPPER_VERSION_OBJ): $(WRAPPER_VERSION_SRC)
	mkdir -p $(dir $@)
	$(CXX) $(CFLAGS) -c $(WRAPPER_VERSION_SRC) -I$(HIGHLIGHT_INCLUDE_DIR) -o $@

wrapper: $(WRAPPER_OBJS)

clean-wrapper:
	rm -f $(WRAPPER_OBJS)

library-native:
	$(MAKE) -f $(THIS_FILE) \
	  CXX="$(CXX)" \
	  CFLAGS="$(CFLAGS)" \
	  libhighlight-native

	$(MAKE) -f $(THIS_FILE) \
	  CXX="$(CXX)" \
	  CFLAGS="$(CFLAGS)" \
	  wrapper

	mkdir -p $(dir $(NATIVE_BINARY_OUTPUT_PATH))

	$(CXX) $(LDFLAGS) -s $(WRAPPER_OBJS) $(OBJS_EXTRA) -L$(HIGHLIGHT_SRC_DIR) -lhighlight $(LUA_LIBS) -o $(NATIVE_BINARY_OUTPUT_PATH)

$(NATIVE_BINARY_OUTPUT_PATH_UBUNTU_22_04_X64): CXX := g++
$(NATIVE_BINARY_OUTPUT_PATH_UBUNTU_22_04_X64): CFLAGS := $(CFLAGS_COMMON) -m64
$(NATIVE_BINARY_OUTPUT_PATH_UBUNTU_22_04_X64): LDFLAGS := -shared
$(NATIVE_BINARY_OUTPUT_PATH_UBUNTU_22_04_X64):
	$(MAKE) -f $(THIS_FILE) \
	  CXX="$(CXX)" \
	  CFLAGS="$(CFLAGS)" \
	  LDFLAGS="$(LDFLAGS)" \
	  NATIVE_BINARY_OUTPUT_PATH="$@" \
	  library-native

$(NATIVE_BINARY_OUTPUT_PATH_UBUNTU_20_04_X64): CXX := g++
$(NATIVE_BINARY_OUTPUT_PATH_UBUNTU_20_04_X64): CFLAGS := $(CFLAGS_COMMON) -m64
$(NATIVE_BINARY_OUTPUT_PATH_UBUNTU_20_04_X64): LDFLAGS := -shared
$(NATIVE_BINARY_OUTPUT_PATH_UBUNTU_20_04_X64):
	$(MAKE) -f $(THIS_FILE) \
	  CXX="$(CXX)" \
	  CFLAGS="$(CFLAGS)" \
	  LDFLAGS="$(LDFLAGS)" \
	  NATIVE_BINARY_OUTPUT_PATH="$@" \
	  library-native

${NATIVE_BINARY_OUTPUT_PATH_MACOS_X64}: CXX := g++
${NATIVE_BINARY_OUTPUT_PATH_MACOS_X64}: CFLAGS := $(CFLAGS_COMMON) -m64
${NATIVE_BINARY_OUTPUT_PATH_MACOS_X64}: LDFLAGS := -shared -dynamiclib
${NATIVE_BINARY_OUTPUT_PATH_MACOS_X64}:
	$(MAKE) -f $(THIS_FILE) \
	  CXX="$(CXX)" \
	  CFLAGS="$(CFLAGS)" \
	  LDFLAGS="$(LDFLAGS)" \
	  NATIVE_BINARY_OUTPUT_PATH="$@" \
	  library-native

highlight-x64.res: highlight.rc
	x86_64-w64-mingw32-windres --input=$< --input-format=rc --output=$@ --output-format=coff

${NATIVE_BINARY_OUTPUT_PATH_WINDOWS_X64}: CXX := x86_64-w64-mingw32-g++-win32
${NATIVE_BINARY_OUTPUT_PATH_WINDOWS_X64}: CFLAGS := $(CFLAGS_COMMON) -m64 -I $(abspath $(MINGW_INCLUDE_DIR))
#${NATIVE_BINARY_OUTPUT_PATH_WINDOWS_X64}: CFLAGS := $(CFLAGS) -DHL_DATA_DIR=... -DHL_CONFIG_DIR=...
${NATIVE_BINARY_OUTPUT_PATH_WINDOWS_X64}: LDFLAGS := -shared -static-libstdc++ -static-libgcc
${NATIVE_BINARY_OUTPUT_PATH_WINDOWS_X64}: highlight-x64.res
	$(MAKE) restore-deps-win-x64 -f deps.mk

	$(MAKE) -f $(THIS_FILE) \
	  CXX="$(CXX)" \
	  CFLAGS="$(CFLAGS)" \
	  OBJS_EXTRA="highlight-x64.res" \
	  LDFLAGS="$(LDFLAGS)" \
	  NATIVE_BINARY_OUTPUT_PATH="$@" \
	  LUA_LIBS="-L$(MINGW_LUA_DLL_DIR_WINDOWS_X64) -Wl,-dy,-l$(basename $(MINGW_LUA_DLL_FILENAME))" \
	  library-native

native-binaries: $(NATIVE_BINARIES)
	pwsh -Command "& { Get-FileHash ('$(NATIVE_BINARIES)' -split ' ') -Algorithm SHA1 }"

clean-win-resource:
	rm -f highlight.rc
	rm -f highlight-x64.res

clean-native-binaries: clean-win-resource
	rm -f $(NATIVE_BINARIES)

.PHONY: libhighlight-native
.PHONY: library-native
.PHONY: native-binaries
.PHONY: clean-native-binaries
.PHONY: wrapper
.PHONY: clean-wrapper
