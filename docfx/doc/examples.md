# Examples

Installing a tool using the APT Cake Module is as simple as:

```
#tool apt:?package=mesa-utils
```

To install a specific version of a package:

```
#tool apt:?package=mesa-utils&version=8.3.0
```

or to tell APT to use the version for a specific release:

```
#tool apt:?package=mesa-utils&release=xenial
```

Full parameter information is covered in the [parameters documentation](parameters.md).