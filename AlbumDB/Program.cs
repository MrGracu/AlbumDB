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
            //Application.Run(new FORMS.AlbumForm());
        }
    }

    public static class WindowManage
    {
        public static void SwitchWindow(int selectedForm, string selectedItem)
        {
            if (selectedForm < 0) return;

            switch (selectedItem)
            {
                case "album":
                    FORMS.AlbumForm albumForm = new FORMS.AlbumForm();
                    albumForm.ShowDialog();
                    break;
                case "czlonek_zespolu":
                    FORMS.CzlonekZespoluForm czlonekZespoluForm = new FORMS.CzlonekZespoluForm();
                    czlonekZespoluForm.ShowDialog();
                    break;
                case "gatunek":
                    FORMS.GatunekForm gatunekForm = new FORMS.GatunekForm();
                    gatunekForm.ShowDialog();
                    break;
                case "muzyk":
                    FORMS.MuzykForm muzykForm = new FORMS.MuzykForm();
                    muzykForm.ShowDialog();
                    break;
                case "ocena_albumu":
                    FORMS.OcenaAlbumuForm ocenaAlbumuForm = new FORMS.OcenaAlbumuForm();
                    ocenaAlbumuForm.ShowDialog();
                    break;
                case "piosenka":
                    FORMS.PiosenkaForm piosenkaForm = new FORMS.PiosenkaForm();
                    piosenkaForm.ShowDialog();
                    break;
                case "stanowisko":
                    FORMS.StanowiskoForm stanowiskoForm = new FORMS.StanowiskoForm();
                    stanowiskoForm.ShowDialog();
                    break;
                case "wytwornia":
                    FORMS.WytworniaForm wytworniaForm = new FORMS.WytworniaForm();
                    wytworniaForm.ShowDialog();
                    break;
                case "zespol":
                    FORMS.ZespolForm zespolForm = new FORMS.ZespolForm();
                    zespolForm.ShowDialog();
                    break;
            }
        }
    }

    public static class InsertIntoDatabase
    {
        public static bool Insert(string table, params object[] list)
        {
            bool returnValue = false;

            string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=D:\\GitHub\\source\\repos\\AlbumDB\\AlbumDB\\albumy_muz.mdb;" + "Persist Security Info=True;" + "Jet OLEDB:Database Password=myPassword;";
            using (OleDbConnection conn = new OleDbConnection(conString))
            {
                Dictionary<string, string> sqlTab = new Dictionary<string, string>();
                sqlTab["gatunek"] = "INSERT INTO gatunek ([nazwa]) VALUES (@first)";

                using (OleDbCommand cmd = new OleDbCommand(sqlTab[table], conn))
                {
                    cmd.Parameters.AddWithValue("@first", list[0]);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    returnValue = true;
                    MessageBox.Show("Pomyślnie dodano nowy element.", "Utworzono", MessageBoxButtons.OK);
                }
            }
            return returnValue;
        }
    }
}
