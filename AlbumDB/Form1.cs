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
            string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=albumy_muz.mdb;" + "Persist Security Info=True;" + "Jet OLEDB:Database Password=myPassword;";
            using (OleDbConnection conn = new OleDbConnection(conString))
            using (OleDbCommand cmd = new OleDbCommand("", conn))
            {
                conn.Open();
                DataTable dt = conn.GetSchema("Tables", new string[] { null, null, null, "TABLE" });
                foreach(DataRow row in dt.Rows)
                {
                    comboBox1.Items.Add(row.Field<string>("TABLE_NAME"));
                }
                
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

                    if (tableName == "album")
                    {
                        dataGridView1.Columns[7].DefaultCellStyle.Format = "T";
                    }
                    if (tableName == "piosenka")
                    {
                        dataGridView1.Columns[4].DefaultCellStyle.Format = "T";
                    }
                }
            }
        }
    }
}
