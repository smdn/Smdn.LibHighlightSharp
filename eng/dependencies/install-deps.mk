# SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
# SPDX-License-Identifier: MIT
#
# This script installs the dependent apt packages to cross-compile the
# Highlight shared library on GitHub Actions' runner image.
#
# For a list of dependencies, see [../src/README.md].
#
# SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
# SPDX-License-Identifier: MIT
install-buildtime-deps-ubuntu.22.04.stamp:
	sudo apt-get install -y \
	  autotools-dev \
	  libboost-dev \
	  liblua5.3 \
	  liblua5.3-dev \
	  g++-mingw-w64-x86-64 \
	  g++-multilib
	touch $@

install-buildtime-deps-ubuntu.22.04: install-buildtime-deps-ubuntu.22.04.stamp

.PHONY: install-buildtime-deps-ubuntu.22.04
