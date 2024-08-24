# SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
# SPDX-License-Identifier: MIT
include config.mk

.DEFAULT_GOAL := bindings

bindings-stamp:
	$(MAKE) highlight-src -f libhighlight.mk

	mkdir -p $(WRAPPER_OUTPUT_PATH)
	mkdir -p $(BINDINGS_OUTPUT_DIR)

	swig $(SWIG_C_OPTIONS) -csharp \
	  -o $(WRAPPER_SRC) \
	  -dllimport $(BINDINGS_DLLIMPORTNAME) \
	  -namespace $(BINDINGS_NAMESPACE) \
	  -outdir $(BINDINGS_OUTPUT_DIR) \
	  $(HIGHLIGHT_SWIG_INTERFACE_FILE)

	touch $@

$(WRAPPER_SRC): bindings-stamp

bindings: bindings-stamp

clean-bindings:
	rm -f bindings-stamp
	rm -f $(BINDINGS_OUTPUT_DIR)*.cs
	rm -f $(WRAPPER_SRC)

.PHONY: bindings clean-bindings
