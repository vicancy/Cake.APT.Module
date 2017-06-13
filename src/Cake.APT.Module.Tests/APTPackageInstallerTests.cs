using System;
using System.Linq;
using Cake.Core.Packaging;
using NSubstitute;
using Xunit;

namespace Cake.APT.Module.Tests
{
    /// <summary>
    /// APTPackageInstaller unit tests
    /// </summary>
    public sealed class APTPackageInstallerTests
    {
        public sealed class TheConstructor
        {
            [Fact]
            public void Should_Throw_If_Environment_Is_Null()
            {
                // Given
                var fixture = new APTPackageInstallerFixture();
                fixture.Environment = null;

                // When
                var result = Record.Exception(() => fixture.CreateInstaller());

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("environment", ((ArgumentNullException)result).ParamName);
            }

            [Fact]
            public void Should_Throw_If_Process_Runner_Is_Null()
            {
                // Given
                var fixture = new APTPackageInstallerFixture();
                fixture.ProcessRunner = null;

                // When
                var result = Record.Exception(() => fixture.CreateInstaller());

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("processRunner", ((ArgumentNullException)result).ParamName);
            }

            [Fact]
            public void Should_Throw_If_Content_Resolver_Is_Null()
            {
                // Given
                var fixture = new APTPackageInstallerFixture();
                fixture.ContentResolver = null;

                // When
                var result = Record.Exception(() => fixture.CreateInstaller());

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("contentResolver", ((ArgumentNullException)result).ParamName);
            }

            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given
                var fixture = new APTPackageInstallerFixture();
                fixture.Log = null;

                // When
                var result = Record.Exception(() => fixture.CreateInstaller());

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("log", ((ArgumentNullException)result).ParamName);
            }
        }

        public sealed class TheCanInstallMethod
        {
            private string APT_CONFIGKEY = "APT_Source";

            [Fact]
            public void Should_Throw_If_URI_Is_Null()
            {
                // Given
                var fixture = new APTPackageInstallerFixture();
                fixture.Package = null;

                // When
                var result = Record.Exception(() => fixture.CanInstall());

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("package", ((ArgumentNullException)result).ParamName);
            }

            [Fact]
            public void Should_Be_Able_To_Install_If_Scheme_Is_Correct()
            {
                // Given
                var fixture = new APTPackageInstallerFixture();
                fixture.Package = new PackageReference("apt:?package=xcowsay");

                // When
                var result = fixture.CanInstall();

                // Then
                Assert.True(result);
            }

            [Fact]
            public void Should_Not_Be_Able_To_Install_If_Scheme_Is_Incorrect()
            {
                // Given
                var fixture = new APTPackageInstallerFixture();
                fixture.Package = new PackageReference("homebrew:?package=windirstat");

                // When
                var result = fixture.CanInstall();

                // Then
                Assert.False(result);
            }

            [Fact]
            public void Should_Ignore_Custom_Source_If_AbsoluteUri_Is_Used()
            {
                var fixture = new APTPackageInstallerFixture();
                fixture.Package = new PackageReference("apt:http://absolute/?package=xcowsay");

                // When
                var result = Record.Exception(() => fixture.Install());

                // Then
                Assert.Null(result);
                fixture.Config.DidNotReceive().GetValue(APT_CONFIGKEY);
            }

            /* [Fact]
            public void Should_Use_Custom_Source_If_RelativeUri_Is_Used()
            {
                var fixture = new APTPackageInstallerFixture();
                fixture.Package = new PackageReference("apt:?package=xcowsay");

                // When
                var result = Record.Exception(() => fixture.Install());

                // Then
                Assert.Null(result);
                fixture.Config.Received().GetValue(DNF_CONFIGKEY);
            } */
        }

        public sealed class TheInstallMethod
        {
            [Fact]
            public void Should_Throw_If_Uri_Is_Null()
            {
                // Given
                var fixture = new APTPackageInstallerFixture();
                fixture.Package = null;

                // When
                var result = Record.Exception(() => fixture.Install());

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("package", ((ArgumentNullException)result).ParamName);
            }


            ///<summary>
            /// This test is the inverse of the normal one since the install path is ignored.
            ///</summary>
            ///<remarks>
            ///An install path makes no sense in an APT context
            ///</remarks>
            [Fact]
            public void Should_Not_Throw_If_Install_Path_Is_Null()
            {
                // Given
                var fixture = new APTPackageInstallerFixture();
                fixture.InstallPath = null;

                // When
                var result = Record.Exception(() => fixture.Install());

                // Then
                Assert.Null(result);
            }
        }

        public sealed class TheGetArgumentsMethod
        {
            [Fact]
            public void Should_Use_Yes_Flag()
            {
                // Given
                var fixture = new APTPackageInstallerFixture();

                // When
                var result = fixture.GetArguments().Render();

                // Then
                Assert.Equal("install -y xcowsay", result);
            }

            [Fact]
            public void Should_Use_Dist_Flag() {
                // Given
                var fixture = new APTPackageInstallerFixture();
                fixture.Package = new PackageReference("apt:?package=xcowsay&dist=stable");

                // When
                var result = fixture.GetArguments().Render();

                // Then
                Assert.Equal("install -y xcowsay/stable", result);
            }

            [Fact]
            public void Should_Use_Release_Option() {
                // Given
                var fixture = new APTPackageInstallerFixture();
                fixture.Package = new PackageReference("apt:?package=xcowsay&release=xenial");

                // When
                var result = fixture.GetArguments();

                // Then
                Assert.Contains(result, a => a.Render() == "-t=xenial");
            }

            [Fact]
            public void Should_Include_Version() {
                // Given
                var fixture = new APTPackageInstallerFixture();
                fixture.Package = new PackageReference("apt:?package=xcowsay&version=1.1.0");

                // When
                var result = fixture.GetArguments().Render();

                // Then
                Assert.Equal("install -y xcowsay=1.1.0", result);
            }
        }
    }
}