using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlbumDB.FORMS
{
    public partial class UzytkownikForm : Form
    {
        Dictionary<string, bool[]> permissionsTab;
        bool modeForm; //0 - insert, 1 - update
        int IDToSQLQuery;
        string hash = "0000";

        private Button addButton = new Button();

        public UzytkownikForm(bool mode, int id, Dictionary<string, bool[]> permTab)
        {
            permissionsTab = permTab;
            InitializeComponent();
            fillComboBox(comboBox1, 1);
            modeForm = mode;
            IDToSQLQuery = id; //przekazuje zmniejszony
            if (modeForm)
            {
                this.Text = "Edytuj Użytkownika";
                button1.Text = "Zamień";
                label2.Text = "Hasło użytkownika:\n(Jeśli puste -\nnie zostanie zmienione)";
                label3.Location = new Point(
                     label3.Location.X,
                     label3.Location.Y + 26
                 );
                comboBox1.Location = new Point(
                     comboBox1.Location.X,
                     comboBox1.Location.Y + 26
                 );
                button1.Location = new Point(
                     button1.Location.X,
                     button1.Location.Y + 26
                 );
                this.Size = new Size(this.Size.Width, this.Size.Height + 26);
                loadValueFromQuery();
            }
            addButton.Click += button1_Click;
            this.AcceptButton = addButton;
        }

        public void loadValueFromQuery()
        {
            string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=..\\..\\albumy_muz.mdb;" + "Persist Security Info=True;" + "Jet OLEDB:Database Password=myPassword;";
            using (OleDbConnection conn = new OleDbConnection(conString))
            {
                conn.Open();
                OleDbDataReader reader;
                using (OleDbCommand cmd = new OleDbCommand("SELECT id_grupy,haslo,login FROM uzytkownik WHERE ID=" + IDToSQLQuery, conn))
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        textBox1.Text = reader["login"].ToString();
                        hash = reader["haslo"].ToString();

                        using (OleDbCommand cmdID = new OleDbCommand("SELECT nazwa FROM grupa WHERE id=" + (int)reader["id_grupy"], conn))
                        {
                            OleDbDataReader readerTemporary = cmdID.ExecuteReader();
                            while (readerTemporary.Read())
                                comboBox1.Text = readerTemporary["nazwa"].ToString();
                        }
                    }
                }
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (((textBox1.Text.Length == 0 || comboBox1.Text == "") && modeForm) || ((textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || comboBox1.Text == "") && !modeForm))
            {
                MessageBox.Show("Wprowadż poprawne wartości do wszystkich pól", "Ostrzeżenie", MessageBoxButtons.OK);
                return;
            }

            if (textBox2.Text.Length > 0)
            {
                if (textBox2.Text.Length < 4)
                {
                    MessageBox.Show("Minimalna długość hasła wynosi 4 znaki!", "Ostrzeżenie", MessageBoxButtons.OK);
                    return;
                }
                else hash = LoginForm.HashSHA256(textBox2.Text);
            }

            if (InsertIntoDatabase.Insert("uzytkownik", textBox1.Text, hash, comboBox1.SelectedItem.ToString(), modeForm, IDToSQLQuery))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void fillComboBox(ComboBox comboBox, int comboValue)
        {
            string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=..\\..\\albumy_muz.mdb;" + "Persist Security Info=True;" + "Jet OLEDB:Database Password=myPassword;";
            using (OleDbConnection conn = new OleDbConnection(conString))
            {
                conn.Open();
                OleDbDataReader reader;

                comboBox.Text = "";

                if (comboValue == 1)
                {
                    if (permissionsTab["grupa"][1]) comboBox.Items.Add("DODAJ NOWĄ WARTOŚĆ...");
                    using (OleDbCommand cmd = new OleDbCommand("SELECT nazwa FROM grupa WHERE [czy_usuniete] = false ORDER BY nazwa", conn))
                    {
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                            comboBox1.Items.Add(reader["nazwa"].ToString());
                    }
                }
                conn.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0 && permissionsTab["grupa"][1])
            {
                WindowManage.SwitchWindow("grupa", false, 0, permissionsTab);
                comboBox1.Items.Clear();
                fillComboBox(comboBox1, 1);
            }
        }
    }
}
