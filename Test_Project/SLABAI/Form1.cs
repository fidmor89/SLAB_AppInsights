using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks;
namespace SLABAI
{
    public partial class Form1 : Form
    {
        private int cont;
        public Form1()
        {
            InitializeComponent();
            var listener1 = new ObservableEventListener();
            var listener2 = new ObservableEventListener();
            listener1.EnableEvents(ApplicationInsightsEventSource.Log, EventLevel.LogAlways, Keywords.All);
            cont = 0;
            listener2.EnableEvents(ApplicationInsightsEventSource.Log, EventLevel.LogAlways, Keywords.All);
            MyTimer.Start();
            OsVersionContextInitializer os = new OsVersionContextInitializer("Windows 8.1");
            ApplicationVersionContextInitializer version = new ApplicationVersionContextInitializer("2.1");
            listener1.LogToApplicationInsights("c9ce96c4-8be1-4368-87d4-1dedd72aaa71",os,version);
            listener2.LogToWindowsForm(this);
            ApplicationInsightsEventSource.Log.Startup();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int random;
            Random r = new Random();
            random = r.Next(1, 6);
            this.RandomLog(random);
        }
        private void RandomLog(int Random)
        {
            switch(Random)
            {
                case 1:
                    ApplicationInsightsEventSource.Log.Log1();
                    break;
                case 2:
                    ApplicationInsightsEventSource.Log.Log2();
                    break;
                case 3:
                    ApplicationInsightsEventSource.Log.Log3();
                    break;
                case 4:
                    ApplicationInsightsEventSource.Log.Log4();
                    break;
                case 5:
                    ApplicationInsightsEventSource.Log.Log5();
                    break;
                default :
                    break;
            }
        }
        public void AddData(String Message,String Severity,String TimeStamp)
        {
            this.dgvLogs.Rows.Add(Message, Severity, TimeStamp);
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            cont++;
            ApplicationInsightsEventSource.Log.TimerTick(cont);
        }
    }
}
