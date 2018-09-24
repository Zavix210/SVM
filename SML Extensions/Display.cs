using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SML_Extensions
{
    public partial class Display : Form
    {
        public Display(Image Image)
        {
            InitializeComponent();
            this.Size = Image.Size;
            pictureBox1.Image = Image;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
        }
    }
}
