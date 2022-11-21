# SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
# SPDX-License-Identifier: MIT
.DEFAULT_GOAL := all

all:
	$(MAKE) bindings -f bindings.mk
	$(MAKE) native-binaries -f native-binaries.mk

clean:
	$(MAKE) clean-libhighlight -f libhighlight.mk
	$(MAKE) clean-bindings -f bindings.mk
	$(MAKE) clean-wrapper -f native-binaries.mk
	$(MAKE) clean-native-binaries -f native-binaries.mk
	$(MAKE) clean-generated -f config.mk
	$(MAKE) clean-config -f config.mk

.PHONY: all clean
