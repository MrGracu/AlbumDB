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
    public partial class CzlonekZespoluForm : Form
    {
        bool modeForm; //0 - insert, 1 - update
        int IDToSQLQuery;
        public CzlonekZespoluForm(bool mode, int id)
        {
            InitializeComponent();
            fillComboBox(comboBox1, 1);
            fillComboBox(comboBox2, 2);
            fillComboBox(comboBox3, 3);
            modeForm = mode;
            IDToSQLQuery = id + 1; //przekazuje zmniejszony
            if (modeForm)
            {
                this.Text = "Edycja zespołu";
                button1.Text = "Zamień";
                loadValueFromQuery();
            }
        }

        public void loadValueFromQuery()
        {
            string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=..\\..\\albumy_muz.mdb;" + "Persist Security Info=True;" + "Jet OLEDB:Database Password=myPassword;";
            using (OleDbConnection conn = new OleDbConnection(conString))
            {
                conn.Open();
                OleDbDataReader reader;
                using (OleDbCommand cmd = new OleDbCommand("SELECT id_zespolu,id_muzyka,id_stanowiska FROM czlonek_zespolu WHERE ID=" + IDToSQLQuery, conn))
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        using (OleDbCommand cmdID = new OleDbCommand("SELECT nazwa FROM zespol WHERE id=" + (int)reader["id_zespolu"], conn))
                        {
                            OleDbDataReader readerTemporary = cmdID.ExecuteReader();
                            while (readerTemporary.Read())
                                comboBox1.Text = readerTemporary["nazwa"].ToString();
                        }

                        using (OleDbCommand cmdID = new OleDbCommand("SELECT nazwisko+' '+imie AS osoba FROM muzyk WHERE id=" + (int)reader["id_muzyka"], conn))
                        {
                            OleDbDataReader readerTemporary = cmdID.ExecuteReader();
                            while (readerTemporary.Read())
                                comboBox2.Text = readerTemporary["osoba"].ToString();
                        }

                        using (OleDbCommand cmdID = new OleDbCommand("SELECT nazwa FROM stanowisko WHERE id=" + (int)reader["id_stanowiska"], conn))
                        {
                            OleDbDataReader readerTemporary = cmdID.ExecuteReader();
                            while (readerTemporary.Read())
                                comboBox3.Text = readerTemporary["nazwa"].ToString();
                        }
                    }
                }
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "")
            {
                MessageBox.Show("Wprowadż poprawne wartości do wszystkich pól", "Ostrzeżenie", MessageBoxButtons.OK);
                return;
            }

            if (InsertIntoDatabase.Insert("czlonek_zespolu", comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString(), comboBox3.SelectedItem.ToString(), modeForm, IDToSQLQuery))
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

                comboBox.Items.Add("DODAJ NOWĄ WARTOŚĆ...");
                comboBox.Text = "";

                if (comboValue == 1)
                    using (OleDbCommand cmd = new OleDbCommand("SELECT nazwa FROM zespol ORDER BY nazwa", conn))
                    {
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                            comboBox1.Items.Add(reader["nazwa"].ToString());
                    }

                if (comboValue == 2)
                    using (OleDbCommand cmd = new OleDbCommand("SELECT nazwisko + ' ' + imie AS osoba FROM muzyk ORDER BY nazwisko", conn))
                    {
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                            comboBox2.Items.Add(reader["osoba"].ToString());
                    }

                if (comboValue == 3)
                    using (OleDbCommand cmd = new OleDbCommand("SELECT nazwa FROM stanowisko ORDER BY nazwa", conn))
                    {
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                            comboBox3.Items.Add(reader["nazwa"].ToString());
                    }

                conn.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                WindowManage.SwitchWindow("zespol", false, 0);
                comboBox1.Items.Clear();
                fillComboBox(comboBox1, 1);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                WindowManage.SwitchWindow("muzyk", false, 0);
                comboBox2.Items.Clear();
                fillComboBox(comboBox1, 2);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0)
            {
                WindowManage.SwitchWindow("stanowisko", false, 0);
                comboBox3.Items.Clear();
                fillComboBox(comboBox1, 3);
            }
        }
    }
}
