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
    public partial class GrupaForm : Form
    {
        Dictionary<string, bool[]> permissionsTab;
        bool modeForm; //0 - insert, 1 - update
        int IDToSQLQuery;

        private Button addButton = new Button();

        public GrupaForm(bool mode, int id, Dictionary<string, bool[]> permTab)
        {
            permissionsTab = permTab;
            InitializeComponent();
            modeForm = mode;
            IDToSQLQuery = id; //przekazuje zmniejszony
            if (modeForm)
            {
                this.Text = "Edytuj Grupę";
                button1.Text = "Zamień";
                loadValueFromQuery();
                if (id == 1)
                {
                    groupBox1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                }
            }
            addButton.Click += button1_Click;
            this.AcceptButton = addButton;
        }

        public void loadValueFromQuery()
        {
            string conString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=..\\..\\albumy_muz.mdb;" + "Persist Security Info=True;" + "Jet OLEDB:Database Password=myPassword;";
            using (OleDbConnection conn = new OleDbConnection(conString))
            {
                conn.Open();
                OleDbDataReader reader;
                using (OleDbCommand cmd = new OleDbCommand("SELECT [nazwa], [wyswietl_album], [wyswietl_czlonek_zespolu], [wyswietl_gatunek], [wyswietl_muzyk], [wyswietl_ocena], [wyswietl_ocena_albumu], [wyswietl_piosenka], [wyswietl_stanowisko], [wyswietl_wytwornia], [wyswietl_zespol], [wyswietl_uzytkownik], [wyswietl_grupa], [dodaj_album], [dodaj_czlonek_zespolu], [dodaj_gatunek], [dodaj_muzyk], [dodaj_ocena_albumu], [dodaj_piosenka], [dodaj_stanowisko], [dodaj_wytwornia], [dodaj_zespol], [dodaj_uzytkownik], [dodaj_grupa], [edytuj_album], [edytuj_czlonek_zespolu], [edytuj_gatunek], [edytuj_muzyk], [edytuj_ocena_albumu], [edytuj_piosenka], [edytuj_stanowisko], [edytuj_wytwornia], [edytuj_zespol], [edytuj_uzytkownik], [edytuj_grupa], [usun_album], [usun_czlonek_zespolu], [usun_gatunek], [usun_muzyk], [usun_ocena_albumu], [usun_piosenka], [usun_stanowisko], [usun_wytwornia], [usun_zespol], [usun_uzytkownik], [usun_grupa] FROM [grupa] WHERE grupa.czy_usuniete=False AND grupa.id=" + IDToSQLQuery, conn))
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        textBox1.Text = reader["nazwa"].ToString();
                        checkBox1.Checked = (bool)reader["wyswietl_album"];
                        checkBox2.Checked = (bool)reader["dodaj_album"];
                        checkBox3.Checked = (bool)reader["edytuj_album"];
                        checkBox4.Checked = (bool)reader["usun_album"];
                        checkBox5.Checked = (bool)reader["wyswietl_czlonek_zespolu"];
                        checkBox6.Checked = (bool)reader["dodaj_czlonek_zespolu"];
                        checkBox7.Checked = (bool)reader["edytuj_czlonek_zespolu"];
                        checkBox8.Checked = (bool)reader["usun_czlonek_zespolu"];
                        checkBox9.Checked = (bool)reader["wyswietl_gatunek"];
                        checkBox10.Checked = (bool)reader["dodaj_gatunek"];
                        checkBox11.Checked = (bool)reader["edytuj_gatunek"];
                        checkBox12.Checked = (bool)reader["usun_gatunek"];
                        checkBox13.Checked = (bool)reader["wyswietl_grupa"];
                        checkBox14.Checked = (bool)reader["dodaj_grupa"];
                        checkBox15.Checked = (bool)reader["edytuj_grupa"];
                        checkBox16.Checked = (bool)reader["usun_grupa"];
                        checkBox17.Checked = (bool)reader["wyswietl_muzyk"];
                        checkBox18.Checked = (bool)reader["dodaj_muzyk"];
                        checkBox19.Checked = (bool)reader["edytuj_muzyk"];
                        checkBox20.Checked = (bool)reader["usun_muzyk"];
                        checkBox21.Checked = (bool)reader["wyswietl_ocena"];
                        checkBox22.Checked = (bool)reader["wyswietl_ocena_albumu"];
                        checkBox23.Checked = (bool)reader["dodaj_ocena_albumu"];
                        checkBox24.Checked = (bool)reader["edytuj_ocena_albumu"];
                        checkBox25.Checked = (bool)reader["usun_ocena_albumu"];
                        checkBox26.Checked = (bool)reader["wyswietl_piosenka"];
                        checkBox27.Checked = (bool)reader["dodaj_piosenka"];
                        checkBox28.Checked = (bool)reader["edytuj_piosenka"];
                        checkBox29.Checked = (bool)reader["usun_piosenka"];
                        checkBox30.Checked = (bool)reader["wyswietl_stanowisko"];
                        checkBox31.Checked = (bool)reader["dodaj_stanowisko"];
                        checkBox32.Checked = (bool)reader["edytuj_stanowisko"];
                        checkBox33.Checked = (bool)reader["usun_stanowisko"];
                        checkBox34.Checked = (bool)reader["wyswietl_uzytkownik"];
                        checkBox35.Checked = (bool)reader["dodaj_uzytkownik"];
                        checkBox36.Checked = (bool)reader["edytuj_uzytkownik"];
                        checkBox37.Checked = (bool)reader["usun_uzytkownik"];
                        checkBox38.Checked = (bool)reader["wyswietl_wytwornia"];
                        checkBox39.Checked = (bool)reader["dodaj_wytwornia"];
                        checkBox40.Checked = (bool)reader["edytuj_wytwornia"];
                        checkBox41.Checked = (bool)reader["usun_wytwornia"];
                        checkBox42.Checked = (bool)reader["wyswietl_zespol"];
                        checkBox43.Checked = (bool)reader["dodaj_zespol"];
                        checkBox44.Checked = (bool)reader["edytuj_zespol"];
                        checkBox45.Checked = (bool)reader["usun_zespol"];
                    }
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

            if (InsertIntoDatabase.Insert("grupa", textBox1.Text, checkBox1.Checked, checkBox2.Checked, checkBox3.Checked, checkBox4.Checked, checkBox5.Checked, checkBox6.Checked, checkBox7.Checked, checkBox8.Checked, checkBox9.Checked, checkBox10.Checked, checkBox11.Checked, checkBox12.Checked, checkBox13.Checked, checkBox14.Checked, checkBox15.Checked, checkBox16.Checked, checkBox17.Checked, checkBox18.Checked, checkBox19.Checked, checkBox20.Checked, checkBox21.Checked, checkBox22.Checked, checkBox23.Checked, checkBox24.Checked, checkBox25.Checked,
                checkBox26.Checked, checkBox27.Checked, checkBox28.Checked, checkBox29.Checked, checkBox30.Checked, checkBox31.Checked, checkBox32.Checked, checkBox33.Checked, checkBox34.Checked, checkBox35.Checked, checkBox36.Checked, checkBox37.Checked, checkBox38.Checked, checkBox39.Checked, checkBox40.Checked, checkBox41.Checked, checkBox42.Checked, checkBox43.Checked, checkBox44.Checked, checkBox45.Checked, modeForm, IDToSQLQuery))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
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
            checkBox50.Checked = checkBox51.Checked = checkBox52.Checked = checkBox53.Checked = checkBox54.Checked = checkBox21.Checked = checkBox56.Checked = checkBox57.Checked = checkBox58.Checked = checkBox59.Checked = checkBox60.Checked = checkBox61.Checked = true;
        }

        private void checkBoxAlbum_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl != checkBox50)
            {
                if (checkBox1.Checked && checkBox2.Checked && checkBox3.Checked && checkBox4.Checked) checkBox50.Checked = true;
                else checkBox50.Checked = false;
            }
        }

        private void checkBoxCzlonekZespolu_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl != checkBox51)
            {
                if (checkBox5.Checked && checkBox6.Checked && checkBox7.Checked && checkBox8.Checked) checkBox51.Checked = true;
                else checkBox51.Checked = false;
            }
        }

        private void checkBoxGatunek_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl != checkBox52)
            {
                if (checkBox9.Checked && checkBox10.Checked && checkBox11.Checked && checkBox12.Checked) checkBox52.Checked = true;
                else checkBox52.Checked = false;
            }
        }

        private void checkBoxGrupa_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl != checkBox53)
            {
                if (checkBox13.Checked && checkBox14.Checked && checkBox15.Checked && checkBox16.Checked) checkBox53.Checked = true;
                else checkBox53.Checked = false;
            }
        }

        private void checkBoxMuzyk_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl != checkBox54)
            {
                if (checkBox17.Checked && checkBox18.Checked && checkBox19.Checked && checkBox20.Checked) checkBox54.Checked = true;
                else checkBox54.Checked = false;
            }
        }

        private void checkBoxOcena_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl == checkBox55)
            {
                checkBox21.Checked = checkBox55.Checked;
            }
            else checkBox55.Checked = checkBox21.Checked;
        }

        private void checkBoxOcenaAlbumu_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl != checkBox56)
            {
                if (checkBox22.Checked && checkBox23.Checked && checkBox24.Checked && checkBox25.Checked) checkBox56.Checked = true;
                else checkBox56.Checked = false;
            }
        }

        private void checkBoxPiosenka_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl != checkBox57)
            {
                if (checkBox26.Checked && checkBox27.Checked && checkBox28.Checked && checkBox29.Checked) checkBox57.Checked = true;
                else checkBox57.Checked = false;
            }
        }

        private void checkBoxStanowisko_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl != checkBox58)
            {
                if (checkBox30.Checked && checkBox31.Checked && checkBox32.Checked && checkBox33.Checked) checkBox58.Checked = true;
                else checkBox58.Checked = false;
            }
        }

        private void checkBoxUzytkownik_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl != checkBox59)
            {
                if (checkBox34.Checked && checkBox35.Checked && checkBox36.Checked && checkBox37.Checked) checkBox59.Checked = true;
                else checkBox59.Checked = false;
            }
        }

        private void checkBoxWytwornia_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl != checkBox60)
            {
                if (checkBox38.Checked && checkBox39.Checked && checkBox40.Checked && checkBox41.Checked) checkBox60.Checked = true;
                else checkBox60.Checked = false;
            }
        }

        private void checkBoxZespol_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl != checkBox61)
            {
                if (checkBox42.Checked && checkBox43.Checked && checkBox44.Checked && checkBox45.Checked) checkBox61.Checked = true;
                else checkBox61.Checked = false;
            }
        }

        private void checkBox50_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox50.Checked)
            {
                checkBox1.Checked = checkBox2.Checked = checkBox3.Checked = checkBox4.Checked = true;
            }
            else
            {
                if(this.ActiveControl == checkBox50) checkBox1.Checked = checkBox2.Checked = checkBox3.Checked = checkBox4.Checked = false;
            }
        }

        private void checkBox51_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox51.Checked)
            {
                checkBox5.Checked = checkBox6.Checked = checkBox7.Checked = checkBox8.Checked = true;
            }
            else
            {
                if (this.ActiveControl == checkBox51) checkBox5.Checked = checkBox6.Checked = checkBox7.Checked = checkBox8.Checked = false;
            }
        }

        private void checkBox52_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox52.Checked)
            {
                checkBox9.Checked = checkBox10.Checked = checkBox11.Checked = checkBox12.Checked = true;
            }
            else
            {
                if (this.ActiveControl == checkBox52) checkBox9.Checked = checkBox10.Checked = checkBox11.Checked = checkBox12.Checked = false;
            }
        }

        private void checkBox53_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox53.Checked)
            {
                checkBox13.Checked = checkBox14.Checked = checkBox15.Checked = checkBox16.Checked = true;
            }
            else
            {
                if (this.ActiveControl == checkBox53) checkBox13.Checked = checkBox14.Checked = checkBox15.Checked = checkBox16.Checked = false;
            }
        }

        private void checkBox54_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox54.Checked)
            {
                checkBox17.Checked = checkBox18.Checked = checkBox19.Checked = checkBox20.Checked = true;
            }
            else
            {
                if (this.ActiveControl == checkBox54) checkBox17.Checked = checkBox18.Checked = checkBox19.Checked = checkBox20.Checked = false;
            }
        }

        private void checkBox56_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox56.Checked)
            {
                checkBox22.Checked = checkBox23.Checked = checkBox24.Checked = checkBox25.Checked = true;
            }
            else
            {
                if (this.ActiveControl == checkBox56) checkBox22.Checked = checkBox23.Checked = checkBox24.Checked = checkBox25.Checked = false;
            }
        }

        private void checkBox57_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox57.Checked)
            {
                checkBox26.Checked = checkBox27.Checked = checkBox28.Checked = checkBox29.Checked = true;
            }
            else
            {
                if (this.ActiveControl == checkBox57) checkBox26.Checked = checkBox27.Checked = checkBox28.Checked = checkBox29.Checked = false;
            }
        }

        private void checkBox58_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox58.Checked)
            {
                checkBox30.Checked = checkBox31.Checked = checkBox32.Checked = checkBox33.Checked = true;
            }
            else
            {
                if (this.ActiveControl == checkBox58) checkBox30.Checked = checkBox31.Checked = checkBox32.Checked = checkBox33.Checked = false;
            }
        }

        private void checkBox59_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox59.Checked)
            {
                checkBox34.Checked = checkBox35.Checked = checkBox36.Checked = checkBox37.Checked = true;
            }
            else
            {
                if (this.ActiveControl == checkBox59) checkBox34.Checked = checkBox35.Checked = checkBox36.Checked = checkBox37.Checked = false;
            }
        }

        private void checkBox60_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox60.Checked)
            {
                checkBox38.Checked = checkBox39.Checked = checkBox40.Checked = checkBox41.Checked = true;
            }
            else
            {
                if (this.ActiveControl == checkBox60) checkBox38.Checked = checkBox39.Checked = checkBox40.Checked = checkBox41.Checked = false;
            }
        }

        private void checkBox61_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox61.Checked)
            {
                checkBox42.Checked = checkBox43.Checked = checkBox44.Checked = checkBox45.Checked = true;
            }
            else
            {
                if (this.ActiveControl == checkBox61) checkBox42.Checked = checkBox43.Checked = checkBox44.Checked = checkBox45.Checked = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = checkBox2.Checked = checkBox3.Checked = checkBox4.Checked = false;
            checkBox5.Checked = checkBox6.Checked = checkBox7.Checked = checkBox8.Checked = false;
            checkBox9.Checked = checkBox10.Checked = checkBox11.Checked = checkBox12.Checked = false;
            checkBox13.Checked = checkBox14.Checked = checkBox15.Checked = checkBox16.Checked = false;
            checkBox17.Checked = checkBox18.Checked = checkBox19.Checked = checkBox20.Checked = checkBox21.Checked = false;
            checkBox22.Checked = checkBox23.Checked = checkBox24.Checked = checkBox25.Checked = false;
            checkBox26.Checked = checkBox27.Checked = checkBox28.Checked = checkBox29.Checked = false;
            checkBox30.Checked = checkBox31.Checked = checkBox32.Checked = checkBox33.Checked = false;
            checkBox34.Checked = checkBox35.Checked = checkBox36.Checked = checkBox37.Checked = false;
            checkBox38.Checked = checkBox39.Checked = checkBox40.Checked = checkBox41.Checked = false;
            checkBox42.Checked = checkBox43.Checked = checkBox44.Checked = checkBox45.Checked = false;
        }
    }
}
