using System;
using Microsoft.ApplicationInsights.Channel;

// ReSharper disable once CheckNamespace
namespace Microsoft.ApplicationInsights.Extensibility
{
    /// <summary>
    /// An <see cref="ITelemetryInitializer"/> implementation that appends the application's version to an Application Insights <see cref="ITelemetry.Context"/>
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
            if (applicationVersion == null)
            {
                throw new ArgumentNullException(nameof(applicationVersion));
            }
            if (String.IsNullOrWhiteSpace(applicationVersion))
            {
                throw new ArgumentException("Application version is empty or consists solely of whitespace characters", nameof(applicationVersion));
            }

            _applicationVersion = applicationVersion;
        }

        #region Implementation of ITelemetryInitializer

        /// <summary>
        /// Initializes the given <see cref="Microsoft.ApplicationInsights.Channel.ITelemetry"/>.
        /// </summary>
        public void Initialize(ITelemetry telemetry)
        {
            if (String.IsNullOrWhiteSpace(telemetry.Context.Component.Version))
            {
                telemetry.Context.Component.Version = _applicationVersion;
            }
        }

        #endregion
    }}