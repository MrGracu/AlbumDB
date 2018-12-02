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
    public partial class CzlonekZespoluForm : Form
    {
        public CzlonekZespoluForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value == 0 || numericUpDown2.Value == 0 || numericUpDown3.Value == 0) return;

            if (InsertIntoDatabase.Insert("czlonek_zespolu", numericUpDown1.Value, numericUpDown2.Value, numericUpDown3.Value))
            {
                this.Close();
            }
        }
    }
}
