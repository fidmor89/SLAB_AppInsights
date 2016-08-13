using System;
using System.Reflection;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace SemanticLogging.ApplicationInsights.Initializers
{
    public class ComponentVersionContextInitializer : ITelemetryInitializer
    {
        /// <summary>
        /// Lazily builds the value for the <see cref="ComponentContext.Version"/> property of the <see cref="TelemetryContext.Component"/> property in <see cref="ITelemetry.Context"/>.
        /// </summary>
        private readonly Lazy<string> _componentVersion;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentVersionContextInitializer" /> class.
        /// </summary>
        public ComponentVersionContextInitializer()
        {
            _componentVersion = new Lazy<string>(() => Assembly.GetEntryAssembly()?.ToString() ?? "Unknown");
        }

        #region Implementation of ITelemetryInitializer

        public void Initialize(ITelemetry telemetry)
        {
            if (String.IsNullOrWhiteSpace(telemetry.Context.Component.Version))
            {
                telemetry.Context.Component.Version = _componentVersion.Value;
            }
        }

        #endregion
    }
}
