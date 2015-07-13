using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Practices.EnterpriseLibrary.SemanticLogging
{
    public static class ApplicationInsightsSinkExtensions
    {
        /// <summary>
        /// Creates an event listener that logs using a <see cref="ApplicationInsightsSink" />.
        /// </summary>
        /// <param name="InstrumentationKey">The ID that determines the application component under which your data appears in Application Insights.</param>
        /// <param name="TelemetryInitializer">The (optional) Application Insights telemetry initializers.</param>
        /// <returns>
        /// An event listener that uses <see cref="ApplicationInsightsSink" /> to log events.
        /// </returns>
        public static EventListener CreateListener(String InstrumentationKey, params ITelemetryInitializer[] telemetryInitializers)
        {
            var listener = new ObservableEventListener();
            listener.LogToApplicationInsights(InstrumentationKey, telemetryInitializers);
            return listener;
        }

        /// <summary>
        /// Creates an event listener that logs using a <see cref="ApplicationInsightsSink" />.
        /// </summary>
        /// <param name="TelemetryInitializer">The (optional) Application Insights telemetry initializers.</param>
        /// <returns>
        /// An event listener that uses <see cref="ApplicationInsightsSink" /> to log events.
        /// </returns>
        public static EventListener CreateListener(params ITelemetryInitializer[] telemetryInitializers)
        {
            var listener = new ObservableEventListener();
            listener.LogToApplicationInsights(telemetryInitializers);
            return listener;
        }

        /// <summary>
        /// Subscribes to an <see cref="IObservable{EventEntry}" /> using a <see cref="ApplicationInsightsSink" />.
        /// </summary>
        /// <param name="eventStream">The event stream. Typically this is an instance of <see cref="ObservableEventListener" />.</param>
        /// <param name="InstrumentationKey">The ID that determines the application component under which your data appears in Application Insights.</param>
        /// <param name="telemetryInitializers">The (optional) Application Insights telemetry initializers.</param>
        /// <returns>
        /// A subscription to the sink that can be disposed to unsubscribe the sink and dispose it, or to get access to the sink instance.
        /// </returns>
        public static SinkSubscription<ApplicationInsightsSink> LogToApplicationInsights(this IObservable<EventEntry> eventStream,
      String InstrumentationKey, params ITelemetryInitializer[] telemetryInitializers)
        {
            var sink = initializeSink(telemetryInitializers, InstrumentationKey);
            var subscription = eventStream.Subscribe(sink);
            return new SinkSubscription<ApplicationInsightsSink>(subscription, sink);
        }

        /// <summary>
        /// Subscribes to an <see cref="IObservable{EventEntry}" /> using a <see cref="ApplicationInsightsSink" />.
        /// </summary>
        /// <param name="eventStream">The event stream. Typically this is an instance of <see cref="ObservableEventListener" />.</param>
        /// <param name="telemetryInitializers">The (optional) Application Insights telemetry initializers.</param>
        /// <returns>
        /// A subscription to the sink that can be disposed to unsubscribe the sink and dispose it, or to get access to the sink instance.
        /// </returns>
        public static SinkSubscription<ApplicationInsightsSink> LogToApplicationInsights(this IObservable<EventEntry> eventStream, params ITelemetryInitializer[] telemetryInitializers)
        {
            var sink = initializeSink(telemetryInitializers);
            var subscription = eventStream.Subscribe(sink);
            return new SinkSubscription<ApplicationInsightsSink>(subscription, sink);
        }

        private static ApplicationInsightsSink initializeSink(ITelemetryInitializer[] telemetryInitializers, String InstrumentationKey = null)
        {
            if (InstrumentationKey != null)
                return new ApplicationInsightsSink(InstrumentationKey, telemetryInitializers);
            else
                return new ApplicationInsightsSink(telemetryInitializers);
        }
    }
}
