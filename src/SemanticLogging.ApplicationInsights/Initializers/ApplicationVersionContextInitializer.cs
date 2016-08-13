using System;
using System.Reflection;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

// ReSharper disable once CheckNamespace
namespace Microsoft.ApplicationInsights.Extensibility
{
    /// <summary>
    /// An <see cref="ITelemetryInitializer"/> implementation that populates the <see cref="ComponentContext.Version"/> of an Application Insights <see cref="ITelemetry.Context"/>
    /// </summary>
    public class ApplicationVersionContextInitializer : ITelemetryInitializer
    {
        /// <summary>
        /// Lazily builds the value for the <see cref="ComponentContext.Version"/> property of the <see cref="TelemetryContext.Component"/> property in <see cref="ITelemetry.Context"/>.
        /// </summary>
        private readonly Lazy<string> _applicationVersion;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationVersionContextInitializer" /> class.
        /// </summary>
        /// <param name="applicationVersion">The application version. If null, calculated from <see cref="Assembly.GetEntryAssembly"/>.</param>
        public ApplicationVersionContextInitializer(string applicationVersion = null)
        {
            _applicationVersion = new Lazy<string>(() =>
                String.IsNullOrWhiteSpace(applicationVersion)
                    ? (Assembly.GetEntryAssembly()?.ToString() ?? Assembly.GetExecutingAssembly().ToString())
                    : applicationVersion);
        }

        #region Implementation of ITelemetryInitializer

        /// <summary>
        /// Initializes the given <see cref="Microsoft.ApplicationInsights.Channel.ITelemetry"/>.
        /// </summary>
        public void Initialize(ITelemetry telemetry)
        {
            if (String.IsNullOrWhiteSpace(telemetry.Context.Component.Version))
            {
                telemetry.Context.Component.Version = _applicationVersion.Value;
            }
        }

        #endregion
    }
}
