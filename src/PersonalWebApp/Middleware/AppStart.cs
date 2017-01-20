using System;
using System.Threading;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace PersonalWebApp.Middleware
{
    public class AppStart : IStartupFilter
    {
        private readonly IApplicationLifetime _appLifetime;
        private readonly CachedWebRootFileProvider _cachedWebRoot;
        private readonly TelemetryClient _telemetry;

        public AppStart(IApplicationLifetime appLifetime, TelemetryClient telemetry, CachedWebRootFileProvider cachedWebRoot)
        {
            _appLifetime = appLifetime;
            _telemetry = telemetry;
            _telemetry.Context.Properties["MachineName"] = Environment.MachineName;
            _cachedWebRoot = cachedWebRoot;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next) => app =>
        {
            // Enable tracking of application start/stop to Application Insights
            if (_telemetry.IsEnabled())
            {
                _appLifetime.ApplicationStarted.Register(() =>
                {
                    var startedEvent = new EventTelemetry($"Application Started @{Environment.MachineName}");
                    _telemetry.TrackEvent(startedEvent);
                });
                _appLifetime.ApplicationStopping.Register(() =>
                {
                    var startedEvent = new EventTelemetry($"Application Stopping @{Environment.MachineName}");
                    _telemetry.TrackEvent(startedEvent);
                });
                _appLifetime.ApplicationStopped.Register(() =>
                {
                    var stoppedEvent = new EventTelemetry($"Application Stopped @{Environment.MachineName}");
                    _telemetry.TrackEvent(stoppedEvent);
                    _telemetry.Flush();

                    // Allow some time for flushing before shutdown.
                    Thread.Sleep(2000);
                });
            }

            // Call next now so that the ILoggerFactory by Startup.Configure is configured before we go any further
            next(app);

            // Prime the cached web root file provider for static file serving
            _cachedWebRoot.PrimeCache();
        };
    }
}
