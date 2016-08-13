﻿using System;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace Microsoft.Practices.EnterpriseLibrary.SemanticLogging.ApplicationInsights.Utility
{
    /// <summary>
    /// An <see cref="ITelemetryInitializer"/> implementation that appends the <see cref="Environment.OSVersion"/> to an Application Insights <see cref="ITelemetry.Context"/>
    /// </summary>
    public class OsVersionContextInitializer : ITelemetryInitializer
    {
        /// <summary>
        /// Lazily builds the value for the <see cref="DeviceContext.OperatingSystem"/> property for the <see cref="TelemetryContext.Device"/> property in <see cref="ITelemetry.Context"/>.
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
                    : String.Empty)
                : osVersion);
        }

        #region Implementation of ITelemetryInitializer

        /// <summary>
        /// Initializes the given <see cref="T:Microsoft.ApplicationInsights.Channel.ITelemetry"/>.
        /// </summary>
        public void Initialize(ITelemetry telemetry)
        {
            if (telemetry?.Context != null && String.IsNullOrWhiteSpace(telemetry.Context.Device.OperatingSystem))
            {
                telemetry.Context.Device.OperatingSystem = _osVersion.Value;
            }
        }

        #endregion
    }
}