using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();
            ordersTableAdapter1.Fill(nwDataSet1.Orders);
            order_DetailsTableAdapter1.Fill(nwDataSet1.Order_Details);
            productsTableAdapter1.Fill(nwDataSet1.Products);
        }
        int nowpage = 0;

        private void button12_Click(object sender, EventArgs e)
        {
            var qry = from q in nwDataSet1.Orders select q;
            int i = int.Parse(textBox1.Text);

            if (nowpage > 0)
            {
                nowpage--;
                gvOrder.DataSource = qry.Skip(i * nowpage).Take(i).ToList();
            }
            //var qry = from q in nwDataSet1.Products select q;
            //int i = int.Parse(textBox1.Text);

            //if (nowpage > 0)
            //{
            //    nowpage--;
            //    gvOrder.DataSource = qry.Skip(i * nowpage).Take(i).ToList();
            //    button1.Text = nowpage.ToString();
            //}
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var qry = from q in nwDataSet1.Orders select q;
            int i = int.Parse(textBox1.Text);
            int totalpage = qry.Count() / int.Parse(textBox1.Text);

            if (nowpage <= totalpage)
            {
                gvOrder.DataSource = qry.Skip(i * nowpage).Take(i).ToList();

                if (nowpage < totalpage)
                    nowpage++;
            }
            //var qry = from q in nwDataSet1.Products select q;
            //int i = int.Parse(textBox1.Text);
            //int totalpage = qry.Count() / int.Parse(textBox1.Text);

            //if (nowpage < totalpage)
            //{
            //    nowpage++;
            //    gvOrder.DataSource = qry.Skip(i * nowpage).Take(i).ToList();
                

            //    button1.Text = nowpage.ToString();
            //}
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            var files = dir.GetFiles("*.log").ToList();
            this.gvOrder.DataSource = files;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {        
            var qry = from q in nwDataSet1.Orders select q;
            gvOrder.DataSource = qry.ToList();
            
            var qry2 = from q in nwDataSet1.Order_Details select q;
            gvOrderDetail.DataSource = qry2.ToList();
        }

        private void btnYear_Click(object sender, EventArgs e)
        {
            var qry = from q in nwDataSet1.Orders where q.OrderDate.Year == (int)cmbYear.SelectedItem select q;
            gvOrder.DataSource = qry.ToList();

            var qry2 = from q in nwDataSet1.Order_Details where q.OrderID == qry.First().OrderID select q;
            gvOrderDetail.DataSource = qry2.ToList();
        }

        private void Frm作業_1_Load(object sender, EventArgs e)
        {
            var qry = from q in nwDataSet1.Orders select q.OrderDate.Year;
            cmbYear.DataSource = qry.Distinct().ToList();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            var files = dir.GetFiles().Where(f=>f.CreationTime.Year == 2020).ToList();
            this.gvOrder.DataSource = files;
        }

        private void btnBig_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            var files = dir.GetFiles().Where(f => f.Length > 1000000).ToList();
            this.gvOrder.DataSource = files;
        }

        private void gvOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                int value = (int)gvOrder.Rows[e.RowIndex].Cells[0].Value;
                var qry2 = from q in nwDataSet1.Order_Details where q.OrderID == value select q;
                gvOrderDetail.DataSource = qry2.ToList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var qry = from q in nwDataSet1.Products select q;
            gvOrder.DataSource = qry.ToList();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var qry = from q in nwDataSet1.Products select q;
            gvOrder.DataSource = qry.ToList();
        }
    }
}
