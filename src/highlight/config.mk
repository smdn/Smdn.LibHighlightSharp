# SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
# SPDX-License-Identifier: MIT
include .config.mk

.config.mk: configure.ps1 ../Highlight.Build.props
	./configure.ps1 $@

highlight.rc: configure.ps1 ../Highlight.Build.props
	./configure.ps1 $@

clean-config:
	rm -f .config.mk

clean-generated:
	rm -f highlight.rc
