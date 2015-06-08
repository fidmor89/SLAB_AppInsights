using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLAB_AI
{
    public class AI_Integration
    {

        private String sInstrumentationKey;

        /// <summary>
        /// Main constructor for the library project.
        /// </summary>
        /// <param name="IK">Instrumentation key to be used.</param>
        public AI_Integration(String IK)
        {
            this.sInstrumentationKey = IK;
        }


        /// <summary>
        /// Instrumentation key setter, used if user wants to alternate IKs without creating new objects.
        /// </summary>
        /// <param name="IK">Instrumentation Key to set, passed as parameter</param>
        public void setInstrumentationKey(String IK)
        {
            this.sInstrumentationKey = IK;
        }
    }
}
