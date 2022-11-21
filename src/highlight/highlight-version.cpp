// SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
// SPDX-License-Identifier: GPL-3.0-or-later
#include <cstdint>
#include <stdio.h>
#include "../../src/include/version.h"

typedef char *LPSTR;

#if defined(_WIN32) || defined(__WIN32__) || defined(__CYGWIN__)
  #define DLLEXPORT extern "C" __declspec(dllexport)
  #define STDCALL __stdcall
#else
  #ifdef __cplusplus
    #define DLLEXPORT extern "C"
  #else
    #define DLLEXPORT
  #endif
  #define STDCALL
#endif

// nVersionPart:
//    version part which is copied to lpVersion
//      -1: full version string
//       0: major version string
//       1: minor version string
// nLength:
//    length written in lpVersion

DLLEXPORT std::int32_t STDCALL smdn_libhighlightsharp_get_highlight_version(
  const std::int32_t nVersionPart,
  LPSTR lpVersion,
  const std::int32_t nLength
)
{
#if defined(HIGHLIGHT_MAJOR) && defined(HIGHLIGHT_MINOR)
  switch (nVersionPart) {
    case 0: return snprintf(lpVersion, nLength, "%s", HIGHLIGHT_MAJOR);
    case 1: return snprintf(lpVersion, nLength, "%s", HIGHLIGHT_MINOR);
  }
#elif defined(HIGHLIGHT_VERSION)
  switch (nVersionPart) {
    case -1: return snprintf(lpVersion, nLength, "%s", HIGHLIGHT_VERSION);
  }
#endif

  return 0;
}
