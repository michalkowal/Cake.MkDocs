using System;
using System.Threading;
using Cake.Core.IO;
using Cake.Testing;

namespace Cake.MkDocs.Tests.Fixtures
{
    public static class MkDocsLongRunningFixtureExtensions
    {
        public static void GivenDefaultToolDoNotExist<TToolSettings>(
            this MkDocsAsyncFixture<TToolSettings> fixture)
            where TToolSettings : MkDocsAsyncSettings, new()
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

        public static void GivenSettingsToolPathExist<TToolSettings>(
            this MkDocsAsyncFixture<TToolSettings> fixture)
            where TToolSettings : MkDocsAsyncSettings, new()
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

        public static void GivenProcessCannotStart<TToolSettings>(
            this MkDocsAsyncFixture<TToolSettings> fixture)
            where TToolSettings : MkDocsAsyncSettings, new()
        {
            if (fixture == null)
            {
                throw new ArgumentNullException(nameof(fixture));
            }
            fixture.ProcessRunner.Process = null;
        }

        public static void GivenProcessExitsWithCode<TToolSettings>(
            this MkDocsAsyncFixture<TToolSettings> fixture, int exitCode)
            where TToolSettings : MkDocsAsyncSettings, new()
        {
            if (fixture == null)
            {
                throw new ArgumentNullException(nameof(fixture));
            }
            fixture.ProcessRunner.Process.SetExitCode(exitCode);
            fixture.GivenCancellationToken(CancellationToken.None);
        }

        public static void GivenEnvironmentPath<TToolSettings>(
            this MkDocsAsyncFixture<TToolSettings> fixture, string variable)
            where TToolSettings : MkDocsAsyncSettings, new()
        {
            fixture.Environment.SetEnvironmentVariable("PATH", variable);
            var filePath = new DirectoryPath(variable).CombineWithFilePath(new FilePath($"{MkDocsAsyncFixture<TToolSettings>.ToolFileName}.exe"));
            fixture.FileSystem.CreateFile(filePath);
        }

        public static void GivenRegistryFile<TToolSettings>(
            this MkDocsAsyncFixture<TToolSettings> fixture, string path)
            where TToolSettings : MkDocsAsyncSettings, new()
        {
            var filePath = new DirectoryPath(path).CombineWithFilePath(new FilePath($"{MkDocsAsyncFixture<TToolSettings>.ToolFileName}.exe"));
            fixture.FileSystem.CreateFile(filePath);
            fixture.Tools.RegisterFile(filePath);
        }

        public static void GivenCancellationToken<TToolSettings>(
            this MkDocsAsyncFixture<TToolSettings> fixture, CancellationToken token)
            where TToolSettings : MkDocsAsyncSettings, new()
        {
            fixture.Settings.Token = token;
            fixture.GivenThrowOperationCanceledException();
        }
    }
}
