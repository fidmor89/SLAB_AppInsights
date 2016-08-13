using System;
using System.Globalization;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

// ReSharper disable once CheckNamespace
namespace Microsoft.ApplicationInsights.Extensibility
{
    /// <summary>
    /// An <see cref="ITelemetryInitializer"/> implementation that populates the <see cref="SessionContext.Id"/> of an Application Insights <see cref="ITelemetry.Context"/>
    /// </summary>
    public class SessionContextInitializer : ITelemetryInitializer
    {
        /// <summary>
        /// Lazily builds the value for the <see cref="SessionContext.Id"/> property of the <see cref="TelemetryContext.Session"/> property in <see cref="ITelemetry.Context"/>.
        /// </summary>
        private readonly Lazy<string> _sessionId;

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionContextInitializer"/> class.
        /// </summary>
        /// <param name="sessionId">The session ID. If null, generated with <see cref="Guid.NewGuid"/>.</param>
        public SessionContextInitializer(string sessionId = null)
        {
            _sessionId = new Lazy<string>(() => String.IsNullOrWhiteSpace(sessionId) ? Guid.NewGuid().ToString(null, CultureInfo.InvariantCulture) : sessionId);
        }

        /// <summary>
        /// Initializes the given <see cref="Microsoft.ApplicationInsights.Channel.ITelemetry"/>.
        /// </summary>
        /// <param name="telemetry"></param>
        public void Initialize(ITelemetry telemetry)
        {
            if (String.IsNullOrWhiteSpace(telemetry.Context.Session.Id))
            {
                telemetry.Context.Session.Id = _sessionId.Value;
            }
        }
    }
}
