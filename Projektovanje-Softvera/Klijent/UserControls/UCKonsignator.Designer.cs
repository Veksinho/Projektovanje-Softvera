namespace Klijent.UserControls
{
    partial class UCKonsignator
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
            groupBox2 = new GroupBox();
            label6 = new Label();
            dtpDatumRegistracije = new DateTimePicker();
            txtAdresa = new TextBox();
            label5 = new Label();
            txtTelefon = new TextBox();
            label4 = new Label();
            txtEmail = new TextBox();
            label3 = new Label();
            txtId = new TextBox();
            label2 = new Label();
            btnNazad = new Button();
            btnIzmeni = new Button();
            btnKreiraj = new Button();
            pnlPodtip = new Panel();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(cmbTipKonsignatora);
            groupBox1.Location = new Point(13, 13);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(561, 73);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tip konsignatora";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 29);
            label1.Name = "label1";
            label1.Size = new Size(30, 20);
            label1.TabIndex = 1;
            label1.Text = "Tip";
            // 
            // cmbTipKonsignatora
            // 
            cmbTipKonsignatora.FormattingEnabled = true;
            cmbTipKonsignatora.Location = new Point(176, 26);
            cmbTipKonsignatora.Name = "cmbTipKonsignatora";
            cmbTipKonsignatora.Size = new Size(253, 28);
            cmbTipKonsignatora.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(dtpDatumRegistracije);
            groupBox2.Controls.Add(txtAdresa);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(txtTelefon);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(txtEmail);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(txtId);
            groupBox2.Controls.Add(label2);
            groupBox2.Location = new Point(13, 92);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(561, 207);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Zajednički podaci";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(15, 167);
            label6.Name = "label6";
            label6.Size = new Size(131, 20);
            label6.TabIndex = 9;
            label6.Text = "Datum registracije";
            // 
            // dtpDatumRegistracije
            // 
            dtpDatumRegistracije.Location = new Point(176, 162);
            dtpDatumRegistracije.Name = "dtpDatumRegistracije";
            dtpDatumRegistracije.Size = new Size(263, 27);
            dtpDatumRegistracije.TabIndex = 8;
            // 
            // txtAdresa
            // 
            txtAdresa.Location = new Point(176, 129);
            txtAdresa.Name = "txtAdresa";
            txtAdresa.Size = new Size(373, 27);
            txtAdresa.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 132);
            label5.Name = "label5";
            label5.Size = new Size(55, 20);
            label5.TabIndex = 6;
            label5.Text = "Adresa";
            // 
            // txtTelefon
            // 
            txtTelefon.Location = new Point(176, 96);
            txtTelefon.Name = "txtTelefon";
            txtTelefon.Size = new Size(373, 27);
            txtTelefon.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 99);
            label4.Name = "label4";
            label4.Size = new Size(58, 20);
            label4.TabIndex = 4;
            label4.Text = "Telefon";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(176, 63);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(373, 27);
            txtEmail.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 66);
            label3.Name = "label3";
            label3.Size = new Size(52, 20);
            label3.TabIndex = 2;
            label3.Text = "E-mail";
            // 
            // txtId
            // 
            txtId.Enabled = false;
            txtId.Location = new Point(176, 30);
            txtId.Name = "txtId";
            txtId.Size = new Size(87, 27);
            txtId.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 33);
            label2.Name = "label2";
            label2.Size = new Size(24, 20);
            label2.TabIndex = 0;
            label2.Text = "ID";
            // 
            // btnNazad
            // 
            btnNazad.Location = new Point(13, 508);
            btnNazad.Name = "btnNazad";
            btnNazad.Size = new Size(194, 29);
            btnNazad.TabIndex = 22;
            btnNazad.Text = "Nazad na pretragu";
            btnNazad.UseVisualStyleBackColor = true;
            // 
            // btnIzmeni
            // 
            btnIzmeni.Location = new Point(424, 508);
            btnIzmeni.Name = "btnIzmeni";
            btnIzmeni.Size = new Size(150, 29);
            btnIzmeni.TabIndex = 24;
            btnIzmeni.Text = "Sačuvaj izmene";
            btnIzmeni.UseVisualStyleBackColor = true;
            // 
            // btnKreiraj
            // 
            btnKreiraj.Location = new Point(367, 508);
            btnKreiraj.Name = "btnKreiraj";
            btnKreiraj.Size = new Size(207, 29);
            btnKreiraj.TabIndex = 23;
            btnKreiraj.Text = "Kreiraj konsignatora";
            btnKreiraj.UseVisualStyleBackColor = true;
            // 
            // pnlPodtip
            // 
            pnlPodtip.Location = new Point(3, 305);
            pnlPodtip.Name = "pnlPodtip";
            pnlPodtip.Size = new Size(582, 197);
            pnlPodtip.TabIndex = 25;
            // 
            // UCKonsignator
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPodtip);
            Controls.Add(btnNazad);
            Controls.Add(btnIzmeni);
            Controls.Add(btnKreiraj);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "UCKonsignator";
            Size = new Size(588, 557);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private ComboBox cmbTipKonsignatora;
        private GroupBox groupBox2;
        private TextBox txtAdresa;
        private Label label5;
        private TextBox txtTelefon;
        private Label label4;
        private TextBox txtEmail;
        private Label label3;
        private TextBox txtId;
        private Label label2;
        private DateTimePicker dtpDatumRegistracije;
        private Label label6;
        private Button btnNazad;
        private Button btnIzmeni;
        private Button btnKreiraj;
        private Panel pnlPodtip;

        public GroupBox GroupBox1 { get => groupBox1; set => groupBox1 = value; }
        public Label Label1 { get => label1; set => label1 = value; }
        public ComboBox CmbTipKonsignatora { get => cmbTipKonsignatora; set => cmbTipKonsignatora = value; }
        public GroupBox GroupBox2 { get => groupBox2; set => groupBox2 = value; }
        public TextBox TxtAdresa { get => txtAdresa; set => txtAdresa = value; }
        public Label Label5 { get => label5; set => label5 = value; }
        public TextBox TxtTelefon { get => txtTelefon; set => txtTelefon = value; }
        public Label Label4 { get => label4; set => label4 = value; }
        public TextBox TxtEmail { get => txtEmail; set => txtEmail = value; }
        public Label Label3 { get => label3; set => label3 = value; }
        public TextBox TxtId { get => txtId; set => txtId = value; }
        public Label Label2 { get => label2; set => label2 = value; }
        public DateTimePicker DtpDatumRegistracije { get => dtpDatumRegistracije; set => dtpDatumRegistracije = value; }
        public Label Label6 { get => label6; set => label6 = value; }
        public Button BtnNazad { get => btnNazad; set => btnNazad = value; }
        public Button BtnIzmeni { get => btnIzmeni; set => btnIzmeni = value; }
        public Button BtnKreiraj { get => btnKreiraj; set => btnKreiraj = value; }
        public Panel PnlPodtip { get => pnlPodtip; set => pnlPodtip = value; }
    }
}
