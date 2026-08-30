namespace Klijent.UserControls
{
    partial class UCPretragaDogadjaj
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
            txtMesto = new TextBox();
            label4 = new Label();
            btnPonisti = new Button();
            dtpDatumOdrzavanja = new DateTimePicker();
            label3 = new Label();
            btnPretrazi = new Button();
            txtNaziv = new TextBox();
            label1 = new Label();
            btnIzmeni = new Button();
            btnObrisi = new Button();
            btnPrikazi = new Button();
            dgvRezultati = new DataGridView();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRezultati).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtMesto);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(btnPonisti);
            groupBox1.Controls.Add(dtpDatumOdrzavanja);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(btnPretrazi);
            groupBox1.Controls.Add(txtNaziv);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 11);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(722, 166);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Kriterijum pretrage";
            // 
            // txtMesto
            // 
            txtMesto.Location = new Point(454, 32);
            txtMesto.Name = "txtMesto";
            txtMesto.Size = new Size(253, 27);
            txtMesto.TabIndex = 17;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(398, 35);
            label4.Name = "label4";
            label4.Size = new Size(50, 20);
            label4.TabIndex = 16;
            label4.Text = "Mesto";
            // 
            // btnPonisti
            // 
            btnPonisti.Location = new Point(613, 120);
            btnPonisti.Name = "btnPonisti";
            btnPonisti.Size = new Size(94, 29);
            btnPonisti.TabIndex = 3;
            btnPonisti.Text = "Poništi";
            btnPonisti.UseVisualStyleBackColor = true;
            // 
            // dtpDatumOdrzavanja
            // 
            dtpDatumOdrzavanja.Checked = false;
            dtpDatumOdrzavanja.Location = new Point(161, 77);
            dtpDatumOdrzavanja.Name = "dtpDatumOdrzavanja";
            dtpDatumOdrzavanja.ShowCheckBox = true;
            dtpDatumOdrzavanja.Size = new Size(287, 27);
            dtpDatumOdrzavanja.TabIndex = 14;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 77);
            label3.Name = "label3";
            label3.Size = new Size(131, 20);
            label3.TabIndex = 13;
            label3.Text = "Datum održavanja";
            // 
            // btnPretrazi
            // 
            btnPretrazi.Location = new Point(513, 120);
            btnPretrazi.Name = "btnPretrazi";
            btnPretrazi.Size = new Size(94, 29);
            btnPretrazi.TabIndex = 2;
            btnPretrazi.Text = "Pretraži";
            btnPretrazi.UseVisualStyleBackColor = true;
            // 
            // txtNaziv
            // 
            txtNaziv.Location = new Point(161, 32);
            txtNaziv.Name = "txtNaziv";
            txtNaziv.Size = new Size(223, 27);
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
            // btnIzmeni
            // 
            btnIzmeni.Location = new Point(525, 343);
            btnIzmeni.Name = "btnIzmeni";
            btnIzmeni.Size = new Size(94, 31);
            btnIzmeni.TabIndex = 12;
            btnIzmeni.Text = "Izmeni";
            btnIzmeni.UseVisualStyleBackColor = true;
            // 
            // btnObrisi
            // 
            btnObrisi.Location = new Point(625, 343);
            btnObrisi.Name = "btnObrisi";
            btnObrisi.Size = new Size(94, 31);
            btnObrisi.TabIndex = 11;
            btnObrisi.Text = "Obriši";
            btnObrisi.UseVisualStyleBackColor = true;
            // 
            // btnPrikazi
            // 
            btnPrikazi.Location = new Point(425, 343);
            btnPrikazi.Name = "btnPrikazi";
            btnPrikazi.Size = new Size(94, 31);
            btnPrikazi.TabIndex = 10;
            btnPrikazi.Text = "Prikaži";
            btnPrikazi.UseVisualStyleBackColor = true;
            // 
            // dgvRezultati
            // 
            dgvRezultati.AllowUserToAddRows = false;
            dgvRezultati.AllowUserToDeleteRows = false;
            dgvRezultati.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRezultati.Location = new Point(12, 183);
            dgvRezultati.Name = "dgvRezultati";
            dgvRezultati.ReadOnly = true;
            dgvRezultati.RowHeadersWidth = 51;
            dgvRezultati.Size = new Size(722, 136);
            dgvRezultati.TabIndex = 9;
            // 
            // UCPretragaDogadjaj
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox1);
            Controls.Add(btnIzmeni);
            Controls.Add(btnObrisi);
            Controls.Add(btnPrikazi);
            Controls.Add(dgvRezultati);
            Name = "UCPretragaDogadjaj";
            Size = new Size(747, 402);
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
        private Button btnIzmeni;
        private Button btnObrisi;
        private Button btnPrikazi;
        private DataGridView dgvRezultati;
        private DateTimePicker dtpDatumOdrzavanja;
        private Label label3;
        private TextBox txtMesto;
        private Label label4;

        public GroupBox GroupBox1 { get => groupBox1; set => groupBox1 = value; }
        public Button BtnPonisti { get => btnPonisti; set => btnPonisti = value; }
        public Button BtnPretrazi { get => btnPretrazi; set => btnPretrazi = value; }
        public TextBox TxtNaziv { get => txtNaziv; set => txtNaziv = value; }
        public Label Label1 { get => label1; set => label1 = value; }
        public Button BtnIzmeni { get => btnIzmeni; set => btnIzmeni = value; }
        public Button BtnObrisi { get => btnObrisi; set => btnObrisi = value; }
        public Button BtnPrikazi { get => btnPrikazi; set => btnPrikazi = value; }
        public DataGridView DgvRezultati { get => dgvRezultati; set => dgvRezultati = value; }
        public DateTimePicker DtpDatumOdrzavanja { get => dtpDatumOdrzavanja; set => dtpDatumOdrzavanja = value; }
        public Label Label3 { get => label3; set => label3 = value; }
        public TextBox TxtMesto { get => txtMesto; set => txtMesto = value; }
        public Label Label4 { get => label4; set => label4 = value; }
    }
}
