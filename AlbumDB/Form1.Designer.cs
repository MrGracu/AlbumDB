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
            this.albumBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.albumyDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.albumyDataSet = new AlbumDB.albumyDataSet();
            this.albumTableAdapter = new AlbumDB.albumyDataSetTableAdapters.albumTableAdapter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.albumBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.albumyDataSetBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.albumBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.albumBindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumyDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumyDataSet)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.albumBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumyDataSetBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumBindingSource3)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1, 61);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(801, 390);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // albumBindingSource
            // 
            this.albumBindingSource.DataMember = "album";
            this.albumBindingSource.DataSource = this.albumyDataSetBindingSource;
            // 
            // albumyDataSetBindingSource
            // 
            this.albumyDataSetBindingSource.DataSource = this.albumyDataSet;
            this.albumyDataSetBindingSource.Position = 0;
            // 
            // albumyDataSet
            // 
            this.albumyDataSet.DataSetName = "albumyDataSet";
            this.albumyDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // albumTableAdapter
            // 
            this.albumTableAdapter.ClearBeforeFill = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Location = new System.Drawing.Point(0, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(802, 33);
            this.panel1.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(627, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(152, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // albumBindingSource1
            // 
            this.albumBindingSource1.DataMember = "album";
            this.albumBindingSource1.DataSource = this.albumyDataSetBindingSource;
            // 
            // albumyDataSetBindingSource1
            // 
            this.albumyDataSetBindingSource1.DataSource = this.albumyDataSet;
            this.albumyDataSetBindingSource1.Position = 0;
            // 
            // albumBindingSource2
            // 
            this.albumBindingSource2.DataMember = "album";
            this.albumBindingSource2.DataSource = this.albumyDataSetBindingSource1;
            // 
            // albumBindingSource3
            // 
            this.albumBindingSource3.DataMember = "album";
            this.albumBindingSource3.DataSource = this.albumyDataSet;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumyDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumyDataSet)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.albumBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumyDataSetBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumBindingSource3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource albumyDataSetBindingSource;
        private albumyDataSet albumyDataSet;
        private System.Windows.Forms.BindingSource albumBindingSource;
        private albumyDataSetTableAdapters.albumTableAdapter albumTableAdapter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource albumBindingSource1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.BindingSource albumyDataSetBindingSource1;
        private System.Windows.Forms.BindingSource albumBindingSource2;
        private System.Windows.Forms.BindingSource albumBindingSource3;
    }
}

