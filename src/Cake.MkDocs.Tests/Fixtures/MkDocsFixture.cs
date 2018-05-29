using Cake.Core.IO;
using Cake.Testing;
using Cake.Testing.Fixtures;

namespace Cake.MkDocs.Tests.Fixtures
{
    public abstract class MkDocsFixture<TSettings> : ToolFixture<TSettings>
        where TSettings : MkDocsSettings, new()
    {
        protected MkDocsFixture()
            : base("mkdocs")
        {
        }

        public void GivenEnvironmentPath(string variable)
        {
            Environment.SetEnvironmentVariable("PATH", variable);
            var filePath = new DirectoryPath(variable).CombineWithFilePath(new FilePath("mkdocs.exe"));
            FileSystem.CreateFile(filePath);
        }

        public void GivenRegistryFile(string path)
        {
            var filePath = new DirectoryPath(path).CombineWithFilePath(new FilePath("mkdocs.exe"));
            FileSystem.CreateFile(filePath);
            Tools.RegisterFile(filePath);
        }
    }
}
