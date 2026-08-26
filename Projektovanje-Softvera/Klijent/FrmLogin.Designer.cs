namespace Klijent
{
    partial class FrmLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtKorisnickoIme = new TextBox();
            txtSifra = new TextBox();
            label2 = new Label();
            btnPrijavi = new Button();
            btnOdustani = new Button();
            lblPoruka = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(40, 38);
            label1.Name = "label1";
            label1.Size = new Size(106, 20);
            label1.TabIndex = 0;
            label1.Text = "Korisničko ime";
            // 
            // txtKorisnickoIme
            // 
            txtKorisnickoIme.Location = new Point(170, 35);
            txtKorisnickoIme.Name = "txtKorisnickoIme";
            txtKorisnickoIme.Size = new Size(250, 27);
            txtKorisnickoIme.TabIndex = 1;
            // 
            // txtSifra
            // 
            txtSifra.Location = new Point(170, 87);
            txtSifra.Name = "txtSifra";
            txtSifra.Size = new Size(250, 27);
            txtSifra.TabIndex = 3;
            txtSifra.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(40, 90);
            label2.Name = "label2";
            label2.Size = new Size(39, 20);
            label2.TabIndex = 2;
            label2.Text = "Šifra";
            // 
            // btnPrijavi
            // 
            btnPrijavi.Location = new Point(198, 155);
            btnPrijavi.Name = "btnPrijavi";
            btnPrijavi.Size = new Size(94, 29);
            btnPrijavi.TabIndex = 4;
            btnPrijavi.Text = "Prijavi se";
            btnPrijavi.UseVisualStyleBackColor = true;
            btnPrijavi.Click += btnPrijavi_Click;
            // 
            // btnOdustani
            // 
            btnOdustani.Location = new Point(326, 155);
            btnOdustani.Name = "btnOdustani";
            btnOdustani.Size = new Size(94, 29);
            btnOdustani.TabIndex = 5;
            btnOdustani.Text = "Odustani";
            btnOdustani.UseVisualStyleBackColor = true;
            btnOdustani.Click += btnOdustani_Click;
            // 
            // lblPoruka
            // 
            lblPoruka.AutoSize = true;
            lblPoruka.ForeColor = Color.Salmon;
            lblPoruka.Location = new Point(49, 126);
            lblPoruka.Name = "lblPoruka";
            lblPoruka.Size = new Size(0, 20);
            lblPoruka.TabIndex = 6;
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(471, 206);
            Controls.Add(lblPoruka);
            Controls.Add(btnOdustani);
            Controls.Add(btnPrijavi);
            Controls.Add(txtSifra);
            Controls.Add(label2);
            Controls.Add(txtKorisnickoIme);
            Controls.Add(label1);
            Name = "FrmLogin";
            Text = "Prijava na sistem";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtKorisnickoIme;
        private TextBox txtSifra;
        private Label label2;
        private Button btnPrijavi;
        private Button btnOdustani;
        private Label lblPoruka;
    }
}
