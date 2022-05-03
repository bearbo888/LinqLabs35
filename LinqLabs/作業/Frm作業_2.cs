using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            productPhotoTableAdapter1.Fill(this.awDataSet1.ProductPhoto);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var query = from q in this.awDataSet1.ProductPhoto select q;
            lblMaster.Text += " 總共:" + query.Count();
            dataGridView1.DataSource = query.ToList();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                int value = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                
                var qry = from q in this.awDataSet1.ProductPhoto where q.ProductPhotoID == value select q.LargePhoto;
                MemoryStream ms = new MemoryStream((byte[]) qry.FirstOrDefault());
                pictureBox1.Image = Image.FromStream(ms);
            }           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var query = from q in this.awDataSet1.ProductPhoto where q.ModifiedDate>=dateTimePicker1.Value && q.ModifiedDate<=dateTimePicker2.Value select q;
            lblMaster.Text += " 總共:" + query.Count();
            dataGridView1.DataSource = query.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var query = from q in this.awDataSet1.ProductPhoto where q.ModifiedDate.Year == (int)comboBox3.SelectedItem select q;
            lblMaster.Text += " 總共:" + query.Count();
            dataGridView1.DataSource = query.ToList();
        }

        private void Frm作業_2_Load(object sender, EventArgs e)
        {
            var query = from q in this.awDataSet1.ProductPhoto  select q.ModifiedDate.Year ;
            comboBox3.DataSource = query.Distinct().ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int min = 3 * comboBox2.SelectedIndex;
            int max = 3 * (comboBox2.SelectedIndex + 1) + 1;

            var query = from q in this.awDataSet1.ProductPhoto where q.ModifiedDate.Year == (int)comboBox3.SelectedItem select q;

            if (comboBox2.Text != "All")
            {
                query = query.Where(q => q.ModifiedDate.Month > min && q.ModifiedDate.Month < max);
            }
            lblMaster.Text +=  " 總共:" + query.Count();
            dataGridView1.DataSource = query.ToList();
        }
    }
}
