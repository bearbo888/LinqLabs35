using LinqLabs;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_3 : Form
    {
        public Frm作業_3()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

            var query = from q in nums group q by Group(q) into g select new { Mykey = g.Key, MyGroup = g, MyCount = g.Count() };

            foreach (var i in query)
            {
                TreeNode node = treeView1.Nodes.Add(i.MyGroup.ToString(), $"{i.Mykey}({i.MyCount})");

                foreach (var n in i.MyGroup)
                {
                    node.Nodes.Add(n.ToString());
                }
            }
        }

        private object Group(int n)
        {
            if (n < 5) { return "small"; }
            else if (n < 10) { return "Medium"; }
            else { return "Large"; }
        }

        private object Group2(int n)
        {
            if (n < 1000) { return "small"; }
            else if (n < 10000) { return "Medium"; }
            else { return "Large"; }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            //依 檔案大小 分組檔案 (大=>小)
            DirectoryInfo dir = new DirectoryInfo(@"c://Windows");
            FileInfo[] files = dir.GetFiles();

            var qry = from q in files
                      orderby q.Length descending
                      group q by Group2((int)q.Length) into g
                      select new { Key = g.Key, Count = g.Count(), Group = g };

            dataGridView1.DataSource = qry.ToList();

            foreach (var i in qry)
            {
                TreeNode node = treeView1.Nodes.Add(i.Key.ToString(), $"{i.Key}({i.Count})");

                foreach (var n in i.Group)
                {
                    node.Nodes.Add(n.ToString());
                }
            }
            treeView1.ExpandAll();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(@"c://Windows");
            FileInfo[] files = dir.GetFiles();

            var qry = from q in files
                      orderby q.CreationTime.Year descending
                      group q by q.CreationTime.Year into g
                      select new { Key = g.Key, Count = g.Count(), Group = g };

            dataGridView1.DataSource = qry.ToList();

            foreach (var i in qry)
            {
                TreeNode node = treeView1.Nodes.Add(i.Key.ToString(), $"{i.Key}({i.Count})");

                foreach (var n in i.Group)
                {
                    node.Nodes.Add(n.ToString());
                }
            }

            treeView1.ExpandAll();
        }
        NorthwindEntities entities = new NorthwindEntities();

        private void button8_Click(object sender, EventArgs e)
        {
            var qry = from q in entities.Products.AsEnumerable()
                      group q by Group((int)q.UnitPrice) into g
                      select new { Key = g.Key, Count = g.Count(), Group = g };

            dataGridView1.DataSource = qry.ToList();

            foreach (var i in qry)
            {
                TreeNode node = treeView1.Nodes.Add(i.Key.ToString(), $"{i.Key} ({i.Count})");
                foreach (var n in i.Group)
                {
                    node.Nodes.Add(n.ProductName);
                }
            }

            treeView1.ExpandAll();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var qry = from q in entities.Orders
                      group q by q.OrderDate.Value.Year into g
                      select new { Key = g.Key, Count = g.Count(), Group = g };

            dataGridView1.DataSource = qry.ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var qry = from q in entities.Orders
                      group q by new { q.OrderDate.Value.Year, q.OrderDate.Value.Month } into g
                      select new { Key = g.Key, Count = g.Count(), Group = g };

            dataGridView1.DataSource = qry.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var qry = entities.Order_Details.Sum(p => (double)p.UnitPrice * p.Quantity * (1 - p.Discount));
            MessageBox.Show(qry.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var qqqqq = entities.Employees.Where(x => (x.EmployeeID==4)).Select(x => new { x.FirstName });
            //dataGridView1.DataSource = qqqqq.ToList();


            //銷售最好的top 5業務員
            //var qry = from q in entities.Order_Details
            //           group q by q.Orders.Employee.EmployeeID
            //           into g
            //           select new 
            //           { 
            //               Name = entities.Employees.Where(x=>x.EmployeeID.Equals(4)).Select(x => new { x.FirstName }), 
            //               Count = entities.Order_Details.Where(x=>x.Orders.EmployeeID==g.Key).Sum(p => (double)p.UnitPrice * p.Quantity * (1 - p.Discount)) 
            //           };
            //dataGridView1.DataSource = qry.OrderByDescending(x=>x.Count).Take(5).ToList();


            var qry = from q in entities.Employees.AsEnumerable()
                      from p in q.Orders
                      from c in p.Order_Details
                      group q by q.EmployeeID
                      into g
                      select new
                      {
                          EmployeeID = entities.Employees.Where(x => x.EmployeeID.Equals(4)).Select(x => new { x.FirstName }),
                          Count = $"{entities.Order_Details.Where(x => x.Orders.EmployeeID == g.Key).Sum(p => (double)p.UnitPrice * p.Quantity * (1 - p.Discount)):c2}"
                      };
            dataGridView1.DataSource = qry.OrderByDescending(x => x.Count).Take(5).ToList();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            var qry = entities.Products.Where(x => x.UnitPrice > 300);

            MessageBox.Show(qry.ToList().Count>0?"True":"False");

            


        }
    }
}
