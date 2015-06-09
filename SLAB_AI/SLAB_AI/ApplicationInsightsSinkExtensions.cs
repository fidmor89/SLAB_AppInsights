using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Practices.EnterpriseLibrary.SemanticLogging.SLAB_AI
{
    public static class ApplicationInsightsSinkExtensions
    {
        public static EventListener CreateListener()
        {
            var listener = new ObservableEventListener();
            listener.LogToTrackTrace(host, port, recipients, subject, credentials, formatter);
            return listener;
        }
        public static SinkSubscription<ApplicationInsightsSink> LogToTrackTrace(this IObservable<EventEntry> eventStream)
        {
         var sink = new ApplicationInsightsSink(String InstrumentationKey);
         var subscription = eventStream.Subscribe(sink);
         return new SinkSubscription<ApplicationInsightsSink>(subscription, sink);
        }
    }
}
