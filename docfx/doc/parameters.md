The folllowing URI parameters are supported by the Cake.APT.Module.

# Source

By default, APT will attempt to install packages using the system default repositories (i.e. the same as invoked using `apt` with no extra arguments). This is controlled by the `/etc/apt/sources.list` and `/etc/apt/sources.list.d/*.list` files.

Note that since APT itself doesn't include any way to update these repository files or install from a non-configured repository, this module does not support custom sources. If you need custom sources for your packages, make sure they are present in `/etc/apt/sources.list` first!

# Package

This is the name of the APT package that you would like to install.  This should match the package name exactly (no version or architecture).

### Example

```
#tool apt:?package=mesa-utils
```

# Version

The specific version of the application that is being installed.  If not provided, APT will install the latest package version that is available.

### Example

```
#tool apt:?package=mesa-utils&version=8.3.0
```

# Release

This corresponds to the APT `-t` option, and will tell APT to try to install the package using the given release. This means you can install packages from any configured repository, even from another release.

### Example

```
#tool apt:?package=mesa-utils&release=xenial
```

# Distribution (archive)

This allows explicitly setting the distribution/archive the package should be sourced from, and allows APT to install packages from `testing` into a `stable` system, for example.

### Example

```
#tool apt:?package=mesa-utils&dist=stable
```