using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form7 : Form
    {
        Form1 frm1;
        
        String addColumn = "";
        public Form7(Form1 form1)
        {
            InitializeComponent();
            frm1 = form1;
        }

        private void Form7_Load(object sender, EventArgs e)
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


            dataGridView1.Columns[1].CellTemplate = Cb;
            dataGridView1.Rows.Add();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows[0].Cells[2].Value == null)
            {
                addColumn = String.Format("ALTER TABLE [dbo].[{0}] ADD {1} {2} NULL", frm1.tableName, dataGridView1.Rows[0].Cells[0].Value, dataGridView1.Rows[0].Cells[1].Value);
            }
            else
            {
                addColumn = String.Format("ALTER TABLE [dbo].[{0}] ADD {1} {2}({3}) NULL", frm1.tableName, dataGridView1.Rows[0].Cells[0].Value, dataGridView1.Rows[0].Cells[1].Value, dataGridView1.Rows[0].Cells[2].Value);
            }

            
            
            using (SqlConnection connection = new SqlConnection(frm1.conStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(addColumn, connection);
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();

                DataTable dataTable = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                SqlCommand cmd1 = new SqlCommand(String.Format("SELECT * FROM {0}", frm1.tableName), connection);
                dataAdapter.SelectCommand = cmd1;
                dataAdapter.Fill(dataTable);
                frm1.dataGridView1.DataSource = dataTable;
            }


        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        
    }
}
