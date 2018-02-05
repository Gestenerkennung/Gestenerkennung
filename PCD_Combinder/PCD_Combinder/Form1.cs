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
            return text.Replace(",g", "").Replace(",", " ");
        }
        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                String path = folderBrowserDialog1.SelectedPath;
                String AllText = "";
                /*
                 * k = Gestenklasse (0 - 3)
                 * g = Beispiel (also in diesem Fall von 0 - 99, da jeweils 100 Testgeesten)
                 */
                int i = 0, j, k, tmp;
                int[] a = new int[4];
                for(; i<10; i++)
                {
                    for (j = 0; j < 40; j++)
                    {
                        AllText = getText(path + @"/p" + i + @"/g" + j + @"/cropped");
                        File.WriteAllText(path+@"/lcc_p1_k" + k + "_g" + a[k] + ".txt", AllText.Substring(0,AllText.Length-2));
                        a[k]++;
                        if (++tmp == 10) //Limit 10, da eine Person jeweils 10x eine Geste aufgenommen hat.
                        {
                            k++;
                            tmp = 0;
                        }
                    }
                }
                MessageBox.Show(this, "Erfolgreich konvertiert!", "Beendet", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
