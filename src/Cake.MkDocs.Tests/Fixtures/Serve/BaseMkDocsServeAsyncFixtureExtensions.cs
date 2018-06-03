using System;
using System.Threading;
using Cake.Core.IO;
using Cake.MkDocs.Tests.Fixtures.Serve;
using Cake.Testing;

namespace Cake.MkDocs.Tests.Fixtures
{
    public static class BaseMkDocsServeAsyncFixtureExtensions
    {
        public static void GivenDefaultToolDoNotExist(
            this BaseMkDocsServeAsyncFixture fixture)
        {
            if (fixture == null)
            {
                throw new ArgumentNullException(nameof(fixture));
            }
            var file = fixture.FileSystem.GetFile(fixture.DefaultToolPath);
            if (file.Exists)
            {
                file.Delete();
            }
        }

        public static void GivenSettingsToolPathExist(
            this BaseMkDocsServeAsyncFixture fixture)
        {
            if (fixture == null)
            {
                throw new ArgumentNullException(nameof(fixture));
            }
            if (fixture.Settings.ToolPath != null)
            {
                var path = fixture.Settings.ToolPath.MakeAbsolute(fixture.Environment);
                fixture.FileSystem.CreateFile(path);
            }
        }

        public static void GivenProcessCannotStart(
            this BaseMkDocsServeAsyncFixture fixture)
        {
            if (fixture == null)
            {
                throw new ArgumentNullException(nameof(fixture));
            }
            fixture.ProcessRunner.Process = null;
        }

        public static void GivenProcessExitsWithCode(
            this BaseMkDocsServeAsyncFixture fixture, int exitCode)
        {
            if (fixture == null)
            {
                throw new ArgumentNullException(nameof(fixture));
            }
            fixture.ProcessRunner.Process.SetExitCode(exitCode);
            fixture.GivenCancellationToken(CancellationToken.None);
        }

        public static void GivenEnvironmentPath(
            this BaseMkDocsServeAsyncFixture fixture, string variable)
        {
            fixture.Environment.SetEnvironmentVariable("PATH", variable);
            var filePath = new DirectoryPath(variable).CombineWithFilePath(new FilePath($"{BaseMkDocsServeAsyncFixture.ToolFileName}.exe"));
            fixture.FileSystem.CreateFile(filePath);
        }

        public static void GivenRegistryFile(
            this BaseMkDocsServeAsyncFixture fixture, string path)
        {
            var filePath = new DirectoryPath(path).CombineWithFilePath(new FilePath($"{BaseMkDocsServeAsyncFixture.ToolFileName}.exe"));
            fixture.FileSystem.CreateFile(filePath);
            fixture.Tools.RegisterFile(filePath);
        }

        public static void GivenCancellationToken(
            this BaseMkDocsServeAsyncFixture fixture, CancellationToken token)
        {
            fixture.Settings.Token = token;
            fixture.GivenThrowOperationCanceledException();
        }
    }
}
