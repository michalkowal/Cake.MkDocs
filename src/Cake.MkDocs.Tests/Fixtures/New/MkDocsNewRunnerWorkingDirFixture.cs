using Cake.MkDocs.New;

namespace Cake.MkDocs.Tests.Fixtures.New
{
    public sealed class MkDocsNewRunnerWorkingDirFixture : MkDocsFixture<MkDocsNewSettings>
    {
        protected override void RunTool()
        {
            var tool = new MkDocsNewRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.New(Settings);
        }
    }
}
