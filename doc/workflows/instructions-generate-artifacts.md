# Document for generating the artifacts of Highlight with the specific version
This document describes the instruction steps for generating the artifacts of Highlight with the specific version.

Specifically, this document describes the instructions for generating SWIG C# bindings and native libraries.

It also describes the instructions for releasing new packages including the generated artifacts.

## Notes for remote repositories
In the following steps, there are points where you need to run the workflow on GitHub.

For clarity, the remote repository is named `github` in the following example.

```
# Push changes to `main` branch on *GitHub*
git push github main
```

When actually executing, replace the remote name with `origin` and so on as appropriate.

## Notes for the version used in this document
The instructions in this document generates artifacts for Highlight version **4.11**, and references **4.10** as the previous version.

Replace the version number with the desired version.

# Instruction steps for generating artifacts targeting specific versions of Highlight

## Create a working branch for generating artifacts
```
git switch -c artifact-highlight-4.11
```

From then on, work on this branch.

### Set the target version of the Highlight source
Change the value of `Highlight_SourceVersionMajorMinor` property in the file [src/Highlight.Build.props](../../src/Highlight.Build.props).

```
sed -i -e 's/Highlight_SourceVersionMajorMinor>4\.10/Highlight_SourceVersionMajorMinor>4\.11/g' src/Highlight.Build.props

git commit -m "bump build target version up to highlight-4.11" -- src/Highlight.Build.props
```

Commit this change as it will be used in subsequent GitHub workflows.

### Generate SWIG bindings
```
cd src/highlight/
make clean-bindings -f bindings.mk
make bindings -f bindings.mk
cd -
```

Check for differences in generated source files of the bindings.

```
cd src/Smdn.LibHighlightSharp.Bindings/Smdn.LibHighlightSharp.Bindings/
diff highlight-4.10/ highlight-4.11/
cd -
```

Then, commit the generated source files.

```
git add src/Smdn.LibHighlightSharp.Bindings/Smdn.LibHighlightSharp.Bindings/highlight-4.11/

git commit -m "add bindings for highlight-4.11" -- src/Smdn.LibHighlightSharp.Bindings/Smdn.LibHighlightSharp.Bindings/highlight-4.11/
```

### Push the working branch
```
git push github artifact-highlight-4.11
```

### Generate native binaries
Run the GitHub workflow to generate native libraries with the current version configuration.

```
eng/build-artifact-for-all-targets.sh
```

Then, merge the PRs that will be created by this workflow.

These PRs merge the created artifacts into a working branch. Changes to `SHA1SUMS.txt` will conflict during the merge, so resolve it accordingly.

### Create a PR and merge the working branch
Fetch the changes up to this point.

```
git pull github artifact-highlight-4.11
```

Create a PR to merge the changes up to this point into the `main` branch.

```
gh pr create --base main --head artifact-highlight-4.11 --assignee @me --title "Add artifact highlight-4.11" --fill
```

Then, merge a PR created by this command, after testing and other checks have been completed.

### Fetch the main branch after the merge has completed
```
git fetch github --prune
git switch main
git pull github main
git branch -d artifact-highlight-4.11
```

## Prepare for packaging and release

### Run tests with newly generated artifacts
```
dotnet test tests/Smdn.LibHighlightSharp.Bindings/
dotnet test tests/Smdn.LibHighlightSharp/
```

### Update `CompatibilitySuppressions.xml`
Compare with the automatically generated `CompatibilitySuppressions.xml`, and merge the changes.

```
dotnet pack -c Release /p:GenerateCompatibilitySuppressionFile=true src/Smdn.LibHighlightSharp.Bindings/ && git restore doc/api-list
git diff src/Smdn.LibHighlightSharp.Bindings/CompatibilitySuppressions.xml
```

Commit if there are any changes.

```
git commit -m "add CompatibilitySuppressions entries for highlight >=4.11" -- src/Smdn.LibHighlightSharp.Bindings/CompatibilitySuppressions.xml
```

### Push changes made up to this point
```
git push github main
```

If necessary, also run a test workflow.

## Release new versions of packages

### Run package release workflow

Set the tag and trigger the release workflow. When trigger the workflow, ensure that the Actions secrets are properly updated and not expired.

```
git tag -m "new release" new-release/main/Smdn.LibHighlightSharp.LangDefs-4.11.0
git tag -m "new release" new-release/main/Smdn.LibHighlightSharp.Themes-4.11.0
git tag -m "new release" new-release/main/Smdn.LibHighlightSharp.Bindings-4.11.0

git push github main --tags

git tag -d new-release/main/Smdn.LibHighlightSharp.LangDefs-4.11.0
git tag -d new-release/main/Smdn.LibHighlightSharp.Themes-4.11.0
git tag -d new-release/main/Smdn.LibHighlightSharp.Bindings-4.11.0
```

### Pull changes in the release
When the release workflow is complete, fetch the changes made up to this point.

```
git pull github main --tags
```
