namespace Klijent.UserControls
{
    partial class UCPretragaKategorijaDogadjaja
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            btnPonisti = new Button();
            btnPretrazi = new Button();
            txtNaziv = new TextBox();
            label1 = new Label();
            dgvRezultati = new DataGridView();
            btnPrikazi = new Button();
            btnObrisi = new Button();
            btnIzmeni = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRezultati).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnPonisti);
            groupBox1.Controls.Add(btnPretrazi);
            groupBox1.Controls.Add(txtNaziv);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(13, 18);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(722, 78);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Kriterijum pretrage";
            // 
            // btnPonisti
            // 
            btnPonisti.Location = new Point(613, 35);
            btnPonisti.Name = "btnPonisti";
            btnPonisti.Size = new Size(94, 29);
            btnPonisti.TabIndex = 3;
            btnPonisti.Text = "Poništi";
            btnPonisti.UseVisualStyleBackColor = true;
            // 
            // btnPretrazi
            // 
            btnPretrazi.Location = new Point(513, 35);
            btnPretrazi.Name = "btnPretrazi";
            btnPretrazi.Size = new Size(94, 29);
            btnPretrazi.TabIndex = 2;
            btnPretrazi.Text = "Pretraži";
            btnPretrazi.UseVisualStyleBackColor = true;
            // 
            // txtNaziv
            // 
            txtNaziv.Location = new Point(103, 32);
            txtNaziv.Name = "txtNaziv";
            txtNaziv.Size = new Size(218, 27);
            txtNaziv.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 35);
            label1.Name = "label1";
            label1.Size = new Size(46, 20);
            label1.TabIndex = 0;
            label1.Text = "Naziv";
            // 
            // dgvRezultati
            // 
            dgvRezultati.AllowUserToAddRows = false;
            dgvRezultati.AllowUserToDeleteRows = false;
            dgvRezultati.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRezultati.Location = new Point(13, 102);
            dgvRezultati.Name = "dgvRezultati";
            dgvRezultati.ReadOnly = true;
            dgvRezultati.RowHeadersWidth = 51;
            dgvRezultati.Size = new Size(722, 134);
            dgvRezultati.TabIndex = 1;
            // 
            // btnPrikazi
            // 
            btnPrikazi.Location = new Point(426, 263);
            btnPrikazi.Name = "btnPrikazi";
            btnPrikazi.Size = new Size(94, 29);
            btnPrikazi.TabIndex = 4;
            btnPrikazi.Text = "Prikaži";
            btnPrikazi.UseVisualStyleBackColor = true;
            // 
            // btnObrisi
            // 
            btnObrisi.Location = new Point(626, 263);
            btnObrisi.Name = "btnObrisi";
            btnObrisi.Size = new Size(94, 29);
            btnObrisi.TabIndex = 6;
            btnObrisi.Text = "Obriši";
            btnObrisi.UseVisualStyleBackColor = true;
            // 
            // btnIzmeni
            // 
            btnIzmeni.Location = new Point(526, 263);
            btnIzmeni.Name = "btnIzmeni";
            btnIzmeni.Size = new Size(94, 29);
            btnIzmeni.TabIndex = 7;
            btnIzmeni.Text = "Izmeni";
            btnIzmeni.UseVisualStyleBackColor = true;
            // 
            // UCPretragaKategorijaDogadjaja
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnIzmeni);
            Controls.Add(btnObrisi);
            Controls.Add(btnPrikazi);
            Controls.Add(dgvRezultati);
            Controls.Add(groupBox1);
            Name = "UCPretragaKategorijaDogadjaja";
            Size = new Size(748, 322);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRezultati).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnPonisti;
        private Button btnPretrazi;
        private TextBox txtNaziv;
        private Label label1;
        private DataGridView dgvRezultati;
        private Button btnPrikazi;
        private Button btnObrisi;
        private Button btnIzmeni;

        public GroupBox GroupBox1 { get => groupBox1; set => groupBox1 = value; }
        public Button BtnPonisti { get => btnPonisti; set => btnPonisti = value; }
        public Button BtnPretrazi { get => btnPretrazi; set => btnPretrazi = value; }
        public TextBox TxtNaziv { get => txtNaziv; set => txtNaziv = value; }
        public Label Label1 { get => label1; set => label1 = value; }
        public DataGridView DgvRezultati { get => dgvRezultati; set => dgvRezultati = value; }
        public Button BtnPrikazi { get => btnPrikazi; set => btnPrikazi = value; }
        public Button BtnObrisi { get => btnObrisi; set => btnObrisi = value; }
        public Button BtnIzmeni { get => btnIzmeni; set => btnIzmeni = value; }
    }
}
