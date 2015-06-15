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

namespace Microsoft.Practices.EnterpriseLibrary.SemanticLogging.SLAB_AI
{
    public sealed class ApplicationInsightsSink : IObserver<EventEntry>
    {
        private TelemetryClient telemetryClient;
        /// <summary>
        /// Provides the sink with new data to write to Application Insights.
        /// </summary>
        /// <param name="InstrumentationKey">The current entry.</param>
        /// <param name="contextInitializers"></param>
        public ApplicationInsightsSink(String InstrumentationKey,params IContextInitializer[] contextInitializers)
        {
            telemetryClient = new TelemetryClient();
            TelemetryConfiguration.Active.InstrumentationKey = InstrumentationKey;
            if (contextInitializers != null)
            {
                foreach (var contextInitializer in contextInitializers)
                {
                    TelemetryConfiguration.Active.ContextInitializers.Add(contextInitializer);
                }
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

        }
        /// <summary>
        /// Notifies the observer that the provider has experienced an error condition.
        /// </summary>
        /// <param name="error">An object that provides additional information about the error.</param>
        public void OnError(Exception error)
        {

        }
        /// <summary>
        /// Gets the SLAB log and convert to ApplicationInsights TraceTelemetry
        /// </summary>
        /// <param name="value">The current entry.</param>
        private void EventEntryToAITrace(EventEntry value)
        {
            int i; // iterator
            TraceTelemetry trace = new TraceTelemetry(value.FormattedMessage, LogEventLevelExtensions.ToSeverityLevel(value.Schema.Level));
            trace.Timestamp = value.Timestamp;

            #region EventSchema Properties

            trace.Properties.Add("Id", value.Schema.Id.ToString());
            trace.Properties.Add("Event Name", value.Schema.EventName);
            //trace.Properties.Add("Keywords", value.Schema.Keywords.ToString()) Is this needed? .ToString() is deprecated
            trace.Properties.Add("Keywords", value.Schema.KeywordsDescription);
            //trace.Properties.Add("Operation Code", value.Schema.Opcode.ToString()); Is this needed? .ToString() is deprecated
            trace.Properties.Add("Operation Code Name", value.Schema.OpcodeName);
            // Review if Payload is necesary and what's the best name for each one (key identifier on trace properties dictionary).
            if (value.Schema.Payload != null) 
            {
                for (i = 0; i < value.Schema.Payload.Length; i++)
                {
                    trace.Properties.Add("Paylod " + i.ToString(), value.Schema.Payload[i]);
                }
            }
            trace.Properties.Add("Provider Id", value.Schema.ProviderId.ToString()); // Is this needed?
            trace.Properties.Add("Provider Name", value.Schema.ProviderName);
            //trace.Properties.Add("Task", value.Schema.Task.ToString()); Is this needed? .ToString() is deprecated
            trace.Properties.Add("Task Name", value.Schema.TaskName);
            trace.Properties.Add("Event Version", value.Schema.Version.ToString());

            #endregion EventSchema Properties

            #region EventValue Properties

            trace.Properties.Add("Activity Id", value.ActivityId.ToString());
            trace.Properties.Add("Event Id", value.EventId.ToString()); // Is this equals to trace.Properties.Add("Id", value.Schema.Id.ToString());?
            // Review if Payload is necesary and what's the best name for each one (key identifier on trace properties dictionary).
            if (value.Payload != null)
            {
                i = 0;
                foreach (object o in value.Payload)
                {
                    trace.Properties.Add("Event Payload " + i.ToString(), o.ToString());
                    i++;
                }
            }
            trace.Properties.Add("Process Id", value.ProcessId.ToString());
            trace.Properties.Add("Event Provider Id", value.ProviderId.ToString()); // Is this equals to  trace.Properties.Add("Provider Id", value.Schema.ProviderId.ToString());
            trace.Properties.Add("Related Activity Id", value.RelatedActivityId.ToString());
            trace.Properties.Add("Thread Id", value.ThreadId.ToString());
            trace.Properties.Add("Formatted TimeStamp", value.GetFormattedTimestamp((new CultureInfo(Thread.CurrentThread.CurrentCulture.LCID)).DateTimeFormat.FullDateTimePattern)); // Is this needed? Can it replace or be replaced by trace.Timestamp = value.Timestamp;

            #endregion EventValue Properties

            telemetryClient.TrackTrace(trace);
            telemetryClient.Flush();
        }
    }
}
