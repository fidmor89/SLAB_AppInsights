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
        /// <param name="contextInitializers">The (optional) Application Insights context initializers.</param>
        /// <returns>
        /// An event listener that uses <see cref="ApplicationInsightsSink" /> to log events.
        /// </returns>
        public static EventListener CreateListener(String InstrumentationKey, params IContextInitializer[] contextInitializers)
        {
            var listener = new ObservableEventListener();
            listener.LogToApplicationInsights(InstrumentationKey, contextInitializers);
            return listener;
        }

        /// <summary>
        /// Subscribes to an <see cref="IObservable{EventEntry}" /> using a <see cref="ApplicationInsightsSink" />.
        /// </summary>
        /// <param name="eventStream">The event stream. Typically this is an instance of <see cref="ObservableEventListener" />.</param>
        /// <param name="InstrumentationKey">The ID that determines the application component under which your data appears in Application Insights.</param>
        /// <param name="contextInitializers">The (optional) Application Insights context initializers.</param>
        /// <returns>
        /// A subscription to the sink that can be disposed to unsubscribe the sink and dispose it, or to get access to the sink instance.
        /// </returns>
        public static SinkSubscription<ApplicationInsightsSink> LogToApplicationInsights(this IObservable<EventEntry> eventStream,
      String InstrumentationKey, params IContextInitializer[] contextInitializers)
        {
            var sink = new ApplicationInsightsSink(InstrumentationKey, contextInitializers);
            var subscription = eventStream.Subscribe(sink);
            return new SinkSubscription<ApplicationInsightsSink>(subscription, sink);
        }
    }
}
