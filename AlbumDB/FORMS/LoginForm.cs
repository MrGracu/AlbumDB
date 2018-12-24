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
    public partial class LoginForm : Form
    {
        string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=..\\..\\albumy_muz.mdb;" + "Persist Security Info=True;" + "Jet OLEDB:Database Password=myPassword;";

        public int ReturnGroup { get; set; }
        public string ReturnUser { get; set; }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.icon;
            textBox1_SetText();
            textBox2_SetText();
            textBox3_SetText();
            textBox4_SetText();
            textBox5_SetText();
        }

        private void textBox1_SetText()
        {
            this.textBox1.Text = "Twój login...";
            this.textBox1.ForeColor = Color.Gray;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.ForeColor == Color.Black)
                return;
            this.textBox1.Text = "";
            this.textBox1.ForeColor = Color.Black;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
                textBox1_SetText();
        }

        private void textBox2_SetText()
        {
            this.textBox2.Text = "Twoje hasło...";
            this.textBox2.ForeColor = Color.Gray;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.ForeColor == Color.Black)
                return;
            this.textBox2.Text = "";
            this.textBox2.ForeColor = Color.Black;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim() == "")
                textBox2_SetText();
        }

        private void textBox3_SetText()
        {
            this.textBox3.Text = "Wpisz swój login...";
            this.textBox3.ForeColor = Color.Gray;
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.ForeColor == Color.Black)
                return;
            this.textBox3.Text = "";
            this.textBox3.ForeColor = Color.Black;
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text.Trim() == "")
                textBox3_SetText();
        }

        private void textBox4_SetText()
        {
            this.textBox4.Text = "Podaj hasło...";
            this.textBox4.ForeColor = Color.Gray;
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.ForeColor == Color.Black)
                return;
            this.textBox4.Text = "";
            this.textBox4.ForeColor = Color.Black;
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text.Trim() == "")
                textBox4_SetText();
        }

        private void textBox5_SetText()
        {
            this.textBox5.Text = "Powtórz hasło...";
            this.textBox5.ForeColor = Color.Gray;
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.ForeColor == Color.Black)
                return;
            this.textBox5.Text = "";
            this.textBox5.ForeColor = Color.Black;
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text.Trim() == "")
                textBox5_SetText();
        }

        private void button1_Click(object sender, EventArgs e) //inny
        {
            if (textBox1.Text.Trim().Length == 0 || textBox1.ForeColor == Color.Gray || textBox2.Text.Trim().Length == 0 || textBox2.ForeColor == Color.Gray) return;

            string password = textBox2.Text;

            using (OleDbConnection conn = new OleDbConnection(conString))
            using (OleDbCommand cmd = new OleDbCommand("SELECT id_grupy FROM uzytkownik WHERE [login]=\"" + textBox1.Text + "\" AND [haslo]=\"" + password + "\" AND [czy_usuniete]=false", conn))
            {
                conn.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    this.ReturnUser = textBox1.Text;
                    this.ReturnGroup = (int)reader["id_grupy"];
                    this.DialogResult = DialogResult.OK;
                    label1.Visible = false;
                }
                conn.Close();
            }

            if (this.DialogResult == DialogResult.OK) this.Close();
            else label1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e) //gosc
        {
            this.ReturnGroup = 3; //GOŚĆ!!!
            this.ReturnUser = "Gość";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox2.Focus();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                label1.Visible = false;
                textBox1_SetText();
                textBox2_SetText();
            }
            else
            {
                textBox3_SetText();
                textBox4_SetText();
                textBox5_SetText();
            }
        }
    }
}
