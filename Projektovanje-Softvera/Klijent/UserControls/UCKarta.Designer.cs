namespace Klijent.UserControls
{
    partial class UCKarta
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
            cmbKonsignator = new ComboBox();
            cmbDogadjaj = new ComboBox();
            label2 = new Label();
            groupBox2 = new GroupBox();
            label4 = new Label();
            txtSektor = new TextBox();
            txtRed = new TextBox();
            label3 = new Label();
            txtSediste = new TextBox();
            label5 = new Label();
            groupBox3 = new GroupBox();
            cmbTip = new ComboBox();
            label6 = new Label();
            label7 = new Label();
            cmbStatus = new ComboBox();
            label8 = new Label();
            cmbFormat = new ComboBox();
            label9 = new Label();
            txtNominalnaCena = new TextBox();
            groupBox4 = new GroupBox();
            label11 = new Label();
            txtListing = new TextBox();
            btnNazad = new Button();
            btnIzmeni = new Button();
            btnKreiraj = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cmbDogadjaj);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(cmbKonsignator);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 15);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(704, 80);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Poreklo";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 35);
            label1.Name = "label1";
            label1.Size = new Size(89, 20);
            label1.TabIndex = 0;
            label1.Text = "Konsignator";
            // 
            // cmbKonsignator
            // 
            cmbKonsignator.FormattingEnabled = true;
            cmbKonsignator.Location = new Point(159, 32);
            cmbKonsignator.Name = "cmbKonsignator";
            cmbKonsignator.Size = new Size(200, 28);
            cmbKonsignator.TabIndex = 1;
            // 
            // cmbDogadjaj
            // 
            cmbDogadjaj.FormattingEnabled = true;
            cmbDogadjaj.Location = new Point(484, 32);
            cmbDogadjaj.Name = "cmbDogadjaj";
            cmbDogadjaj.Size = new Size(200, 28);
            cmbDogadjaj.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(369, 35);
            label2.Name = "label2";
            label2.Size = new Size(67, 20);
            label2.TabIndex = 2;
            label2.Text = "Događaj";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtSediste);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(txtRed);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(txtSektor);
            groupBox2.Controls.Add(label4);
            groupBox2.Location = new Point(12, 104);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(704, 80);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Mesto";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 36);
            label4.Name = "label4";
            label4.Size = new Size(51, 20);
            label4.TabIndex = 0;
            label4.Text = "Sektor";
            // 
            // txtSektor
            // 
            txtSektor.Location = new Point(99, 33);
            txtSektor.Name = "txtSektor";
            txtSektor.Size = new Size(123, 27);
            txtSektor.TabIndex = 1;
            // 
            // txtRed
            // 
            txtRed.Location = new Point(330, 33);
            txtRed.Name = "txtRed";
            txtRed.Size = new Size(123, 27);
            txtRed.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(251, 36);
            label3.Name = "label3";
            label3.Size = new Size(35, 20);
            label3.TabIndex = 2;
            label3.Text = "Red";
            // 
            // txtSediste
            // 
            txtSediste.Location = new Point(561, 33);
            txtSediste.Name = "txtSediste";
            txtSediste.Size = new Size(123, 27);
            txtSediste.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(482, 36);
            label5.Name = "label5";
            label5.Size = new Size(57, 20);
            label5.TabIndex = 4;
            label5.Text = "Sedište";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(txtNominalnaCena);
            groupBox3.Controls.Add(cmbStatus);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(cmbFormat);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(cmbTip);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(label7);
            groupBox3.Location = new Point(12, 193);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(704, 118);
            groupBox3.TabIndex = 4;
            groupBox3.TabStop = false;
            groupBox3.Text = "Svojstva karte";
            // 
            // cmbTip
            // 
            cmbTip.FormattingEnabled = true;
            cmbTip.Location = new Point(484, 32);
            cmbTip.Name = "cmbTip";
            cmbTip.Size = new Size(200, 28);
            cmbTip.TabIndex = 3;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(369, 35);
            label6.Name = "label6";
            label6.Size = new Size(30, 20);
            label6.TabIndex = 2;
            label6.Text = "Tip";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(16, 35);
            label7.Name = "label7";
            label7.Size = new Size(117, 20);
            label7.TabIndex = 0;
            label7.Text = "Nominalna cena";
            // 
            // cmbStatus
            // 
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(484, 70);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(200, 28);
            cmbStatus.TabIndex = 7;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(369, 73);
            label8.Name = "label8";
            label8.Size = new Size(49, 20);
            label8.TabIndex = 6;
            label8.Text = "Status";
            // 
            // cmbFormat
            // 
            cmbFormat.FormattingEnabled = true;
            cmbFormat.Location = new Point(159, 70);
            cmbFormat.Name = "cmbFormat";
            cmbFormat.Size = new Size(200, 28);
            cmbFormat.TabIndex = 5;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(16, 73);
            label9.Name = "label9";
            label9.Size = new Size(56, 20);
            label9.TabIndex = 4;
            label9.Text = "Format";
            // 
            // txtNominalnaCena
            // 
            txtNominalnaCena.Location = new Point(159, 32);
            txtNominalnaCena.Name = "txtNominalnaCena";
            txtNominalnaCena.Size = new Size(200, 27);
            txtNominalnaCena.TabIndex = 8;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(txtListing);
            groupBox4.Controls.Add(label11);
            groupBox4.Location = new Point(12, 320);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(704, 116);
            groupBox4.TabIndex = 4;
            groupBox4.TabStop = false;
            groupBox4.Text = "Listing";
            // 
            // label11
            // 
            label11.Location = new Point(16, 38);
            label11.Name = "label11";
            label11.Size = new Size(117, 50);
            label11.TabIndex = 0;
            label11.Text = "Trenutno na listingu";
            // 
            // txtListing
            // 
            txtListing.Enabled = false;
            txtListing.Location = new Point(159, 47);
            txtListing.Name = "txtListing";
            txtListing.Size = new Size(525, 27);
            txtListing.TabIndex = 1;
            // 
            // btnNazad
            // 
            btnNazad.Location = new Point(13, 458);
            btnNazad.Name = "btnNazad";
            btnNazad.Size = new Size(194, 29);
            btnNazad.TabIndex = 24;
            btnNazad.Text = "Nazad na pretragu";
            btnNazad.UseVisualStyleBackColor = true;
            // 
            // btnIzmeni
            // 
            btnIzmeni.Location = new Point(566, 458);
            btnIzmeni.Name = "btnIzmeni";
            btnIzmeni.Size = new Size(150, 29);
            btnIzmeni.TabIndex = 23;
            btnIzmeni.Text = "Sačuvaj izmene";
            btnIzmeni.UseVisualStyleBackColor = true;
            // 
            // btnKreiraj
            // 
            btnKreiraj.Location = new Point(509, 458);
            btnKreiraj.Name = "btnKreiraj";
            btnKreiraj.Size = new Size(207, 29);
            btnKreiraj.TabIndex = 22;
            btnKreiraj.Text = "Kreiraj kartu";
            btnKreiraj.UseVisualStyleBackColor = true;
            // 
            // UCKarta
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnNazad);
            Controls.Add(btnIzmeni);
            Controls.Add(btnKreiraj);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "UCKarta";
            Size = new Size(737, 512);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private ComboBox cmbDogadjaj;
        private Label label2;
        private ComboBox cmbKonsignator;
        private Label label1;
        private GroupBox groupBox2;
        private TextBox txtSediste;
        private Label label5;
        private TextBox txtRed;
        private Label label3;
        private TextBox txtSektor;
        private Label label4;
        private GroupBox groupBox3;
        private ComboBox cmbTip;
        private Label label6;
        private Label label7;
        private ComboBox cmbStatus;
        private Label label8;
        private ComboBox cmbFormat;
        private Label label9;
        private TextBox txtNominalnaCena;
        private GroupBox groupBox4;
        private Label label11;
        private TextBox txtListing;
        private Button btnNazad;
        private Button btnIzmeni;
        private Button btnKreiraj;

        public GroupBox GroupBox1 { get => groupBox1; set => groupBox1 = value; }
        public ComboBox CmbDogadjaj { get => cmbDogadjaj; set => cmbDogadjaj = value; }
        public Label Label2 { get => label2; set => label2 = value; }
        public ComboBox CmbKonsignator { get => cmbKonsignator; set => cmbKonsignator = value; }
        public Label Label1 { get => label1; set => label1 = value; }
        public GroupBox GroupBox2 { get => groupBox2; set => groupBox2 = value; }
        public TextBox TxtSediste { get => txtSediste; set => txtSediste = value; }
        public Label Label5 { get => label5; set => label5 = value; }
        public TextBox TxtRed { get => txtRed; set => txtRed = value; }
        public Label Label3 { get => label3; set => label3 = value; }
        public TextBox TxtSektor { get => txtSektor; set => txtSektor = value; }
        public Label Label4 { get => label4; set => label4 = value; }
        public GroupBox GroupBox3 { get => groupBox3; set => groupBox3 = value; }
        public ComboBox CmbTip { get => cmbTip; set => cmbTip = value; }
        public Label Label6 { get => label6; set => label6 = value; }
        public Label Label7 { get => label7; set => label7 = value; }
        public ComboBox CmbStatus { get => cmbStatus; set => cmbStatus = value; }
        public Label Label8 { get => label8; set => label8 = value; }
        public ComboBox CmbFormat { get => cmbFormat; set => cmbFormat = value; }
        public Label Label9 { get => label9; set => label9 = value; }
        public TextBox TxtNominalnaCena { get => txtNominalnaCena; set => txtNominalnaCena = value; }
        public GroupBox GroupBox4 { get => groupBox4; set => groupBox4 = value; }
        public Label Label11 { get => label11; set => label11 = value; }
        public TextBox TxtListing { get => txtListing; set => txtListing = value; }
        public Button BtnNazad { get => btnNazad; set => btnNazad = value; }
        public Button BtnIzmeni { get => btnIzmeni; set => btnIzmeni = value; }
        public Button BtnKreiraj { get => btnKreiraj; set => btnKreiraj = value; }
    }
}
