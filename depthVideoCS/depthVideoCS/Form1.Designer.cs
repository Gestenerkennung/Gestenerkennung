namespace depthVideoCS
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.preview = new System.Windows.Forms.GroupBox();
            this.prevBox = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.frameCountPicker = new System.Windows.Forms.NumericUpDown();
            this.capture = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.console = new System.Windows.Forms.ListBox();
            this.savePCDButton = new System.Windows.Forms.Button();
            this.dismissButton = new System.Windows.Forms.Button();
            this.person = new System.Windows.Forms.NumericUpDown();
            this.gesture = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.preview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prevBox)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frameCountPicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.person)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gesture)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 25;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // preview
            // 
            this.preview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.preview.Controls.Add(this.prevBox);
            this.preview.Location = new System.Drawing.Point(9, 10);
            this.preview.Margin = new System.Windows.Forms.Padding(2);
            this.preview.Name = "preview";
            this.preview.Padding = new System.Windows.Forms.Padding(2);
            this.preview.Size = new System.Drawing.Size(379, 312);
            this.preview.TabIndex = 0;
            this.preview.TabStop = false;
            this.preview.Text = "Preview";
            // 
            // prevBox
            // 
            this.prevBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.prevBox.Location = new System.Drawing.Point(4, 17);
            this.prevBox.Margin = new System.Windows.Forms.Padding(2);
            this.prevBox.Name = "prevBox";
            this.prevBox.Size = new System.Drawing.Size(370, 290);
            this.prevBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.prevBox.TabIndex = 0;
            this.prevBox.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 322);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(818, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(471, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Frames";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // frameCountPicker
            // 
            this.frameCountPicker.Location = new System.Drawing.Point(517, 8);
            this.frameCountPicker.Margin = new System.Windows.Forms.Padding(2);
            this.frameCountPicker.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.frameCountPicker.Name = "frameCountPicker";
            this.frameCountPicker.Size = new System.Drawing.Size(41, 20);
            this.frameCountPicker.TabIndex = 4;
            this.frameCountPicker.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.frameCountPicker.ValueChanged += new System.EventHandler(this.frameCountPicker_ValueChanged);
            // 
            // capture
            // 
            this.capture.Location = new System.Drawing.Point(704, 7);
            this.capture.Margin = new System.Windows.Forms.Padding(2);
            this.capture.Name = "capture";
            this.capture.Size = new System.Drawing.Size(105, 19);
            this.capture.TabIndex = 5;
            this.capture.Text = "Start Capture";
            this.capture.UseVisualStyleBackColor = true;
            this.capture.Click += new System.EventHandler(this.capture_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(473, 31);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(335, 19);
            this.progressBar1.TabIndex = 6;
            // 
            // console
            // 
            this.console.FormattingEnabled = true;
            this.console.Location = new System.Drawing.Point(473, 249);
            this.console.Margin = new System.Windows.Forms.Padding(2);
            this.console.Name = "console";
            this.console.Size = new System.Drawing.Size(336, 69);
            this.console.TabIndex = 9;
            // 
            // savePCDButton
            // 
            this.savePCDButton.Enabled = false;
            this.savePCDButton.Location = new System.Drawing.Point(704, 108);
            this.savePCDButton.Margin = new System.Windows.Forms.Padding(2);
            this.savePCDButton.Name = "savePCDButton";
            this.savePCDButton.Size = new System.Drawing.Size(105, 19);
            this.savePCDButton.TabIndex = 10;
            this.savePCDButton.Text = "Save as PCDs";
            this.savePCDButton.UseVisualStyleBackColor = true;
            this.savePCDButton.Click += new System.EventHandler(this.savePCDButton_Click);
            // 
            // dismissButton
            // 
            this.dismissButton.Enabled = false;
            this.dismissButton.Location = new System.Drawing.Point(473, 108);
            this.dismissButton.Margin = new System.Windows.Forms.Padding(2);
            this.dismissButton.Name = "dismissButton";
            this.dismissButton.Size = new System.Drawing.Size(105, 19);
            this.dismissButton.TabIndex = 11;
            this.dismissButton.Text = "Dismiss";
            this.dismissButton.UseVisualStyleBackColor = true;
            this.dismissButton.Click += new System.EventHandler(this.dismissButton_Click);
            // 
            // person
            // 
            this.person.Location = new System.Drawing.Point(517, 63);
            this.person.Margin = new System.Windows.Forms.Padding(2);
            this.person.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.person.Name = "person";
            this.person.Size = new System.Drawing.Size(41, 20);
            this.person.TabIndex = 12;
            this.person.ValueChanged += new System.EventHandler(this.person_ValueChanged);
            // 
            // gesture
            // 
            this.gesture.Location = new System.Drawing.Point(517, 85);
            this.gesture.Margin = new System.Windows.Forms.Padding(2);
            this.gesture.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.gesture.Name = "gesture";
            this.gesture.Size = new System.Drawing.Size(41, 20);
            this.gesture.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(471, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Person";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(471, 87);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Gesture";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(473, 132);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(335, 112);
            this.button1.TabIndex = 16;
            this.button1.Text = "Click me!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(735, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Timer";
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 344);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gesture);
            this.Controls.Add(this.person);
            this.Controls.Add(this.dismissButton);
            this.Controls.Add(this.savePCDButton);
            this.Controls.Add(this.console);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.capture);
            this.Controls.Add(this.frameCountPicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.preview);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "depthVideo Grabber";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.preview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.prevBox)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frameCountPicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.person)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gesture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox preview;
        private System.Windows.Forms.PictureBox prevBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown frameCountPicker;
        private System.Windows.Forms.Button capture;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ListBox console;
        private System.Windows.Forms.Button savePCDButton;
        private System.Windows.Forms.Button dismissButton;
        private System.Windows.Forms.NumericUpDown person;
        private System.Windows.Forms.NumericUpDown gesture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer2;
    }
}

