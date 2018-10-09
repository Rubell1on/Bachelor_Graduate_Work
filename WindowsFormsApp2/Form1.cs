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
using Microsoft.Office.Interop.Excel;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public string instr1;
        static Image image;
        Form5 form5 = null;
        System.Drawing.Point mousePos;
        static public int matchR = 0;

        private System.Drawing.Point mouseOffset;
        private bool isMouseDown = false;

        public string[,] arrOfNames = null;
        public string selStr = "SELECT * FROM table1 ";
        public string strSql = "SELECT * FROM table1";
        static public string defstrSql = "SELECT * FROM table1";
        public int lenOfStr;
        public int add1 = 0;
        public int cc = 0;
        public int rc1 = 0;
        public int foundedColumn = 0;

        static public string str22 = System.IO.Path.GetFullPath("Database1.mdf");
        public string conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dabla\Documents\Visual Studio 2017\Projects\09022018\WindowsFormsApp2\Database1.mdf";
        public string tableName = "table1";
        //static string conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + str22 + "";

        static SqlConnection cn = new SqlConnection();
        SqlDataAdapter dataAdapter = null;
        System.Data.DataTable dataTable = null;
        SqlCommandBuilder bldr = null;

        public SqlCommand myCommand = new SqlCommand("SELECT * FROM table1", cn);
        SqlCommand select = new SqlCommand("SELECT * FROM table1", cn);

        string cons;
        //SqlCommand update = new SqlCommand("UPDATE table1 SET  [1] = @1, [2] = @2, [3] = @3", cn);
        //SqlCommand insert = new SqlCommand("INSERT INTO table1( [1], [2], [3]) VALUES(@1, @2, @3)",cn);


        public Form1()
        {

            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {


            lenOfStr = selStr.Length;
            //label1.Text = this.Text;
            //update.Parameters.Add("@id", SqlDbType.Int, 10, "id");
            //update.Parameters.Add("@1", SqlDbType.NChar, 10, "1");
            // update.Parameters.Add("@2", SqlDbType.NChar, 10, "2");
            // update.Parameters.Add("@3", SqlDbType.NChar, 10, "3");

            //insert.Parameters.Add("@id", SqlDbType.Int, 10, "id");
            // insert.Parameters.Add("@1", SqlDbType.NChar, 10, "1");
            // insert.Parameters.Add("@2", SqlDbType.NChar, 10, "2");
            // insert.Parameters.Add("@3", SqlDbType.NChar, 10, "3");


            

            using (cn)
            {
                //cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+str22+"";

                cn.ConnectionString = conStr;
                try
                {
                    dataAdapter = new SqlDataAdapter();
                    bldr = new SqlCommandBuilder(dataAdapter);

                    // dataAdapter.SelectCommand = myCommand;
                    //dataAdapter.InsertCommand = insert;
                    //dataAdapter.UpdateCommand = update;

                    dataAdapter.SelectCommand = myCommand;
                    dataAdapter.InsertCommand = bldr.GetInsertCommand();
                    dataAdapter.UpdateCommand = bldr.GetUpdateCommand();

                    cn.Open();

                    dataTable = new System.Data.DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;

                   


                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK);
                }
                finally
                {
                    cn.Close();
                }

            }
            
            


            /*SqlConnection conn = new SqlConnection();
            conn.ConnectionString = conStr;

            using (conn)
            {
                conn.Open();

                string sql = "SELECT TABLE_NAME FROM information_schema.TABLES";

                SqlCommand command = new SqlCommand(sql, conn);

                SqlDataReader reader = command.ExecuteReader();
                
                treeView1.BeginUpdate();
                treeView1.Nodes.Add("Таблицы");

                while (reader.Read())
                {
                    treeView1.Nodes[0].Nodes.Add(reader.GetString(0));
                }

                treeView1.EndUpdate();
                treeView1.ExpandAll();

                conn.Close();


            }*/

        }

        private void GroupSelection(string string1, string strSql)
        {
            cn.ConnectionString = conStr;
            using (cn)
            {
                //cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + str22 + ";Integrated Security=True;";
                try
                {
                    dataGridView1.Columns.Clear();
                    cn.Open();

                    strSql = String.Format("SELECT [{1}].[{0}] FROM {1} GROUP BY [{1}].[{0}]", string1, tableName);
                    myCommand = new SqlCommand(strSql, cn);

                    //myCommand.ExecuteNonQuery();


                    SqlDataReader dataReader = myCommand.ExecuteReader();

                    System.Data.DataTable dataTable = new System.Data.DataTable();
                    //dataAdapter.Fill(dataTable);
                    dataTable.Load(dataReader);

                    dataGridView1.DataSource = dataTable;
                    dataReader.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK);
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        public void GroupSelection(string strSql)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                //cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + str22 + ";Integrated Security=True;";
                cn.ConnectionString = conStr;
                //try
                {
                    dataGridView1.Columns.Clear();
                    cn.Open();

                    //strSql = String.Format("SELECT [{1}].[{0}] FROM {1} GROUP BY [{1}].[{0}]", i, tableName);
                    //strSql = String.Format("SELECT * FROM {0}",tableName);
                    myCommand = new SqlCommand(strSql, cn);


                    SqlDataReader dataReader = myCommand.ExecuteReader();

                    System.Data.DataTable dataTable = new System.Data.DataTable();

                    dataTable.Load(dataReader);

                    dataGridView1.DataSource = dataTable;
                    dataReader.Close();
                    //}

                }
                /*catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK);
                }
                finally
                {*/
                cn.Close();
                // }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selStr = String.Format("SELECT * FROM {0} WHERE ", tableName);
            обновитьToolStripMenuItem_Click(sender, e);

            //for (int i = 0; i < cc; i++)
            //{
            //    point.X = point.X + add1;
            //    add1 = 100;
            //    if (point.X > this.Size.Width - 100)
            //    {
            //        point.Y = point.Y + 50;
            //        point.X = 25;
            //    }

            //    Button btn1 = new Button();
            //    btn1.Text = arrOfNames[0, i];
            //    //btn1.Name = String.Format("butn{0}", i);
            //    btn1.Name = arrOfNames[0, i].ToString();
            //    btn1.Location = point;
            //    this.Controls.Add(btn1);
            //    btn1.Click += Btn1_Click;

            //}
            /*    DataGridViewColumn newColumn =
            dataGridView1.Columns.GetColumnCount(
            DataGridViewElementStates.Selected) == 1 ?
            dataGridView1.SelectedColumns[1] : null;
                dataGridView1.Sort(newColumn, ListSortDirection.Ascending);*/
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            //String name = ((Button)sender).Name;
            for (int i = 0; i < cc; i++)
            {
                if (((System.Windows.Forms.Button)sender).Name == arrOfNames[0, i])
                {
                    foundedColumn = i + 1;
                    button2.Text = foundedColumn.ToString();
                }
            }
            //throw new NotImplementedException();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cn.Close();
            System.Windows.Forms.Application.Exit();
        }

        private void иИлиДеревоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            defstrSql = String.Format("SELECT * FROM {0}", tableName);
            GroupSelection(defstrSql);
            int rc = dataGridView1.RowCount;
            cc = dataGridView1.Columns.Count - 2;


            if (arrOfNames == null)
            {
                arrOfNames = new string[dataGridView1.RowCount + 1, dataGridView1.Columns.Count - 2];
            }
            else
            {
                arrOfNames = null;
                arrOfNames = new string[dataGridView1.RowCount + 1, dataGridView1.Columns.Count - 2];
            }

            for (int i = 2; i < dataGridView1.Columns.Count; i++)
            {
                arrOfNames[0, i - 2] = dataGridView1.Columns[i].Name;


            }
            strSql = String.Format("SELECT * FROM {0}", tableName);
            for (int i = 0; i < cc; i++)
            {
                GroupSelection(arrOfNames[0, i], strSql);
                for (int j = 0; j < dataGridView1.RowCount; j++)
                {
                    arrOfNames[j + 1, i] = dataGridView1.Rows[j].Cells[0].Value.ToString();
                    //if (rc1 < dataGridView1.RowCount)
                    // {
                    rc1 = dataGridView1.RowCount;
                    // }
                }

            }

            strSql = String.Format("SELECT * FROM {0}", tableName);
            //dataGridView1.Rows.Clear();
            
            GroupSelection(strSql);
            rc1 = dataGridView1.RowCount;
            Form2 form2 = new Form2(this);
            form2.Show();

        }

        private void создатьЗапросToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(this);
            form3.Show();
        }

        private void сохранитьБазуДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + str22 + ";Integrated Security=True;";
            cn.ConnectionString = conStr;
            using (cn)
            {
                try
                {
                    cn.Open();

                    dataAdapter.Update(dataTable);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK);
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        private void строкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            matchR = dataGridView1.RowCount + 1;
            Form6 form6 = new Form6(this);
            form6.ShowDialog();

            /*SqlCommand insert1 = new SqlCommand(instr1, cn);
            cn.ConnectionString = conStr;

            using (cn)
            {
                cn.Open();
                //dataAdapter.InsertCommand.ExecuteNonQuery();
                insert1.ExecuteNonQuery();

                dataTable = new DataTable();
                //dataAdapter.Update(dataTable);
                dataAdapter.Fill(dataTable);
                //dataAdapter.SelectCommand.ExecuteNonQuery();
                dataGridView1.DataSource = dataTable;


                cn.Close();
            }*/



        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(String.Format("Вы действительно хотите удалить {0} строку?", dataGridView1.CurrentRow.Index+1), "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                cn.ConnectionString = conStr;
                using (cn)
                {
                    SqlCommand delete = new SqlCommand(String.Format("DELETE FROM {1} WHERE [id]={0}", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value, tableName), cn);

                    dataAdapter.DeleteCommand = delete;

                    cn.Open();
                    dataAdapter.SelectCommand = myCommand;
                    dataAdapter.DeleteCommand.ExecuteNonQuery();

                    dataTable = new System.Data.DataTable();
                    //dataAdapter.Update(dataTable);
                    dataAdapter.Fill(dataTable);
                    //dataAdapter.SelectCommand.ExecuteNonQuery();
                    dataGridView1.DataSource = dataTable;

                    cn.Close();
                    dbInr();
                }
            }
        }
        
        void dbInr(bool a)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) != i+1)
                {
                    if (MessageBox.Show(String.Format("Обнаружен неправильный индекс: {0}. Исправить?", i+1), "Ошибка!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        for (int j = 1; j < dataGridView1.RowCount + 1; j++)
                            dataGridView1.Rows[j - 1].Cells[0].Value = j;
                    }
                }
            }   
        }

        void dbInr()
        {

            for (int j = 1; j < dataGridView1.RowCount + 1; j++)
                dataGridView1.Rows[j - 1].Cells[0].Value = j;
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cn.ConnectionString = conStr;
            using (cn)
            {
                cn.Open();
                dataTable = new System.Data.DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                cn.Close();
            }
        }

        private void подгрузитьБазуДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //System.IO.StreamReader sr = new System.IO.StreamReader(;


                //using(this.cn)
                //this.cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + str22 + ";Integrated Security=True;";
                //sr.Close();

                cons = openFileDialog1.FileName;
                /* using ()
                 {
                     //cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + cons + "";
                     DataTable dt1 = new DataTable();
                     //Доделать получение имени таблицы из БД
                     dataAdapter.Fill(dt1);
                     dataGridView1.DataSource = dt1;
                 }*/
                conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + cons + "";
                //textBox1.Text = conStr;

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = conStr;

                using (conn)
                {
                    conn.Open();

                    string sql = "SELECT TABLE_NAME FROM information_schema.TABLES";

                    SqlCommand command = new SqlCommand(sql, conn);

                    SqlDataReader reader = command.ExecuteReader();

                    treeView1.BeginUpdate();
                    treeView1.Nodes.Clear();
                    treeView1.Nodes.Add("Таблицы");

                    while (reader.Read())
                    {
                        treeView1.Nodes[0].Nodes.Add(reader.GetString(0));
                    }

                    treeView1.EndUpdate();
                    treeView1.ExpandAll();

                    conn.Close();


                }
            }

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int xOffset;
            int yOffset;

            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X - SystemInformation.FrameBorderSize.Width + 5;
                yOffset = -e.Y - SystemInformation.FrameBorderSize.Height + 5;
                mouseOffset = new System.Drawing.Point(xOffset, yOffset);
                isMouseDown = true;
            }
        }

        private void customImageButton1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void customImageButton2_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Normal)
            {
                this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                System.Drawing.Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        public string SubStrDel(String str, string substr)
        {
            //Console.Write("Введите подстроку: ");
            //string substr = "[1] = 'a' ";
            int n = str.IndexOf(substr);
            str = str.Remove(n, substr.Length);
            return str;
        }

        private void подключениеБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel1Collapsed == true)
                splitContainer1.Panel1Collapsed = false;
            else
                splitContainer1.Panel1Collapsed = true;
        }

        private void добToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //System.IO.StreamReader sr = new System.IO.StreamReader(;


                //using(this.cn)
                //this.cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + str22 + ";Integrated Security=True;";
                //sr.Close();

                cons = openFileDialog1.FileName;
                /* using ()
                 {
                     //cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + cons + "";
                     DataTable dt1 = new DataTable();
                     //Доделать получение имени таблицы из БД
                     dataAdapter.Fill(dt1);
                     dataGridView1.DataSource = dt1;
                 }*/
                conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + cons + "";
                //textBox1.Text = conStr;

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = conStr;

                using (conn)
                {
                    conn.Open();

                    string sql = "SELECT TABLE_NAME FROM information_schema.TABLES";

                    SqlCommand command = new SqlCommand(sql, conn);

                    SqlDataReader reader = command.ExecuteReader();

                    treeView1.BeginUpdate();
                    treeView1.Nodes.Clear();
                    treeView1.Nodes.Add("Таблицы");

                    while (reader.Read())
                    {
                        treeView1.Nodes[0].Nodes.Add(reader.GetString(0));
                    }

                    treeView1.EndUpdate();
                    treeView1.ExpandAll();

                    conn.Close();


                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
            tableName = treeView1.Nodes[0].Nodes[treeView1.SelectedNode.Index].Text;
            conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + cons + "";

            using (cn)
            {
                //cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+str22+"";

                cn.ConnectionString = conStr;
                try
                {
                    dataAdapter = new SqlDataAdapter();
                    bldr = new SqlCommandBuilder(dataAdapter);

                    // dataAdapter.SelectCommand = myCommand;
                    //dataAdapter.InsertCommand = insert;
                    //dataAdapter.UpdateCommand = update;
                    myCommand = new SqlCommand(String.Format("SELECT * FROM [{0}]", tableName), cn);
                    dataAdapter.SelectCommand = myCommand;
                    dataAdapter.InsertCommand = bldr.GetInsertCommand();
                    dataAdapter.UpdateCommand = bldr.GetUpdateCommand();

                    cn.Open();

                    dataTable = new System.Data.DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK);
                }
                finally
                {
                    cn.Close();
                }

            }
            dataGridView1.Columns[1].Visible = false;
            dbInr(true);
        }

        private void добавитьТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.Nodes.Count != 0)
            {
                Form4 form4 = new Form4(this);
                form4.ShowDialog();
                refreshTableList();
            }
            else
                MessageBox.Show("Необходимо подключить базу данных!", "Ошибка!");

        }

        private void обновитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            refreshTableList();
        }

        private void удалитьТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.Nodes.Count != 0)
            {
                string tableDelete = String.Format("DROP TABLE [dbo].[{0}]", treeView1.Nodes[0].Nodes[treeView1.SelectedNode.Index].Text);

                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    connection.Open();
                    //string sql = "SELECT * FROM Table1";
                    SqlCommand command = new SqlCommand(tableDelete, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Close();
                }
                
                    refreshTableList();
               
            }
            else
                MessageBox.Show("Необходимо подключить базу данных!", "Ошибка!");
        }

        void refreshTableList()
        {
            
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = conStr;

                using (conn)
                {
                    conn.Open();

                    string sql = "SELECT TABLE_NAME FROM information_schema.TABLES";

                    SqlCommand command = new SqlCommand(sql, conn);

                    SqlDataReader reader = command.ExecuteReader();

                    treeView1.BeginUpdate();
                    treeView1.Nodes.Clear();
                    treeView1.Nodes.Add("Таблицы");

                    while (reader.Read())
                    {
                        treeView1.Nodes[0].Nodes.Add(reader.GetString(0));
                    }

                    treeView1.EndUpdate();
                    treeView1.ExpandAll();

                    conn.Close();


                }
            
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            form5 = new Form5(this);


            mousePos.X = Control.MousePosition.X - form5.Size.Width;
            mousePos.Y = Control.MousePosition.Y - form5.Size.Height;

            if (mousePos.X < 0 & mousePos.Y < 0)
            {

                mousePos.X = Control.MousePosition.X;
                mousePos.Y = Control.MousePosition.Y;
            }
            else
            {
                if (mousePos.Y < 0)
                {
                    mousePos.Y = 0;
                }
                if (mousePos.X < 0)
                {
                    mousePos.X = 0;
                }
            }

            timer1.Enabled = true;


            //textBox1.Text = string.Format("{0};{1}", mousePos.X, mousePos.Y);
            //textBox2.Text = string.Format("{0};{1}", form2.Location.X, form2.Location.Y);
            //form2.StartPosition = FormStartPosition.Manual;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            form5.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            form5.Show();
            form5.Location = mousePos;
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            timer1.Enabled = false;
            form5.Close();
        }

        private void столбецToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7(this);
            form7.ShowDialog();
        }

        private void удалитьСтолбецToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(String.Format("Вы действительно хотите удалить столбец {0}?", dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name), "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                String delColumn = String.Format("ALTER TABLE [dbo].[{0}] DROP COLUMN [{1}]", tableName, dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name);

                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(delColumn, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Close();

                    System.Data.DataTable dataTable = new System.Data.DataTable();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    SqlCommand cmd1 = new SqlCommand(String.Format("SELECT * FROM {0}", tableName), connection);
                    dataAdapter.SelectCommand = cmd1;
                    dataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
        }

        private void экспортироватьТаблицуВExcelФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();

            Workbook wb = Excel.Workbooks.Add(XlSheetType.xlWorksheet);

            Worksheet ws = (Worksheet)Excel.ActiveSheet;
            //Excel.Visible = true;

            for(int i = 2; i < dataGridView1.ColumnCount; i++)
            {
                    ws.Cells[1, i-1] = dataGridView1.Columns[i].Name;
                    for (int j = 1; j < dataGridView1.RowCount; j++)
                    {
                        ws.Cells[j + 1, i-1] = dataGridView1.Rows[j].Cells[i].Value;
                    }
            }
            wb.SaveAs("Выборка.xls", XlFileFormat.xlWorkbookNormal,
    System.Reflection.Missing.Value, System.Reflection.Missing.Value, false, false,
    XlSaveAsAccessMode.xlShared, false, false, System.Reflection.Missing.Value,
    System.Reflection.Missing.Value, System.Reflection.Missing.Value);
            //wb.Save();
            Excel.Quit();

        }
    }
}
