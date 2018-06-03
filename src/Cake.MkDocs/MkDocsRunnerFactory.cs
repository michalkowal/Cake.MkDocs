using System;
using Cake.Core;
using Cake.MkDocs.Build;
using Cake.MkDocs.GhDeploy;
using Cake.MkDocs.New;
using Cake.MkDocs.Serve;
using Cake.MkDocs.Version;

namespace Cake.MkDocs
{
    internal static class MkDocsRunnerFactory
    {
        public static object CreateRunner<TSettings>(ICakeContext context)
            where TSettings : MkDocsSettings
        {
            object result = null;

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (typeof(TSettings) == typeof(MkDocsVersionSettings))
            {
                result = new MkDocsVersionRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            }
            else if (typeof(TSettings) == typeof(MkDocsNewSettings))
            {
                result = new MkDocsNewRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            }
            else if (typeof(TSettings) == typeof(MkDocsBuildSettings))
            {
                result = new MkDocsBuildRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            }
            else if (typeof(TSettings) == typeof(MkDocsServeSettings))
            {
                result = new MkDocsServeRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            }
            else if (typeof(TSettings) == typeof(MkDocsServeAsyncSettings))
            {
                result = new MkDocsServeAsyncRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            }
            else if (typeof(TSettings) == typeof(MkDocsGhDeploySettings))
            {
                result = new MkDocsGhDeployRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            }

            return result ?? throw new ArgumentException("Unknown settings.");
        }

        public static MkDocsVersionRunner CreateVersionRunner(ICakeContext context)
            => (MkDocsVersionRunner)CreateRunner<MkDocsVersionSettings>(context);

        public static MkDocsNewRunner CreateNewRunner(ICakeContext context)
            => (MkDocsNewRunner)CreateRunner<MkDocsNewSettings>(context);

        public static MkDocsBuildRunner CreateBuildRunner(ICakeContext context)
            => (MkDocsBuildRunner)CreateRunner<MkDocsBuildSettings>(context);

        public static MkDocsServeRunner CreateServeRunner(ICakeContext context)
            => (MkDocsServeRunner)CreateRunner<MkDocsServeSettings>(context);

        public static MkDocsServeAsyncRunner CreateServeAsyncRunner(ICakeContext context)
            => (MkDocsServeAsyncRunner)CreateRunner<MkDocsServeAsyncSettings>(context);

        public static MkDocsGhDeployRunner CreateGhDeployRunner(ICakeContext context)
            => (MkDocsGhDeployRunner)CreateRunner<MkDocsGhDeploySettings>(context);
    }
}
