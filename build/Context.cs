using Cake.Core;
using Cake.Frosting;

namespace Build
{
    public class Context : FrostingContext
    {
        public string Target { get; set; }
        public string Configuration { get; set; }
        public string BuildNumber { get; set; }
        public string OctoServer { get; set; }
        public string OctoApiKey { get; set; }
        public string OctoProject { get; set; }
        public string OctoTargetEnvironment { get; set; }
        public string OutputPath { get; set; }
        public string PackagePath { get; set; }
        public string ProjectPath { get; set; }
        public string BlogPath { get; set; }
        public string PackageFullName { get; set; }
        public bool IsContinuousIntegrationBuild { get; set; }

        public Context(ICakeContext context) 
            : base(context)
        {
        }
    }
}