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
            fillComboBoxes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(comboBox1.SelectedItem.ToString() + ' ' + comboBox2.SelectedItem.ToString() + ' ' + comboBox3.SelectedItem.ToString());
            //if (numericUpDown1.Value == 0 || numericUpDown2.Value == 0 || numericUpDown3.Value == 0)
            //{
            //    MessageBox.Show("Wprowadż poprawne wartości do wszystkich pól", "Ostrzeżenie", MessageBoxButtons.OK);
            //    return;
            //}

            //if (InsertIntoDatabase.Insert("czlonek_zespolu", numericUpDown1.Value, numericUpDown2.Value, numericUpDown3.Value))
            //{
            //    this.Close();
            //}

            if (InsertIntoDatabase.Insert("czlonek_zespolu", comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString(), comboBox3.SelectedItem.ToString()))
            {
                this.Close();
            }
        }

        private void fillComboBoxes()
        {
            string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=..\\..\\albumy_muz.mdb;" + "Persist Security Info=True;" + "Jet OLEDB:Database Password=myPassword;";
            using (OleDbConnection conn = new OleDbConnection(conString))
            {
                conn.Open();
                OleDbDataReader reader;

                using (OleDbCommand cmd = new OleDbCommand("SELECT nazwa FROM zespol ORDER BY nazwa", conn))
                {
                    reader = cmd.ExecuteReader();
                    comboBox1.Items.Add("DODAJ NOWĄ WARTOŚĆ...");
                    while (reader.Read())
                        comboBox1.Items.Add(reader["nazwa"].ToString());
                }
                using (OleDbCommand cmd = new OleDbCommand("SELECT nazwisko + ' ' + imie AS osoba FROM muzyk ORDER BY nazwisko", conn))
                {
                    reader = cmd.ExecuteReader();
                    comboBox2.Items.Add("DODAJ NOWĄ WARTOŚĆ...");
                    while (reader.Read())
                        comboBox2.Items.Add(reader["osoba"].ToString());
                }
                using (OleDbCommand cmd = new OleDbCommand("SELECT nazwa FROM stanowisko ORDER BY nazwa", conn))
                {
                    reader = cmd.ExecuteReader();
                    comboBox3.Items.Add("DODAJ NOWĄ WARTOŚĆ...");
                    while (reader.Read())
                        comboBox3.Items.Add(reader["nazwa"].ToString());
                }

                conn.Close();
            }
        }
    }
}
