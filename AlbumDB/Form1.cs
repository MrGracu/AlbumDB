using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlbumDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'albumyDataSet.album' table. You can move, or remove it, as needed.
            this.albumTableAdapter.Fill(this.albumyDataSet.album);
            int i = albumyDataSet.Tables.Count;
            for (int j = 0; j < i; j++)
            {
                comboBox1.Items.Add(Convert.ToString(albumyDataSet.Tables[j].TableName));
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=albumy_muz.mdb;" + "Persist Security Info=True;" + "Jet OLEDB:Database Password=myPassword;";
            string tableName = comboBox1.SelectedItem.ToString();
            using (OleDbConnection conn = new OleDbConnection(conString))
            using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + tableName + "]", conn))
            {
                conn.Open();
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, tableName);
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
            //DataTable table = new DataTable();
            //table = albumyDataSet.Tables[comboBox1.SelectedItem.ToString()];
            //MessageBox.Show(Convert.ToString(albumyDataSet.Tables[comboBox1.SelectedItem.ToString()].Rows[0][1]));
            //dataGridView1.DataSource = table;
            //SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM "+ comboBox1.SelectedItem.ToString(), "Data Source=localhost;Initial Catalog=AlbumDB;" + "Integrated Security=true;");
            //dataGridView1.DataSource = albumyDataSet;
            //dataGridView1.DataMember = comboBox1.SelectedItem.ToString();
            //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
