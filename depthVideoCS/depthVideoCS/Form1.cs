using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using DSB;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace depthVideoCS
{
    public partial class Form1 : Form
    {
        Thread grabberThread = null;
        private int timer = 5;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            DSB.DSBridge.init();
            grabberThread = new Thread(DSB.DSBridge.run);
            grabberThread.Start();

            timer1.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            DSB.DSBridge.quit();
            DSB.DSBridge.destroy();
        }

        unsafe private void timer1_Tick(object sender, EventArgs e)
        {
            Bitmap back = new Bitmap(640,
                480,
                3*640,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb,
                new IntPtr(DSB.DSBridge.getCachedRGB()));

            prevBox.Image = back;

            toolStripStatusLabel1.Text = "RGBFrame: " + DSB.DSBridge.getRGBCount() + " DepthFrame: " + DSB.DSBridge.getDepthCount();

            int val = DSBridge.getCaptureProgress();

            if (val >= 0)
            {
                progressBar1.Value = val;
            }
            else
            {
                progressBar1.Value = 0;

                if (val == -1)
                {
                    post("Capture finished");
                    DSBridge.resetVideoCapture();
                    
                    dismissButton.Enabled = true;
                    savePCDButton.Enabled = true;
                }
            }

        }

        private void capture_Click(object sender, EventArgs e)
        {
            capture.Enabled = false;

            DSB.DSBridge.startVideoCapture((int)frameCountPicker.Value);

            progressBar1.Maximum = (int)frameCountPicker.Value;

            post("Starting capture...");
        }

        private void post(string s)
        {
            console.Items.Add(s);
            if (console.Items.Count > 4) console.Items.RemoveAt(0);
        }

        private void dismissButton_Click(object sender, EventArgs e)
        {
            dismissButton.Enabled = false;
            savePCDButton.Enabled = false;
            capture.Enabled = true;
        }

        private void savePCDButton_Click(object sender, EventArgs e)
        {
            String n_dir = Directory.GetCurrentDirectory() + @"\datasets\p" + person.Value + @"\g" + gesture.Value + @"\uncropped\";
            String x_dir = Directory.GetCurrentDirectory() + @"\datasets\p" + person.Value + @"\g" + gesture.Value + @"\cropped\";
            
            if (!Directory.Exists(n_dir))
            {
                Directory.CreateDirectory(n_dir);
                Directory.CreateDirectory(x_dir);
            }
            
                DSBridge.saveToPCDs((int)person.Value, (int)gesture.Value);

            dismissButton.Enabled = false;
            savePCDButton.Enabled = false;
            capture.Enabled = true;

            
            string str = person.Value.ToString();
            string str2 = gesture.Value.ToString();
            string str3 = frameCountPicker.Value.ToString();
            this.gesture.Value++;
            Process pc2 = new Process();
            pc2.StartInfo.FileName = "pc2.exe";
            pc2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            pc2.StartInfo.Arguments = n_dir.Replace("\\","\\\\")+ " " + str + " "+ str2 + " "+ str3 + " "+ x_dir.Replace("\\", "\\\\");
            pc2.Start();
       //     pc2.WaitForExit();
        //    pc2.CloseMainWindow(); 
         //   pc2.Close();
            this.timer2.Start();
        }


            private void CaptureButton()
        {
            capture.Enabled = false;

            DSB.DSBridge.startVideoCapture((int)frameCountPicker.Value);

            progressBar1.Maximum = (int)frameCountPicker.Value;

            post("Starting capture...");
        }

        int clicks = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "Clicks: " + ++clicks;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if(--this.timer == 0)
            {
                this.label4.Text = "Aufnahme...";
                this.CaptureButton();
                this.timer2.Stop();
                this.timer = 5;
                return;
            }
            this.label4.Text = "Start in: " + this.timer + " Sekunden!";
        }

        private void person_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void frameCountPicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
