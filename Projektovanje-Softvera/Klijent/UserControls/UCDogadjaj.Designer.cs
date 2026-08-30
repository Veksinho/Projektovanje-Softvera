namespace Klijent.UserControls
{
    partial class UCDogadjaj
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
            dtpDatumOdrzavanja = new DateTimePicker();
            txtMesto = new TextBox();
            txtNaziv = new TextBox();
            txtId = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            btnNazad = new Button();
            btnIzmeni = new Button();
            btnKreiraj = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dtpDatumOdrzavanja);
            groupBox1.Controls.Add(txtMesto);
            groupBox1.Controls.Add(txtNaziv);
            groupBox1.Controls.Add(txtId);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(13, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(506, 198);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Događaj";
            // 
            // dtpDatumOdrzavanja
            // 
            dtpDatumOdrzavanja.Location = new Point(170, 115);
            dtpDatumOdrzavanja.Name = "dtpDatumOdrzavanja";
            dtpDatumOdrzavanja.Size = new Size(265, 27);
            dtpDatumOdrzavanja.TabIndex = 7;
            // 
            // txtMesto
            // 
            txtMesto.Location = new Point(170, 152);
            txtMesto.Name = "txtMesto";
            txtMesto.Size = new Size(319, 27);
            txtMesto.TabIndex = 6;
            // 
            // txtNaziv
            // 
            txtNaziv.Location = new Point(170, 77);
            txtNaziv.Name = "txtNaziv";
            txtNaziv.Size = new Size(319, 27);
            txtNaziv.TabIndex = 5;
            // 
            // txtId
            // 
            txtId.Enabled = false;
            txtId.Location = new Point(170, 41);
            txtId.Name = "txtId";
            txtId.Size = new Size(120, 27);
            txtId.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 153);
            label4.Name = "label4";
            label4.Size = new Size(50, 20);
            label4.TabIndex = 3;
            label4.Text = "Mesto";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 115);
            label3.Name = "label3";
            label3.Size = new Size(131, 20);
            label3.TabIndex = 2;
            label3.Text = "Datum održavanja";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 78);
            label2.Name = "label2";
            label2.Size = new Size(46, 20);
            label2.TabIndex = 1;
            label2.Text = "Naziv";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 42);
            label1.Name = "label1";
            label1.Size = new Size(24, 20);
            label1.TabIndex = 0;
            label1.Text = "ID";
            // 
            // btnNazad
            // 
            btnNazad.Location = new Point(13, 225);
            btnNazad.Name = "btnNazad";
            btnNazad.Size = new Size(194, 29);
            btnNazad.TabIndex = 11;
            btnNazad.Text = "Nazad na pretragu";
            btnNazad.UseVisualStyleBackColor = true;
            // 
            // btnIzmeni
            // 
            btnIzmeni.Location = new Point(369, 225);
            btnIzmeni.Name = "btnIzmeni";
            btnIzmeni.Size = new Size(150, 29);
            btnIzmeni.TabIndex = 10;
            btnIzmeni.Text = "Sačuvaj izmene";
            btnIzmeni.UseVisualStyleBackColor = true;
            // 
            // btnKreiraj
            // 
            btnKreiraj.Location = new Point(312, 225);
            btnKreiraj.Name = "btnKreiraj";
            btnKreiraj.Size = new Size(207, 29);
            btnKreiraj.TabIndex = 9;
            btnKreiraj.Text = "Kreiraj događaj";
            btnKreiraj.UseVisualStyleBackColor = true;
            // 
            // UCDogadjaj
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnNazad);
            Controls.Add(btnIzmeni);
            Controls.Add(btnKreiraj);
            Controls.Add(groupBox1);
            Name = "UCDogadjaj";
            Size = new Size(533, 271);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private DateTimePicker dtpDatumOdrzavanja;
        private TextBox txtMesto;
        private TextBox txtNaziv;
        private TextBox txtId;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button btnNazad;
        private Button btnIzmeni;
        private Button btnKreiraj;

        public GroupBox GroupBox1 { get => groupBox1; set => groupBox1 = value; }
        public DateTimePicker DtpDatumOdrzavanja { get => dtpDatumOdrzavanja; set => dtpDatumOdrzavanja = value; }
        public TextBox TxtMesto { get => txtMesto; set => txtMesto = value; }
        public TextBox TxtNaziv { get => txtNaziv; set => txtNaziv = value; }
        public TextBox TxtId { get => txtId; set => txtId = value; }
        public Label Label4 { get => label4; set => label4 = value; }
        public Label Label3 { get => label3; set => label3 = value; }
        public Label Label2 { get => label2; set => label2 = value; }
        public Label Label1 { get => label1; set => label1 = value; }
        public Button BtnNazad { get => btnNazad; set => btnNazad = value; }
        public Button BtnIzmeni { get => btnIzmeni; set => btnIzmeni = value; }
        public Button BtnKreiraj { get => btnKreiraj; set => btnKreiraj = value; }
    }
}
