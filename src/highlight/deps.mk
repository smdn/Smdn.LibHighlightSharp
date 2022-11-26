# SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
# SPDX-License-Identifier: MIT
include config.mk

.DEFAULT_GOAL := restore-deps

MINGW_INCLUDE_DIR_BOOST = $(MINGW_INCLUDE_DIR)boost
LUA_ZIP_WINDOWS_X64 = lua-$(LUA_VERSION_WINDOWS)_Win64_dllw6_lib.zip
LUA_DLL_WINDOWS_X64 = $(MINGW_LUA_DLL_DIR_WINDOWS_X64)$(MINGW_LUA_DLL_FILENAME)

restore-deps: restore-deps-win-x64

restore-deps-win-x64: restore-mingw-boost restore-mingw-lua-win-x64

restore-mingw-boost: $(MINGW_INCLUDE_DIR_BOOST)

$(MINGW_INCLUDE_DIR_BOOST):
	mkdir -p $(MINGW_INCLUDE_DIR)
	ln -s -t $(MINGW_INCLUDE_DIR) /usr/include/boost || true

restore-mingw-lua-win-x64: $(LUA_DLL_WINDOWS_X64)

$(LUA_DLL_WINDOWS_X64):
	mkdir -p $(MINGW_LUA_DLL_DIR_WINDOWS_X64)

	cd $(MINGW_LUA_DLL_DIR_WINDOWS_X64) && \
	  curl -O -J -L https://sourceforge.net/projects/luabinaries/files/$(LUA_VERSION_WINDOWS)/Windows%20Libraries/Dynamic/$(LUA_ZIP_WINDOWS_X64)/download && \
	  unzip ${LUA_ZIP_WINDOWS_X64}

copy-runtime-deps: copy-runtime-deps-win-x64

copy-runtime-deps-win-x64: $(LUA_DLL_WINDOWS_X64)
	cp -f $(LUA_DLL_WINDOWS_X64) ${NATIVE_BINARY_OUTPUT_PATH_LUA_WINDOWS_X64}

.PHONY: restore-deps
.PHONY: restore-deps-win-x64
.PHONY: restore-mingw-boost
.PHONY: restore-mingw-lua-win-x64
.PHONY: copy-runtime-deps
.PHONY: copy-runtime-deps-win-x64
