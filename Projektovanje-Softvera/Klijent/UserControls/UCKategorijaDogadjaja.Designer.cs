namespace Klijent.UserControls
{
    partial class UCKategorijaDogadjaja
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
            txtOpis = new RichTextBox();
            txtNaziv = new TextBox();
            txtId = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            btnKreiraj = new Button();
            btnIzmeni = new Button();
            btnNazad = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtOpis);
            groupBox1.Controls.Add(txtNaziv);
            groupBox1.Controls.Add(txtId);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(13, 13);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(435, 224);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Kategorija događaja";
            // 
            // txtOpis
            // 
            txtOpis.Location = new Point(119, 110);
            txtOpis.Name = "txtOpis";
            txtOpis.Size = new Size(301, 95);
            txtOpis.TabIndex = 5;
            txtOpis.Text = "";
            // 
            // txtNaziv
            // 
            txtNaziv.Location = new Point(119, 74);
            txtNaziv.Name = "txtNaziv";
            txtNaziv.Size = new Size(301, 27);
            txtNaziv.TabIndex = 4;
            // 
            // txtId
            // 
            txtId.Enabled = false;
            txtId.Location = new Point(119, 41);
            txtId.Name = "txtId";
            txtId.Size = new Size(81, 27);
            txtId.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 110);
            label3.Name = "label3";
            label3.Size = new Size(39, 20);
            label3.TabIndex = 2;
            label3.Text = "Opis";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 77);
            label2.Name = "label2";
            label2.Size = new Size(46, 20);
            label2.TabIndex = 1;
            label2.Text = "Naziv";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 44);
            label1.Name = "label1";
            label1.Size = new Size(39, 20);
            label1.TabIndex = 0;
            label1.Text = "Šifra";
            // 
            // btnKreiraj
            // 
            btnKreiraj.Location = new Point(226, 243);
            btnKreiraj.Name = "btnKreiraj";
            btnKreiraj.Size = new Size(207, 29);
            btnKreiraj.TabIndex = 6;
            btnKreiraj.Text = "Kreiraj kategoriju događaja";
            btnKreiraj.UseVisualStyleBackColor = true;
            // 
            // btnIzmeni
            // 
            btnIzmeni.Location = new Point(283, 243);
            btnIzmeni.Name = "btnIzmeni";
            btnIzmeni.Size = new Size(150, 29);
            btnIzmeni.TabIndex = 7;
            btnIzmeni.Text = "Sačuvaj izmene";
            btnIzmeni.UseVisualStyleBackColor = true;
            // 
            // btnNazad
            // 
            btnNazad.Location = new Point(13, 243);
            btnNazad.Name = "btnNazad";
            btnNazad.Size = new Size(194, 29);
            btnNazad.TabIndex = 8;
            btnNazad.Text = "Nazad na pretragu";
            btnNazad.UseVisualStyleBackColor = true;
            // 
            // UCKategorijaDogadjaja
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnNazad);
            Controls.Add(btnIzmeni);
            Controls.Add(btnKreiraj);
            Controls.Add(groupBox1);
            Name = "UCKategorijaDogadjaja";
            Size = new Size(465, 282);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private RichTextBox txtOpis;
        private TextBox txtNaziv;
        private TextBox txtId;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button btnKreiraj;
        private Button btnIzmeni;
        private Button btnNazad;

        public GroupBox GroupBox1 { get => groupBox1; set => groupBox1 = value; }
        public RichTextBox TxtOpis { get => txtOpis; set => txtOpis = value; }
        public TextBox TxtNaziv { get => txtNaziv; set => txtNaziv = value; }
        public TextBox TxtId { get => txtId; set => txtId = value; }
        public Label Label3 { get => label3; set => label3 = value; }
        public Label Label2 { get => label2; set => label2 = value; }
        public Label Label1 { get => label1; set => label1 = value; }
        public Button BtnKreiraj { get => btnKreiraj; set => btnKreiraj = value; }
        public Button BtnIzmeni { get => btnIzmeni; set => btnIzmeni = value; }
        public Button BtnNazad { get => btnNazad; set => btnNazad = value; }
    }
}
