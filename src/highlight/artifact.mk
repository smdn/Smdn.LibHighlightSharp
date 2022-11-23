# SPDX-FileCopyrightText: 2022 smdn <smdn@smdn.jp>
# SPDX-License-Identifier: MIT
include config.mk

.DEFAULT_GOAL := artifact

artifact:
	@echo "================================ configurations ================================"
	@cat .config.mk
	@echo "================================================================================"

	$(MAKE) native-binaries -f native-binaries.mk

	git switch -c $(ARTIFACT_BRANCH_NAME)
	git add $(ARTIFACT_OUTPUTS)
	git commit -m 'add artifact $(notdir $(ARTIFACT_OUTPUTS)) ($(ARTIFACT_BRANCH_NAME))'

.PHONY: artifact
