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
    public partial class WytworniaForm : Form
    {
        public WytworniaForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0) return;

            if (InsertIntoDatabase.Insert("wytwornia", textBox1.Text))
            {
                this.Close();
            }
        }
    }
}
