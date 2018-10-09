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
    public partial class Form3 : Form
    {
        Form1 frm1;
        public Form3(Form1 form1)
        {
            frm1 = form1;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void выполнитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                string strSql = textBox1.Text;
                frm1.GroupSelection(strSql);
            }
            else
            {
                MessageBox.Show("Необходимо ввести команду SQL!", "Ошибка!", MessageBoxButtons.OK);
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.BackColor = frm1.BackColor;
            this.menuStrip1.BackColor = frm1.menuStrip1.BackColor;
            this.menuStrip1.RenderMode = frm1.menuStrip1.RenderMode;
            this.menuStrip1.Font.Name.Replace(this.menuStrip1.Font.Name, frm1.menuStrip1.Font.Name);

        }
    }
}
