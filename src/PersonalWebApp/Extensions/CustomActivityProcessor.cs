using System.Diagnostics;
using JetBrains.Annotations;
using OpenTelemetry;

namespace PersonalWebApp.Extensions;

[UsedImplicitly]
public sealed class CustomActivityProcessor : BaseProcessor<Activity>
{
    public override void OnEnd(Activity activity)
    {
        if (activity.DisplayName.Contains("/hc") || 
            activity.GetTagItem("http.target")?.ToString()?.Contains("/hc") == true)
        {
            activity.ActivityTraceFlags &= ~ActivityTraceFlags.Recorded;
            return;
        }
        
        base.OnEnd(activity);
    }
}