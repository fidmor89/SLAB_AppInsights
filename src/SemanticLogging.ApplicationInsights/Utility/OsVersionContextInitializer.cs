using System;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Microsoft.Practices.EnterpriseLibrary.SemanticLogging.ApplicationInsights.Utility
{
    /// <summary>
    /// An <see cref="ITelemetryInitializer"/> implementation that appends the <see cref="Environment.OSVersion"/> to an Application Insights <see cref="ITelemetry.Context"/>
    /// </summary>
    public class OsVersionContextInitializer : ITelemetryInitializer
    {
        /// <summary>
        /// Builds the (lazy) Value for the .OperatingSystem property for the <see cref="ITelemetry.Context.Device"/> information.
        /// </summary>
        private readonly Lazy<string> _osVersion;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        /// <param name="osVersion">The os version.</param>
        public OsVersionContextInitializer(string osVersion = null)
        {
            _osVersion = new Lazy<string>(() => string.IsNullOrWhiteSpace(osVersion)
                ? (Environment.OSVersion != null
                    ? Environment.OSVersion.ToString()
                    : string.Empty)
                : osVersion);
        }

        #region Implementation of ITelemetryInitializer

        /// <summary>
        /// Initializes the given <see cref="T:Microsoft.ApplicationInsights.Channel.ITelemetry"/>.
        /// </summary>
        public void Initialize(Microsoft.ApplicationInsights.Channel.ITelemetry telemetry)
        {
            if (telemetry != null && telemetry.Context != null)
            {
                if (String.IsNullOrWhiteSpace(telemetry.Context.Device.OperatingSystem))
                {
                    telemetry.Context.Device.OperatingSystem = _osVersion.Value;
                }
            }
        }
        
        #endregion
    }
}