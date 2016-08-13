using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.ApplicationInsights.Utility;
using System;
using System.Collections.Generic;

namespace Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks
{
    public sealed class ApplicationInsightsSink : IObserver<EventEntry>
    {
        /// <summary>
        /// TelemetryClient used for sending logs to Application Insights
        /// </summary>
        private readonly TelemetryClient _telemetryClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationInsightsSink" /> class with the specified Instrumentation Key and the optional telemetryInitialiazers.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if InstrumentationKey value is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the InstrumentationKey is empty</exception>
        /// <param name="instrumentationKey">The ID that determines the application component under which your data appears in Application Insights.</param>
        /// <param name="telemetryInitializers">The (optional) Application Insights telemetry initializers.</param>
        public ApplicationInsightsSink(string instrumentationKey, params ITelemetryInitializer[] telemetryInitializers)
        {
            _telemetryClient = new TelemetryClient();
            CheckInstrumentationkey(instrumentationKey);

            _telemetryClient.InstrumentationKey = instrumentationKey;

            AddInitializers(telemetryInitializers);
        }
        
        /// <sumary>
        /// Class destructor. Ensures all telemetry items are sent before object destruction.
        /// This is done through a manual flush of the Telemetry Client object.
        /// </sumary>
        ~ApplicationInsightsSink() {
            _telemetryClient.Flush();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationInsightsSink" /> class and uses the default Instrumentation Key In the config file.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if InstrumentationKey value is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the InstrumentationKey is empty</exception>
        /// <param name="telemetryInitializers">The (optional) Application Insights telemetry initializers.</param>
        public ApplicationInsightsSink(params ITelemetryInitializer[] telemetryInitializers)
        {
            _telemetryClient = new TelemetryClient();
            CheckInstrumentationkey(TelemetryConfiguration.Active.InstrumentationKey);

            AddInitializers(telemetryInitializers);
        }

        /// <summary>
        /// Helper method to add initializers into <see cref="TelemetryConfiguration.TelemetryInitializers"/> in <see cref="TelemetryConfiguration.Active"/>
        /// </summary>
        /// <param name="telemetryInitializers">The (optional) Application Insights telemetry initializers.</param>
        private static void AddInitializers(IEnumerable<ITelemetryInitializer> telemetryInitializers)
        {
            foreach (var telemetryInitializer in telemetryInitializers)
            {
                TelemetryConfiguration.Active.TelemetryInitializers.Add(telemetryInitializer);
            }
        }

        /// <summary>
        /// Method used to check if the instrumentation key provided is not null, whitespace or empty.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if InstrumentationKey value is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the InstrumentationKey is empty</exception>
        /// <param name="instrumentationKey"></param>
        private static void CheckInstrumentationkey(string instrumentationKey)
        {
            if (String.IsNullOrWhiteSpace(instrumentationKey))
            {
                throw new ArgumentNullException(nameof(instrumentationKey));
            }
            if (instrumentationKey.Length == 0)
            {
                throw new ArgumentException("The Instrumentation Key is empty", nameof(instrumentationKey));
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
            var trace = new TraceTelemetry();
            if (!String.IsNullOrEmpty(value.FormattedMessage))
            {
                trace.Message = value.FormattedMessage;
            }
            trace.SeverityLevel = value.Schema.Level.ToSeverityLevel();
            trace.Timestamp = value.Timestamp;

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

            trace.Properties.Add("Activity Id", value.ActivityId.ToString());
            trace.Properties.Add("Event Id", value.EventId.ToString());
            if (value.Payload != null)
            {
                var i = 0; // iterator
                foreach (object o in value.Payload)
                {
                    var payloadValue = o?.ToString() ?? "null";
                    trace.Properties.Add("Payload " + value.Schema.Payload[i], payloadValue);
                    i++;
                }
            }
            trace.Properties.Add("Process Id", value.ProcessId.ToString());
            trace.Properties.Add("Thread Id", value.ThreadId.ToString());

            _telemetryClient.TrackTrace(trace);
        }   
    }
}
