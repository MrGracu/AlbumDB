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
            Application.Run(new Form1());
        }
    }

    public static class WindowManage
    {
        public static bool SwitchWindow(string selectedItem, bool mode, int id)
        {
            bool returnValue = false;
            DialogResult result = DialogResult.Cancel;
            switch (selectedItem)
            {
                case "album":
                    FORMS.AlbumForm albumForm = new FORMS.AlbumForm(mode,id);
                    result = albumForm.ShowDialog();
                    break;
                case "czlonek_zespolu":
                    FORMS.CzlonekZespoluForm czlonekZespoluForm = new FORMS.CzlonekZespoluForm(mode, id);
                    result = czlonekZespoluForm.ShowDialog();
                    break;
                case "gatunek":
                    FORMS.GatunekForm gatunekForm = new FORMS.GatunekForm(mode,id);
                    result = gatunekForm.ShowDialog();
                    break;
                case "muzyk":
                    FORMS.MuzykForm muzykForm = new FORMS.MuzykForm(mode, id);
                    result = muzykForm.ShowDialog();
                    break;
                case "ocena_albumu":
                    FORMS.OcenaAlbumuForm ocenaAlbumuForm = new FORMS.OcenaAlbumuForm(mode,id);
                    result = ocenaAlbumuForm.ShowDialog();
                    break;
                case "piosenka":
                    FORMS.PiosenkaForm piosenkaForm = new FORMS.PiosenkaForm(mode, id);
                    result = piosenkaForm.ShowDialog();
                    break;
                case "stanowisko":
                    FORMS.StanowiskoForm stanowiskoForm = new FORMS.StanowiskoForm(mode, id);
                    result = stanowiskoForm.ShowDialog();
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
            if (deleteOcena_albumu_Piosenka(false, "piosenka", id))
                if (deleteOcena_albumu_Piosenka(false, "ocena_albumu", id))
                {
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
            if(returnValue)
            {
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
                    returnValue = deleteAlbum((int)reader["id"]);
                }
                conn.Close();
            }
            if (returnValue)
            {
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
                    returnValue = deleteAlbum((int)reader["id"]);
                }
                conn.Close();
            }
            if(returnValue)
            {
                using (OleDbConnection conn = new OleDbConnection(conString))
                using (OleDbCommand cmd = new OleDbCommand("SELECT id FROM czlonek_zespolu WHERE id_zespolu=" + id.ToString() + " AND [czy_usuniete]=false", conn))
                {
                    conn.Open();
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        returnValue = deleteCzlonek_zespolu((int)reader["id"]);
                    }
                    conn.Close();
                }
                if (returnValue)
                {
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
                    returnValue = deleteCzlonek_zespolu((int)reader["id"]);
                }
                conn.Close();
            }
            if (returnValue)
            {
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
                    returnValue = deleteCzlonek_zespolu((int)reader["id"]);
                }
                conn.Close();
            }
            if (returnValue)
            {
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
            }
            return returnValue;
        }

        public static bool Delete(string table, DataGridViewSelectedCellCollection list) //Ustawienie wartosci czy_usuniete na true
        {
            DialogResult result = MessageBox.Show("Czy na pewno chcesz usunąć ten rekord wraz z jego powiązaniami (jeśli istnieją)?", "Ostrzeżenie", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return false;

            bool returnValue = false;

            switch (table)
            {
                case "album":
                    returnValue = deleteAlbum((int)list[1].Value);
                    break;
                case "czlonek_zespolu":
                    returnValue = deleteCzlonek_zespolu((int)list[1].Value);
                    break;
                case "gatunek":
                    returnValue = deleteGatunek((int)list[1].Value);
                    break;
                case "muzyk":
                    returnValue = deleteMuzyk((int)list[1].Value);
                    break;
                case "ocena_albumu":
                    returnValue = deleteOcena_albumu_Piosenka(true, table, (int)list[1].Value);
                    break;
                case "piosenka":
                    returnValue = deleteOcena_albumu_Piosenka(true, table, (int)list[1].Value);
                    break;
                case "stanowisko":
                    returnValue = deleteStanowisko((int)list[1].Value);
                    break;
                case "wytwornia":
                    returnValue = deleteWytwornia((int)list[1].Value);
                    break;
                case "zespol":
                    returnValue = deleteZespol((int)list[1].Value);
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
                int dataExists = 0;

                if (table == "ocena_albumu" || table == "piosenka")
                {
                    using (OleDbCommand cmd = new OleDbCommand("SELECT ID FROM album WHERE ([nazwa] = @first)", conn))
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
                        using (OleDbCommand cmd = new OleDbCommand("SELECT COUNT(*) FROM piosenka WHERE ([nr_piosenki] = @first AND [id_albumu] = @second)", conn))
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
                    using (OleDbCommand cmd = new OleDbCommand("SELECT ID FROM stanowisko WHERE ([nazwa] = @third)", conn))
                    {
                        cmd.Parameters.AddWithValue("@third", list[2]);
                        conn.Open();
                        dataExists = (int)cmd.ExecuteScalar();
                        conn.Close();
                        list[2] = dataExists;
                    }

                    using (OleDbCommand cmd = new OleDbCommand("SELECT ID FROM muzyk WHERE (nazwisko + ' ' + imie LIKE @second)", conn))
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
                    using (OleDbCommand cmd = new OleDbCommand("SELECT ID FROM zespol WHERE ([nazwa] = @first)", conn))
                    {
                        if (table == "czlonek_zespolu") cmd.Parameters.AddWithValue("@first", list[0]);
                        else cmd.Parameters.AddWithValue("@first", list[4]);
                        conn.Open();
                        dataExists = (int)cmd.ExecuteScalar();
                        conn.Close();
                        if (table == "czlonek_zespolu") list[0] = dataExists;
                        else list[4] = dataExists;
                    }
                }

                if (table == "album")
                {
                    using (OleDbCommand cmd = new OleDbCommand("SELECT ID FROM gatunek WHERE ([nazwa] = @sixth)", conn))
                    {
                        cmd.Parameters.AddWithValue("@seventh", list[5]);
                        conn.Open();
                        dataExists = (int)cmd.ExecuteScalar();
                        conn.Close();
                        list[5]= dataExists;
                    }

                    using (OleDbCommand cmd = new OleDbCommand("SELECT ID FROM wytwornia WHERE ([nazwa] = @seventh)", conn))
                    {
                        cmd.Parameters.AddWithValue("@eigth", list[6]);
                        conn.Open();
                        dataExists = (int)cmd.ExecuteScalar();
                        conn.Close();
                        list[6] = dataExists;
                    }
                }

                string sqlQuery = "";
                dataExists = 0;

                if (table == "gatunek") sqlQuery = "SELECT COUNT(*) FROM gatunek WHERE ([nazwa] = @first)";
                if (table == "wytwornia") sqlQuery = "SELECT COUNT(*) FROM wytwornia WHERE ([nazwa] = @first)";
                if (table == "stanowisko") sqlQuery = "SELECT COUNT(*) FROM stanowisko WHERE ([nazwa] = @first)";
                if (table == "muzyk") sqlQuery = "SELECT COUNT(*) FROM muzyk WHERE ([imie] = @first AND [nazwisko] = @second AND [data_urodzenia] = @third)";
                if (table == "piosenka") sqlQuery = "SELECT COUNT(*) FROM piosenka WHERE ([tytul] = @first AND [czas] = @second AND [nr_piosenki] = @third AND [id_albumu] = @fourth)";
                if (table == "ocena_albumu") sqlQuery = "SELECT COUNT(*) FROM ocena_albumu WHERE ([id_albumu] = @first AND [id_ocena] = @second AND [recenzja] = @third)";
                if (table == "czlonek_zespolu") sqlQuery = "SELECT COUNT(*) FROM czlonek_zespolu WHERE ([id_zespolu] = @first AND [id_muzyka] = @second AND [id_stanowiska] = @third)";
                if (table == "zespol") sqlQuery = "SELECT COUNT(*) FROM zespol WHERE ([nazwa] = @first AND [pochodzenie] = @second AND [rok_zalozenia] = @third)";
                if (table == "album") sqlQuery = "SELECT COUNT(*) FROM album WHERE ([nazwa] = @first AND [opis] = @second AND [dlugosc] = @third AND [data_wydania] = @fourth AND [id_zespolu] = @fifth AND [id_gatunek] = @sixth AND [id_wytwornia] = @seventh)";
                
                using (OleDbCommand cmd = new OleDbCommand(sqlQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@first", list[0]);
                    if (table == "muzyk" || table == "piosenka" || table == "ocena_albumu" || table == "czlonek_zespolu" || table == "zespol" || table == "album")
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
                        cmd.Parameters.AddWithValue("@fifth", list[4]);
                        cmd.Parameters.AddWithValue("@sixth", list[5]);
                        cmd.Parameters.AddWithValue("@seventh", list[6]);
                    }

                    //********ZMIENIONE NUMERKI GDYBY KIEDYŚ NIE DZIAŁAŁO ******************
                    //cmd.Parameters.AddWithValue("@first", list[0]);
                    //cmd.Parameters.AddWithValue("@second", list[1]);
                    //cmd.Parameters.AddWithValue("@third", list[2]);
                    //if (table == "muzyk" || table == "piosenka" || table == "ocena_albumu" || table == "czlonek_zespolu" || table == "zespol" || table == "album")
                    //{
                    //    cmd.Parameters.AddWithValue("@fourth", list[3]);
                    //    cmd.Parameters.AddWithValue("@fifth", list[4]);
                    //}
                    //if (table == "piosenka" || table == "album")
                    //{
                    //    cmd.Parameters.AddWithValue("@sixth", list[5]);
                    //}
                    //if (table == "album")
                    //{
                    //    cmd.Parameters.AddWithValue("@seventh", list[6]);
                    //    cmd.Parameters.AddWithValue("@eight", list[7]);
                    //    cmd.Parameters.AddWithValue("@ninth", list[8]);
                    //    //cmd.Parameters.AddWithValue("@eigth", list[7]);
                    //}

                    //sprawdzenie jaki tryb się wykonuje + przypisanie ID wiersza do zmiany
                    if (table=="gatunek" || table=="wytwornia" || table == "stanowisko")
                    {
                        modeForm = (bool)list[1];
                        IDSQLToQuery = (int)list[2];
                    }
                    if (table == "muzyk" || table == "ocena_albumu" || table == "czlonek_zespolu" || table=="zespol")
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
                        modeForm = (bool)list[7];
                        IDSQLToQuery = (int)list[8];
                    }

                    conn.Open();
                    dataExists = (int)cmd.ExecuteScalar();
                    conn.Close();
                }

                if(dataExists > 0)
                {
                    MessageBox.Show("Podany element juz istnieje!", "Ostrzeżenie", MessageBoxButtons.OK);
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
                        sqlInsertTab["album"] = "INSERT INTO album ([nazwa],[opis],[dlugosc],[data_wydania],[id_zespolu],[id_gatunek],[id_wytwornia]) VALUES (@first,@second,@third,@fourth,@fifth,@sixth,@seventh)";

                        using (OleDbCommand cmd = new OleDbCommand(sqlInsertTab[table], conn))
                        {
                            cmd.Parameters.AddWithValue("@first", list[0]);
                            if (table == "muzyk" || table == "piosenka" || table == "ocena_albumu" || table == "czlonek_zespolu" || table == "zespol" || table == "album")
                            {
                                cmd.Parameters.AddWithValue("@second", list[1]);
                                cmd.Parameters.AddWithValue("@third", list[2]);
                            }
                            if (table == "piosenka" || table == "album")
                            {
                                cmd.Parameters.AddWithValue("@fourth", list[3]);
                            }
                            if (table == "album")
                            {
                                cmd.Parameters.AddWithValue("@fifth", list[4]);
                                cmd.Parameters.AddWithValue("@sixth", list[5]);
                                cmd.Parameters.AddWithValue("@seventh", list[6]);
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
                        Dictionary<string, string> sqlInsertTab = new Dictionary<string, string>();
                        sqlInsertTab["gatunek"] = "UPDATE gatunek SET [nazwa]=@first WHERE ID=" + IDSQLToQuery;
                        sqlInsertTab["wytwornia"] = "UPDATE wytwornia SET [nazwa]=@first WHERE ID=" + IDSQLToQuery;
                        sqlInsertTab["stanowisko"] = "UPDATE stanowisko SET [nazwa]=@first WHERE ID=" + IDSQLToQuery;
                        sqlInsertTab["muzyk"] = "UPDATE muzyk SET [imie]=@first,[nazwisko]=@second,[data_urodzenia]=@third WHERE ID=" + IDSQLToQuery;
                        sqlInsertTab["piosenka"] = "UPDATE piosenka SET [tytul]=@first,[czas]=@second,[nr_piosenki]=@third,[id_albumu]=@fourth WHERE ID=" + IDSQLToQuery; ;
                        sqlInsertTab["ocena_albumu"] = "UPDATE ocena_albumu SET [id_albumu]=@first,[id_ocena]=@second,[recenzja]=@third WHERE ID=" + IDSQLToQuery;
                        sqlInsertTab["czlonek_zespolu"] = "UPDATE czlonek_zespolu SET [id_zespolu]=@first,[id_muzyka]=@second,[id_stanowiska]=@third WHERE ID=" + IDSQLToQuery;
                        sqlInsertTab["zespol"] = "UPDATE zespol SET [nazwa]=@first,[pochodzenie]=@second,[rok_zalozenia]=@third WHERE ID=" + IDSQLToQuery;
                        sqlInsertTab["album"] = "UPDATE album SET [nazwa]=@first, [opis]=@second, [dlugosc]=@third, [data_wydania]=@fourth,[id_zespolu]=@fifth,[id_gatunek]=@sixth,[id_wytwornia]=@seventh WHERE ID="+ IDSQLToQuery;

                        using (OleDbCommand cmd = new OleDbCommand(sqlInsertTab[table], conn))
                        {
                            cmd.Parameters.AddWithValue("@first", list[0]);
                            if (table == "muzyk" || table == "piosenka" || table == "ocena_albumu" || table == "czlonek_zespolu" || table == "zespol" || table == "album")
                            {
                                cmd.Parameters.AddWithValue("@second", list[1]);
                                cmd.Parameters.AddWithValue("@third", list[2]);
                            }
                            if (table == "piosenka" || table == "album")
                            {
                                cmd.Parameters.AddWithValue("@fourth", list[3]);
                            }
                            if (table == "album")
                            {
                                cmd.Parameters.AddWithValue("@fifth", list[4]);
                                cmd.Parameters.AddWithValue("@sixth", list[5]);
                                cmd.Parameters.AddWithValue("@seventh", list[6]);
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
