# SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
# SPDX-License-Identifier: MIT
include config.mk

.DEFAULT_GOAL := libhighlight

$(HIGHLIGHT_SOURCE_ROOT_DIR):
	git clone ${HIGHLIGHT_SOURCE_REPO} -b v$(HIGHLIGHT_SOURCE_VERSION) --depth 1 $(HIGHLIGHT_SOURCE_ROOT_DIR) || \
	git clone ${HIGHLIGHT_SOURCE_REPO} -b  $(HIGHLIGHT_SOURCE_VERSION) --depth 1 $(HIGHLIGHT_SOURCE_ROOT_DIR)

	patch -d $(HIGHLIGHT_SOURCE_ROOT_DIR) -p0 < $(HIGHLIGHT_PATCH_DIR)highlight-4.11-remove-using-namespace-std.patch

highlight-src: $(HIGHLIGHT_SOURCE_ROOT_DIR)

libhighlight: $(HIGHLIGHT_SOURCE_ROOT_DIR)
	cd $(HIGHLIGHT_SWIG_DIR); $(MAKE) LUA_CFLAGS=$(LUA_CFLAGS) LUA_LIBS=$(LUA_LIBS) lib-stamp

clean-libhighlight:
	(test -d $(HIGHLIGHT_SWIG_DIR) && (cd $(HIGHLIGHT_SWIG_DIR); $(MAKE) clean)) || true

.PHONY: libhighlight highlight-src clean-libhighlight
