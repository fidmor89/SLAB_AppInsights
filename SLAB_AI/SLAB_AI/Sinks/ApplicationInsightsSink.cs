using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Threading;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.ApplicationInsights.Utility;

namespace Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks
{

    public sealed class ApplicationInsightsSink : IObserver<EventEntry>
    {
        /// <summary>
        /// TelemetryClient used for sending logs to Application Insights
        /// </summary>
        private TelemetryClient telemetryClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationInsightsSink" /> class with the specified Instrumentation Key and the optional telemetryInitialiazers.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if InstrumentationKey value is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the InstrumentationKey is empty</exception>
        /// <param name="InstrumentationKey">The ID that determines the application component under which your data appears in Application Insights.</param>
        /// <param name="telemetryInitializers">The (optional) Application Insights telemetry initializers.</param>
        public ApplicationInsightsSink(String InstrumentationKey, params ITelemetryInitializer[] telemetryInitializers)
        {
            telemetryClient = new TelemetryClient();
            checkIkey(InstrumentationKey);

            telemetryClient.InstrumentationKey = InstrumentationKey;

            addInitializers(telemetryInitializers);
        }
        
        /// <sumary>
        /// Class destructor. Ensures all telemtry items are sent before object destruction.
        /// This is done through a manual flush of the Telemetry Client object.
        /// </sumary>
        ~ApplicationInsightsSink() {
            this.telemetryClient.Flush();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationInsightsSink" /> class and uses the default Instrumentation Key In the config file.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if InstrumentationKey value is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the InstrumentationKey is empty</exception>
        /// <param name="telemetryInitializers">The (optional) Application Insights telemetry initializers.</param>
        public ApplicationInsightsSink(params ITelemetryInitializer[] telemetryInitializers)
        {
            telemetryClient = new TelemetryClient();
            checkIkey(TelemetryConfiguration.Active.InstrumentationKey);

            addInitializers(telemetryInitializers);
        }

        /// <summary>
        /// Helper method to add initializers into <see cref="TelemetryConfiguration.Active.TelemetryInitializers"/>
        /// </summary>
        /// <param name="telemetryInitializers">The (optional) Application Insights telemetry initializers.</param>
        private static void addInitializers(ITelemetryInitializer[] telemetryInitializers)
        {
            if (telemetryInitializers != null)
            {
                foreach (var telemetryInitializer in telemetryInitializers)
                {
                    TelemetryConfiguration.Active.TelemetryInitializers.Add(telemetryInitializer);
                }
            }
        }

        /// <summary>
        /// Method used to check if the iKey provided is not null, whitespace or empty.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if InstrumentationKey value is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the InstrumentationKey is empty</exception>
        /// <param name="iKey"></param>
        private static void checkIkey(String iKey)
        {
            if (String.IsNullOrWhiteSpace(iKey))
            {
                throw new ArgumentNullException("Instrumentation Key");
            }
            if (iKey.Length == 0)
            {
                throw new ArgumentException("The Instrumentation Key is empty", "Instrumentation Key");
            }
        }

        /// <summary>
        /// Provides the sink with new data to write to Application Insights.
        /// </summary>
        /// <param name="value">The current entry.</param>
        public void OnNext(EventEntry value)
        {
            if (value != null)
            {
                EventEntryToAITrace(value);
            }
        }

        /// <summary>
        /// Notifies the observer that the provider has finished sending push-based notifications.
        /// </summary>
        public void OnCompleted()
        {
            Console.WriteLine("Additional telemetry data will not be transmitted.");
        }

        /// <summary>
        /// Notifies the observer that the provider has experienced an error condition.
        /// </summary>
        /// <param name="error">An object that provides additional information about the error.</param>
        public void OnError(Exception error)
        {

        }

        /// <summary>
        /// Gets the Semantic Logging Application Block log and convert it to Application Insights TraceTelemetry
        /// </summary>
        /// <param name="value">The current entry.</param>
        private void EventEntryToAITrace(EventEntry value)
        {
            int i; // iterator
            TraceTelemetry trace = new TraceTelemetry();
            if (!String.IsNullOrEmpty(value.FormattedMessage))
            {
                trace.Message = value.FormattedMessage;
            }
            trace.SeverityLevel = LogEventLevelExtensions.ToSeverityLevel(value.Schema.Level);
            trace.Timestamp = value.Timestamp;

            #region EventSchema Properties
            trace.Properties.Add("Event Name", value.Schema.EventName);
            if (value.Schema.KeywordsDescription != null)
            {
                trace.Properties.Add("Keywords", value.Schema.KeywordsDescription);
            }
            trace.Properties.Add("Operation Code Name", value.Schema.OpcodeName);
            trace.Properties.Add("Provider Id", value.Schema.ProviderId.ToString());
            trace.Properties.Add("Provider Name", value.Schema.ProviderName);
            trace.Properties.Add("Task", value.Schema.Task.ToString());
            trace.Properties.Add("Task Name", value.Schema.TaskName);
            trace.Properties.Add("Event Version", value.Schema.Version.ToString());

            #endregion EventSchema Properties

            #region EventValue Properties

            trace.Properties.Add("Activity Id", value.ActivityId.ToString());
            trace.Properties.Add("Event Id", value.EventId.ToString());
            if (value.Payload != null)
            {
                i = 0;
                foreach (object o in value.Payload)
                {
                    var payloadValue = o == null ? "null" : o.ToString();
                    trace.Properties.Add("Payload " + value.Schema.Payload[i], payloadValue);
                    i++;
                }
            }
            trace.Properties.Add("Process Id", value.ProcessId.ToString());
            trace.Properties.Add("Thread Id", value.ThreadId.ToString());
            #endregion EventValue Properties

            telemetryClient.TrackTrace(trace);                                          //call the TrackTrace method to send the log to Application Insights
        }
        
    }

}
