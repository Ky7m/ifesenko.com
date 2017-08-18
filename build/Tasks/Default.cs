using Cake.Frosting;

namespace Build.Tasks
{
    [Dependency(typeof(Clean))]
    public sealed class Default : FrostingTask<Context>
    {
    }
}