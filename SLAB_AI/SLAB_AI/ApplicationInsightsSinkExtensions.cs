using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Practices.EnterpriseLibrary.SemanticLogging.SLAB_AI
{
    public static class ApplicationInsightsSinkExtensions
    {
        public static EventListener CreateListener(String InstrumentationKey, params IContextInitializer[] contextInitializers)
        {
            var listener = new ObservableEventListener();
            listener.LogToApplicationInsights(InstrumentationKey,contextInitializers);
            return listener;
        }
        public static SinkSubscription<ApplicationInsightsSink> LogToApplicationInsights(this IObservable<EventEntry> eventStream,
            String InstrumentationKey, params IContextInitializer[] contextInitializers)
        {
            var sink = new ApplicationInsightsSink(InstrumentationKey,contextInitializers);
            var subscription = eventStream.Subscribe(sink);
            return new SinkSubscription<ApplicationInsightsSink>(subscription, sink);
        }
    }
}
