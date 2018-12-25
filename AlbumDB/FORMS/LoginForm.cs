using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
            tabControl1.SelectedIndex = 0;
            this.AcceptButton = button1;
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
            this.textBox2.PasswordChar = '\0'; //Wyzerowanie (null character)
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.ForeColor == Color.Black)
                return;
            this.textBox2.Text = "";
            this.textBox2.ForeColor = Color.Black;
            this.textBox2.PasswordChar = '●';
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

        private bool TextBox3Verify() //Czy znaleziono blad? (true - tak)
        {
            Regex pattern = new Regex("^[a-zA-Z0-9żźćńółęąśŻŹĆĄŚĘŁÓŃ]+$");
            if (pattern.IsMatch(textBox3.Text))
            {
                label2.Visible = false;
                if (textBox3.Text.Length < 3)
                {
                    label2.Text = "Minimalna długość loginu to 3 znaki!";
                    label2.Visible = true;
                    return true;
                }
            }
            else
            {
                label2.Text = "Login zawiera niedozwolone znaki!";
                label2.Visible = true;
                return true;
            }
            return false;
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text.Trim() == "")
                textBox3_SetText();
            else
            {
                TextBox3Verify();
            }
        }

        private void textBox4_SetText()
        {
            this.textBox4.Text = "Podaj hasło...";
            this.textBox4.ForeColor = Color.Gray;
            this.textBox4.PasswordChar = '\0'; //Wyzerowanie (null character)
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.ForeColor == Color.Black)
                return;
            this.textBox4.Text = "";
            this.textBox4.ForeColor = Color.Black;
            this.textBox4.PasswordChar = '●';
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
            this.textBox5.PasswordChar = '\0'; //Wyzerowanie (null character)
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.ForeColor == Color.Black)
                return;
            this.textBox5.Text = "";
            this.textBox5.ForeColor = Color.Black;
            this.textBox5.PasswordChar = '●';
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text.Trim() == "")
                textBox5_SetText();
        }

        private static string HashSHA256(string text) //HASHING
        {
            string result = "";
            using (var sha = new SHA256Managed())
            {
                sha.ComputeHash(Encoding.UTF8.GetBytes(text));
                var hash = sha.Hash;
                result = string.Join(string.Empty,hash.Select(x => x.ToString("x2"))); //Return as hexadecimal string
            }
            return result;
        }

        private void LogIn()
        {
            if (textBox1.Text.Trim().Length == 0 || textBox1.ForeColor == Color.Gray || textBox2.Text.Trim().Length == 0 || textBox2.ForeColor == Color.Gray) return;

            Regex pattern = new Regex("^[a-zA-Z0-9żźćńółęąśŻŹĆĄŚĘŁÓŃ]+$");
            if (pattern.IsMatch(textBox1.Text))
            {
                label2.Visible = false;
                if (textBox1.Text.Length < 3)
                {
                    label1.Text = "Podaj prawidłowy login!";
                    label1.Visible = true;
                    return;
                }
            }
            else
            {
                label1.Text = "Podaj prawidłowy login!";
                label1.Visible = true;
                return;
            }

            string hash = HashSHA256(textBox2.Text);

            using (OleDbConnection conn = new OleDbConnection(conString))
            using (OleDbCommand cmd = new OleDbCommand("SELECT id_grupy FROM uzytkownik WHERE StrComp([login], \"" + textBox1.Text + "\", 0)=0 AND [haslo]=\"" + hash + "\" AND [czy_usuniete]=false", conn))
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
            else
            {
                label1.Text = "Podany użytkownik nie istnieje!";
                label1.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e) //inny
        {
            LogIn();
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
                this.AcceptButton = button1;
                label1.Visible = false;
                textBox1_SetText();
                textBox2_SetText();
            }
            else
            {
                this.AcceptButton = button3;
                label2.Visible = false;
                textBox3_SetText();
                textBox4_SetText();
                textBox5_SetText();
            }
        }

        private void Register()
        {
            if (textBox3.Text.Trim().Length == 0 || textBox3.ForeColor == Color.Gray || textBox4.Text.Trim().Length == 0 || textBox4.ForeColor == Color.Gray || textBox5.Text.Trim().Length == 0 || textBox5.ForeColor == Color.Gray) return;

            if (TextBox3Verify()) return;

            if (textBox4.Text != textBox5.Text)
            {
                label2.Text = "Podane hasła są różne!";
                label2.Visible = true;
                return;
            }
            else label2.Visible = false;

            if (textBox4.Text.Length < 4)
            {
                label2.Text = "Minimalna długość hasła wynosi 4 znaki!";
                label2.Visible = true;
                return;
            }
            else label2.Visible = false;

            using (OleDbConnection conn = new OleDbConnection(conString))
            {
                int dataExists = 0;
                using (OleDbCommand cmd = new OleDbCommand("SELECT COUNT(*) FROM uzytkownik WHERE (StrComp([login], \"" + textBox3.Text + "\", 0)=0 AND [czy_usuniete]=false)", conn))
                {
                    conn.Open();
                    dataExists = (int)cmd.ExecuteScalar();
                    conn.Close();
                }

                if (dataExists > 0)
                {
                    label2.Text = "Użytkownik o podanym loginie już istnieje!";
                    label2.Visible = true;
                }
                else
                {
                    label2.Visible = false;

                    string hash = HashSHA256(textBox4.Text);

                    using (OleDbCommand cmd = new OleDbCommand("INSERT INTO uzytkownik ([id_grupy],[login],[haslo]) VALUES (2,\"" + textBox3.Text + "\",\"" + hash + "\")", conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Pomyślnie zarejestrowano nowego użytkownika!", "Utworzono", MessageBoxButtons.OK);
                        tabControl1.SelectedIndex = 0;
                        login.Focus();
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Register();
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Register();
            }
        }
    }
}
