using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Practices.EnterpriseLibrary.SemanticLogging.ApplicationInsights.Utility
{
    internal static class LogEventLevelExtensions
    {
        /// <summary>
        ///Converts an Event level object of Semantic Logging Application Block to a severity level object of Application Insights.
        /// </summary>
        /// <param name="logEventLevel">The log event level.</param>
        /// <returns>The severity level associated to that Event level object</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">The EventLevel is out of range</exception>
        public static SeverityLevel ToSeverityLevel(this EventLevel logEventLevel)
        {
            switch (logEventLevel)
            {
                case EventLevel.Verbose:
                    return SeverityLevel.Verbose;
                case EventLevel.Informational:
                    return SeverityLevel.Information;
                case EventLevel.Warning:
                    return SeverityLevel.Warning;
                case EventLevel.Error:
                    return SeverityLevel.Error;
                case EventLevel.Critical:
                    return SeverityLevel.Critical;
                case EventLevel.LogAlways:
                    return SeverityLevel.Information;
                default:
                    throw new ArgumentOutOfRangeException("EventLevel", logEventLevel, "The EventLevel is not recognized or out of range.");
            }
        }

    }
}

