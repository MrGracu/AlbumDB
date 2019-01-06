using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.OleDb;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlbumDB
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            DialogResult mainResult = DialogResult.OK;
            while (mainResult == DialogResult.OK)
            {
                FORMS.LoginForm loginForm = new FORMS.LoginForm();
                DialogResult result = loginForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    MainForm mainForm = new MainForm(loginForm.ReturnGroup, loginForm.ReturnUser);
                    mainResult = mainForm.ShowDialog();
                }
                else mainResult = DialogResult.Cancel;
            }
        }
    }

    public static class WindowManage
    {
        public static bool SwitchWindow(string selectedItem, bool mode, int id, Dictionary<string, bool[]> permissionsTab)
        {
            bool returnValue = false;
            DialogResult result = DialogResult.Cancel;
            switch (selectedItem)
            {
                case "album":
                    FORMS.AlbumForm albumForm = new FORMS.AlbumForm(mode, id, permissionsTab);
                    result = albumForm.ShowDialog();
                    break;
                case "czlonek_zespolu":
                    FORMS.CzlonekZespoluForm czlonekZespoluForm = new FORMS.CzlonekZespoluForm(mode, id, permissionsTab);
                    result = czlonekZespoluForm.ShowDialog();
                    break;
                case "gatunek":
                    FORMS.GatunekForm gatunekForm = new FORMS.GatunekForm(mode, id);
                    result = gatunekForm.ShowDialog();
                    break;
                case "grupa":
                    FORMS.GrupaForm grupaForm = new FORMS.GrupaForm(mode, id, permissionsTab);
                    result = grupaForm.ShowDialog();
                    break;
                case "muzyk":
                    FORMS.MuzykForm muzykForm = new FORMS.MuzykForm(mode, id);
                    result = muzykForm.ShowDialog();
                    break;
                case "ocena_albumu":
                    FORMS.OcenaAlbumuForm ocenaAlbumuForm = new FORMS.OcenaAlbumuForm(mode, id, permissionsTab);
                    result = ocenaAlbumuForm.ShowDialog();
                    break;
                case "piosenka":
                    FORMS.PiosenkaForm piosenkaForm = new FORMS.PiosenkaForm(mode, id, permissionsTab);
                    result = piosenkaForm.ShowDialog();
                    break;
                case "stanowisko":
                    FORMS.StanowiskoForm stanowiskoForm = new FORMS.StanowiskoForm(mode, id);
                    result = stanowiskoForm.ShowDialog();
                    break;
                case "uzytkownik":
                    FORMS.UzytkownikForm uzytkownikForm = new FORMS.UzytkownikForm(mode, id, permissionsTab);
                    result = uzytkownikForm.ShowDialog();
                    break;
                case "wytwornia":
                    FORMS.WytworniaForm wytworniaForm = new FORMS.WytworniaForm(mode, id);
                    result = wytworniaForm.ShowDialog();
                    break;
                case "zespol":
                    FORMS.ZespolForm zespolForm = new FORMS.ZespolForm(mode, id);
                    result = zespolForm.ShowDialog();
                    break;
            }

            if (result == DialogResult.OK) returnValue = true;
            else returnValue = false;

            return returnValue;
        }
    }

    public static class DeleteFromDatabase
    {
        const string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=..\\..\\albumy_muz.mdb;" + "Persist Security Info=True;" + "Jet OLEDB:Database Password=myPassword;";

        private static bool deleteOcena_albumu_Piosenka(bool mode, string table, int id) //true - bezposrednio po id; false - posrednio po id_albumu
        {
            bool returnValue = false;
            string wybor = "";
            if (mode) wybor = "UPDATE " + table + " SET [czy_usuniete]=true WHERE id=" + id.ToString() + " AND [czy_usuniete]=false";
            else wybor = "UPDATE " + table + " SET [czy_usuniete]=true WHERE id_albumu=" + id.ToString() + " AND [czy_usuniete]=false";
            using (OleDbConnection conn = new OleDbConnection(conString))
            {
                using (OleDbCommand cmd = new OleDbCommand(wybor, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    returnValue = true;
                }
            }
            return returnValue;
        }

        private static bool deleteAlbum(int id)
        {
            bool returnValue = false;
            deleteOcena_albumu_Piosenka(false, "piosenka", id);
            deleteOcena_albumu_Piosenka(false, "ocena_albumu", id);
            string wybor = "UPDATE album SET [czy_usuniete]=true WHERE id=" + id.ToString() + " AND [czy_usuniete]=false";
            using (OleDbConnection conn = new OleDbConnection(conString))
            {
                using (OleDbCommand cmd = new OleDbCommand(wybor, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    returnValue = true;
                }
            }
            return returnValue;
        }

        private static bool deleteWytwornia(int id)
        {
            bool returnValue = false;
            using (OleDbConnection conn = new OleDbConnection(conString))
            using (OleDbCommand cmd = new OleDbCommand("SELECT id FROM album WHERE id_wytwornia=" + id.ToString() + " AND [czy_usuniete]=false", conn))
            {
                conn.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    returnValue = deleteAlbum((int)reader["id"]);
                }
                conn.Close();
            }
            string wybor = "UPDATE wytwornia SET [czy_usuniete]=true WHERE id=" + id.ToString() + " AND [czy_usuniete]=false";
            using (OleDbConnection conn = new OleDbConnection(conString))
            {
                using (OleDbCommand cmd = new OleDbCommand(wybor, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    returnValue = true;
                }
            }
            return returnValue;
        }

        private static bool deleteGatunek(int id)
        {
            bool returnValue = false;
            using (OleDbConnection conn = new OleDbConnection(conString))
            using (OleDbCommand cmd = new OleDbCommand("SELECT id FROM album WHERE id_gatunek=" + id.ToString() + " AND [czy_usuniete]=false", conn))
            {
                conn.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    deleteAlbum((int)reader["id"]);
                }
                conn.Close();
            }
            string wybor = "UPDATE gatunek SET [czy_usuniete]=true WHERE id=" + id.ToString() + " AND [czy_usuniete]=false";
            using (OleDbConnection conn = new OleDbConnection(conString))
            {
                using (OleDbCommand cmd = new OleDbCommand(wybor, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    returnValue = true;
                }
            }
            return returnValue;
        }

        private static bool deleteCzlonek_zespolu(int id)
        {
            bool returnValue = false;
            string wybor = "UPDATE czlonek_zespolu SET [czy_usuniete]=true WHERE id=" + id.ToString() + " AND [czy_usuniete]=false";
            using (OleDbConnection conn = new OleDbConnection(conString))
            {
                using (OleDbCommand cmd = new OleDbCommand(wybor, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    returnValue = true;
                }
            }
            return returnValue;
        }

        private static bool deleteZespol(int id)
        {
            bool returnValue = false;
            using (OleDbConnection conn = new OleDbConnection(conString))
            using (OleDbCommand cmd = new OleDbCommand("SELECT id FROM album WHERE id_zespolu=" + id.ToString() + " AND [czy_usuniete]=false", conn))
            {
                conn.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    deleteAlbum((int)reader["id"]);
                }
                conn.Close();
            }
            using (OleDbConnection conn = new OleDbConnection(conString))
            using (OleDbCommand cmd = new OleDbCommand("SELECT id FROM czlonek_zespolu WHERE id_zespolu=" + id.ToString() + " AND [czy_usuniete]=false", conn))
            {
                conn.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    deleteCzlonek_zespolu((int)reader["id"]);
                }
                conn.Close();
            }
            string wybor = "UPDATE zespol SET [czy_usuniete]=true WHERE id=" + id.ToString() + " AND [czy_usuniete]=false";
            using (OleDbConnection conn = new OleDbConnection(conString))
            {
                using (OleDbCommand cmd = new OleDbCommand(wybor, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    returnValue = true;
                }
            }
            return returnValue;
        }

        private static bool deleteMuzyk(int id)
        {
            bool returnValue = false;
            using (OleDbConnection conn = new OleDbConnection(conString))
            using (OleDbCommand cmd = new OleDbCommand("SELECT id FROM czlonek_zespolu WHERE id_muzyka=" + id.ToString() + " AND [czy_usuniete]=false", conn))
            {
                conn.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    deleteCzlonek_zespolu((int)reader["id"]);
                }
                conn.Close();
            }
            string wybor = "UPDATE muzyk SET [czy_usuniete]=true WHERE id=" + id.ToString() + " AND [czy_usuniete]=false";
            using (OleDbConnection conn = new OleDbConnection(conString))
            {
                using (OleDbCommand cmd = new OleDbCommand(wybor, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    returnValue = true;
                }
            }
            return returnValue;
        }

        private static bool deleteStanowisko(int id)
        {
            bool returnValue = false;
            using (OleDbConnection conn = new OleDbConnection(conString))
            using (OleDbCommand cmd = new OleDbCommand("SELECT id FROM czlonek_zespolu WHERE id_stanowiska=" + id.ToString() + " AND [czy_usuniete]=false", conn))
            {
                conn.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    deleteCzlonek_zespolu((int)reader["id"]);
                }
                conn.Close();
            }
            string wybor = "UPDATE stanowisko SET [czy_usuniete]=true WHERE id=" + id.ToString() + " AND [czy_usuniete]=false";
            using (OleDbConnection conn = new OleDbConnection(conString))
            {
                using (OleDbCommand cmd = new OleDbCommand(wybor, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    returnValue = true;
                }
            }
            return returnValue;
        }

        private static bool deleteUzytkownik(int id)
        {
            bool returnValue = false;
            using (OleDbConnection conn = new OleDbConnection(conString))
            using (OleDbCommand cmd = new OleDbCommand("UPDATE uzytkownik SET [czy_usuniete]=true WHERE id=" + id.ToString() + " AND [czy_usuniete]=false", conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                returnValue = true;
            }
            return returnValue;
        }

        private static bool deleteGrupa(int id)
        {
            bool returnValue = false;
            using (OleDbConnection conn = new OleDbConnection(conString))
            {
                using (OleDbCommand cmd = new OleDbCommand("SELECT id FROM uzytkownik WHERE id_grupy=" + id.ToString() + " AND [czy_usuniete]=false", conn))
                {
                    conn.Open();
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        deleteUzytkownik((int)reader["id"]);
                    }
                    conn.Close();
                }
                using (OleDbCommand cmd = new OleDbCommand("UPDATE grupa SET [czy_usuniete]=true WHERE id=" + id.ToString() + " AND [czy_usuniete]=false", conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    returnValue = true;
                }
            }
            return returnValue;
        }

        public static bool Delete(string table, bool mode, DataGridViewSelectedCellCollection list) //Ustawienie wartosci czy_usuniete na true; mode - zmienia nr kolumny z id w zaleznosci czy mozna edytowac
        {
            DialogResult result = MessageBox.Show("Czy na pewno chcesz usunąć ten rekord wraz z jego powiązaniami (jeśli istnieją)?", "Ostrzeżenie", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return false;

            int nr = 1;
            if (!mode) nr = 0;
            bool returnValue = false;

            switch (table)
            {
                case "album":
                    returnValue = deleteAlbum((int)list[nr].Value);
                    break;
                case "czlonek_zespolu":
                    returnValue = deleteCzlonek_zespolu((int)list[nr].Value);
                    break;
                case "gatunek":
                    returnValue = deleteGatunek((int)list[nr].Value);
                    break;
                case "muzyk":
                    returnValue = deleteMuzyk((int)list[nr].Value);
                    break;
                case "ocena_albumu":
                    returnValue = deleteOcena_albumu_Piosenka(true, table, (int)list[nr].Value);
                    break;
                case "piosenka":
                    returnValue = deleteOcena_albumu_Piosenka(true, table, (int)list[nr].Value);
                    break;
                case "stanowisko":
                    returnValue = deleteStanowisko((int)list[nr].Value);
                    break;
                case "wytwornia":
                    returnValue = deleteWytwornia((int)list[nr].Value);
                    break;
                case "zespol":
                    returnValue = deleteZespol((int)list[nr].Value);
                    break;
                case "grupa":
                    returnValue = deleteGrupa((int)list[nr].Value);
                    break;
                case "uzytkownik":
                    returnValue = deleteUzytkownik((int)list[nr].Value);
                    break;
            }
            return returnValue;
        }
    }

    public static class InsertIntoDatabase
    {
        public static bool Insert(string table, params object[] list)
        {
            bool returnValue = false;
            bool modeForm = false; //dodawanie; true - update
            int IDSQLToQuery = 0;
            string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=..\\..\\albumy_muz.mdb;" + "Persist Security Info=True;" + "Jet OLEDB:Database Password=myPassword;";
            using (OleDbConnection conn = new OleDbConnection(conString))
            {
                /* ZAMIANA TRESCI Z INNEJ TABELI NA KLUCZE OBCE */

                int dataExists = 0;

                if (table == "ocena_albumu" || table == "piosenka")
                {
                    using (OleDbCommand cmd = new OleDbCommand("SELECT ID FROM album WHERE ([nazwa] = @first AND [czy_usuniete]=false)", conn))
                    {
                        if(table == "ocena_albumu") cmd.Parameters.AddWithValue("@first", list[0]);
                        else cmd.Parameters.AddWithValue("@first", list[3]);
                        conn.Open();
                        dataExists = (int)cmd.ExecuteScalar();
                        conn.Close();
                        if (table == "ocena_albumu") list[0] = dataExists;
                        else list[3] = dataExists;
                    }
                }

                if (table == "ocena_albumu")
                {
                    using (OleDbCommand cmd = new OleDbCommand("SELECT ID FROM ocena WHERE ([wartosc] = @second)", conn))
                    {
                        cmd.Parameters.AddWithValue("@second", list[1]);
                        conn.Open();
                        dataExists = (int)cmd.ExecuteScalar();
                        conn.Close();
                        list[1] = dataExists;
                    }
                }

                if (table == "piosenka")
                {
                    if((bool)list[4]==false)
                        using (OleDbCommand cmd = new OleDbCommand("SELECT COUNT(*) FROM piosenka WHERE ([nr_piosenki] = @first AND [id_albumu] = @second AND [czy_usuniete]=false)", conn))
                        {
                            cmd.Parameters.AddWithValue("@first", list[2]);
                            cmd.Parameters.AddWithValue("@second", list[3]);
                            conn.Open();
                            dataExists = (int)cmd.ExecuteScalar();
                            conn.Close();
                            if (dataExists > 0)
                            {
                                MessageBox.Show("Piosenka o danym numerze w danym albumie już istnieje!", "Ostrzeżenie", MessageBoxButtons.OK);
                                return false;
                            }
                        }
                }

                if (table == "czlonek_zespolu")
                {
                    using (OleDbCommand cmd = new OleDbCommand("SELECT ID FROM stanowisko WHERE ([nazwa] = @third AND [czy_usuniete]=false)", conn))
                    {
                        cmd.Parameters.AddWithValue("@third", list[2]);
                        conn.Open();
                        dataExists = (int)cmd.ExecuteScalar();
                        conn.Close();
                        list[2] = dataExists;
                    }

                    using (OleDbCommand cmd = new OleDbCommand("SELECT ID FROM muzyk WHERE (nazwisko + ' ' + imie LIKE @second AND [czy_usuniete]=false)", conn))
                    {
                        cmd.Parameters.AddWithValue("@second", list[1]);
                        conn.Open();
                        dataExists = (int)cmd.ExecuteScalar();
                        conn.Close();
                        list[1] = dataExists;

                    }
                }

                if (table == "czlonek_zespolu" || table == "album")
                {
                    using (OleDbCommand cmd = new OleDbCommand("SELECT ID FROM zespol WHERE ([nazwa] = @first AND [czy_usuniete]=false)", conn))
                    {
                        if (table == "czlonek_zespolu") cmd.Parameters.AddWithValue("@first", list[0]);
                        else cmd.Parameters.AddWithValue("@first", list[3]);
                        conn.Open();
                        dataExists = (int)cmd.ExecuteScalar();
                        conn.Close();
                        if (table == "czlonek_zespolu") list[0] = dataExists;
                        else list[3] = dataExists;
                    }
                }

                if (table == "album")
                {
                    using (OleDbCommand cmd = new OleDbCommand("SELECT ID FROM gatunek WHERE ([nazwa] = @sixth AND [czy_usuniete]=false)", conn))
                    {
                        cmd.Parameters.AddWithValue("@seventh", list[4]);
                        conn.Open();
                        dataExists = (int)cmd.ExecuteScalar();
                        conn.Close();
                        list[4]= dataExists;
                    }

                    using (OleDbCommand cmd = new OleDbCommand("SELECT ID FROM wytwornia WHERE ([nazwa] = @seventh AND [czy_usuniete]=false)", conn))
                    {
                        cmd.Parameters.AddWithValue("@eigth", list[5]);
                        conn.Open();
                        dataExists = (int)cmd.ExecuteScalar();
                        conn.Close();
                        list[5] = dataExists;
                    }
                }

                if (table == "uzytkownik")
                {
                    using (OleDbCommand cmd = new OleDbCommand("SELECT ID FROM grupa WHERE ([nazwa] = @first)", conn))
                    {
                        cmd.Parameters.AddWithValue("@first", list[2]);
                        conn.Open();
                        dataExists = (int)cmd.ExecuteScalar();
                        conn.Close();
                        list[2] = dataExists;
                    }
                }

                /* SPRAWDZENIE CZY DANY REKORD JUZ ISTNIEJE */

                string sqlQuery = "";
                dataExists = 0;

                if (table == "gatunek") sqlQuery = "SELECT COUNT(*) FROM gatunek WHERE ([nazwa] = @first AND [czy_usuniete]=false)";
                if (table == "wytwornia") sqlQuery = "SELECT COUNT(*) FROM wytwornia WHERE ([nazwa] = @first AND [czy_usuniete]=false)";
                if (table == "stanowisko") sqlQuery = "SELECT COUNT(*) FROM stanowisko WHERE ([nazwa] = @first AND [czy_usuniete]=false)";
                if (table == "muzyk") sqlQuery = "SELECT COUNT(*) FROM muzyk WHERE ([imie] = @first AND [nazwisko] = @second AND [data_urodzenia] = @third AND [czy_usuniete]=false)";
                if (table == "piosenka") sqlQuery = "SELECT COUNT(*) FROM piosenka WHERE ([tytul] = @first AND [czas] = @second AND [nr_piosenki] = @third AND [id_albumu] = @fourth AND [czy_usuniete]=false)";
                if (table == "ocena_albumu") sqlQuery = "SELECT COUNT(*) FROM ocena_albumu WHERE ([id_albumu] = @first AND [id_ocena] = @second AND [recenzja] = @third AND [czy_usuniete]=false)";
                if (table == "czlonek_zespolu") sqlQuery = "SELECT COUNT(*) FROM czlonek_zespolu WHERE ([id_zespolu] = @first AND [id_muzyka] = @second AND [id_stanowiska] = @third AND [czy_usuniete]=false)";
                if (table == "zespol") sqlQuery = "SELECT COUNT(*) FROM zespol WHERE ([nazwa] = @first AND [pochodzenie] = @second AND [rok_zalozenia] = @third AND [czy_usuniete]=false)";
                if (table == "album") sqlQuery = "SELECT COUNT(*) FROM album WHERE ([nazwa] = @first AND [opis] = @second AND [data_wydania] = @third AND [id_zespolu] = @fourth AND [id_gatunek] = @fifth AND [id_wytwornia] = @sixth AND [czy_usuniete]=false)";
                if (table == "grupa")
                {
                    sqlQuery = "SELECT COUNT(*) FROM grupa WHERE ([nazwa] = @first AND [id] NOT LIKE @second AND [czy_usuniete]=false)";
                    if (!(bool)list[46]) sqlQuery = "SELECT COUNT(*) FROM grupa WHERE ([nazwa] = @first AND [czy_usuniete]=false)";
                }
                if (table == "uzytkownik")
                {
                    sqlQuery = "SELECT COUNT(*) FROM uzytkownik WHERE (StrComp([login], \"@first\", 0)=0 AND [id] NOT LIKE @second AND [czy_usuniete]=false)";
                    if (!(bool)list[3]) sqlQuery = "SELECT COUNT(*) FROM uzytkownik WHERE (StrComp([login], \"@first\", 0)=0 AND [czy_usuniete]=false)";
                }

                using (OleDbCommand cmd = new OleDbCommand(sqlQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@first", list[0]);
                    if (table == "uzytkownik" && (bool)list[3] || table == "grupa" && (bool)list[46])
                    {
                        if (table == "uzytkownik") cmd.Parameters.AddWithValue("@second", list[4]);
                        else cmd.Parameters.AddWithValue("@second", list[47]);
                    }
                    if (table == "muzyk" || table == "piosenka" || table == "ocena_albumu" || table == "czlonek_zespolu" || table == "zespol" || table == "album" || table=="uzytkownik")
                    {
                        cmd.Parameters.AddWithValue("@second", list[1]);
                        cmd.Parameters.AddWithValue("@third", list[2]);
                    }
                    if (table == "piosenka" || table == "zespol" || table == "album")
                    {
                        cmd.Parameters.AddWithValue("@fourth", list[3]);
                    }
                    if (table == "album")
                    {
                        cmd.Parameters.AddWithValue("@fifth", list[3]);
                        cmd.Parameters.AddWithValue("@sixth", list[4]);
                        cmd.Parameters.AddWithValue("@seventh", list[5]);
                    }

                    //sprawdzenie jaki tryb się wykonuje + przypisanie ID wiersza do zmiany
                    if (table=="gatunek" || table=="wytwornia" || table == "stanowisko")
                    {
                        modeForm = (bool)list[1];
                        IDSQLToQuery = (int)list[2];
                    }
                    if (table == "muzyk" || table == "ocena_albumu" || table == "czlonek_zespolu" || table=="zespol" || table=="uzytkownik")
                    {
                        modeForm = (bool)list[3];
                        IDSQLToQuery = (int)list[4];
                    }
                    if (table == "piosenka")
                    {
                        modeForm = (bool)list[4];
                        IDSQLToQuery = (int)list[5];
                    }
                    if (table == "album")
                    {
                        modeForm = (bool)list[6];
                        IDSQLToQuery = (int)list[7];
                    }
                    if (table == "grupa")
                    {
                        modeForm = (bool)list[46];
                        IDSQLToQuery = (int)list[47];
                    }
                    if (table == "uzytkownik")
                    {
                        modeForm = (bool)list[3];
                        IDSQLToQuery = (int)list[4];
                    }

                    conn.Open();
                    if (table == "uzytkownik" && list[0].ToString() == "Gość") dataExists = 1;
                    else dataExists = (int)cmd.ExecuteScalar();
                    conn.Close();
                }

                if(dataExists > 0)
                {
                    if (table == "uzytkownik") MessageBox.Show("Użytkownik o podanym loginie już istnieje!", "Ostrzeżenie", MessageBoxButtons.OK);
                    else
                    {
                        if (table == "grupa") MessageBox.Show("Grupa o podanej nazwie już istnieje!", "Ostrzeżenie", MessageBoxButtons.OK);
                        else MessageBox.Show("Podany element juz istnieje!", "Ostrzeżenie", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    if (modeForm == false) //dodawanie
                    {
                        Dictionary<string, string> sqlInsertTab = new Dictionary<string, string>();
                        sqlInsertTab["gatunek"] = "INSERT INTO gatunek ([nazwa]) VALUES (@first)";
                        sqlInsertTab["wytwornia"] = "INSERT INTO wytwornia ([nazwa]) VALUES (@first)";
                        sqlInsertTab["stanowisko"] = "INSERT INTO stanowisko ([nazwa]) VALUES (@first)";
                        sqlInsertTab["muzyk"] = "INSERT INTO muzyk ([imie],[nazwisko],[data_urodzenia]) VALUES (@first,@second,@third)";
                        sqlInsertTab["piosenka"] = "INSERT INTO piosenka ([tytul],[czas],[nr_piosenki],[id_albumu]) VALUES (@first,@second,@third,@fourth)";
                        sqlInsertTab["ocena_albumu"] = "INSERT INTO ocena_albumu ([id_albumu],[id_ocena],[recenzja]) VALUES (@first,@second,@third)";
                        sqlInsertTab["czlonek_zespolu"] = "INSERT INTO czlonek_zespolu ([id_zespolu],[id_muzyka],[id_stanowiska]) VALUES (@first,@second,@third)";
                        sqlInsertTab["zespol"] = "INSERT INTO zespol ([nazwa],[pochodzenie],[rok_zalozenia]) VALUES (@first,@second,@third)";
                        sqlInsertTab["album"] = "INSERT INTO album ([nazwa],[opis],[data_wydania],[id_zespolu],[id_gatunek],[id_wytwornia]) VALUES (@first,@second,@third,@fourth,@fifth,@sixth)";
                        sqlInsertTab["grupa"] = "INSERT INTO grupa ([nazwa],[wyswietl_album],[dodaj_album],[edytuj_album],[usun_album],[wyswietl_czlonek_zespolu],[dodaj_czlonek_zespolu],[edytuj_czlonek_zespolu],[usun_czlonek_zespolu],[wyswietl_gatunek],[dodaj_gatunek],[edytuj_gatunek],[usun_gatunek],[wyswietl_grupa],[dodaj_grupa],[edytuj_grupa],[usun_grupa],[wyswietl_muzyk],[dodaj_muzyk],[edytuj_muzyk],[usun_muzyk],[wyswietl_ocena],[wyswietl_ocena_albumu],[dodaj_ocena_albumu],[edytuj_ocena_albumu],[usun_ocena_albumu],[wyswietl_piosenka],[dodaj_piosenka],[edytuj_piosenka],[usun_piosenka],[wyswietl_stanowisko],[dodaj_stanowisko],[edytuj_stanowisko],[usun_stanowisko],[wyswietl_uzytkownik],[dodaj_uzytkownik],[edytuj_uzytkownik],[usun_uzytkownik],[wyswietl_wytwornia],[dodaj_wytwornia],[edytuj_wytwornia],[usun_wytwornia],[wyswietl_zespol],[dodaj_zespol],[edytuj_zespol],[usun_zespol]) VALUES (@first,@second,@third,@fourth,@fifth,@sixth,@perm6,@perm7,@perm8,@perm9,@perm10,@perm11,@perm12,@perm13,@perm14,@perm15,@perm16,@perm17,@perm18,@perm19,@perm20,@perm21,@perm22,@perm23,@perm24,@perm25,@perm26,@perm27,@perm28,@perm29,@perm30,@perm31,@perm32,@perm33,@perm34,@perm35,@perm36,@perm37,@perm38,@perm39,@perm40,@perm41,@perm42,@perm43,@perm44,@perm45)";
                        sqlInsertTab["uzytkownik"] = "INSERT INTO uzytkownik ([login],[haslo],[id_grupy]) VALUES (@first,@second,@third)";

                        using (OleDbCommand cmd = new OleDbCommand(sqlInsertTab[table], conn))
                        {
                            cmd.Parameters.AddWithValue("@first", list[0]);
                            if (table == "muzyk" || table == "piosenka" || table == "ocena_albumu" || table == "czlonek_zespolu" || table == "zespol" || table == "album" || table == "grupa" || table == "uzytkownik")
                            {
                                cmd.Parameters.AddWithValue("@second", list[1]);
                                cmd.Parameters.AddWithValue("@third", list[2]);
                            }
                            if (table == "piosenka" || table == "album" || table == "grupa")
                            {
                                cmd.Parameters.AddWithValue("@fourth", list[3]);
                            }
                            if (table == "album" || table == "grupa")
                            {
                                cmd.Parameters.AddWithValue("@fifth", list[4]);
                                cmd.Parameters.AddWithValue("@sixth", list[5]);
                            }
                            if (table == "grupa")
                            {
                                for (int i = 6; i <= 45; i++)
                                {
                                    cmd.Parameters.AddWithValue("@perm"+i.ToString(), list[i]);
                                }
                            }

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            returnValue = true;
                            MessageBox.Show("Pomyślnie dodano nowy element.", "Utworzono", MessageBoxButtons.OK);
                        }
                    }
                    else //true = update
                    {
                        Dictionary<string, string> sqlUpdateTab = new Dictionary<string, string>();
                        sqlUpdateTab["gatunek"] = "UPDATE gatunek SET [nazwa]=@first WHERE ID=" + IDSQLToQuery;
                        sqlUpdateTab["wytwornia"] = "UPDATE wytwornia SET [nazwa]=@first WHERE ID=" + IDSQLToQuery;
                        sqlUpdateTab["stanowisko"] = "UPDATE stanowisko SET [nazwa]=@first WHERE ID=" + IDSQLToQuery;
                        sqlUpdateTab["muzyk"] = "UPDATE muzyk SET [imie]=@first,[nazwisko]=@second,[data_urodzenia]=@third WHERE ID=" + IDSQLToQuery;
                        sqlUpdateTab["piosenka"] = "UPDATE piosenka SET [tytul]=@first,[czas]=@second,[nr_piosenki]=@third,[id_albumu]=@fourth WHERE ID=" + IDSQLToQuery; ;
                        sqlUpdateTab["ocena_albumu"] = "UPDATE ocena_albumu SET [id_albumu]=@first,[id_ocena]=@second,[recenzja]=@third WHERE ID=" + IDSQLToQuery;
                        sqlUpdateTab["czlonek_zespolu"] = "UPDATE czlonek_zespolu SET [id_zespolu]=@first,[id_muzyka]=@second,[id_stanowiska]=@third WHERE ID=" + IDSQLToQuery;
                        sqlUpdateTab["zespol"] = "UPDATE zespol SET [nazwa]=@first,[pochodzenie]=@second,[rok_zalozenia]=@third WHERE ID=" + IDSQLToQuery;
                        sqlUpdateTab["album"] = "UPDATE album SET [nazwa]=@first, [opis]=@second, [data_wydania]=@third, [id_zespolu]=@fourth,[id_gatunek]=@fifth,[id_wytwornia]=@sixth WHERE ID=" + IDSQLToQuery;
                        sqlUpdateTab["grupa"] = "UPDATE grupa SET [nazwa]=@first,[wyswietl_album]=@second,[dodaj_album]=@third,[edytuj_album]=@fourth,[usun_album]=@fifth,[wyswietl_czlonek_zespolu]=@sixth,[dodaj_czlonek_zespolu]=@perm6,[edytuj_czlonek_zespolu]=@perm7,[usun_czlonek_zespolu]=@perm8,[wyswietl_gatunek]=@perm9,[dodaj_gatunek]=@perm10,[edytuj_gatunek]=@perm11,[usun_gatunek]=@perm12,[wyswietl_grupa]=@perm13,[dodaj_grupa]=@perm14,[edytuj_grupa]=@perm15,[usun_grupa]=@perm16,[wyswietl_muzyk]=@perm17,[dodaj_muzyk]=@perm18,[edytuj_muzyk]=@perm19,[usun_muzyk]=@perm20,[wyswietl_ocena]=@perm21,[wyswietl_ocena_albumu]=@perm22,[dodaj_ocena_albumu]=@perm23,[edytuj_ocena_albumu]=@perm24,[usun_ocena_albumu]=@perm25,[wyswietl_piosenka]=@perm26,[dodaj_piosenka]=@perm27,[edytuj_piosenka]=@perm28,[usun_piosenka]=@perm29,[wyswietl_stanowisko]=@perm30,[dodaj_stanowisko]=@perm31,[edytuj_stanowisko]=@perm32,[usun_stanowisko]=@perm33,[wyswietl_uzytkownik]=@perm34,[dodaj_uzytkownik]=@perm35,[edytuj_uzytkownik]=@perm36,[usun_uzytkownik]=@perm37,[wyswietl_wytwornia]=@perm38,[dodaj_wytwornia]=@perm39,[edytuj_wytwornia]=@perm40,[usun_wytwornia]=@perm41,[wyswietl_zespol]=@perm42,[dodaj_zespol]=@perm43,[edytuj_zespol]=@perm44,[usun_zespol]=@perm45 WHERE ID=" + IDSQLToQuery;
                        if(modeForm) sqlUpdateTab["uzytkownik"] = "UPDATE uzytkownik SET [login]=@first, [haslo]=@second, [id_grupy]=@third WHERE ID=" + IDSQLToQuery;
                        else sqlUpdateTab["uzytkownik"] = "UPDATE uzytkownik SET [login]=@first, [id_grupy]=@second WHERE ID=" + IDSQLToQuery;

                        using (OleDbCommand cmd = new OleDbCommand(sqlUpdateTab[table], conn))
                        {
                            cmd.Parameters.AddWithValue("@first", list[0]);
                            if (table == "uzytkownik" && !modeForm) cmd.Parameters.AddWithValue("@second", list[2]);
                            if (table == "muzyk" || table == "piosenka" || table == "ocena_albumu" || table == "czlonek_zespolu" || table == "zespol" || table == "album" || table == "uzytkownik" && modeForm || table == "grupa")
                            {
                                cmd.Parameters.AddWithValue("@second", list[1]);
                                cmd.Parameters.AddWithValue("@third", list[2]);
                            }
                            if (table == "piosenka" || table == "album" || table == "grupa")
                            {
                                cmd.Parameters.AddWithValue("@fourth", list[3]);
                            }
                            if (table == "album" || table == "grupa")
                            {
                                cmd.Parameters.AddWithValue("@fifth", list[4]);
                                cmd.Parameters.AddWithValue("@sixth", list[5]);
                            }
                            if (table == "grupa")
                            {
                                for (int i = 6; i <= 45; i++)
                                {
                                    cmd.Parameters.AddWithValue("@perm" + i.ToString(), list[i]);
                                }
                            }

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            returnValue = true;
                            MessageBox.Show("Pomyślnie zedytowano wybrany element.", "Edycja zakończona", MessageBoxButtons.OK);
                        }
                    }
                }
            }
            return returnValue;
        }
    }
}
