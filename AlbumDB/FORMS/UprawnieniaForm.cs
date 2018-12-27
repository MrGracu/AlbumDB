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
    public partial class UprawnieniaForm : Form
    {
        public UprawnieniaForm()
        {
            InitializeComponent();
            setInitialValue();
        }

        private void setInitialValue()
        {
            
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            

            if (e.Row > 0 && e.Row % 2 != 1)
                e.Graphics.FillRectangle(Brushes.Gainsboro, e.CellBounds);
            else
            {
                if (e.Row == 0)
                    e.Graphics.FillRectangle(Brushes.Silver, e.CellBounds);
                else
                    e.Graphics.FillRectangle(Brushes.White, e.CellBounds);
            }

            if (e.Column == 5)
                e.Graphics.DrawLine(new Pen(Color.Black), e.CellBounds.Location, new Point(e.CellBounds.Left,e.CellBounds.Bottom));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void checkBox24_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox check = sender as CheckBox;
            var k = check.TabIndex;
            MessageBox.Show(k.ToString());
        }
    }
}
