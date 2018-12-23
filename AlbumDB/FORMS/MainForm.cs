﻿using System;
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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.icon;
            button1.Image = new Bitmap(Properties.Resources.add, 16, 16);
            button2.Image = new Bitmap(Properties.Resources.remove, 16, 16);
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
                if (tabControl1.TabPages[i].Name == "uzytkownik") tabControl1.TabPages[i].Text = "Użytkownik";
                if (tabControl1.TabPages[i].Name == "grupa") tabControl1.TabPages[i].Text = "Grupa";
            }

            tabControl1.SelectedIndex = -1;
            tabControl1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(WindowManage.SwitchWindow(tabControl1.SelectedTab.Name,false,0))
            {
                int temp = tabControl1.SelectedIndex;
                tabControl1.SelectedIndex = -1;
                tabControl1.SelectedIndex = temp;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dataGridView1.Columns[0].Index && e.RowIndex >= 0 && tabControl1.SelectedTab.Name != "ocena")
            {
                if (WindowManage.SwitchWindow(tabControl1.SelectedTab.Name, true, (int)dataGridView1.Rows[e.RowIndex].Cells[1].Value))
                {
                    int temp = tabControl1.SelectedIndex;
                    tabControl1.SelectedIndex = -1;
                    tabControl1.SelectedIndex = temp;
                }
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && tabControl1.SelectedTab.Name != "ocena")
            {
                if (WindowManage.SwitchWindow(tabControl1.SelectedTab.Name, true, (int)dataGridView1.Rows[e.RowIndex].Cells[1].Value))
                {
                    int temp = tabControl1.SelectedIndex;
                    tabControl1.SelectedIndex = -1;
                    tabControl1.SelectedIndex = temp;
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex < 0) return;
            if(tabControl1.SelectedTab.Name == "ocena")
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }
            string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=..\\..\\albumy_muz.mdb;" + "Persist Security Info=True;" + "Jet OLEDB:Database Password=myPassword;";
            string tableName = tabControl1.SelectedTab.Name;

            Dictionary<string, string> sqlSelectTab = new Dictionary<string, string>();
            sqlSelectTab["ocena"] = "SELECT [ocena.id] As [ID], [ocena.wartosc] As [Wartość] FROM [ocena]";
            sqlSelectTab["gatunek"] = "SELECT [gatunek.id] As [ID], [gatunek.nazwa] As [Nazwa] FROM [gatunek] WHERE gatunek.czy_usuniete=False";
            sqlSelectTab["grupa"] = "SELECT [grupa.id] As [ID], [grupa.nazwa] As [Nazwa] FROM [grupa] WHERE grupa.czy_usuniete=False";
            sqlSelectTab["uzytkownik"] = "SELECT [uzytkownik.id] As [ID], [grupa.nazwa] As [Nazwa grupy], [uzytkownik.login] AS [Login], [uzytkownik.haslo] AS [Hasło] FROM ([uzytkownik] INNER JOIN grupa ON uzytkownik.id_grupy = grupa.id) WHERE grupa.czy_usuniete=False";
            sqlSelectTab["wytwornia"] = "SELECT [wytwornia.id] As [ID], [wytwornia.nazwa] As [Nazwa] FROM [wytwornia] WHERE wytwornia.czy_usuniete=False";
            sqlSelectTab["stanowisko"] = "SELECT [stanowisko.id] As [ID], [stanowisko.nazwa] As [Nazwa] FROM [stanowisko] WHERE stanowisko.czy_usuniete=False";
            sqlSelectTab["muzyk"] = "SELECT [muzyk.id] AS [ID], [muzyk.imie] As [Imię], [muzyk.nazwisko] As [Nazwisko], [muzyk.data_urodzenia] As [Data urodzenia] FROM [muzyk] WHERE muzyk.czy_usuniete=False";
            sqlSelectTab["piosenka"] = "SELECT [piosenka.id] As [ID], [album.nazwa] As [Nazwa albumu], [piosenka.nr_piosenki] As [Nr piosenki], [piosenka.tytul] As [Tytuł], [piosenka.czas] As [Czas] FROM [piosenka] INNER JOIN album ON piosenka.id_albumu=album.id WHERE piosenka.czy_usuniete=False";
            sqlSelectTab["ocena_albumu"] = "SELECT [ocena_albumu.id] As [ID], [album.nazwa] As [Tytuł albumu], [ocena.wartosc] As [Ocena], [ocena_albumu.recenzja] As [Recenzja] FROM (([ocena_albumu] INNER JOIN album ON ocena_albumu.id_albumu=album.id) INNER JOIN ocena ON ocena_albumu.id_ocena=ocena.id) WHERE ocena_albumu.czy_usuniete=False";
            sqlSelectTab["czlonek_zespolu"] = "SELECT [czlonek_zespolu.id] As [ID], [zespol.nazwa] As [Nazwa zespołu], [muzyk.imie] As [Imię muzyka], [muzyk.nazwisko] As [Nazwisko muzyka], [stanowisko.nazwa] As [Stanowisko] FROM ((([czlonek_zespolu] INNER JOIN zespol ON czlonek_zespolu.id_zespolu=zespol.id) INNER JOIN muzyk ON czlonek_zespolu.id_muzyka=muzyk.id) INNER JOIN stanowisko ON czlonek_zespolu.id_stanowiska=stanowisko.id) WHERE czlonek_zespolu.czy_usuniete=False";
            sqlSelectTab["zespol"] = "SELECT [zespol.id] As [ID], [zespol.nazwa] As [Nazwa zespołu], [zespol.rok_zalozenia] As [Rok założenia], [zespol.pochodzenie] As [Pochodzenie], (SELECT COUNT(*) FROM czlonek_zespolu WHERE czlonek_zespolu.id_zespolu=zespol.id AND czlonek_zespolu.czy_usuniete = False) As [Liczba członków] FROM [zespol] WHERE zespol.czy_usuniete=False";
            sqlSelectTab["album"] = "SELECT [album.id] As [ID], [album.nazwa] As [Tytuł albumu], [zespol.nazwa] As [Nazwa zespołu], [gatunek.nazwa] As [Gatunek], [wytwornia.nazwa] As [Wytwórnia], [album.data_wydania] As [Data wydania], (SELECT COUNT(*) FROM piosenka WHERE piosenka.id_albumu=album.id AND piosenka.czy_usuniete = False) As [Liczba piosenek], (SELECT IIF(LEN(Format(Sum(piosenka.czas),\"hh:nn:ss\")) = 0, \"00:00:00\", Format(Sum(piosenka.czas),\"hh:nn:ss\")) from piosenka where piosenka.id_albumu = album.id AND piosenka.czy_usuniete = False) As [Czas trwania], [album.opis] As [Opis albumu] FROM ((([album] INNER JOIN zespol ON album.id_zespolu=zespol.id) INNER JOIN gatunek ON album.id_gatunek=gatunek.id) INNER JOIN wytwornia ON album.id_wytwornia=wytwornia.id) WHERE album.czy_usuniete=False";

            using (OleDbConnection conn = new OleDbConnection(conString))
            using (OleDbCommand cmd = new OleDbCommand(sqlSelectTab[tableName], conn))
            {
                conn.Open();
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, tableName);
                    dataGridView1.Columns.Clear();
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

                if(tableName != "ocena")
                {
                    DataGridViewImageColumn imageColumnEdit = new DataGridViewImageColumn();
                    imageColumnEdit.Name = "edytuj";
                    imageColumnEdit.HeaderText = "     ";
                    imageColumnEdit.Image = new Bitmap(Properties.Resources.edit, 16, 16);
                    dataGridView1.Columns.Insert(0, imageColumnEdit);
                }
            }
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns[0].Index && e.RowIndex >= 0 && tabControl1.SelectedTab.Name != "ocena")
                dataGridView1.Cursor = Cursors.Hand;
            else
                dataGridView1.Cursor = Cursors.Default;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DeleteFromDatabase.Delete(tabControl1.SelectedTab.Name, dataGridView1.SelectedCells))
            {
                int temp = tabControl1.SelectedIndex;
                tabControl1.SelectedIndex = -1;
                tabControl1.SelectedIndex = temp;
                MessageBox.Show("Pomyślnie usunięto podany rekord wraz z jego powiązaniami (jeśli występowały).", "Informacja", MessageBoxButtons.OK);
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(tabControl1.SelectedTab.Name != "ocena")
            {
                if(e.KeyCode == Keys.Delete)
                {
                    if (DeleteFromDatabase.Delete(tabControl1.SelectedTab.Name, dataGridView1.SelectedCells))
                    {
                        int temp = tabControl1.SelectedIndex;
                        tabControl1.SelectedIndex = -1;
                        tabControl1.SelectedIndex = temp;
                        MessageBox.Show("Pomyślnie usunięto podany rekord wraz z jego powiązaniami (jeśli występowały).", "Informacja", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (tabControl1.SelectedTab.Name != "ocena")
                        {
                            if (WindowManage.SwitchWindow(tabControl1.SelectedTab.Name, true, (int)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[1].Value))
                            {
                                int temp = tabControl1.SelectedIndex;
                                tabControl1.SelectedIndex = -1;
                                tabControl1.SelectedIndex = temp;
                            }
                        }
                        e.SuppressKeyPress = true; //avoid default behavior
                    }
                    else
                    {
                        if(e.KeyCode == Keys.Insert)
                        {
                            if (WindowManage.SwitchWindow(tabControl1.SelectedTab.Name, false, 0))
                            {
                                int temp = tabControl1.SelectedIndex;
                                tabControl1.SelectedIndex = -1;
                                tabControl1.SelectedIndex = temp;
                            }
                        }
                    }
                }
            }
        }
    }
}
