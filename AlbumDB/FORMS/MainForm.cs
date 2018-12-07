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
            this.Icon = Properties.Resources.icon;
            string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=..\\..\\albumy_muz.mdb;" + "Persist Security Info=True;" + "Jet OLEDB:Database Password=;";
            using (OleDbConnection conn = new OleDbConnection(conString))
            using (OleDbCommand cmd = new OleDbCommand("", conn))
            {
                conn.Open();
                DataTable dt = conn.GetSchema("Tables", new string[] { null, null, null, "TABLE" });
                conn.Close();

                foreach (DataRow row in dt.Rows)
                {
                    tabControl1.TabPages.Add(row.Field<string>("TABLE_NAME"));
                }
            }
            tabControl1.SelectedIndex = -1;
            tabControl1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex < 0) return;

            WindowManage.SwitchWindow(comboBox3.SelectedItem.ToString());
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex < 0) return;
            string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=..\\..\\albumy_muz.mdb;" + "Persist Security Info=True;" + "Jet OLEDB:Database Password=myPassword;";
            string tableName = tabControl1.SelectedTab.Text;

            Dictionary<string, string> sqlSelectTab = new Dictionary<string, string>();
            sqlSelectTab["ocena"] = "SELECT * FROM [@first]";
            sqlSelectTab["gatunek"] = "SELECT * FROM [@first]";
            sqlSelectTab["wytwornia"] = "SELECT * FROM [@first]";
            sqlSelectTab["stanowisko"] = "SELECT * FROM [@first]";
            sqlSelectTab["muzyk"] = "SELECT * FROM [@first]";
            sqlSelectTab["piosenka"] = "SELECT * FROM [@first]";
            sqlSelectTab["ocena_albumu"] = "SELECT * FROM [@first]";
            sqlSelectTab["czlonek_zespolu"] = "SELECT * FROM [@first]";
            sqlSelectTab["zespol"] = "SELECT * FROM [@first]";
            sqlSelectTab["album"] = "SELECT * FROM [@first]";

            using (OleDbConnection conn = new OleDbConnection(conString))
            using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + tableName + "]", conn))
            {
                cmd.Parameters.AddWithValue("@first", tableName);
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
                conn.Close();
            }
        }
    }
}
