using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form5 : Form
    {
        Form1 frm1;
        public Form5(Form1 form1)
        {
            InitializeComponent();
            frm1 = form1;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            
            ImageConverter ic = new ImageConverter();

            //string str1 = frm1.dataGridView1.Rows[frm1.dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
            Image img = (Image)ic.ConvertFrom(frm1.dataGridView1.Rows[frm1.dataGridView1.CurrentRow.Index].Cells[1].Value);

            Bitmap bitmap1 = new Bitmap(img);


            pictureBox1.Image = bitmap1;
        }
    }
}
