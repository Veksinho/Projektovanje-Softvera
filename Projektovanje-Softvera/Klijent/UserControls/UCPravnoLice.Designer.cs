namespace Klijent.UserControls
{
    partial class UCPravnoLice
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
            txtMaticniBroj = new TextBox();
            label3 = new Label();
            txtPib = new TextBox();
            label2 = new Label();
            txtNaziv = new TextBox();
            label1 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtMaticniBroj);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtPib);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtNaziv);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(574, 141);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Podaci o pravnom licu";
            // 
            // txtMaticniBroj
            // 
            txtMaticniBroj.Location = new Point(176, 92);
            txtMaticniBroj.Name = "txtMaticniBroj";
            txtMaticniBroj.Size = new Size(373, 27);
            txtMaticniBroj.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 95);
            label3.Name = "label3";
            label3.Size = new Size(89, 20);
            label3.TabIndex = 4;
            label3.Text = "Matični broj";
            // 
            // txtPib
            // 
            txtPib.Location = new Point(176, 59);
            txtPib.Name = "txtPib";
            txtPib.Size = new Size(373, 27);
            txtPib.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 62);
            label2.Name = "label2";
            label2.Size = new Size(30, 20);
            label2.TabIndex = 2;
            label2.Text = "PIB";
            // 
            // txtNaziv
            // 
            txtNaziv.Location = new Point(176, 26);
            txtNaziv.Name = "txtNaziv";
            txtNaziv.Size = new Size(373, 27);
            txtNaziv.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 29);
            label1.Name = "label1";
            label1.Size = new Size(46, 20);
            label1.TabIndex = 0;
            label1.Text = "Naziv";
            // 
            // UCPravnoLice
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox1);
            Name = "UCPravnoLice";
            Size = new Size(574, 141);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox txtNaziv;
        private Label label1;
        private TextBox txtMaticniBroj;
        private Label label3;
        private TextBox txtPib;
        private Label label2;

        public GroupBox GroupBox1 { get => groupBox1; set => groupBox1 = value; }
        public TextBox TxtNaziv { get => txtNaziv; set => txtNaziv = value; }
        public Label Label1 { get => label1; set => label1 = value; }
        public TextBox TxtMaticniBroj { get => txtMaticniBroj; set => txtMaticniBroj = value; }
        public Label Label3 { get => label3; set => label3 = value; }
        public TextBox TxtPib { get => txtPib; set => txtPib = value; }
        public Label Label2 { get => label2; set => label2 = value; }
    }
}
