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
                textBox2.ReadOnly = true;
                button1.Text = "Zamień";
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
                using (OleDbCommand cmd = new OleDbCommand("SELECT id_grupy,login,haslo FROM uzytkownik WHERE ID=" + IDToSQLQuery, conn))
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        textBox1.Text = reader["login"].ToString();
                        textBox2.Text = reader["haslo"].ToString();

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
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || comboBox1.Text == "")
            {
                MessageBox.Show("Wprowadż poprawne wartości do wszystkich pól", "Ostrzeżenie", MessageBoxButtons.OK);
                return;
            }

            if (!modeForm)
                textBox2.Text = LoginForm.HashSHA256(textBox2.Text);

            if (InsertIntoDatabase.Insert("uzytkownik", comboBox1.SelectedItem.ToString(), textBox1.Text, textBox2.Text, modeForm, IDToSQLQuery))
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
