using SLABAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks
{
    public sealed class WindowsFormSink : IObserver<EventEntry>
    {
        Form1 form;
        public WindowsFormSink(Form1 form)
        {
            this.form = form;
        }

        public void OnNext(EventEntry entry)
        {
            if (entry != null)
            {
                this.form.AddData(entry.FormattedMessage,entry.Schema.Level.ToString(),entry.Timestamp.ToString());
            }
        }
        public void OnCompleted()
        {
        }
        public void OnError(Exception error)
        {
        }
    }
}
