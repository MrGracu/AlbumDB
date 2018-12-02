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
    public partial class OcenaAlbumuForm : Form
    {
        public OcenaAlbumuForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value == 0 || numericUpDown2.Value == 0 || richTextBox1.Text.Length == 0) return;

            if (InsertIntoDatabase.Insert("ocena_albumu", numericUpDown1.Value, numericUpDown2.Value, richTextBox1.Text))
            {
                this.Close();
            }
        }
    }
}
