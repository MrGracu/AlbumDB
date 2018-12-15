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
        public CzlonekZespoluForm()
        {
            InitializeComponent();
            fillComboBox(comboBox1, 1);
            fillComboBox(comboBox2, 2);
            fillComboBox(comboBox3, 3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "")
            {
                MessageBox.Show("Wprowadż poprawne wartości do wszystkich pól", "Ostrzeżenie", MessageBoxButtons.OK);
                return;
            }

            if (InsertIntoDatabase.Insert("czlonek_zespolu", comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString(), comboBox3.SelectedItem.ToString()))
            {
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
                WindowManage.SwitchWindow("zespol");
                comboBox1.Items.Clear();
                fillComboBox(comboBox1, 1);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                WindowManage.SwitchWindow("muzyk");
                comboBox2.Items.Clear();
                fillComboBox(comboBox1, 2);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0)
            {
                WindowManage.SwitchWindow("stanowisko");
                comboBox3.Items.Clear();
                fillComboBox(comboBox1, 3);
            }
        }
    }
}
