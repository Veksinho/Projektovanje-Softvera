namespace Klijent.UserControls
{
    partial class UCPretragaKonsignator
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
            label1 = new Label();
            cmbTipKonsignatora = new ComboBox();
            txtTelefon = new TextBox();
            label4 = new Label();
            txtEmail = new TextBox();
            label3 = new Label();
            txtNaziv = new TextBox();
            label2 = new Label();
            btnPonisti = new Button();
            btnPretrazi = new Button();
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
            groupBox1.Controls.Add(btnPonisti);
            groupBox1.Controls.Add(btnPretrazi);
            groupBox1.Controls.Add(txtNaziv);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtTelefon);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtEmail);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(cmbTipKonsignatora);
            groupBox1.Location = new Point(13, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(815, 149);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Kriterijum pretrage";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 29);
            label1.Name = "label1";
            label1.Size = new Size(30, 20);
            label1.TabIndex = 3;
            label1.Text = "Tip";
            // 
            // cmbTipKonsignatora
            // 
            cmbTipKonsignatora.FormattingEnabled = true;
            cmbTipKonsignatora.Location = new Point(115, 29);
            cmbTipKonsignatora.Name = "cmbTipKonsignatora";
            cmbTipKonsignatora.Size = new Size(253, 28);
            cmbTipKonsignatora.TabIndex = 2;
            // 
            // txtTelefon
            // 
            txtTelefon.Location = new Point(520, 29);
            txtTelefon.Name = "txtTelefon";
            txtTelefon.Size = new Size(272, 27);
            txtTelefon.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(397, 32);
            label4.Name = "label4";
            label4.Size = new Size(58, 20);
            label4.TabIndex = 8;
            label4.Text = "Telefon";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(115, 63);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(253, 27);
            txtEmail.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 66);
            label3.Name = "label3";
            label3.Size = new Size(52, 20);
            label3.TabIndex = 6;
            label3.Text = "E-mail";
            // 
            // txtNaziv
            // 
            txtNaziv.Location = new Point(520, 62);
            txtNaziv.Name = "txtNaziv";
            txtNaziv.Size = new Size(272, 27);
            txtNaziv.TabIndex = 11;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(397, 66);
            label2.Name = "label2";
            label2.Size = new Size(46, 20);
            label2.TabIndex = 10;
            label2.Text = "Naziv";
            // 
            // btnPonisti
            // 
            btnPonisti.Location = new Point(700, 103);
            btnPonisti.Name = "btnPonisti";
            btnPonisti.Size = new Size(94, 29);
            btnPonisti.TabIndex = 13;
            btnPonisti.Text = "Poništi";
            btnPonisti.UseVisualStyleBackColor = true;
            // 
            // btnPretrazi
            // 
            btnPretrazi.Location = new Point(600, 103);
            btnPretrazi.Name = "btnPretrazi";
            btnPretrazi.Size = new Size(94, 29);
            btnPretrazi.TabIndex = 12;
            btnPretrazi.Text = "Pretraži";
            btnPretrazi.UseVisualStyleBackColor = true;
            // 
            // btnIzmeni
            // 
            btnIzmeni.Location = new Point(634, 382);
            btnIzmeni.Name = "btnIzmeni";
            btnIzmeni.Size = new Size(94, 31);
            btnIzmeni.TabIndex = 20;
            btnIzmeni.Text = "Izmeni";
            btnIzmeni.UseVisualStyleBackColor = true;
            // 
            // btnObrisi
            // 
            btnObrisi.Location = new Point(734, 382);
            btnObrisi.Name = "btnObrisi";
            btnObrisi.Size = new Size(94, 31);
            btnObrisi.TabIndex = 19;
            btnObrisi.Text = "Obriši";
            btnObrisi.UseVisualStyleBackColor = true;
            // 
            // btnPrikazi
            // 
            btnPrikazi.Location = new Point(534, 382);
            btnPrikazi.Name = "btnPrikazi";
            btnPrikazi.Size = new Size(94, 31);
            btnPrikazi.TabIndex = 18;
            btnPrikazi.Text = "Prikaži";
            btnPrikazi.UseVisualStyleBackColor = true;
            // 
            // dgvRezultati
            // 
            dgvRezultati.AllowUserToAddRows = false;
            dgvRezultati.AllowUserToDeleteRows = false;
            dgvRezultati.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRezultati.Location = new Point(13, 176);
            dgvRezultati.Name = "dgvRezultati";
            dgvRezultati.ReadOnly = true;
            dgvRezultati.RowHeadersWidth = 51;
            dgvRezultati.Size = new Size(815, 186);
            dgvRezultati.TabIndex = 17;
            // 
            // UCPretragaKonsignator
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnIzmeni);
            Controls.Add(btnObrisi);
            Controls.Add(btnPrikazi);
            Controls.Add(dgvRezultati);
            Controls.Add(groupBox1);
            Name = "UCPretragaKonsignator";
            Size = new Size(850, 436);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRezultati).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private ComboBox cmbTipKonsignatora;
        private TextBox txtTelefon;
        private Label label4;
        private TextBox txtEmail;
        private Label label3;
        private TextBox txtNaziv;
        private Label label2;
        private Button btnPonisti;
        private Button btnPretrazi;
        private Button btnIzmeni;
        private Button btnObrisi;
        private Button btnPrikazi;
        private DataGridView dgvRezultati;

        public GroupBox GroupBox1 { get => groupBox1; set => groupBox1 = value; }
        public Label Label1 { get => label1; set => label1 = value; }
        public ComboBox CmbTipKonsignatora { get => cmbTipKonsignatora; set => cmbTipKonsignatora = value; }
        public TextBox TxtTelefon { get => txtTelefon; set => txtTelefon = value; }
        public Label Label4 { get => label4; set => label4 = value; }
        public TextBox TxtEmail { get => txtEmail; set => txtEmail = value; }
        public Label Label3 { get => label3; set => label3 = value; }
        public TextBox TxtNaziv { get => txtNaziv; set => txtNaziv = value; }
        public Label Label2 { get => label2; set => label2 = value; }
        public Button BtnPonisti { get => btnPonisti; set => btnPonisti = value; }
        public Button BtnPretrazi { get => btnPretrazi; set => btnPretrazi = value; }
        public Button BtnIzmeni { get => btnIzmeni; set => btnIzmeni = value; }
        public Button BtnObrisi { get => btnObrisi; set => btnObrisi = value; }
        public Button BtnPrikazi { get => btnPrikazi; set => btnPrikazi = value; }
        public DataGridView DgvRezultati { get => dgvRezultati; set => dgvRezultati = value; }
    }
}
