# SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
# SPDX-License-Identifier: MIT
#
# This script installs the dependent apt packages to cross-compile the
# Highlight shared library on GitHub Actions' runner image.
#
# For a list of dependencies, see [../src/README.md].
#
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

install-buildtime-deps-ubuntu.20.04.stamp:
	sudo apt-get install -y \
	  autotools-dev \
	  libboost-dev \
	  liblua5.3 \
	  liblua5.3-dev \
	  g++-mingw-w64-x86-64 \
	  g++-multilib
	touch $@

install-buildtime-deps-ubuntu.20.04: install-buildtime-deps-ubuntu.20.04.stamp

install-buildtime-deps-osx.stamp:
	brew install \
	  automake \
	  boost \
	  lua@5.3
	touch $@

install-buildtime-deps-osx: install-buildtime-deps-osx.stamp

install-runtime-deps-ubuntu.20.04.stamp:
	sudo apt-get install -y \
	  liblua5.3
	touch $@

install-runtime-deps-ubuntu.20.04: install-runtime-deps-ubuntu.20.04.stamp

install-runtime-deps-osx.stamp:
	brew install \
	  lua@5.3
	touch $@

install-runtime-deps-osx: install-runtime-deps-osx.stamp

.PHONY: install-buildtime-deps-ubuntu.22.04
.PHONY: install-buildtime-deps-ubuntu.20.04
.PHONY: install-buildtime-deps-osx
.PHONY: install-runtime-deps-ubuntu.20.04
.PHONY: install-runtime-deps-osx
