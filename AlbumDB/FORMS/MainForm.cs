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
                    if (!Equals(row.Field<string>("TABLE_NAME"), "ocena"))
                        comboBox3.Items.Add(row.Field<string>("TABLE_NAME"));
                }
            }

            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                tabControl1.TabPages[i].Name = tabControl1.TabPages[i].Text;
                if (tabControl1.TabPages[i].Name == "ocena") tabControl1.TabPages[i].Text = "Ocena";
                if (tabControl1.TabPages[i].Name == "gatunek") tabControl1.TabPages[i].Text = "Gatunek";
                if (tabControl1.TabPages[i].Name == "wytwornia") tabControl1.TabPages[i].Text = "Wytwórnia";
                if (tabControl1.TabPages[i].Name == "stanowisko") tabControl1.TabPages[i].Text = "Stanowisko";
                if (tabControl1.TabPages[i].Name == "muzyk") tabControl1.TabPages[i].Text = "Muzyk";
                if (tabControl1.TabPages[i].Name == "piosenka") tabControl1.TabPages[i].Text = "Piosenka";
                if (tabControl1.TabPages[i].Name == "ocena_albumu") tabControl1.TabPages[i].Text = "Ocena albumu";
                if (tabControl1.TabPages[i].Name == "zespol") tabControl1.TabPages[i].Text = "Zespół";
                if (tabControl1.TabPages[i].Name == "czlonek_zespolu") tabControl1.TabPages[i].Text = "Członek zespołu";
                if (tabControl1.TabPages[i].Name == "album") tabControl1.TabPages[i].Text = "Album";
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
            string tableName = tabControl1.SelectedTab.Name;

            Dictionary<string, string> sqlSelectTab = new Dictionary<string, string>();
            sqlSelectTab["ocena"] = "SELECT [ocena.id] As [ID], [ocena.wartosc] As [Wartość] FROM [ocena]";
            sqlSelectTab["gatunek"] = "SELECT [gatunek.id] As [ID], [gatunek.nazwa] As [Nazwa] FROM [gatunek] WHERE gatunek.czy_usuniete=False";
            sqlSelectTab["wytwornia"] = "SELECT [wytwornia.id] As [ID], [wytwornia.nazwa] As [Nazwa] FROM [wytwornia] WHERE wytwornia.czy_usuniete=False";
            sqlSelectTab["stanowisko"] = "SELECT [stanowisko.id] As [ID], [stanowisko.nazwa] As [Nazwa] FROM [stanowisko] WHERE stanowisko.czy_usuniete=False";
            sqlSelectTab["muzyk"] = "SELECT [muzyk.id] AS [ID], [muzyk.imie] As [Imię], [muzyk.nazwisko] As [Nazwisko], [muzyk.data_urodzenia] As [Data urodzenia] FROM [muzyk] WHERE muzyk.czy_usuniete=False";
            sqlSelectTab["piosenka"] = "SELECT [piosenka.id] As [ID], [album.nazwa] As [Nazwa albumu], [piosenka.nr_piosenki] As [Nr piosenki], [piosenka.tytul] As [Tytuł], [piosenka.czas] As [Czas] FROM [piosenka] INNER JOIN album ON piosenka.id_albumu=album.id WHERE piosenka.czy_usuniete=False";
            sqlSelectTab["ocena_albumu"] = "SELECT [ocena_albumu.id] As [ID], [album.nazwa] As [Tytuł albumu], [ocena.wartosc] As [Ocena], [ocena_albumu.recenzja] As [Recenzja] FROM (([ocena_albumu] INNER JOIN album ON ocena_albumu.id_albumu=album.id) INNER JOIN ocena ON ocena_albumu.id_ocena=ocena.id) WHERE ocena_albumu.czy_usuniete=False";
            sqlSelectTab["czlonek_zespolu"] = "SELECT [czlonek_zespolu.id] As [ID], [zespol.nazwa] As [Nazwa zespołu], [muzyk.imie] As [Imię muzyka], [muzyk.nazwisko] As [Nazwisko muzyka], [stanowisko.nazwa] As [Stanowisko] FROM ((([czlonek_zespolu] INNER JOIN zespol ON czlonek_zespolu.id_zespolu=zespol.id) INNER JOIN muzyk ON czlonek_zespolu.id_muzyka=muzyk.id) INNER JOIN stanowisko ON czlonek_zespolu.id_stanowiska=stanowisko.id) WHERE czlonek_zespolu.czy_usuniete=False";
            sqlSelectTab["zespol"] = "SELECT [zespol.id] As [ID], [zespol.nazwa] As [Nazwa zespołu], [zespol.rok_zalozenia] As [Rok założenia], [zespol.pochodzenie] As [Pochodzenie], (SELECT COUNT(*) FROM czlonek_zespolu WHERE czlonek_zespolu.id_zespolu=zespol.id AND czlonek_zespolu.czy_usuniete = False) As [Liczba członków] FROM [zespol] WHERE zespol.czy_usuniete=False";
            sqlSelectTab["album"] = "SELECT [album.id] As [ID], [zespol.nazwa] As [Nazwa zespołu], [gatunek.nazwa] As [Gatunek], [wytwornia.nazwa] As [Wytwórnia], [album.nazwa] As [Tytuł albumu], [album.data_wydania] As [Data wydania], (SELECT COUNT(*) FROM piosenka WHERE piosenka.id_albumu=album.id AND piosenka.czy_usuniete = False) As [Liczba piosenek], [album.dlugosc] As [Czas trwania], [album.opis] As [Opis albumu] FROM ((([album] INNER JOIN zespol ON album.id_zespolu=zespol.id) INNER JOIN gatunek ON album.id_gatunek=gatunek.id) INNER JOIN wytwornia ON album.id_wytwornia=wytwornia.id) WHERE album.czy_usuniete=False";

            using (OleDbConnection conn = new OleDbConnection(conString))
            using (OleDbCommand cmd = new OleDbCommand(sqlSelectTab[tableName], conn))
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
        }
    }
}
