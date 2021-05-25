using Cake.Common.IO;
using Cake.Frosting;

namespace Build.Tasks
{
    [Dependency(typeof(Publish))]
    public sealed class ZipFiles : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            context.Zip(context.BinariesDirectoryPath, context.PackageFullName);
        }
    }
}