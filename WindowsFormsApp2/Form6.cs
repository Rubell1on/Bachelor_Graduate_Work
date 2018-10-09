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
    public partial class Form6 : Form
    {
        Form1 frm1;
        string filename = "";
        byte[] imageData;
        bool match = false;
        int[] arrofint = new int[999];

        public Form6(Form1 form1)
        {
            InitializeComponent();
            frm1 = form1;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(frm1.conStr))
            {
                
                    
                    DataTable dataTable = new DataTable();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter();

                    SqlCommand myCommand = new SqlCommand(String.Format("SELECT * FROM {0}", frm1.tableName), connection);
                    dataAdapter.SelectCommand = myCommand;
                    connection.Open();
                    dataAdapter.Fill(dataTable);
                    frm1.dataGridView1.DataSource = dataTable;
                    connection.Close();
                
            }

            filename = "";

            for (int i = 0; i < frm1.dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns.Add(String.Format("Column {0}", i), frm1.dataGridView1.Columns[i].HeaderText);
            }


            for(int i = 0; i< frm1.dataGridView1.RowCount; i++)
            {
                if ((int)frm1.dataGridView1.Rows[i].Cells[0].Value == frm1.dataGridView1.RowCount)
                {
                    match = true;
                    break;
                }
                else
                {
                    match = false;
                }
            }

            if (match == false)
            {
                dataGridView1.Rows[0].Cells[0].Value = frm1.dataGridView1.RowCount + 1;
            }
            else
            {
                for(int i = 0; i < frm1.dataGridView1.RowCount; i++)
                {
                    arrofint[i] = Convert.ToInt32(frm1.dataGridView1.Rows[i].Cells[0].Value);
                }
                dataGridView1.Rows[0].Cells[0].Value = arrofint.Max()+1;

            }



        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filename == "")
            {
                MessageBox.Show("Перед добавлением новой строки необходимо добавить изображение", "Внимание!");
                
                if (getImageData() == System.Windows.Forms.DialogResult.OK)
                {
                    frm1.instr1 = String.Format("INSERT INTO {0} VALUES(", frm1.tableName);

                    for (int i = 0; i < frm1.dataGridView1.ColumnCount; i++)
                    {

                        if (i != 1)
                        {
                            frm1.instr1 += String.Format("'{0}'", dataGridView1.Rows[0].Cells[i].Value);
                        }
                        else
                        {
                            frm1.instr1 += "@picture";
                        }
                        if (i != frm1.dataGridView1.ColumnCount - 1)
                        {
                            frm1.instr1 += ", ";
                        }

                    }
                    frm1.instr1 += ")";

                    
                    using (SqlConnection connection = new SqlConnection(frm1.conStr))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand();
                        command.Connection = connection;
                        
                        command.CommandText = @frm1.instr1;
                        
                        command.Parameters.Add("@picture", SqlDbType.Image, 1000000);

                        command.Parameters["@picture"].Value = imageData;

                        command.ExecuteNonQuery();
                    }
                    this.Close();
                }
            }
            else
            {
                frm1.instr1 = String.Format("INSERT INTO {0} VALUES(", frm1.tableName);

                for (int i = 0; i < frm1.dataGridView1.ColumnCount; i++)
                {

                    if (i != 1)
                    {
                        frm1.instr1 += String.Format("N'{0}'", dataGridView1.Rows[0].Cells[i].Value);
                    }
                    else
                    {
                        frm1.instr1 += "@picture";
                    }
                    if (i != frm1.dataGridView1.ColumnCount - 1)
                    {
                        frm1.instr1 += ", ";
                    }

                }
                frm1.instr1 += ")";

                using (SqlConnection connection = new SqlConnection(frm1.conStr))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;

                    command.CommandText = frm1.instr1;

                    command.Parameters.Add("@picture", SqlDbType.Image, 1000000);

                    command.Parameters["@picture"].Value = imageData;

                    command.ExecuteNonQuery();

                    DataTable dataTable = new DataTable();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    SqlCommand cmd1 = new SqlCommand(String.Format("SELECT * FROM {0}", frm1.tableName), connection);
                    dataAdapter.SelectCommand = cmd1;
                    dataAdapter.Fill(dataTable);
                    frm1.dataGridView1.DataSource = dataTable;
                }
                //this.Close();
            }
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            getImageData();

        }

        System.Windows.Forms.DialogResult getImageData()
        {

            System.Windows.Forms.DialogResult dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {

                filename = openFileDialog1.FileName;

                using (System.IO.FileStream fs = new System.IO.FileStream(filename, FileMode.Open))
                {
                    imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, imageData.Length);
                }

            }
            return dialogResult;
        }
    }
}
