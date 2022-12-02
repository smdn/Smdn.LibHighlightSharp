# SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
# SPDX-License-Identifier: MIT
include config.mk

.DEFAULT_GOAL := artifact

artifact:
	@echo "================================ configurations ================================"
	@cat .config.mk
	@echo "================================================================================"

	$(MAKE) native-binaries -f native-binaries.mk
#	@for artifact_output in $(NATIVE_BINARIES); do\
#	  artifact_output_dir=`dirname $$artifact_output`;\
#	  mkdir -p $$artifact_output_dir;\
#	  echo $$artifact_output >> $$artifact_output;\
#	done

	mkdir -p $(dir $(NATIVE_BINARY_SHA1SUM_FILE))

	pwsh -Command "& { \
	  \$$hash_list = Get-FileHash ('$(NATIVE_BINARIES)' -split ' ') -Algorithm SHA1;\
	  \$$sha1sum_list = \$$hash_list | Select-Object \
	    @{ Name='Path'; Expression={[System.IO.Path]::GetRelativePath('$(dir $(NATIVE_BINARY_SHA1SUM_FILE))', \$$_.Path)} }, \
	    @{ Name='Hash'; Expression={\$$_.Hash} }; \
	  \$$sha1sum_list_artifact = \$$sha1sum_list | foreach { \$$_.Hash.ToLowerInvariant() + \" *\" + \$$_.Path } ;\
	  if (Test-Path -Path '$(NATIVE_BINARY_SHA1SUM_FILE)' -PathType Leaf) { \
	    [string[]]\$$sha1sum_list_stored = Get-Content '$(NATIVE_BINARY_SHA1SUM_FILE)' ;\
	  } \
	  \$$sha1sum_list_merged = \$$sha1sum_list_artifact + \$$sha1sum_list_stored ;\
	  \$$sha1sum_list_distinct_ordered = \$$sha1sum_list_merged | Sort-Object { (\$$_ -split ' ')[1] } -Unique ;\
	  \$$sha1sum_list_distinct_ordered -join \"\`n\" | Out-File -FilePath '$(NATIVE_BINARY_SHA1SUM_FILE)' -Encoding utf8NoBOM ;\
	}"

	git switch -c $(ARTIFACT_BRANCH_NAME)
	git add $(NATIVE_BINARIES)
	git add $(NATIVE_BINARY_SHA1SUM_FILE)
	git commit -m 'add artifact $(notdir $(NATIVE_BINARIES)) ($(ARTIFACT_BRANCH_NAME))'

.PHONY: artifact
