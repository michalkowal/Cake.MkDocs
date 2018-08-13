using System;
using System.Threading.Tasks;
using Cake.Core.IO;
using Cake.MkDocs.Build;
using Cake.MkDocs.GhDeploy;
using Cake.MkDocs.New;
using Cake.MkDocs.Serve;
using Cake.MkDocs.Version;
using Cake.Testing;
using Xunit;

namespace Cake.MkDocs.Tests.Unit
{
    public sealed class MkDocsAliasesTests
    {
        public sealed class TheMkDocsVersionMethod
        {
            private readonly FakeProcess _process;

            public TheMkDocsVersionMethod()
            {
                _process = new FakeProcess();
                _process.SetStandardOutput(new[] { "mkdocs, version 1.0 from \\local\\python\\python36\\lib\\site-packages\\mkdocs (Python 3.6)" });
            }

            [Fact]
            public void Should_Return_Result_For_Default_Settings()
            {
                // Given
                var context = new MkDocsContextFixture(_process);

                // When
                var result = context.MkDocsVersion();

                // Then
                Assert.NotNull(result);
                Assert.NotEqual(new System.Version(), result);
            }

            [Fact]
            public void Should_Return_Result_For_Defined_Settings()
            {
                // Given
                var context = new MkDocsContextFixture(_process);
                var settings = new MkDocsVersionSettings();

                // When
                var result = context.MkDocsVersion(settings);

                // Then
                Assert.NotNull(result);
                Assert.NotEqual(new System.Version(), result);
            }

            [Fact]
            public void Should_Throw_For_Null_Context()
            {
                // Given
                var settings = new MkDocsVersionSettings();

                // When
                var result = Record.Exception(() => MkDocsAliases.MkDocsVersion(null, settings));

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("context", ((ArgumentNullException)result).ParamName);
            }
        }

        public sealed class TheMkDocsIsSupportedVersionMethod
        {
            private FakeProcess _process;

            public TheMkDocsIsSupportedVersionMethod()
            {
                _process = new FakeProcess();
                _process.SetStandardOutput(new[] { "mkdocs, version 1.0 from \\local\\python\\python36\\lib\\site-packages\\mkdocs (Python 3.6)" });
            }

            [Fact]
            public void Should_Return_Result_For_Default_Settings()
            {
                // Given
                var context = new MkDocsContextFixture(_process);

                // When
                var result = context.MkDocsIsSupportedVersion();

                // Then
                Assert.True(result);
            }

            [Fact]
            public void Should_Return_Result_For_Defined_Settings()
            {
                // Given
                var context = new MkDocsContextFixture(_process);
                var settings = new MkDocsVersionSettings();

                // When
                var result = context.MkDocsIsSupportedVersion(settings);

                // Then
                Assert.True(result);
            }

            [Fact]
            public void Should_Throw_For_Null_Context()
            {
                // Given
                var settings = new MkDocsVersionSettings();

                // When
                var result = Record.Exception(() => MkDocsAliases.MkDocsIsSupportedVersion(null, settings));

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("context", ((ArgumentNullException)result).ParamName);
            }
        }

        public sealed class TheMkDocsNewMethod
        {
            [Fact]
            public void Should_Not_Throw_For_Default_Settings_For_Working_Directory()
            {
                // Given
                var context = new MkDocsContextFixture();

                // When
                var result = Record.Exception(() => context.MkDocsNew());

                // Then
                Assert.Null(result);
            }

            [Fact]
            public void Should_Not_Throw_For_Default_Settings()
            {
                // Given
                var context = new MkDocsContextFixture();
                var projectDir = new DirectoryPath("./project");

                // When
                var result = Record.Exception(() => context.MkDocsNew(projectDir));

                // Then
                Assert.Null(result);
            }

            [Fact]
            public void Should_Not_Throw_For_Defined_Settings_For_Working_Directory()
            {
                // Given
                var context = new MkDocsContextFixture();
                var settings = new MkDocsNewSettings();

                // When
                var result = Record.Exception(() => context.MkDocsNew(settings));

                // Then
                Assert.Null(result);
            }

            [Fact]
            public void Should_Not_Throw_For_Defined_Settings()
            {
                // Given
                var context = new MkDocsContextFixture();
                var settings = new MkDocsNewSettings();
                var projectDir = new DirectoryPath("./project");

                // When
                var result = Record.Exception(() => context.MkDocsNew(projectDir, settings));

                // Then
                Assert.Null(result);
            }

            [Fact]
            public void Should_Throw_For_Null_Context_For_Working_Directory()
            {
                // Given
                var settings = new MkDocsNewSettings();

                // When
                var result = Record.Exception(() => MkDocsAliases.MkDocsNew(null, settings));

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("context", ((ArgumentNullException)result).ParamName);
            }

            [Fact]
            public void Should_Throw_For_Null_Context()
            {
                // Given
                var settings = new MkDocsNewSettings();
                var projectDir = new DirectoryPath("./project");

                // When
                var result = Record.Exception(() => MkDocsAliases.MkDocsNew(null, projectDir, settings));

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("context", ((ArgumentNullException)result).ParamName);
            }
        }

        public sealed class TheMkDocsBuildMethod
        {
            [Fact]
            public void Should_Not_Throw_For_Default_Settings_For_Working_Directory()
            {
                // Given
                var context = new MkDocsContextFixture();

                // When
                var result = Record.Exception(() => context.MkDocsBuild());

                // Then
                Assert.Null(result);
            }

            [Fact]
            public void Should_Not_Throw_For_Default_Settings()
            {
                // Given
                var context = new MkDocsContextFixture();
                var projectDir = new DirectoryPath("./project");

                // When
                var result = Record.Exception(() => context.MkDocsBuild(projectDir));

                // Then
                Assert.Null(result);
            }

            [Fact]
            public void Should_Not_Throw_For_Defined_Settings_For_Working_Directory()
            {
                // Given
                var context = new MkDocsContextFixture();
                var settings = new MkDocsBuildSettings();

                // When
                var result = Record.Exception(() => context.MkDocsBuild(settings));

                // Then
                Assert.Null(result);
            }

            [Fact]
            public void Should_Not_Throw_For_Defined_Settings()
            {
                // Given
                var context = new MkDocsContextFixture();
                var settings = new MkDocsBuildSettings();
                var projectDir = new DirectoryPath("./project");

                // When
                var result = Record.Exception(() => context.MkDocsBuild(projectDir, settings));

                // Then
                Assert.Null(result);
            }

            [Fact]
            public void Should_Throw_For_Null_Context_For_Working_Directory()
            {
                // Given
                var settings = new MkDocsBuildSettings();

                // When
                var result = Record.Exception(() => MkDocsAliases.MkDocsBuild(null, settings));

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("context", ((ArgumentNullException)result).ParamName);
            }

            [Fact]
            public void Should_Throw_For_Null_Context()
            {
                // Given
                var settings = new MkDocsBuildSettings();
                var projectDir = new DirectoryPath("./project");

                // When
                var result = Record.Exception(() => MkDocsAliases.MkDocsBuild(null, projectDir, settings));

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("context", ((ArgumentNullException)result).ParamName);
            }
        }

        public sealed class TheMkDocsServeMethod
        {
            [Fact]
            public void Should_Not_Throw_For_Default_Settings_For_Working_Directory()
            {
                // Given
                var context = new MkDocsContextFixture();

                // When
                var result = Record.Exception(() => context.MkDocsServe());

                // Then
                Assert.Null(result);
            }

            [Fact]
            public void Should_Not_Throw_For_Default_Settings()
            {
                // Given
                var context = new MkDocsContextFixture();
                var projectDir = new DirectoryPath("./project");

                // When
                var result = Record.Exception(() => context.MkDocsServe(projectDir));

                // Then
                Assert.Null(result);
            }

            [Fact]
            public void Should_Not_Throw_For_Defined_Settings_For_Working_Directory()
            {
                // Given
                var context = new MkDocsContextFixture();
                var settings = new MkDocsServeSettings();

                // When
                var result = Record.Exception(() => context.MkDocsServe(settings));

                // Then
                Assert.Null(result);
            }

            [Fact]
            public void Should_Not_Throw_For_Defined_Settings()
            {
                // Given
                var context = new MkDocsContextFixture();
                var settings = new MkDocsServeSettings();
                var projectDir = new DirectoryPath("./project");

                // When
                var result = Record.Exception(() => context.MkDocsServe(projectDir, settings));

                // Then
                Assert.Null(result);
            }

            [Fact]
            public void Should_Throw_For_Null_Context_For_Working_Directory()
            {
                // Given
                var settings = new MkDocsServeSettings();

                // When
                var result = Record.Exception(() => MkDocsAliases.MkDocsServe(null, settings));

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("context", ((ArgumentNullException)result).ParamName);
            }

            [Fact]
            public void Should_Throw_For_Null_Context()
            {
                // Given
                var settings = new MkDocsServeSettings();
                var projectDir = new DirectoryPath("./project");

                // When
                var result = Record.Exception(() => MkDocsAliases.MkDocsServe(null, projectDir, settings));

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("context", ((ArgumentNullException)result).ParamName);
            }
        }

        public sealed class TheMkDocsServeAsyncMethod
        {
            [Fact]
            public async void Should_Not_Throw_For_Default_Settings_For_Working_Directory()
            {
                // Given
                var context = new MkDocsContextFixture();

                // When
                var result = await (Record.ExceptionAsync(() => context.MkDocsServeAsync()) ?? Task.FromResult<Exception>(null));

                // Then
                Assert.Null(result);
            }

            [Fact]
            public async void Should_Not_Throw_For_Default_Settings()
            {
                // Given
                var context = new MkDocsContextFixture();
                var projectDir = new DirectoryPath("./project");

                // When
                var result = await (Record.ExceptionAsync(() => context.MkDocsServeAsync(projectDir)) ?? Task.FromResult<Exception>(null));

                // Then
                Assert.Null(result);
            }

            [Fact]
            public async void Should_Not_Throw_For_Defined_Settings_For_Working_Directory()
            {
                // Given
                var context = new MkDocsContextFixture();
                var settings = new MkDocsServeAsyncSettings();

                // When
                var result = await (Record.ExceptionAsync(() => context.MkDocsServeAsync(settings)) ?? Task.FromResult<Exception>(null));

                // Then
                Assert.Null(result);
            }

            [Fact]
            public async void Should_Not_Throw_For_Defined_Settings()
            {
                // Given
                var context = new MkDocsContextFixture();
                var settings = new MkDocsServeAsyncSettings();
                var projectDir = new DirectoryPath("./project");

                // When
                var result = await (Record.ExceptionAsync(() => context.MkDocsServeAsync(projectDir, settings)) ?? Task.FromResult<Exception>(null));

                // Then
                Assert.Null(result);
            }

            [Fact]
            public async void Should_Throw_For_Null_Context_For_Working_Directory()
            {
                // Given
                var settings = new MkDocsServeAsyncSettings();

                // When
                var result = await (Record.ExceptionAsync(() => MkDocsAliases.MkDocsServeAsync(null, settings)) ?? Task.FromResult<Exception>(null));

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("context", ((ArgumentNullException)result).ParamName);
            }

            [Fact]
            public async void Should_Throw_For_Null_Context()
            {
                // Given
                var settings = new MkDocsServeAsyncSettings();
                var projectDir = new DirectoryPath("./project");

                // When
                var result = await (Record.ExceptionAsync(() => MkDocsAliases.MkDocsServeAsync(null, projectDir, settings)) ?? Task.FromResult<Exception>(null));

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("context", ((ArgumentNullException)result).ParamName);
            }
        }

        public sealed class TheMkDocsGhDeployMethod
        {
            [Fact]
            public void Should_Not_Throw_For_Default_Settings_For_Working_Directory()
            {
                // Given
                var context = new MkDocsContextFixture();

                // When
                var result = Record.Exception(() => context.MkDocsGhDeploy());

                // Then
                Assert.Null(result);
            }

            [Fact]
            public void Should_Not_Throw_For_Default_Settings()
            {
                // Given
                var context = new MkDocsContextFixture();
                var projectDir = new DirectoryPath("./project");

                // When
                var result = Record.Exception(() => context.MkDocsGhDeploy(projectDir));

                // Then
                Assert.Null(result);
            }

            [Fact]
            public void Should_Not_Throw_For_Defined_Settings_For_Working_Directory()
            {
                // Given
                var context = new MkDocsContextFixture();
                var settings = new MkDocsGhDeploySettings();

                // When
                var result = Record.Exception(() => context.MkDocsGhDeploy(settings));

                // Then
                Assert.Null(result);
            }

            [Fact]
            public void Should_Not_Throw_For_Defined_Settings()
            {
                // Given
                var context = new MkDocsContextFixture();
                var settings = new MkDocsGhDeploySettings();
                var projectDir = new DirectoryPath("./project");

                // When
                var result = Record.Exception(() => context.MkDocsGhDeploy(projectDir, settings));

                // Then
                Assert.Null(result);
            }

            [Fact]
            public void Should_Throw_For_Null_Context_For_Working_Directory()
            {
                // Given
                var settings = new MkDocsGhDeploySettings();

                // When
                var result = Record.Exception(() => MkDocsAliases.MkDocsGhDeploy(null, settings));

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("context", ((ArgumentNullException)result).ParamName);
            }

            [Fact]
            public void Should_Throw_For_Null_Context()
            {
                // Given
                var settings = new MkDocsGhDeploySettings();
                var projectDir = new DirectoryPath("./project");

                // When
                var result = Record.Exception(() => MkDocsAliases.MkDocsGhDeploy(null, projectDir, settings));

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("context", ((ArgumentNullException)result).ParamName);
            }
        }
    }
}
