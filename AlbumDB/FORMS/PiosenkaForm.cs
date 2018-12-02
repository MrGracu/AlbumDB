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
    public partial class PiosenkaForm : Form
    {
        public PiosenkaForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime date1 = new DateTime(2010, 1, 1, 0, 0, 0);
            if (textBox1.Text.Length == 0 || dateTimePicker2.Value.TimeOfDay <= date1.TimeOfDay || numericUpDown1.Value == 0 || numericUpDown3.Value == 0) return;

            if (InsertIntoDatabase.Insert("piosenka", textBox1.Text, dateTimePicker2.Value.TimeOfDay, numericUpDown1.Value, numericUpDown3.Value))
            {
                this.Close();
            }
        }
    }
}
