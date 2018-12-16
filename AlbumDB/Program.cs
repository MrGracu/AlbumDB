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
        public static bool SwitchWindow(string selectedItem)
        {
            bool returnValue = false;
            DialogResult result = DialogResult.Cancel;
            switch (selectedItem)
            {
                case "album":
                    FORMS.AlbumForm albumForm = new FORMS.AlbumForm();
                    result = albumForm.ShowDialog();
                    break;
                case "czlonek_zespolu":
                    FORMS.CzlonekZespoluForm czlonekZespoluForm = new FORMS.CzlonekZespoluForm();
                    result = czlonekZespoluForm.ShowDialog();
                    break;
                case "gatunek":
                    FORMS.GatunekForm gatunekForm = new FORMS.GatunekForm();
                    result = gatunekForm.ShowDialog();
                    break;
                case "muzyk":
                    FORMS.MuzykForm muzykForm = new FORMS.MuzykForm();
                    result = muzykForm.ShowDialog();
                    break;
                case "ocena_albumu":
                    FORMS.OcenaAlbumuForm ocenaAlbumuForm = new FORMS.OcenaAlbumuForm();
                    result = ocenaAlbumuForm.ShowDialog();
                    break;
                case "piosenka":
                    FORMS.PiosenkaForm piosenkaForm = new FORMS.PiosenkaForm();
                    result = piosenkaForm.ShowDialog();
                    break;
                case "stanowisko":
                    FORMS.StanowiskoForm stanowiskoForm = new FORMS.StanowiskoForm();
                    result = stanowiskoForm.ShowDialog();
                    break;
                case "wytwornia":
                    FORMS.WytworniaForm wytworniaForm = new FORMS.WytworniaForm();
                    result = wytworniaForm.ShowDialog();
                    break;
                case "zespol":
                    FORMS.ZespolForm zespolForm = new FORMS.ZespolForm();
                    result = zespolForm.ShowDialog();
                    break;
            }

            if (result == DialogResult.OK) returnValue = true;
            else returnValue = false;
            return returnValue;
        }
    }

    public static class InsertIntoDatabase
    {
        public static bool Insert(string table, params object[] list)
        {
            bool returnValue = false;

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


                //???????????????? działa - nic nie zmieniłem
                if (table == "piosenka")
                {
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
                if (table == "zespol") sqlQuery = "SELECT COUNT(*) FROM zespol WHERE ([nazwa] = @first AND [pochodzenie] = @second AND [rok_zalozenia] = @third AND [liczba_czlonkow] = @fourth)";
                if (table == "album") sqlQuery = "SELECT COUNT(*) FROM album WHERE ([nazwa] = @first AND [opis] = @second AND [ilosc_piosenek] = @third AND [dlugosc] = @fourth AND [data_wydania] = @fifth AND [id_zespolu] = @sixth AND [id_gatunek] = @seventh AND [id_wytwornia] = @eigth)";

                using (OleDbCommand cmd = new OleDbCommand(sqlQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@first", list[0]);
                    if(table == "muzyk" || table == "piosenka" || table == "ocena_albumu" || table == "czlonek_zespolu" || table == "zespol" || table == "album")
                    {
                        cmd.Parameters.AddWithValue("@second", list[1]);
                        cmd.Parameters.AddWithValue("@third", list[2]);
                    }
                    if(table == "piosenka" || table == "zespol" || table == "album")
                    {
                        cmd.Parameters.AddWithValue("@fourth", list[3]);
                    }
                    if (table == "album")
                    {
                        cmd.Parameters.AddWithValue("@fifth", list[4]);
                        cmd.Parameters.AddWithValue("@sixth", list[5]);
                        cmd.Parameters.AddWithValue("@seventh", list[6]);
                        cmd.Parameters.AddWithValue("@eigth", list[7]);
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
            }
            return returnValue;
        }
    }
}
