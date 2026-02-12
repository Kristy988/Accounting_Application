using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace 記帳APP.Views
{
    public partial class ShowPic : Form
    {
        public ShowPic(string path)
        {

            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Image.FromFile(path.ToString().Replace("small_", ""));

        }

        private void ShowPic_FormClosing(object sender, FormClosingEventArgs e)
        {
            pictureBox1.Image.Dispose();
            GC.Collect();
        }
    }
}
