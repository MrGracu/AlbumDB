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
    public partial class PiosenkaForm : Form
    {
        public PiosenkaForm()
        {
            InitializeComponent();
            fillComboBox(comboBox1, 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime date1 = new DateTime(2010, 1, 1, 0, 0, 0);
            if (textBox1.Text.Length == 0 || dateTimePicker2.Value.TimeOfDay <= date1.TimeOfDay || numericUpDown1.Value == 0 || comboBox1.Text=="")
            {
                MessageBox.Show("Wprowadż poprawne wartości do wszystkich pól", "Ostrzeżenie", MessageBoxButtons.OK);
                return;
            }

            if (InsertIntoDatabase.Insert("piosenka", textBox1.Text, dateTimePicker2.Value.TimeOfDay, numericUpDown1.Value, comboBox1.SelectedItem.ToString()))
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
                    comboBox.Items.Add("DODAJ NOWĄ WARTOŚĆ...");
                    using (OleDbCommand cmd = new OleDbCommand("SELECT nazwa FROM album ORDER BY nazwa", conn))
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
            if (comboBox1.SelectedIndex == 0)
            {
                WindowManage.SwitchWindow("album");
                comboBox1.Items.Clear();
                fillComboBox(comboBox1, 1);
            }
        }
    }
}
