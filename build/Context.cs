using Cake.Core;
using Cake.Frosting;
using JetBrains.Annotations;

namespace Build
{
    [UsedImplicitly]
    public class Context : FrostingContext
    {
        public string Target { get; set; }
        public string Configuration { get; set; }
        public string BuildNumber { get; set; }
        public string BinariesDirectoryPath { get; set; }
        public string ArtifactDirectoryPath { get; set; }
        public string ProjectPath { get; set; }
        public string BlogPath { get; set; }
        public string PackageFullName { get; set; }

        public Context(ICakeContext context) 
            : base(context)
        {
        }
    }
}