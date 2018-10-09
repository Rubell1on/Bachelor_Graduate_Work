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
    public partial class Form2 : Form
    {

        Form1 frm1;

        public int ncounter;
        public Point point1 = new Point(0, 25);
        public Point point = new Point(20, 70);
        //public Form2()
        //{
        //    InitializeComponent();


        //}
        public Form2(Form1 form1)
        {


            InitializeComponent();
            frm1 = form1;
            //this.Size = frm1.Size;
            this.BackColor = frm1.BackColor;
            point1.X = frm1.Size.Width / 2;
            point1.Y = 20;

            //form1.BackColor = Color.Yellow;
            //for (int j = 0; j < frm1.cc; j++)
            //{

            //    frm1.point.X = frm1.point.X + frm1.add1;
            //    frm1.add1 = 100;
            //    if (frm1.point.X > this.Size.Width - 100)
            //    {
            //        frm1.point.Y = frm1.point.Y + 50;
            //        frm1.point.X = 25;
            //    }

            //    //if (j == 0)
            //    //{
            //    //    TextBox t1 = new TextBox();
            //    //    t1.Text = frm1.arrOfNames[j, 0];
            //    //}

            //    Button btn1 = new Button();
            //    btn1.Text = frm1.arrOfNames[0, j];
            //    //btn1.Name = String.Format("butn{0}", i);
            //    btn1.Name = frm1.arrOfNames[0, j].ToString();
            //    btn1.Location = frm1.point;
            //    this.Controls.Add(btn1);
            //    btn1.Click += Btn1_Click; ;

            //}
            point1.X = this.Size.Width / 2;
            for (int j = 0; j < frm1.cc; j++)
            {

                for (int i = 0; i < frm1.rc1+1; i++)
                {
                    if (frm1.arrOfNames[i, j] == null)
                    {
                       
                        continue;
                    }
                    else
                    {
                        if (i == 0)
                        {
                            CreateLabel(i, j);
                            
                        }
                        else
                        {
                            CreateButton(i, j);
                            
                        }
                    }

                }
                point.X = 20;
                point1.Y = point.Y + 50;
                point.Y = point.Y + 100;
                
            }

        }

        private void Btn1_Click(object sender, EventArgs e)
        {

            //throw new NotImplementedException();
            for (int i = 1; i < frm1.rc1+1; i++)
            {

                for (int j = 0; j < frm1.cc; j++)
                {
                    if (frm1.arrOfNames[i, j] == null)
                    {
                        continue;
                    }
                    if (((Button)sender).Name == frm1.arrOfNames[i, j])
                    {
                        String temp = frm1.arrOfNames[i, j];
                                if(temp.Contains(",") == true)
                                {
                                    temp = temp.Replace(",", ".");
                                }

                        if(((Button)sender).BackColor.Equals(System.Drawing.Color.Gainsboro))
                        {
                            ((Button)sender).BackColor = System.Drawing.Color.DarkGray;
                            if (frm1.lenOfStr == frm1.selStr.Length)
                            {
                                frm1.selStr = String.Format("SELECT * FROM {0} ",frm1.tableName) + String.Format("WHERE {2}.[{0}] = N'{1}'", frm1.arrOfNames[0, j], temp,frm1.tableName);
                            }
                            else
                            {
                                frm1.selStr = frm1.selStr + String.Format(" and {2}.[{0}] = N'{1}'", frm1.arrOfNames[0, j], temp, frm1.tableName);
                                ncounter++;
                            }
                        }
                        else
                        {
                            ((Button)sender).BackColor = System.Drawing.Color.Gainsboro;
                            if(ncounter>0)
                            {
                                frm1.selStr = frm1.SubStrDel(frm1.selStr, String.Format(" and {2}.[{0}] = N'{1}'", frm1.arrOfNames[0, j], temp, frm1.tableName));
                                ncounter--;
                            }
                            else
                            {
                                frm1.selStr = frm1.SubStrDel(frm1.selStr, String.Format("{2}.[{0}] = N'{1}'", frm1.arrOfNames[0, j], temp, frm1.tableName));
                                frm1.selStr = frm1.SubStrDel(frm1.selStr,"WHERE ");
                            }
                        }
                        //frm1.foundedColumn = i + 1;
                        //frm1.button2.Text = frm1.foundedColumn.ToString();
                        frm1.button2.Text = frm1.arrOfNames[i,j].ToString();

                        //frm1.selStr = String.Format("SELECT * FROM table1 WHERE table1.[{0}] = '{1}'",frm1.arrOfNames[0,j],frm1.arrOfNames[i,j]);
                        frm1.GroupSelection(frm1.selStr);
                    }
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (frm1.WindowState != FormWindowState.Maximized)
            {
                Point startPosition = new Point(frm1.Location.X + frm1.Size.Width - 15, frm1.Location.Y);
                this.Location = startPosition;
            }
            ncounter = 0;
            frm1.selStr = String.Format("SELECT * FROM {0} ", frm1.tableName);
            frm1.lenOfStr = frm1.selStr.Length;
            
        }

        void CreateLabel(int i, int j)
        {
            Label l1 = new Label();

            
            //l1.Anchor = AnchorStyles.Left;
            
            l1.Text = frm1.arrOfNames[i, j];
            point1.X = this.Size.Width / 2 - l1.Size.Width / 2;
            l1.Location = point1;
            
            Controls.Add(l1);
            //point1.Y = point.Y + 50;
        }

        void CreateButton(int i, int j)
        {
            frm1.add1 = 100;
            if (point.X > this.Size.Width - 100)
            {
                point.Y = point.Y + 50;
                point.X = 20;
            }

            Button btn1 = new Button();
            btn1.Text = frm1.arrOfNames[i, j].ToString();
            btn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn1.BackColor = System.Drawing.Color.Gainsboro;
            btn1.TextAlign = ContentAlignment.MiddleCenter;
            //btn1.Name = String.Format("butn{0}", i);
            btn1.Name = frm1.arrOfNames[i, j].ToString();
            btn1.Location = point;
            this.Controls.Add(btn1);
            btn1.Click += Btn1_Click;
            point.X = point.X + frm1.add1;
        }


    }

}
