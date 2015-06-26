﻿using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
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
namespace SLABAI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var listener1 = new ObservableEventListener();

            listener1.EnableEvents(MyCompanyEventSource.Log, EventLevel.LogAlways, Keywords.All);
            listener1.LogToApplicationInsights("c9ce96c4-8be1-4368-87d4-1dedd72aaa71");
            MyCompanyEventSource.Log.Startup();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyCompanyEventSource.Log.Boton("Se presiono el botón");
        }
    }
}