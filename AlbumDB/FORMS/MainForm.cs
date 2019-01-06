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
    public partial class MainForm : Form
    {
        int userGroup = 0;
        string userName = "";
        Dictionary<string, bool[]> permissionsTab = new Dictionary<string, bool[]>();

        public MainForm(int group, string user)
        {
            userGroup = group;
            userName = user;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.icon;
            button1.Image = new Bitmap(Properties.Resources.add, 16, 16);
            button2.Image = new Bitmap(Properties.Resources.remove, 16, 16);
            button3.Text = "Wyloguj (" + userName + ")";
            /* SET MainForm WIDTH & HEIGHT */
            this.MinimumSize = new Size(340 + button3.Width, 240);
            /* END SET MainForm WIDTH & HEIGHT */
            string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=..\\..\\albumy_muz.mdb;" + "Persist Security Info=True;" + "Jet OLEDB:Database Password=;";
            using (OleDbConnection conn = new OleDbConnection(conString))
            using (OleDbCommand cmd = new OleDbCommand("SELECT [wyswietl_album], [wyswietl_czlonek_zespolu], [wyswietl_gatunek], [wyswietl_muzyk], [wyswietl_ocena], [wyswietl_ocena_albumu], [wyswietl_piosenka], [wyswietl_stanowisko], [wyswietl_wytwornia], [wyswietl_zespol], [wyswietl_uzytkownik], [wyswietl_grupa], [dodaj_album], [dodaj_czlonek_zespolu], [dodaj_gatunek], [dodaj_muzyk], [dodaj_ocena_albumu], [dodaj_piosenka], [dodaj_stanowisko], [dodaj_wytwornia], [dodaj_zespol], [dodaj_uzytkownik], [dodaj_grupa], [edytuj_album], [edytuj_czlonek_zespolu], [edytuj_gatunek], [edytuj_muzyk], [edytuj_ocena_albumu], [edytuj_piosenka], [edytuj_stanowisko], [edytuj_wytwornia], [edytuj_zespol], [edytuj_uzytkownik], [edytuj_grupa], [usun_album], [usun_czlonek_zespolu], [usun_gatunek], [usun_muzyk], [usun_ocena_albumu], [usun_piosenka], [usun_stanowisko], [usun_wytwornia], [usun_zespol], [usun_uzytkownik], [usun_grupa] FROM [grupa] WHERE grupa.czy_usuniete=False AND grupa.id="+userGroup.ToString(), conn))
            {
                conn.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    permissionsTab["album"] = new bool[] {(bool)reader["wyswietl_album"],(bool)reader["dodaj_album"],(bool)reader["edytuj_album"],(bool)reader["usun_album"]};
                    permissionsTab["czlonek_zespolu"] = new bool[] {(bool)reader["wyswietl_czlonek_zespolu"],(bool)reader["dodaj_czlonek_zespolu"],(bool)reader["edytuj_czlonek_zespolu"],(bool)reader["usun_czlonek_zespolu"]};
                    permissionsTab["gatunek"] = new bool[] {(bool)reader["wyswietl_gatunek"],(bool)reader["dodaj_gatunek"],(bool)reader["edytuj_gatunek"],(bool)reader["usun_gatunek"] };
                    permissionsTab["muzyk"] = new bool[] {(bool)reader["wyswietl_muzyk"],(bool)reader["dodaj_muzyk"],(bool)reader["edytuj_muzyk"],(bool)reader["usun_muzyk"] };
                    permissionsTab["ocena"] = new bool[] {(bool)reader["wyswietl_ocena"],false,false,false};
                    permissionsTab["ocena_albumu"] = new bool[] {(bool)reader["wyswietl_ocena_albumu"],(bool)reader["dodaj_ocena_albumu"],(bool)reader["edytuj_ocena_albumu"],(bool)reader["usun_ocena_albumu"] };
                    permissionsTab["piosenka"] = new bool[] {(bool)reader["wyswietl_piosenka"],(bool)reader["dodaj_piosenka"],(bool)reader["edytuj_piosenka"],(bool)reader["usun_piosenka"] };
                    permissionsTab["stanowisko"] = new bool[] {(bool)reader["wyswietl_stanowisko"],(bool)reader["dodaj_stanowisko"],(bool)reader["edytuj_stanowisko"],(bool)reader["usun_stanowisko"] };
                    permissionsTab["wytwornia"] = new bool[] {(bool)reader["wyswietl_wytwornia"],(bool)reader["dodaj_wytwornia"],(bool)reader["edytuj_wytwornia"],(bool)reader["usun_wytwornia"] };
                    permissionsTab["zespol"] = new bool[] {(bool)reader["wyswietl_zespol"],(bool)reader["dodaj_zespol"],(bool)reader["edytuj_zespol"],(bool)reader["usun_zespol"] };
                    permissionsTab["uzytkownik"] = new bool[] {(bool)reader["wyswietl_uzytkownik"],(bool)reader["dodaj_uzytkownik"],(bool)reader["edytuj_uzytkownik"],(bool)reader["usun_uzytkownik"] };
                    permissionsTab["grupa"] = new bool[] {(bool)reader["wyswietl_grupa"],(bool)reader["dodaj_grupa"],(bool)reader["edytuj_grupa"],(bool)reader["usun_grupa"] };
                }
                DataTable dt = conn.GetSchema("Tables", new string[] { null, null, null, "TABLE" });
                conn.Close();

                foreach (DataRow row in dt.Rows)
                {
                    string tableName = row.Field<string>("TABLE_NAME");
                    if(permissionsTab[tableName][0]) tabControl1.TabPages.Add(tableName);
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

        private void ManageElement(bool mode)
        {
            int nr = 1;
            if (!permissionsTab[tabControl1.SelectedTab.Name][2]) nr = 0;

            int UserID = -1;
            if (tabControl1.SelectedTab.Name == "uzytkownik" && mode)
            {
                string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=..\\..\\albumy_muz.mdb;" + "Persist Security Info=True;" + "Jet OLEDB:Database Password=myPassword;";
                using (OleDbConnection conn = new OleDbConnection(conString))
                using (OleDbCommand cmd = new OleDbCommand("SELECT [id] FROM uzytkownik WHERE [login]=\"" + userName + "\" AND [id_grupy]="+userGroup+" AND [czy_usuniete]=false", conn))
                {
                    conn.Open();
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserID = (int)reader["id"];
                    }
                    conn.Close();
                }
            }

            if (WindowManage.SwitchWindow(tabControl1.SelectedTab.Name, mode, (int)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[nr].Value, permissionsTab))
            {
                if ((tabControl1.SelectedTab.Name == "uzytkownik" && UserID == (int)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[nr].Value || tabControl1.SelectedTab.Name == "grupa" && userGroup == (int)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[nr].Value) && mode)
                {
                    if(tabControl1.SelectedTab.Name == "grupa") MessageBox.Show("Edytowano uprawnienia grupy do której należy obecnie zalogowany użytkownik.\nAby kontynuować proszę zalogować się ponownie.", "Informacja", MessageBoxButtons.OK);
                    else MessageBox.Show("Edytowano dane obecnie użytkownika, który jest obecnie zalogowany.\nAby kontynuować proszę zalogować się ponownie.", "Informacja", MessageBoxButtons.OK);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                int temp = tabControl1.SelectedIndex;
                tabControl1.SelectedIndex = -1;
                tabControl1.SelectedIndex = temp;
            }
        }

        private void DeleteElement()
        {
            if (DeleteFromDatabase.Delete(tabControl1.SelectedTab.Name, permissionsTab[tabControl1.SelectedTab.Name][2], dataGridView1.SelectedCells))
            {
                int temp = tabControl1.SelectedIndex;
                tabControl1.SelectedIndex = -1;
                tabControl1.SelectedIndex = temp;
                MessageBox.Show("Pomyślnie usunięto podany rekord wraz z jego powiązaniami (jeśli występowały).", "Informacja", MessageBoxButtons.OK);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ManageElement(false);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dataGridView1.Columns[0].Index && e.RowIndex >= 0 && permissionsTab[tabControl1.SelectedTab.Name][2])
            {
                ManageElement(true);
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && permissionsTab[tabControl1.SelectedTab.Name][2])
            {
                ManageElement(true);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex < 0) return;

            string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=..\\..\\albumy_muz.mdb;" + "Persist Security Info=True;" + "Jet OLEDB:Database Password=myPassword;";
            string tableName = tabControl1.SelectedTab.Name;

            button1.Enabled = permissionsTab[tableName][1]; //Add
            button2.Enabled = permissionsTab[tableName][3]; //Delete

            Dictionary<string, string> sqlSelectTab = new Dictionary<string, string>();
            sqlSelectTab["ocena"] = "SELECT [ocena.id] As [ID], [ocena.wartosc] As [Wartość] FROM [ocena]";
            sqlSelectTab["gatunek"] = "SELECT [gatunek.id] As [ID], [gatunek.nazwa] As [Nazwa] FROM [gatunek] WHERE gatunek.czy_usuniete=False";
            sqlSelectTab["grupa"] = "SELECT [grupa.id] As [ID], [grupa.nazwa] As [Nazwa], [grupa.wyswietl_album] As [Wyświetl album], [grupa.wyswietl_czlonek_zespolu] As [Wyświetl członek zespołu], [grupa.wyswietl_gatunek] As [Wyświetl gatunek], [grupa.wyswietl_muzyk] As [Wyświetl muzyk], [grupa.wyswietl_ocena] As [Wyświetl ocena], [grupa.wyswietl_ocena_albumu] As [Wyświetl ocena albumu], [grupa.wyswietl_piosenka] As [Wyświetl piosenka], [grupa.wyswietl_stanowisko] As [Wyświetl stanowisko], [grupa.wyswietl_wytwornia] As [Wyświetl wytwórnia], [grupa.wyswietl_zespol] As [Wyświetl zespół], [grupa.wyswietl_uzytkownik] As [Wyświetl użytkownik], [grupa.wyswietl_grupa] As [Wyświetl grupa], [grupa.dodaj_album] As [Dodaj album], [grupa.dodaj_czlonek_zespolu] As [Dodaj członek zespołu], [grupa.dodaj_gatunek] As [Dodaj gatunek], [grupa.dodaj_muzyk] As [Dodaj muzyk], [grupa.dodaj_ocena_albumu] As [Dodaj ocena albumu], [grupa.dodaj_piosenka] As [Dodaj piosenka], [grupa.dodaj_stanowisko] As [Dodaj stanowisko], [grupa.dodaj_wytwornia] As [Dodaj wytwórnia], [grupa.dodaj_zespol] As [Dodaj zespół], [grupa.dodaj_uzytkownik] As [Dodaj użytkownik], [grupa.dodaj_grupa] As [Dodaj grupa], [grupa.edytuj_album] As [Edytuj album], [grupa.edytuj_czlonek_zespolu] As [Edytuj członek zespołu], [grupa.edytuj_gatunek] As [Edytuj gatunek], [grupa.edytuj_muzyk] As [Edytuj muzyk], [grupa.edytuj_ocena_albumu] As [Edytuj ocena albumu], [grupa.edytuj_piosenka] As [Edytuj piosenka], [grupa.edytuj_stanowisko] As [Edytuj stanowisko], [grupa.edytuj_wytwornia] As [Edytuj wytwórnia], [grupa.edytuj_zespol] As [Edytuj zespół], [grupa.edytuj_uzytkownik] As [Edytuj użytkownik], [grupa.edytuj_grupa] As [Edytuj grupa], [grupa.usun_album] As [Usuń album], [grupa.usun_czlonek_zespolu] As [Usuń członek zespołu], [grupa.usun_gatunek] As [Usuń gatunek], [grupa.usun_muzyk] As [Usuń muzyk], [grupa.usun_ocena_albumu] As [Usuń ocena albumu], [grupa.usun_piosenka] As [Usuń piosenka], [grupa.usun_stanowisko] As [Usuń stanowisko], [grupa.usun_wytwornia] As [Usuń wytwórnia], [grupa.usun_zespol] As [Usuń zespół], [grupa.usun_uzytkownik] As [Usuń użytkownik], [grupa.usun_grupa] As [Usuń grupa] FROM [grupa] WHERE grupa.czy_usuniete=False";
            sqlSelectTab["uzytkownik"] = "SELECT [uzytkownik.id] As [ID], [grupa.nazwa] As [Nazwa grupy], [uzytkownik.login] AS [Login], [uzytkownik.haslo] AS [Hasło] FROM ([uzytkownik] INNER JOIN grupa ON uzytkownik.id_grupy = grupa.id) WHERE uzytkownik.czy_usuniete=False";
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

                if(permissionsTab[tableName][2])
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
            if (e.ColumnIndex == dataGridView1.Columns[0].Index && e.RowIndex >= 0 && permissionsTab[tabControl1.SelectedTab.Name][2])
                dataGridView1.Cursor = Cursors.Hand;
            else
                dataGridView1.Cursor = Cursors.Default;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteElement();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (tabControl1.SelectedIndex < 0) return;

            if(e.KeyCode == Keys.Delete && permissionsTab[tabControl1.SelectedTab.Name][3])
            {
                if ((tabControl1.SelectedTab.Name == "grupa" && dataGridView1.SelectedRows[0].Index <= 2) || (tabControl1.SelectedTab.Name == "uzytkownik" && dataGridView1.SelectedRows[0].Index == 0)) return;

                DeleteElement();
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (permissionsTab[tabControl1.SelectedTab.Name][2])
                    {
                        ManageElement(true);
                    }
                    e.SuppressKeyPress = true; //avoid default behavior
                }
                else
                {
                    if(e.KeyCode == Keys.Insert && permissionsTab[tabControl1.SelectedTab.Name][1])
                    {
                        ManageElement(false);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if((tabControl1.SelectedTab.Name == "grupa" || tabControl1.SelectedTab.Name == "uzytkownik") && dataGridView1.SelectedRows.Count > 0)
            {
                if ((tabControl1.SelectedTab.Name == "grupa" && dataGridView1.SelectedRows[0].Index <= 2) || (tabControl1.SelectedTab.Name == "uzytkownik" && dataGridView1.SelectedRows[0].Index == 0)) button2.Enabled = false;
                else button2.Enabled = true;
            }
        }
    }
}
