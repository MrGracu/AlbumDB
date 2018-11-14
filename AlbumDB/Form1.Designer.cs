namespace AlbumDB
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.albumyDataSet = new AlbumDB.albumyDataSet();
            this.albumyDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.albumBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.albumTableAdapter = new AlbumDB.albumyDataSetTableAdapters.albumTableAdapter();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idzespoluDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idgatunekDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idwytworniaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nazwaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datawydaniaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iloscpiosenekDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dlugoscDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.opisDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumyDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumyDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.idzespoluDataGridViewTextBoxColumn,
            this.idgatunekDataGridViewTextBoxColumn,
            this.idwytworniaDataGridViewTextBoxColumn,
            this.nazwaDataGridViewTextBoxColumn,
            this.datawydaniaDataGridViewTextBoxColumn,
            this.iloscpiosenekDataGridViewTextBoxColumn,
            this.dlugoscDataGridViewTextBoxColumn,
            this.opisDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.albumBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(172, 139);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(565, 150);
            this.dataGridView1.TabIndex = 0;
            // 
            // albumyDataSet
            // 
            this.albumyDataSet.DataSetName = "albumyDataSet";
            this.albumyDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // albumyDataSetBindingSource
            // 
            this.albumyDataSetBindingSource.DataSource = this.albumyDataSet;
            this.albumyDataSetBindingSource.Position = 0;
            // 
            // albumBindingSource
            // 
            this.albumBindingSource.DataMember = "album";
            this.albumBindingSource.DataSource = this.albumyDataSetBindingSource;
            // 
            // albumTableAdapter
            // 
            this.albumTableAdapter.ClearBeforeFill = true;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // idzespoluDataGridViewTextBoxColumn
            // 
            this.idzespoluDataGridViewTextBoxColumn.DataPropertyName = "id_zespolu";
            this.idzespoluDataGridViewTextBoxColumn.HeaderText = "id_zespolu";
            this.idzespoluDataGridViewTextBoxColumn.Name = "idzespoluDataGridViewTextBoxColumn";
            // 
            // idgatunekDataGridViewTextBoxColumn
            // 
            this.idgatunekDataGridViewTextBoxColumn.DataPropertyName = "id_gatunek";
            this.idgatunekDataGridViewTextBoxColumn.HeaderText = "id_gatunek";
            this.idgatunekDataGridViewTextBoxColumn.Name = "idgatunekDataGridViewTextBoxColumn";
            // 
            // idwytworniaDataGridViewTextBoxColumn
            // 
            this.idwytworniaDataGridViewTextBoxColumn.DataPropertyName = "id_wytwornia";
            this.idwytworniaDataGridViewTextBoxColumn.HeaderText = "id_wytwornia";
            this.idwytworniaDataGridViewTextBoxColumn.Name = "idwytworniaDataGridViewTextBoxColumn";
            // 
            // nazwaDataGridViewTextBoxColumn
            // 
            this.nazwaDataGridViewTextBoxColumn.DataPropertyName = "nazwa";
            this.nazwaDataGridViewTextBoxColumn.HeaderText = "nazwa";
            this.nazwaDataGridViewTextBoxColumn.Name = "nazwaDataGridViewTextBoxColumn";
            // 
            // datawydaniaDataGridViewTextBoxColumn
            // 
            this.datawydaniaDataGridViewTextBoxColumn.DataPropertyName = "data_wydania";
            this.datawydaniaDataGridViewTextBoxColumn.HeaderText = "data_wydania";
            this.datawydaniaDataGridViewTextBoxColumn.Name = "datawydaniaDataGridViewTextBoxColumn";
            // 
            // iloscpiosenekDataGridViewTextBoxColumn
            // 
            this.iloscpiosenekDataGridViewTextBoxColumn.DataPropertyName = "ilosc_piosenek";
            this.iloscpiosenekDataGridViewTextBoxColumn.HeaderText = "ilosc_piosenek";
            this.iloscpiosenekDataGridViewTextBoxColumn.Name = "iloscpiosenekDataGridViewTextBoxColumn";
            // 
            // dlugoscDataGridViewTextBoxColumn
            // 
            this.dlugoscDataGridViewTextBoxColumn.DataPropertyName = "dlugosc";
            this.dlugoscDataGridViewTextBoxColumn.HeaderText = "dlugosc";
            this.dlugoscDataGridViewTextBoxColumn.Name = "dlugoscDataGridViewTextBoxColumn";
            // 
            // opisDataGridViewTextBoxColumn
            // 
            this.opisDataGridViewTextBoxColumn.DataPropertyName = "opis";
            this.opisDataGridViewTextBoxColumn.HeaderText = "opis";
            this.opisDataGridViewTextBoxColumn.Name = "opisDataGridViewTextBoxColumn";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumyDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumyDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource albumyDataSetBindingSource;
        private albumyDataSet albumyDataSet;
        private System.Windows.Forms.BindingSource albumBindingSource;
        private albumyDataSetTableAdapters.albumTableAdapter albumTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idzespoluDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idgatunekDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idwytworniaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nazwaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datawydaniaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iloscpiosenekDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dlugoscDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn opisDataGridViewTextBoxColumn;
    }
}

