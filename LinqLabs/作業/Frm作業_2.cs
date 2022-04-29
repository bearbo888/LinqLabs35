using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
            productTableAdapter1.Fill(this.awDataSet1.Product);
            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var query = from q in this.awDataSet1.Product select q;
            bindingSource1.DataSource = query.ToList();
            dataGridView1.DataSource = bindingSource1;
        }
    }
}
