using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Microsoft.Practices.EnterpriseLibrary.SemanticLogging.SLAB_AI
{
    public sealed class ApplicationInsightsSink : IObserver<EventEntry>
    {
        private TelemetryClient telemetryClient;
        public ApplicationInsightsSink(String InstrumentationKey)
        {
            telemetryClient = new TelemetryClient();
            TelemetryConfiguration.Active.InstrumentationKey = InstrumentationKey;
            
        }
        /// <summary>
        /// Provides the sink with new data to write to Application Insights.
        /// </summary>
        /// <param name="value">The current entry.</param>
        public void OnNext(EventEntry value)
        {
            EventEntryToAITrace(value);
        }

        /// <summary>
        /// Notifies the observer that the provider has finished sending push-based notifications.
        /// </summary>
        public void OnCompleted()
        {

        }
        /// <summary>
        /// Notifies the observer that the provider has experienced an error condition.
        /// </summary>
        /// <param name="error">An object that provides additional information about the error.</param>
        public void OnError(Exception error)
        {

        }
        /// <summary>
        /// gets the SLAB log and convert to ApplicationInsights TraceTelemetry
        /// </summary>
        /// <param name="value">The current entry.</param>
        private void EventEntryToAITrace(EventEntry value)
        {
            TraceTelemetry trace = new TraceTelemetry();
            telemetryClient.TrackTrace(trace);
        }
    }
}
