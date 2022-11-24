#!/usr/bin/env pwsh
# SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
# SPDX-License-Identifier: MIT

Param($Target)

function Get-HighlightBuildProps {
  param ($name)

  return $(dotnet msbuild ../Highlight.Build.props /noLogo /t:Highlight_PrintBuildProperty /p:Highlight_QueryPropertyName=${name}).Trim()
}

function Build-ConfigMk {
  param ($TargetFilename)

  $lines = @()

  $lines += "#"
  $lines += "# This file was automatically generated by ${PSCommandPath}."
  $lines += "# Do not edit this file by hand."
  $lines += "#"

  # $lines += "MAKE_OPTION_JOBS := -j$([System.Environment]::ProcessorCount + 1)"

  $LUA_VERSION = Get-HighlightBuildProps LUA_VERSION

  $lines += "LUA_VERSION := ${LUA_VERSION}"

  if ([System.Runtime.InteropServices.RuntimeInformation]::RuntimeIdentifier.StartsWith('osx.')) {
    # Instructions suggested by homebrew:
    #   For compilers to find lua@5.3 you may need to set:
    #     export LDFLAGS="-L/usr/local/opt/lua@5.3/lib"
    #     export CPPFLAGS="-I/usr/local/opt/lua@5.3/include"
    $lines += "LUA_LIBS := -L/usr/local/opt/lua@${LUA_VERSION}/lib"
    $lines += "LUA_CFLAGS := -I/usr/local/opt/lua@${LUA_VERSION}/include"
  }
  else {
    $lines += "LUA_LIBS := $(pkg-config --libs lua${LUA_VERSION})"
    $lines += "LUA_CFLAGS := $(pkg-config --cflags lua${LUA_VERSION})"
  }

  $lines += "LUA_VERSION_WINDOWS := $(Get-HighlightBuildProps LUA_VERSION_WINDOWS)"

  $HIGHLIGHT_SOURCE_VERSION = Get-HighlightBuildProps HIGHLIGHT_SOURCE_VERSION

  $lines += "HIGHLIGHT_SOURCE_REPO := $(Get-HighlightBuildProps HIGHLIGHT_SOURCE_REPO)"
  $lines += "HIGHLIGHT_SOURCE_VERSION := ${HIGHLIGHT_SOURCE_VERSION}"

  $HIGHLIGHT_SOURCE_ROOT_DIR = Get-HighlightBuildProps HIGHLIGHT_SOURCE_ROOT_DIR

  $lines += "HIGHLIGHT_SOURCE_ROOT_DIR := ${HIGHLIGHT_SOURCE_ROOT_DIR}"
  $lines += "HIGHLIGHT_INCLUDE_DIR := ${HIGHLIGHT_SOURCE_ROOT_DIR}src/include/"
  $lines += "HIGHLIGHT_SRC_DIR := ${HIGHLIGHT_SOURCE_ROOT_DIR}src/"

  # SWIG directory moved from examples to extras on v3.41
  # ref: https://gitlab.com/saalen/highlight/-/commit/295d9abb5de50f947a7f6c52e1a603ece3ea913d
  if ([System.Version]$HIGHLIGHT_SOURCE_VERSION -ge [System.Version]"3.41") {
    $HIGHLIGHT_SWIG_DIR = "${HIGHLIGHT_SOURCE_ROOT_DIR}extras/swig/"
  }
  else {
    $HIGHLIGHT_SWIG_DIR = "${HIGHLIGHT_SOURCE_ROOT_DIR}examples/swig/"
  }

  $HIGHLIGHT_SWIG_INTERFACE_FILE = "${HIGHLIGHT_SWIG_DIR}highlight.i"

  $lines += "HIGHLIGHT_SWIG_DIR := ${HIGHLIGHT_SWIG_DIR}"
  $lines += "HIGHLIGHT_SWIG_INTERFACE_FILE := ${HIGHLIGHT_SWIG_INTERFACE_FILE}"

  $BINDINGS_DLLIMPORTNAME = Get-HighlightBuildProps BINDINGS_DLLIMPORTNAME

  $lines += "BINDINGS_OUTPUT_DIR := $(Get-HighlightBuildProps BINDINGS_OUTPUT_DIR)"
  $lines += "BINDINGS_DLLIMPORTNAME := ${BINDINGS_DLLIMPORTNAME}"
  $lines += "BINDINGS_NAMESPACE := $(Get-HighlightBuildProps BINDINGS_NAMESPACE)"

  $NATIVE_BINARY_OUTPUT_BASEDIR = Get-HighlightBuildProps NATIVE_BINARY_OUTPUT_BASEDIR
  $MINGW_LUA_DLL_FILENAME = Get-HighlightBuildProps MINGW_LUA_DLL_FILENAME

  $lines += "NATIVE_BINARY_OUTPUT_BASEDIR := ${NATIVE_BINARY_OUTPUT_BASEDIR}"
  $lines += "NATIVE_BINARY_OUTPUT_PATH_UBUNTU_22_04_X64 := ${NATIVE_BINARY_OUTPUT_BASEDIR}ubuntu.22.04-x64/native/lib${BINDINGS_DLLIMPORTNAME}.so"
  $lines += "NATIVE_BINARY_OUTPUT_PATH_UBUNTU_20_04_X64 := ${NATIVE_BINARY_OUTPUT_BASEDIR}ubuntu.20.04-x64/native/lib${BINDINGS_DLLIMPORTNAME}.so"
  $lines += "NATIVE_BINARY_OUTPUT_PATH_MACOS_X64 := ${NATIVE_BINARY_OUTPUT_BASEDIR}osx-x64/native/lib${BINDINGS_DLLIMPORTNAME}.dylib"
  $lines += "NATIVE_BINARY_OUTPUT_PATH_WINDOWS_X64 := ${NATIVE_BINARY_OUTPUT_BASEDIR}win-x64/native/${BINDINGS_DLLIMPORTNAME}.dll"
  $lines += "NATIVE_BINARY_OUTPUT_PATH_LUA_WINDOWS_X64 := ${NATIVE_BINARY_OUTPUT_BASEDIR}win-x64/native/${MINGW_LUA_DLL_FILENAME}"

  #
  # Determine build targets for current environment
  #
  if ([System.Runtime.InteropServices.RuntimeInformation]::RuntimeIdentifier.StartsWith('ubuntu.22.04-x64')) {
    # Target 'ubuntu.22.04-x64' and 'win-x64'(+lua.dll)
    $artifact_rid = "ubuntu.22.04-x64"
    $lines += "NATIVE_BINARIES :=" +
      " `$(NATIVE_BINARY_OUTPUT_PATH_UBUNTU_22_04_X64)" +
      " `$(NATIVE_BINARY_OUTPUT_PATH_WINDOWS_X64)" +
      " `$(NATIVE_BINARY_OUTPUT_PATH_LUA_WINDOWS_X64)"
    $lines += "ARTIFACT_OUTPUTS := " +
      " `$(NATIVE_BINARY_OUTPUT_PATH_UBUNTU_22_04_X64)" +
      " `$(NATIVE_BINARY_OUTPUT_PATH_WINDOWS_X64)"
  }
  elseif ([System.Runtime.InteropServices.RuntimeInformation]::RuntimeIdentifier.StartsWith('ubuntu.20.04-x64')) {
    # Target 'ubuntu.20.04-x64'
    $artifact_rid = "ubuntu.20.04-x64"
    $lines += "NATIVE_BINARIES := `$(NATIVE_BINARY_OUTPUT_PATH_UBUNTU_20_04_X64)"
    $lines += "ARTIFACT_OUTPUTS := `$(NATIVE_BINARY_OUTPUT_PATH_UBUNTU_20_04_X64)"
  }
  elseif ([System.Runtime.InteropServices.RuntimeInformation]::RuntimeIdentifier.StartsWith('osx.11.0-x64')) {
    # Target 'osx-x64'
    $artifact_rid = "osx-x64"
    $lines += "NATIVE_BINARIES := `$(NATIVE_BINARY_OUTPUT_PATH_MACOS_X64)"
    $lines += "ARTIFACT_OUTPUTS := `$(NATIVE_BINARY_OUTPUT_PATH_MACOS_X64)"
  }
  else {
    Write-Error "unsupported build environment: $([System.Runtime.InteropServices.RuntimeInformation]::RuntimeIdentifier)"
    exit(1)
  }

  $lines += "ARTIFACT_BRANCH_NAME := artifact-${BINDINGS_DLLIMPORTNAME}-${artifact_rid}"

  $WRAPPER_OUTPUT_DIR = './'

  $lines += "WRAPPER_OUTPUT_PATH := ${WRAPPER_OUTPUT_DIR}"
  $lines += "WRAPPER_VERSION_SRC := ./highlight-version.cpp"
  $lines += "WRAPPER_VERSION_OBJ := ${WRAPPER_OUTPUT_DIR}highlight-version.o"
  $lines += "WRAPPER_SRC := ${WRAPPER_OUTPUT_DIR}highlight.cpp"
  $lines += "WRAPPER_OBJ := ${WRAPPER_OUTPUT_DIR}highlight.o"
  $lines += "WRAPPER_OBJS := `$(WRAPPER_OBJ) `$(WRAPPER_VERSION_OBJ)"

  $lines += "MINGW_INCLUDE_DIR := ./mingw/include/"
  $lines += "MINGW_LUA_DLL_DIR_WINDOWS_X64 := ./mingw/lib/win-x64/lua/"
  $lines += "MINGW_LUA_DLL_FILENAME := ${MINGW_LUA_DLL_FILENAME}"

  if (Test-Path -LiteralPath $TargetFilename -PathType leaf) {
    Remove-Item -Force -LiteralPath $TargetFilename
  }

  $lines | Out-File -Append -LiteralPath $TargetFilename
}

function Build-HighlightRc {
  param ($TargetFilename)

  $dll_fileversion = $(Get-HighlightBuildProps HIGHLIGHT_DLL_FILEVERSION)
  $dll_productname = $(Get-HighlightBuildProps HIGHLIGHT_DLL_PRODUCTNAME)
  $dll_filename = $(Get-HighlightBuildProps BINDINGS_DLLIMPORTNAME)

  $content = @"
//
// This file was automatically generated by ${PSCommandPath}.
// Do not edit this file by hand.
//

#include <winver.h>

#define VER_FILEVERSION             $($dll_fileversion.Replace('.', ','))
#define VER_FILEVERSION_STR         "${dll_fileversion}\0"

#define VER_PRODUCTVERSION          $($dll_fileversion.Replace('.', ','))
#define VER_PRODUCTVERSION_STR      "${dll_fileversion}\0"

#define VER_COMPANYNAME_STR         "smdn.jp (https://smdn.jp)\0"
#define VER_PRODUCTNAME_STR         "${dll_productname}\0"
#define VER_COMMENT_STR             "${dll_productname} build for Smdn.LibHighlightSharp, originally from $(Get-HighlightBuildProps HIGHLIGHT_SOURCE_REPO). This file is licensed under the GNU General Public License v3.0.\0"
#define VER_INTERNALNAME_STR        "${dll_filename}\0"
#define VER_ORIGINALFILENAME_STR    "${dll_filename}.dll\0"
#define VER_LEGALCOPYRIGHT_STR      "Copyright (C) 2022 smdn\0"
#define VER_LEGALTRADEMARKS1_STR    "GNU General Public License v3.0\0"
#define VER_LEGALTRADEMARKS2_STR    VER_LEGALTRADEMARKS1_STR

//
// Version Information
//
VS_VERSION_INFO     VERSIONINFO
FILEVERSION         VER_FILEVERSION
PRODUCTVERSION      VER_PRODUCTVERSION
FILEFLAGSMASK       VS_FFI_FILEFLAGSMASK
FILEFLAGS           VS_FF_PATCHED
FILEOS              VOS_NT_WINDOWS32
FILETYPE            VFT_DLL
FILESUBTYPE         VFT2_UNKNOWN
{
    BLOCK "StringFileInfo"
    {
        BLOCK "040904b0"
        {
            VALUE "CompanyName",      VER_COMPANYNAME_STR
            VALUE "FileDescription",  VER_COMMENT_STR
            VALUE "Comments",         VER_COMMENT_STR
            VALUE "FileVersion",      VER_FILEVERSION_STR
            VALUE "InternalName",     VER_INTERNALNAME_STR
            VALUE "LegalCopyright",   VER_LEGALCOPYRIGHT_STR
            VALUE "LegalTrademarks",  VER_LEGALTRADEMARKS1_STR
            VALUE "LegalTrademarks1", VER_LEGALTRADEMARKS1_STR
            VALUE "LegalTrademarks2", VER_LEGALTRADEMARKS2_STR
            VALUE "OriginalFilename", VER_ORIGINALFILENAME_STR
            VALUE "ProductName",      VER_PRODUCTNAME_STR
            VALUE "ProductVersion",   VER_PRODUCTVERSION_STR
        }
    }
    BLOCK "VarFileInfo"
    {
        VALUE "Translation", 0x0409, 1200
    }
}
"@

  $content | Out-File -LiteralPath $TargetFilename
}

#
# main
#
switch ($Target) {
  ".config.mk" { Build-ConfigMk $Target }
  "highlight.rc" { Build-HighlightRc $Target }
}
