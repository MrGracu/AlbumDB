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
    public partial class GatunekForm : Form
    {
        bool modeForm; //0 - insert, 1 - update
        int IDToSQLQuery;
        public GatunekForm(bool mode, int id)
        {
            InitializeComponent();
            modeForm = mode;
            IDToSQLQuery = id + 1; //przekazuje zmniejszony
            if (modeForm)
            {
                this.Text = "Edycja albumu";
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
                using (OleDbCommand cmd = new OleDbCommand("SELECT nazwa FROM gatunek WHERE ID=" + IDToSQLQuery, conn))
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                        textBox1.Text = reader["nazwa"].ToString();
                }
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("Wprowadż poprawne wartości do wszystkich pól", "Ostrzeżenie", MessageBoxButtons.OK);
                return;
            }

            if (InsertIntoDatabase.Insert("gatunek",textBox1.Text,modeForm,IDToSQLQuery))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
