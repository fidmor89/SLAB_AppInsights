using System;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Microsoft.Practices.EnterpriseLibrary.SemanticLogging.ApplicationInsights.Utility
{
    /// <summary>
    /// An <see cref="ITelemetryInitializer"/> implementation that appends the applicationVersion to an Application Insights <see cref="ITelemetry.Context"/>
    /// </summary>
    public class ApplicationVersionContextInitializer : ITelemetryInitializer
    {
        /// <summary>
        /// Gets or sets the application version.
        /// </summary>
        /// <value>
        /// The application version.
        /// </value>
        private readonly string _applicationVersion;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationVersionContextInitializer"/> class.
        /// </summary>
        /// <param name="applicationVersion">The application version.</param>
        public ApplicationVersionContextInitializer(string applicationVersion)
        {
            if (applicationVersion == null) throw new ArgumentNullException("applicationVersion");

            _applicationVersion = applicationVersion;
        }

        #region Implementation of IContextInitializer

        /// <summary>
        /// Initializes the given <see cref="T:Microsoft.ApplicationInsights.Channel.ITelemetry"/>.
        /// </summary>
        public void Initialize(Microsoft.ApplicationInsights.Channel.ITelemetry telemetry)
        {
            if (telemetry != null && telemetry.Context != null)
            {
                if (String.IsNullOrWhiteSpace(telemetry.Context.Component.Version))
                {
                    telemetry.Context.Component.Version = _applicationVersion;
                }
            }
        }
        #endregion
    }
}