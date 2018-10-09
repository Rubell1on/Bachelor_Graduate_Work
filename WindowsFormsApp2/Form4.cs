using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form4 : Form
    {
        Form1 frm1;
        //string cons;

        public Form4(Form1 form1)
        {
            InitializeComponent();
            frm1 = form1;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Введите название таблицы")
            {
                MessageBox.Show("Необходимо ввести название таблицы", "Ошибка!");
            }
            else
            {
                addTable(frm1.conStr, textBox1.Text);
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            DataGridViewComboBoxCell Cb = new DataGridViewComboBoxCell();
            Cb.Items.Add("BigInt");
            Cb.Items.Add("Binary");
            Cb.Items.Add("Bit");
            Cb.Items.Add("Char");
            Cb.Items.Add("Date");
            Cb.Items.Add("DateTime");
            Cb.Items.Add("DateTime2");
            Cb.Items.Add("DateTimeOffset");
            Cb.Items.Add("Decimal");
            Cb.Items.Add("Float");
            Cb.Items.Add("Image");
            Cb.Items.Add("Int");
            Cb.Items.Add("Money");
            Cb.Items.Add("NChar");
            Cb.Items.Add("NText");
            Cb.Items.Add("NVarChar");
            Cb.Items.Add("Real");
            Cb.Items.Add("SmallDateTime");
            Cb.Items.Add("SmallInt");
            Cb.Items.Add("SmallMoney");
            Cb.Items.Add("Structured");
            Cb.Items.Add("Text");
            Cb.Items.Add("Time");
            Cb.Items.Add("Timestamp");
            Cb.Items.Add("TinyInt");
            Cb.Items.Add("Udt");
            Cb.Items.Add("UniqueIdentifier");
            Cb.Items.Add("VarBinary");
            Cb.Items.Add("VarChar");
            Cb.Items.Add("Variant");
            Cb.Items.Add("Xml");


            dataGridView1.Columns[2].CellTemplate = Cb;

            // TODO: данная строка кода позволяет загрузить данные в таблицу "информационная_модель_торговой_компанииDataSet.Client". При необходимости она может быть перемещена или удалена.
            dataGridView1.Rows.Add(20);
            //dataGridView1.Rows[0].Selected = true;
            //dataGridView1.Rows[0].ReadOnly = true;
            dataGridView1.Rows[0].Cells[0].Value = true;
            dataGridView1.Rows[0].Cells[1].Value = "Id";
            dataGridView1.Rows[0].Cells[2].Value = "Int";
            dataGridView1.Rows[0].Cells[4].Value = null;

            //dataGridView1.Rows[1].ReadOnly = true;
            dataGridView1.Rows[1].Cells[1].Value = "Picture";
            dataGridView1.Rows[1].Cells[2].Value = "Image";
            //dataGridView1.Rows[1].Cells[3].Value = 1000000;
        }

        void addTable(string conStr, string tableName)
        {

            string tableCreate = String.Format("CREATE TABLE [dbo].[{0}](",tableName);
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value == null)
                    continue;
                if (i > 0)
                    tableCreate += ",";
                if (dataGridView1.Rows[i].Cells[0].Value != null)
                {

                    tableCreate += String.Format("PRIMARY KEY ([{0}]), ", dataGridView1.Rows[i].Cells[1].Value);

                }
                if (dataGridView1.Rows[i].Cells[3].Value != null)
                {
                    tableCreate += String.Format("[{0}] {1}({2}) ", dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[2].Value, dataGridView1.Rows[i].Cells[3].Value);
                }
                else
                {
                    tableCreate += String.Format("[{0}] {1} ", dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[2].Value);
                }
                if (dataGridView1.Rows[i].Cells[4].Value != null)
                {
                    tableCreate += "NULL ";
                }
                else
                {
                    tableCreate += "NOT NULL ";
                }
                tableCreate += String.Format("{0}", dataGridView1.Rows[i].Cells[5].Value);

            }
            tableCreate = tableCreate + ");";

            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();
                //string sql = "SELECT * FROM Table1";
                SqlCommand command = new SqlCommand(@tableCreate, connection);
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }


        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
        }
    }


}
