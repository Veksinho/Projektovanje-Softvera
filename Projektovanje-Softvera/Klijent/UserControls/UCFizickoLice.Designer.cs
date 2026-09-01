namespace Klijent.UserControls
{
    partial class UCFizickoLice
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
            txtJmbg = new TextBox();
            txtBrojLicneKarte = new TextBox();
            label4 = new Label();
            txtPrezime = new TextBox();
            label3 = new Label();
            txtIme = new TextBox();
            label2 = new Label();
            label1 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtJmbg);
            groupBox1.Controls.Add(txtBrojLicneKarte);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtPrezime);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtIme);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(574, 178);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Podaci o fizičkom licu";
            // 
            // txtJmbg
            // 
            txtJmbg.Location = new Point(176, 26);
            txtJmbg.Name = "txtJmbg";
            txtJmbg.Size = new Size(373, 27);
            txtJmbg.TabIndex = 7;
            // 
            // txtBrojLicneKarte
            // 
            txtBrojLicneKarte.Location = new Point(176, 125);
            txtBrojLicneKarte.Name = "txtBrojLicneKarte";
            txtBrojLicneKarte.Size = new Size(373, 27);
            txtBrojLicneKarte.TabIndex = 13;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 128);
            label4.Name = "label4";
            label4.Size = new Size(108, 20);
            label4.TabIndex = 12;
            label4.Text = "Broj lične karte";
            // 
            // txtPrezime
            // 
            txtPrezime.Location = new Point(176, 92);
            txtPrezime.Name = "txtPrezime";
            txtPrezime.Size = new Size(373, 27);
            txtPrezime.TabIndex = 11;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 95);
            label3.Name = "label3";
            label3.Size = new Size(62, 20);
            label3.TabIndex = 10;
            label3.Text = "Prezime";
            // 
            // txtIme
            // 
            txtIme.Location = new Point(176, 59);
            txtIme.Name = "txtIme";
            txtIme.Size = new Size(373, 27);
            txtIme.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 62);
            label2.Name = "label2";
            label2.Size = new Size(34, 20);
            label2.TabIndex = 8;
            label2.Text = "Ime";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 29);
            label1.Name = "label1";
            label1.Size = new Size(46, 20);
            label1.TabIndex = 6;
            label1.Text = "JMBG";
            // 
            // UCFizickoLice
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox1);
            Name = "UCFizickoLice";
            Size = new Size(574, 178);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox txtPrezime;
        private Label label3;
        private TextBox txtIme;
        private Label label2;
        private TextBox txtJmbg;
        private Label label1;
        private TextBox txtBrojLicneKarte;
        private Label label4;

        public GroupBox GroupBox1 { get => groupBox1; set => groupBox1 = value; }
        public TextBox TxtPrezime { get => txtPrezime; set => txtPrezime = value; }
        public Label Label3 { get => label3; set => label3 = value; }
        public TextBox TxtIme { get => txtIme; set => txtIme = value; }
        public Label Label2 { get => label2; set => label2 = value; }
        public TextBox TxtJmbg { get => txtJmbg; set => txtJmbg = value; }
        public Label Label1 { get => label1; set => label1 = value; }
        public TextBox TxtBrojLicneKarte { get => txtBrojLicneKarte; set => txtBrojLicneKarte = value; }
        public Label Label4 { get => label4; set => label4 = value; }
    }
}
