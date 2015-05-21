using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gui
{
    public partial class Form1 : Form
    {
        public Form1(string tern)
        {
            InitializeComponent();

            _tern = tern;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            But.Enabled = false;

            try
            {
                Drawing.Draw(PictureBox, _tern);
            }
            catch (Exception)
            {
                JimDead.Dead(PictureBox);
            }

            But.Enabled = true;
        }
    }
}
