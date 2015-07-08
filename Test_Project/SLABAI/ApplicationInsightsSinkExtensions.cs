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
    }
}
