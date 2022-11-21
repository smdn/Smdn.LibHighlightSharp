[![GitHub license](https://img.shields.io/github/license/smdn/Smdn.Template.Library)](https://github.com/smdn/Smdn.Template.Library/blob/main/LICENSE.txt)
[![tests/main](https://img.shields.io/github/workflow/status/smdn/Smdn.Template.Library/Run%20tests/main?label=tests%2Fmain)](https://github.com/smdn/Smdn.Template.Library/actions/workflows/test.yml)
[![CodeQL](https://github.com/smdn/Smdn.Template.Library/actions/workflows/codeql-analysis.yml/badge.svg?branch=main)](https://github.com/smdn/Smdn.Template.Library/actions/workflows/codeql-analysis.yml)
[![NuGet](https://img.shields.io/nuget/v/Smdn.svg)](https://www.nuget.org/packages/Smdn/)

# Smdn.Template.Library
Smdn.Template.Library is a .NET library template.

# Initial configurations to do
- [ ] [Configure repository settings](/../../settings)
- [ ] [Set PAT](/../../settings/secrets/actions) for [workflows/generate-release-target](/.github/workflows/generate-release-target.yml) and  [workflows/publish-release-target.yml](/.github/workflows/publish-release-target.yml)
- [ ] Configure the cron schedule of [CodeQL workflow](/.github/workflows/codeql-analysis.yml)
- [ ] [Create a issue label](/../../labels) for the release targets.
  - Label name: `release-target` (Must be the same as that specified in the files of release-target workflows.)
  - Description: `Describing a new release`
  - Color: `#006B75`
- [ ] Change `PackageProjectUrl` and `RepositoryUrl` in [Directory.Build.props](src/Directory.Build.props)
- [ ] Rename project directories and project files.
- [ ] Change links of badges.
