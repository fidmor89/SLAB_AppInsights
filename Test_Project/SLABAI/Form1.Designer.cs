namespace SLABAI
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.bSLAB = new System.Windows.Forms.Button();
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SeverityLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MyTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // bSLAB
            // 
            this.bSLAB.Image = ((System.Drawing.Image)(resources.GetObject("bSLAB.Image")));
            this.bSLAB.Location = new System.Drawing.Point(277, 207);
            this.bSLAB.Name = "bSLAB";
            this.bSLAB.Size = new System.Drawing.Size(105, 100);
            this.bSLAB.TabIndex = 0;
            this.bSLAB.UseVisualStyleBackColor = true;
            this.bSLAB.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvLogs
            // 
            this.dgvLogs.AccessibleName = "dgvLogs";
            this.dgvLogs.AllowUserToAddRows = false;
            this.dgvLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Message,
            this.SeverityLevel,
            this.EventTime});
            this.dgvLogs.Location = new System.Drawing.Point(12, 37);
            this.dgvLogs.Name = "dgvLogs";
            this.dgvLogs.Size = new System.Drawing.Size(613, 150);
            this.dgvLogs.TabIndex = 1;
            // 
            // Message
            // 
            this.Message.HeaderText = "Message";
            this.Message.Name = "Message";
            this.Message.Width = 300;
            // 
            // SeverityLevel
            // 
            this.SeverityLevel.HeaderText = "Severity Level";
            this.SeverityLevel.Name = "SeverityLevel";
            // 
            // EventTime
            // 
            this.EventTime.HeaderText = "Event Time";
            this.EventTime.Name = "EventTime";
            this.EventTime.Width = 170;
            // 
            // MyTimer
            // 
            this.MyTimer.Interval = 5000;
            this.MyTimer.Tick += new System.EventHandler(this.MyTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 329);
            this.Controls.Add(this.dgvLogs);
            this.Controls.Add(this.bSLAB);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Semantic Logging Application Block";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bSLAB;
        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
        private System.Windows.Forms.DataGridViewTextBoxColumn SeverityLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventTime;
        private System.Windows.Forms.Timer MyTimer;
    }
}

