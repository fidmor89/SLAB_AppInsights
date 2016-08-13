using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Diagnostics;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks
{
    public sealed class ApplicationInsightsSink : IObserver<EventEntry>
    {
        /// <summary>
        /// TelemetryClient used for sending logs to Application Insights
        /// </summary>
        private readonly TelemetryClient _telemetryClient;

        /// <summary>
        /// Indicated whether the sink has been disposed or not
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationInsightsSink" /> class and uses the default Instrumentation Key In the config file.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if InstrumentationKey value is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the InstrumentationKey is empty</exception>
        /// <param name="telemetryInitializers">The (optional) Application Insights telemetry initializers.</param>
        public ApplicationInsightsSink(params ITelemetryInitializer[] telemetryInitializers)
            : this(TelemetryConfiguration.Active.InstrumentationKey, telemetryInitializers)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationInsightsSink" /> class with the specified Instrumentation Key and the optional telemetryInitialiazers.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if InstrumentationKey value is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the InstrumentationKey is empty</exception>
        /// <param name="instrumentationKey">The ID that determines the application component under which your data appears in Application Insights.</param>
        /// <param name="telemetryInitializers">The (optional) Application Insights telemetry initializers.</param>
        public ApplicationInsightsSink(string instrumentationKey, params ITelemetryInitializer[] telemetryInitializers)
        {
            if (instrumentationKey == null)
            {
                throw new ArgumentNullException(nameof(instrumentationKey));
            }
            if (String.IsNullOrWhiteSpace(instrumentationKey))
            {
                throw new ArgumentException("The Instrumentation Key is empty or consists solely of whitespace", nameof(instrumentationKey));
            }

            _telemetryClient = new TelemetryClient {InstrumentationKey = instrumentationKey};

            foreach (var telemetryInitializer in telemetryInitializers)
            {
                TelemetryConfiguration.Active.TelemetryInitializers.Add(telemetryInitializer);
            }
        }
        
        ~ApplicationInsightsSink()
        {
            Dispose(false);
        }

        /// <summary>
        /// Provides the sink with new data to write to Application Insights.
        /// </summary>
        /// <param name="value">The current entry.</param>
        public void OnNext(EventEntry value)
        {
            if (value != null)
            {
                Track(value);
            }
        }

        /// <summary>
        /// Notifies the observer that the provider has finished sending push-based notifications.
        /// </summary>
        public void OnCompleted()
        {
            Dispose(true);
        }

        /// <summary>
        /// Notifies the observer that the provider has experienced an error condition.
        /// </summary>
        /// <param name="error">An object that provides additional information about the error.</param>
        public void OnError(Exception error)
        {
            _telemetryClient.TrackException(new InvalidOperationException("ApplicationInsightsSink.OnError() called", error));
        }

        /// <summary>
        /// Gets the Semantic Logging Application Block log and converts it to Application Insights TraceTelemetry
        /// </summary>
        /// <param name="entry">The current event entry.</param>
        private void Track(EventEntry entry)
        {
            var eventTelemetry = new EventTelemetry
            {
                Name = entry.Schema.EventName,
                Timestamp = entry.Timestamp
            };

            eventTelemetry.Properties.Add("ProcessId", entry.ProcessId.ToString(CultureInfo.InvariantCulture));
            eventTelemetry.Properties.Add("ThreadId", entry.ThreadId.ToString(CultureInfo.InvariantCulture));
            eventTelemetry.Properties.Add("ActivityId", entry.ActivityId.ToString(null, CultureInfo.InvariantCulture));
            eventTelemetry.Properties.Add("RelatedActivityId", entry.RelatedActivityId.ToString(null, CultureInfo.InvariantCulture));
            eventTelemetry.Properties.Add("Level", entry.Schema.Level.ToString());
            eventTelemetry.Properties.Add("EventId", entry.EventId.ToString(CultureInfo.InvariantCulture));
            eventTelemetry.Properties.Add("Version", entry.Schema.Version.ToString(CultureInfo.InvariantCulture));
            eventTelemetry.Properties.Add("Message", entry.FormattedMessage);
            eventTelemetry.Properties.Add("Keywords", entry.Schema.KeywordsDescription);
            eventTelemetry.Properties.Add("Task", entry.Schema.Task.ToString());
            eventTelemetry.Properties.Add("TaskName", entry.Schema.TaskName);
            eventTelemetry.Properties.Add("OpCode", entry.Schema.Opcode.ToString());
            eventTelemetry.Properties.Add("OpCodeName", entry.Schema.OpcodeName);
            eventTelemetry.Properties.Add("ProviderId", entry.ProviderId.ToString(null, CultureInfo.InvariantCulture));
            eventTelemetry.Properties.Add("ProviderName", entry.Schema.ProviderName);

            if (entry.Payload.Count != entry.Schema.Payload.Length)
            {
                eventTelemetry.Properties.Add("MismatchedPayloadSchema", 
                    $"Schema: {String.Join(",", entry.Schema.Payload)}; Payload: {String.Join(",", entry.Payload)}");
            }
            else
            {
                for (int i = 0; i < entry.Payload.Count; i++)
                {
                    eventTelemetry.Properties.Add($"Payload_{entry.Schema.Payload[i]}", entry.Payload[i]?.ToString());
                }
            }

            _telemetryClient.TrackEvent(eventTelemetry);
        }

        /// <sumary>
        /// Ensures all telemetry items are sent before object destruction.
        /// This is done by calling <see cref="TelemetryClient.Flush"/>.
        /// </sumary>
        private void Dispose(bool completing)
        {
            if (_disposed)
            {
                return;
            }

            if (!completing)
            {
                //we shouldn't reach here - it means ObservableEventListener.Dispose() wasn't called
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }
                else
                {
                    _telemetryClient.TrackException(new InvalidOperationException($"{nameof(ApplicationInsightsSink)} was not disposed"));
                }
            }

            _telemetryClient.Flush();

            _disposed = true;
        }
    }
}
