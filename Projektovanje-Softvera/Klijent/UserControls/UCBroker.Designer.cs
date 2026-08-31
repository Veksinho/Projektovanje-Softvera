namespace Klijent.UserControls
{
    partial class UCBroker
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtId = new TextBox();
            txtIme = new TextBox();
            txtPrezime = new TextBox();
            txtTelefon = new TextBox();
            groupBox1 = new GroupBox();
            txtSifra = new TextBox();
            txtKorisnickoIme = new TextBox();
            label7 = new Label();
            label8 = new Label();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            clbKategorije = new CheckedListBox();
            btnNazad = new Button();
            btnIzmeni = new Button();
            btnKreiraj = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(45, 50);
            label1.Name = "label1";
            label1.Size = new Size(24, 20);
            label1.TabIndex = 0;
            label1.Text = "ID";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(45, 85);
            label2.Name = "label2";
            label2.Size = new Size(34, 20);
            label2.TabIndex = 1;
            label2.Text = "Ime";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(45, 155);
            label3.Name = "label3";
            label3.Size = new Size(58, 20);
            label3.TabIndex = 3;
            label3.Text = "Telefon";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(45, 120);
            label4.Name = "label4";
            label4.Size = new Size(62, 20);
            label4.TabIndex = 2;
            label4.Text = "Prezime";
            // 
            // txtId
            // 
            txtId.Enabled = false;
            txtId.Location = new Point(149, 32);
            txtId.Name = "txtId";
            txtId.Size = new Size(100, 27);
            txtId.TabIndex = 4;
            // 
            // txtIme
            // 
            txtIme.Location = new Point(149, 65);
            txtIme.Name = "txtIme";
            txtIme.Size = new Size(194, 27);
            txtIme.TabIndex = 5;
            // 
            // txtPrezime
            // 
            txtPrezime.Location = new Point(149, 103);
            txtPrezime.Name = "txtPrezime";
            txtPrezime.Size = new Size(194, 27);
            txtPrezime.TabIndex = 6;
            // 
            // txtTelefon
            // 
            txtTelefon.Location = new Point(149, 138);
            txtTelefon.Name = "txtTelefon";
            txtTelefon.Size = new Size(194, 27);
            txtTelefon.TabIndex = 7;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtIme);
            groupBox1.Controls.Add(txtId);
            groupBox1.Controls.Add(txtTelefon);
            groupBox1.Controls.Add(txtPrezime);
            groupBox1.Location = new Point(17, 20);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(357, 176);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Osnovni podaci";
            // 
            // txtSifra
            // 
            txtSifra.Location = new Point(149, 62);
            txtSifra.Name = "txtSifra";
            txtSifra.Size = new Size(194, 27);
            txtSifra.TabIndex = 14;
            txtSifra.UseSystemPasswordChar = true;
            // 
            // txtKorisnickoIme
            // 
            txtKorisnickoIme.Location = new Point(149, 26);
            txtKorisnickoIme.Name = "txtKorisnickoIme";
            txtKorisnickoIme.Size = new Size(194, 27);
            txtKorisnickoIme.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(45, 267);
            label7.Name = "label7";
            label7.Size = new Size(39, 20);
            label7.TabIndex = 10;
            label7.Text = "Šifra";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(45, 232);
            label8.Name = "label8";
            label8.Size = new Size(106, 20);
            label8.TabIndex = 9;
            label8.Text = "Korisničko ime";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtSifra);
            groupBox2.Controls.Add(txtKorisnickoIme);
            groupBox2.Location = new Point(17, 202);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(357, 101);
            groupBox2.TabIndex = 17;
            groupBox2.TabStop = false;
            groupBox2.Text = "Kredencijali";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(clbKategorije);
            groupBox3.Location = new Point(392, 20);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(248, 283);
            groupBox3.TabIndex = 18;
            groupBox3.TabStop = false;
            groupBox3.Text = "Specijalizacije";
            // 
            // clbKategorije
            // 
            clbKategorije.FormattingEnabled = true;
            clbKategorije.Location = new Point(18, 30);
            clbKategorije.Name = "clbKategorije";
            clbKategorije.Size = new Size(210, 224);
            clbKategorije.TabIndex = 0;
            // 
            // btnNazad
            // 
            btnNazad.Location = new Point(16, 319);
            btnNazad.Name = "btnNazad";
            btnNazad.Size = new Size(194, 29);
            btnNazad.TabIndex = 21;
            btnNazad.Text = "Nazad na pretragu";
            btnNazad.UseVisualStyleBackColor = true;
            // 
            // btnIzmeni
            // 
            btnIzmeni.Location = new Point(490, 319);
            btnIzmeni.Name = "btnIzmeni";
            btnIzmeni.Size = new Size(150, 29);
            btnIzmeni.TabIndex = 20;
            btnIzmeni.Text = "Sačuvaj izmene";
            btnIzmeni.UseVisualStyleBackColor = true;
            // 
            // btnKreiraj
            // 
            btnKreiraj.Location = new Point(433, 319);
            btnKreiraj.Name = "btnKreiraj";
            btnKreiraj.Size = new Size(207, 29);
            btnKreiraj.TabIndex = 19;
            btnKreiraj.Text = "Kreiraj brokera";
            btnKreiraj.UseVisualStyleBackColor = true;
            // 
            // UCBroker
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnNazad);
            Controls.Add(btnIzmeni);
            Controls.Add(btnKreiraj);
            Controls.Add(groupBox3);
            Controls.Add(label7);
            Controls.Add(label8);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Name = "UCBroker";
            Size = new Size(660, 367);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtId;
        private TextBox txtIme;
        private TextBox txtPrezime;
        private TextBox txtTelefon;
        private GroupBox groupBox1;
        private TextBox txtSifra;
        private TextBox txtKorisnickoIme;
        private Label label7;
        private Label label8;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private CheckedListBox clbKategorije;
        private Button btnNazad;
        private Button btnIzmeni;
        private Button btnKreiraj;

        public Label Label1 { get => label1; set => label1 = value; }
        public Label Label2 { get => label2; set => label2 = value; }
        public Label Label3 { get => label3; set => label3 = value; }
        public Label Label4 { get => label4; set => label4 = value; }
        public TextBox TxtId { get => txtId; set => txtId = value; }
        public TextBox TxtIme { get => txtIme; set => txtIme = value; }
        public TextBox TxtPrezime { get => txtPrezime; set => txtPrezime = value; }
        public TextBox TxtTelefon { get => txtTelefon; set => txtTelefon = value; }
        public GroupBox GroupBox1 { get => groupBox1; set => groupBox1 = value; }
        public TextBox TxtSifra { get => txtSifra; set => txtSifra = value; }
        public TextBox TxtKorisnickoIme { get => txtKorisnickoIme; set => txtKorisnickoIme = value; }
        public Label Label7 { get => label7; set => label7 = value; }
        public Label Label8 { get => label8; set => label8 = value; }
        public GroupBox GroupBox2 { get => groupBox2; set => groupBox2 = value; }
        public GroupBox GroupBox3 { get => groupBox3; set => groupBox3 = value; }
        public CheckedListBox ClbKategorije { get => clbKategorije; set => clbKategorije = value; }
        public Button BtnNazad { get => btnNazad; set => btnNazad = value; }
        public Button BtnIzmeni { get => btnIzmeni; set => btnIzmeni = value; }
        public Button BtnKreiraj { get => btnKreiraj; set => btnKreiraj = value; }
    }
}
