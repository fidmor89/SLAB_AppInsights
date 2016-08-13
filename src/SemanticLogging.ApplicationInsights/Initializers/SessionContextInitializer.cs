using System;
using System.Globalization;
using Microsoft.ApplicationInsights.Channel;

// ReSharper disable once CheckNamespace
namespace Microsoft.ApplicationInsights.Extensibility
{
    public class SessionContextInitializer : ITelemetryInitializer
    {
        /// <summary>
        /// Initializes the given <see cref="Microsoft.ApplicationInsights.Channel.ITelemetry"/>.
        /// </summary>
        /// <param name="telemetry"></param>
        public void Initialize(ITelemetry telemetry)
        {
            if (String.IsNullOrWhiteSpace(telemetry.Context.Session.Id))
            {
                telemetry.Context.Session.Id = Guid.NewGuid().ToString(null, CultureInfo.InvariantCulture);
            }
        }
    }
}
