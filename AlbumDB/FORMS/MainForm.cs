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
            string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=..\\..\\albumy_muz.mdb;" + "Persist Security Info=True;" + "Jet OLEDB:Database Password=;";
            using (OleDbConnection conn = new OleDbConnection(conString))
            using (OleDbCommand cmd = new OleDbCommand("", conn))
            {
                conn.Open();
                DataTable dt = conn.GetSchema("Tables", new string[] { null, null, null, "TABLE" });
                foreach (DataRow row in dt.Rows)
                {
                    comboBox1.Items.Add(row.Field<string>("TABLE_NAME"));
                    if(!Equals(row.Field<string>("TABLE_NAME"),"ocena"))
                        comboBox3.Items.Add(row.Field<string>("TABLE_NAME"));
                }
                conn.Close();
            }
            comboBox2.Items.Add("album + zespol + wytwornia + gatunek + piosenka");
            comboBox2.Items.Add("zespol + czlonek_zespolu + muzyk + stanowisko");
            comboBox2.Items.Add("album + ocena + ocena_albumu");

            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0 && comboBox2.SelectedIndex >= 0 || comboBox1.SelectedIndex < 0) return;
            string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=..\\..\\albumy_muz.mdb;" + "Persist Security Info=True;" + "Jet OLEDB:Database Password=myPassword;";
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
                conn.Close();
            }
            comboBox2.SelectedIndex = -1;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0 && comboBox2.SelectedIndex < 0 || comboBox2.SelectedIndex < 0) return;
            string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=..\\..\\albumy_muz.mdb;" + "Persist Security Info=True;" + "Jet OLEDB:Database Password=myPassword;";
            string[] commandTab = { "SELECT album.nazwa AS [Nazwa albumu], zespol.nazwa AS [Zespół], wytwornia.nazwa AS [Wytwórnia], gatunek.nazwa AS [Gatunek], piosenka.nr_piosenki AS [Nr piosenki], piosenka.tytul AS [Tytuł], piosenka.czas AS [Czas trwania] FROM((((album INNER JOIN piosenka ON album.id = piosenka.id_albumu) INNER JOIN wytwornia ON album.id_wytwornia = wytwornia.id) INNER JOIN gatunek ON album.id_gatunek = gatunek.id) INNER JOIN zespol ON album.id_zespolu = zespol.id) ORDER BY album.id",
                                    "SELECT zespol.nazwa AS [Zespół], muzyk.imie AS [Imie], muzyk.nazwisko AS [Nazwisko], muzyk.data_urodzenia AS [Data urodzenia], stanowisko.nazwa AS [Stanowisko] FROM (((zespol INNER JOIN czlonek_zespolu ON zespol.id=czlonek_zespolu.id_zespolu) INNER JOIN muzyk ON czlonek_zespolu.id_muzyka=muzyk.id) INNER JOIN stanowisko ON czlonek_zespolu.id_stanowiska=stanowisko.id) ORDER BY zespol.id",
                                    "SELECT album.nazwa AS [Nazwa albumu], ocena.wartosc AS [Ocena], ocena_albumu.recenzja AS [Recenzja] FROM ((album INNER JOIN ocena_albumu ON album.id = ocena_albumu.id_albumu) INNER JOIN ocena ON ocena_albumu.id_ocena = ocena.id) ORDER BY album.id" };
            int selectedNumber = comboBox2.SelectedIndex;
            using (OleDbConnection conn = new OleDbConnection(conString))
            using (OleDbCommand cmd = new OleDbCommand(commandTab[selectedNumber], conn))
            {
                conn.Open();
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "tab");
                    dataGridView1.DataSource = ds.Tables[0];

                    if (selectedNumber == 0)
                    {
                        dataGridView1.Columns[6].DefaultCellStyle.Format = "T";
                    }
                }
                conn.Close();
            }
            comboBox1.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex < 0) return;

            WindowManage.SwitchWindow(comboBox3.SelectedIndex, comboBox3.SelectedItem.ToString());

            /* Update loaded table */
            if (comboBox1.SelectedIndex >= 0)
            {
                int temp = comboBox1.SelectedIndex;
                comboBox1.SelectedIndex = -1;
                comboBox1.SelectedIndex = temp;
            }
            if (comboBox2.SelectedIndex >= 0)
            {
                int temp = comboBox2.SelectedIndex;
                comboBox2.SelectedIndex = -1;
                comboBox2.SelectedIndex = temp;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
