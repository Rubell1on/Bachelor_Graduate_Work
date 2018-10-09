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
    public partial class Form1 : Form
    {
        SqlConnection SqlConnection;
        public Form1()
        {
            InitializeComponent();
        }

        private  void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\dabla\documents\visual studio 2017\Projects\WindowsFormsApp2\WindowsFormsApp2\Database1.mdf; Integrated Security = True";
            SqlConnection = new SqlConnection(connectionString);
            
            SqlConnection.Open();
            SqlCommand com1 = SqlConnection.CreateCommand();
            com1.CommandType = CommandType.Text;
            com1.CommandText = "SELECT * FROM table1";
            com1.ExecuteNonQuery();

            DataTable dataTable1 = new DataTable();

            SqlDataAdapter dataAdapter1 = new SqlDataAdapter(com1);
            SqlCommandBuilder command1 = new SqlCommandBuilder(dataAdapter1);

            //DataSet dataSet1 = new DataSet();
            dataAdapter1.Fill(dataTable1);
            dataGridView1.DataSource = dataTable1;
            com1.CommandText = "SELECT [1] FROM table1";
            com1.ExecuteNonQuery();

            dataAdapter1.Update(dataTable1);
            dataAdapter1.Fill(dataTable1);
            dataGridView1.DataSource = dataTable1;

            SqlConnection.Close();
            //try
            //{
            //    sqlReader = await command.ExecuteReaderAsync();

            //    while (await sqlReader.ReadAsync())
            //    {

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    if (sqlReader != null)
            //        sqlReader.Close();
            //}
        }
    }
}
