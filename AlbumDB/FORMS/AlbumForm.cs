﻿using System;
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
    public partial class AlbumForm : Form
    {
        Dictionary<string, bool[]> permissionsTab;
        bool modeForm; //0 - insert, 1 - update
        int IDToSQLQuery;

        private Button addButton = new Button();

        public AlbumForm(bool mode, int id, Dictionary<string, bool[]> permTab)
        {
            permissionsTab = permTab;
            InitializeComponent();
            fillComboBox(comboBox1, 1);
            fillComboBox(comboBox2, 2);
            fillComboBox(comboBox3, 3);
            modeForm = mode;
            IDToSQLQuery = id; //przekazuje zmniejszony
            if (modeForm)
            {
                this.Text = "Edytuj Album";
                button1.Text = "Zamień";
                loadValueFromQuery();
            }
            addButton.Click += button1_Click;
            this.AcceptButton = addButton;
        }

        public void loadValueFromQuery()
        {
            string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=albumy_muz.mdb;" + "Persist Security Info=True;" + "Jet OLEDB:Database Password=myPassword;";
            using (OleDbConnection conn = new OleDbConnection(conString))
            {
                conn.Open();
                OleDbDataReader reader;                
                using (OleDbCommand cmd = new OleDbCommand("SELECT id_zespolu,id_gatunek,id_wytwornia,nazwa,data_wydania,opis FROM album WHERE ID="+ IDToSQLQuery, conn))
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        textBox1.Text = reader["nazwa"].ToString();
                        richTextBox1.Text = reader["opis"].ToString();
                        dateTimePicker1.Value = (DateTime)reader["data_wydania"];

                        using (OleDbCommand cmdID = new OleDbCommand("SELECT nazwa FROM zespol WHERE id="+ (int)reader["id_zespolu"], conn))
                        {
                            OleDbDataReader readerTemporary = cmdID.ExecuteReader();
                            while (readerTemporary.Read())
                                comboBox1.Text = readerTemporary["nazwa"].ToString();
                        }

                        using (OleDbCommand cmdID = new OleDbCommand("SELECT nazwa FROM gatunek WHERE id=" + (int)reader["id_gatunek"], conn))
                        {
                            OleDbDataReader readerTemporary = cmdID.ExecuteReader();
                            while (readerTemporary.Read())
                                comboBox2.Text = readerTemporary["nazwa"].ToString();
                        }

                        using (OleDbCommand cmdID = new OleDbCommand("SELECT nazwa FROM wytwornia WHERE id=" + (int)reader["id_wytwornia"], conn))
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
            DateTime date1 = new DateTime(2010, 1, 1, 0, 0, 0);
            if (textBox1.Text.Length == 0 || richTextBox1.Text.Length == 0 || comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "")
            {
                MessageBox.Show("Wprowadż poprawne wartości do wszystkich pól", "Ostrzeżenie", MessageBoxButtons.OK);
                return;
            }

            if (InsertIntoDatabase.Insert("album", textBox1.Text, richTextBox1.Text, dateTimePicker1.Value.Date, comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString(), comboBox3.SelectedItem.ToString(),modeForm,IDToSQLQuery))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void fillComboBox(ComboBox comboBox, int comboValue)
        {
            string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=albumy_muz.mdb;" + "Persist Security Info=True;" + "Jet OLEDB:Database Password=myPassword;";
            using (OleDbConnection conn = new OleDbConnection(conString))
            {
                conn.Open();
                OleDbDataReader reader;

                if (comboValue == 1)
                {
                    if (permissionsTab["zespol"][1]) comboBox.Items.Add("DODAJ NOWĄ WARTOŚĆ...");
                    using (OleDbCommand cmd = new OleDbCommand("SELECT nazwa FROM zespol WHERE [czy_usuniete] = false ORDER BY nazwa", conn))
                    {
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                            comboBox1.Items.Add(reader["nazwa"].ToString());
                    }
                }

                if (comboValue == 2)
                {
                    if (permissionsTab["gatunek"][1]) comboBox.Items.Add("DODAJ NOWĄ WARTOŚĆ...");
                    using (OleDbCommand cmd = new OleDbCommand("SELECT nazwa FROM gatunek WHERE [czy_usuniete] = false ORDER BY nazwa", conn))
                    {
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                            comboBox2.Items.Add(reader["nazwa"].ToString());
                    }
                }

                if (comboValue == 3)
                {
                    if (permissionsTab["wytwornia"][1]) comboBox.Items.Add("DODAJ NOWĄ WARTOŚĆ...");
                    using (OleDbCommand cmd = new OleDbCommand("SELECT nazwa FROM wytwornia WHERE [czy_usuniete] = false ORDER BY nazwa", conn))
                    {
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                            comboBox3.Items.Add(reader["nazwa"].ToString());
                    }
                }
                conn.Close();

                comboBox.Text = "";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0 && permissionsTab["zespol"][1])
            {
                WindowManage.SwitchWindow("zespol",false,0, permissionsTab);
                comboBox1.Items.Clear();
                fillComboBox(comboBox1, 1);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0 && permissionsTab["gatunek"][1])
            {
                WindowManage.SwitchWindow("gatunek", false, 0, permissionsTab);
                comboBox2.Items.Clear();
                fillComboBox(comboBox2, 2);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0 && permissionsTab["wytwornia"][1])
            {
                WindowManage.SwitchWindow("wytwornia", false, 0, permissionsTab);
                comboBox3.Items.Clear();
                fillComboBox(comboBox3, 3);
            }
        }
    }
}
