using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLAB_AI
{
    using Microsoft.ApplicationInsights.Extensibility;
    public class AI_Integration
    {

        /// <summary>
        /// Main constructor for the library project.
        /// </summary>
        /// <param name="IK">Instrumentation key to be used.</param>
        public AI_Integration(String IK)
        {
            if (!String.IsNullOrWhiteSpace(IK))
            {
                TelemetryConfiguration.Active.InstrumentationKey = IK;
            }
        }


        /// <summary>
        /// Instrumentation key setter, used if user wants to alternate IKs without creating new objects.
        /// </summary>
        /// <param name="IK">Instrumentation Key to set, passed as parameter</param>
        public void setInstrumentationKey(String IK)
        {
            if (String.IsNullOrWhiteSpace(IK) != null)
            {
                TelemetryConfiguration.Active.InstrumentationKey = IK;
            }
        }
    }
}
