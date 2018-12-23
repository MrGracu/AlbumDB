using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlbumDB.FORMS
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.icon;
            this.textBox1.Enter += new EventHandler(textBox1_Enter);
            this.textBox1.Leave += new EventHandler(textBox1_Leave);
            this.textBox2.Enter += new EventHandler(textBox1_Enter);
            this.textBox2.Leave += new EventHandler(textBox1_Leave);
            this.textBox3.Enter += new EventHandler(textBox1_Enter);
            this.textBox3.Leave += new EventHandler(textBox1_Leave);
            this.textBox4.Enter += new EventHandler(textBox1_Enter);
            this.textBox4.Leave += new EventHandler(textBox1_Leave);
            this.textBox5.Enter += new EventHandler(textBox1_Enter);
            this.textBox5.Leave += new EventHandler(textBox1_Leave);
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
    }
}
