namespace Klijent.UserControls
{
    partial class UCPretragaBroker
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
            btnIzmeni = new Button();
            btnObrisi = new Button();
            btnPrikazi = new Button();
            dgvRezultati = new DataGridView();
            groupBox1 = new GroupBox();
            label2 = new Label();
            txtIme = new TextBox();
            label4 = new Label();
            txtPrezime = new TextBox();
            label8 = new Label();
            txtKorisnickoIme = new TextBox();
            groupBox2 = new GroupBox();
            cmbKategorija = new ComboBox();
            label1 = new Label();
            btnPonisti = new Button();
            btnPretrazi = new Button();
            txtTelefon = new TextBox();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvRezultati).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // btnIzmeni
            // 
            btnIzmeni.Location = new Point(635, 436);
            btnIzmeni.Name = "btnIzmeni";
            btnIzmeni.Size = new Size(94, 31);
            btnIzmeni.TabIndex = 16;
            btnIzmeni.Text = "Izmeni";
            btnIzmeni.UseVisualStyleBackColor = true;
            // 
            // btnObrisi
            // 
            btnObrisi.Location = new Point(735, 436);
            btnObrisi.Name = "btnObrisi";
            btnObrisi.Size = new Size(94, 31);
            btnObrisi.TabIndex = 15;
            btnObrisi.Text = "Obriši";
            btnObrisi.UseVisualStyleBackColor = true;
            // 
            // btnPrikazi
            // 
            btnPrikazi.Location = new Point(535, 436);
            btnPrikazi.Name = "btnPrikazi";
            btnPrikazi.Size = new Size(94, 31);
            btnPrikazi.TabIndex = 14;
            btnPrikazi.Text = "Prikaži";
            btnPrikazi.UseVisualStyleBackColor = true;
            // 
            // dgvRezultati
            // 
            dgvRezultati.AllowUserToAddRows = false;
            dgvRezultati.AllowUserToDeleteRows = false;
            dgvRezultati.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRezultati.Location = new Point(14, 230);
            dgvRezultati.Name = "dgvRezultati";
            dgvRezultati.ReadOnly = true;
            dgvRezultati.RowHeadersWidth = 51;
            dgvRezultati.Size = new Size(815, 186);
            dgvRezultati.TabIndex = 13;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtTelefon);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtIme);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtPrezime);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(txtKorisnickoIme);
            groupBox1.Location = new Point(14, 27);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(399, 184);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            groupBox1.Text = "Po brokeru";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(9, 40);
            label2.Name = "label2";
            label2.Size = new Size(34, 20);
            label2.TabIndex = 14;
            label2.Text = "Ime";
            // 
            // txtIme
            // 
            txtIme.Location = new Point(128, 37);
            txtIme.Name = "txtIme";
            txtIme.Size = new Size(250, 27);
            txtIme.TabIndex = 16;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(9, 73);
            label4.Name = "label4";
            label4.Size = new Size(62, 20);
            label4.TabIndex = 15;
            label4.Text = "Prezime";
            // 
            // txtPrezime
            // 
            txtPrezime.Location = new Point(128, 70);
            txtPrezime.Name = "txtPrezime";
            txtPrezime.Size = new Size(250, 27);
            txtPrezime.TabIndex = 17;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(9, 106);
            label8.Name = "label8";
            label8.Size = new Size(106, 20);
            label8.TabIndex = 18;
            label8.Text = "Korisničko ime";
            // 
            // txtKorisnickoIme
            // 
            txtKorisnickoIme.Location = new Point(128, 103);
            txtKorisnickoIme.Name = "txtKorisnickoIme";
            txtKorisnickoIme.Size = new Size(250, 27);
            txtKorisnickoIme.TabIndex = 19;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(cmbKategorija);
            groupBox2.Controls.Add(label1);
            groupBox2.Location = new Point(433, 27);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(396, 106);
            groupBox2.TabIndex = 18;
            groupBox2.TabStop = false;
            groupBox2.Text = "Po kategoriji događaja";
            // 
            // cmbKategorija
            // 
            cmbKategorija.FormattingEnabled = true;
            cmbKategorija.Location = new Point(11, 62);
            cmbKategorija.Name = "cmbKategorija";
            cmbKategorija.Size = new Size(285, 28);
            cmbKategorija.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 32);
            label1.Name = "label1";
            label1.Size = new Size(146, 20);
            label1.TabIndex = 0;
            label1.Text = "Kategorija događaja";
            // 
            // btnPonisti
            // 
            btnPonisti.Location = new Point(735, 166);
            btnPonisti.Name = "btnPonisti";
            btnPonisti.Size = new Size(94, 29);
            btnPonisti.TabIndex = 5;
            btnPonisti.Text = "Poništi";
            btnPonisti.UseVisualStyleBackColor = true;
            // 
            // btnPretrazi
            // 
            btnPretrazi.Location = new Point(635, 166);
            btnPretrazi.Name = "btnPretrazi";
            btnPretrazi.Size = new Size(94, 29);
            btnPretrazi.TabIndex = 4;
            btnPretrazi.Text = "Pretraži";
            btnPretrazi.UseVisualStyleBackColor = true;
            // 
            // txtTelefon
            // 
            txtTelefon.Location = new Point(128, 136);
            txtTelefon.Name = "txtTelefon";
            txtTelefon.Size = new Size(250, 27);
            txtTelefon.TabIndex = 21;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 139);
            label3.Name = "label3";
            label3.Size = new Size(58, 20);
            label3.TabIndex = 20;
            label3.Text = "Telefon";
            // 
            // UCPretragaBroker
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnPonisti);
            Controls.Add(btnPretrazi);
            Controls.Add(groupBox2);
            Controls.Add(btnIzmeni);
            Controls.Add(btnObrisi);
            Controls.Add(btnPrikazi);
            Controls.Add(dgvRezultati);
            Controls.Add(groupBox1);
            Name = "UCPretragaBroker";
            Size = new Size(851, 493);
            ((System.ComponentModel.ISupportInitialize)dgvRezultati).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnIzmeni;
        private Button btnObrisi;
        private Button btnPrikazi;
        private DataGridView dgvRezultati;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label2;
        private TextBox txtIme;
        private Label label4;
        private TextBox txtPrezime;
        private Label label8;
        private TextBox txtKorisnickoIme;
        private ComboBox cmbKategorija;
        private Label label1;
        private Button btnPonisti;
        private Button btnPretrazi;
        private TextBox txtTelefon;
        private Label label3;

        public Button BtnIzmeni { get => btnIzmeni; set => btnIzmeni = value; }
        public Button BtnObrisi { get => btnObrisi; set => btnObrisi = value; }
        public Button BtnPrikazi { get => btnPrikazi; set => btnPrikazi = value; }
        public DataGridView DgvRezultati { get => dgvRezultati; set => dgvRezultati = value; }
        public GroupBox GroupBox1 { get => groupBox1; set => groupBox1 = value; }
        public GroupBox GroupBox2 { get => groupBox2; set => groupBox2 = value; }
        public Label Label2 { get => label2; set => label2 = value; }
        public TextBox TxtIme { get => txtIme; set => txtIme = value; }
        public Label Label4 { get => label4; set => label4 = value; }
        public TextBox TxtPrezime { get => txtPrezime; set => txtPrezime = value; }
        public Label Label8 { get => label8; set => label8 = value; }
        public TextBox TxtKorisnickoIme { get => txtKorisnickoIme; set => txtKorisnickoIme = value; }
        public ComboBox CmbKategorija { get => cmbKategorija; set => cmbKategorija = value; }
        public Label Label1 { get => label1; set => label1 = value; }
        public Button BtnPonisti { get => btnPonisti; set => btnPonisti = value; }
        public Button BtnPretrazi { get => btnPretrazi; set => btnPretrazi = value; }
        public TextBox TxtTelefon { get => txtTelefon; set => txtTelefon = value; }
    }
}
