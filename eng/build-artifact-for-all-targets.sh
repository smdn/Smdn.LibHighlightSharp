#!/bin/sh
# SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
# SPDX-License-Identifier: MIT
#
# Summary:
#   This script runs workflow that builds the artifacts (libhighlight,
#   under the directory 'Smdn.LibHighlightSharp.Bindings/runtimes/**') on
#   all build environment.
#
#   The built artifacts will be commited to individual branches and
#   the workflow creates a new Pull Request which requests for merging
#   that branch.
#
#   The branch to merge into is the branch where the workflow was started.
#

BRANCH=`git rev-parse --abbrev-ref HEAD`

read -p "Are you sure to start workflow on the branch '${BRANCH}'? (y/N) " yn

case $yn in
  [Yy]* )
    break
    ;;
  * )
    echo 'cancelled'
    exit 1
    ;;
esac

gh workflow run build-artifact.yml -f 'os=ubuntu-22.04, ubuntu-20.04, macos-12' -f 'verbose=true' --ref ${BRANCH}
