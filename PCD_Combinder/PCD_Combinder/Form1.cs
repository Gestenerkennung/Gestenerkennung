using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PCD_Combinder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private String getText(String path)
        {
            DirectoryInfo ParentDirectory = new DirectoryInfo(path);
            String text = "";
            foreach (FileInfo f in ParentDirectory.GetFiles())
            {
                text += File.ReadAllText(f.FullName);
                File.Delete(f.FullName);
            }
            return text;
        }
        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                String path = folderBrowserDialog1.SelectedPath;
                String AllText = "";
                int i = 0, j;
                for(; i<10; i++)
                {
                    for (j = 0; j < 40; j++)
                    {
                        AllText = getText(path + @"/p" + i + @"/g" + j + @"/cropped");
                        File.WriteAllText(path + @"/p" + i + @"/g" + j + @"/cropped" + @"/konvertiert.txt", AllText.Replace(",g", "").Replace(",", " "));
                    }
                }
                MessageBox.Show(this, "Erfolgreich konvertiert!", "Beendet", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
