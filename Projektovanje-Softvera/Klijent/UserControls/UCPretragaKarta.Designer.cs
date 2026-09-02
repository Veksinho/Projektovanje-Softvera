namespace Klijent.UserControls
{
    partial class UCPretragaKarta
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
            cmbStatus = new ComboBox();
            label8 = new Label();
            cmbTip = new ComboBox();
            label6 = new Label();
            txtSektor = new TextBox();
            label4 = new Label();
            groupBox2 = new GroupBox();
            txtNazivKonsignatora = new TextBox();
            label1 = new Label();
            cmbKonsignator = new ComboBox();
            label2 = new Label();
            groupBox3 = new GroupBox();
            cmbDogadjaj = new ComboBox();
            label7 = new Label();
            txtMesto = new TextBox();
            label9 = new Label();
            btnPonisti = new Button();
            btnPretrazi = new Button();
            btnIzmeni = new Button();
            btnPrikazi = new Button();
            dgvRezultati = new DataGridView();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRezultati).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cmbStatus);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(cmbTip);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(txtSektor);
            groupBox1.Controls.Add(label4);
            groupBox1.Location = new Point(12, 17);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(317, 154);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Po karti";
            // 
            // cmbStatus
            // 
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(97, 104);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(204, 28);
            cmbStatus.TabIndex = 11;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(14, 112);
            label8.Name = "label8";
            label8.Size = new Size(49, 20);
            label8.TabIndex = 10;
            label8.Text = "Status";
            // 
            // cmbTip
            // 
            cmbTip.FormattingEnabled = true;
            cmbTip.Location = new Point(97, 69);
            cmbTip.Name = "cmbTip";
            cmbTip.Size = new Size(204, 28);
            cmbTip.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(14, 73);
            label6.Name = "label6";
            label6.Size = new Size(30, 20);
            label6.TabIndex = 8;
            label6.Text = "Tip";
            // 
            // txtSektor
            // 
            txtSektor.Location = new Point(97, 35);
            txtSektor.Name = "txtSektor";
            txtSektor.Size = new Size(204, 27);
            txtSektor.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 38);
            label4.Name = "label4";
            label4.Size = new Size(51, 20);
            label4.TabIndex = 2;
            label4.Text = "Sektor";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtNazivKonsignatora);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(cmbKonsignator);
            groupBox2.Controls.Add(label2);
            groupBox2.Location = new Point(335, 17);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(336, 154);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "Po konsignatoru";
            // 
            // txtNazivKonsignatora
            // 
            txtNazivKonsignatora.Location = new Point(110, 70);
            txtNazivKonsignatora.Name = "txtNazivKonsignatora";
            txtNazivKonsignatora.Size = new Size(208, 27);
            txtNazivKonsignatora.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 73);
            label1.Name = "label1";
            label1.Size = new Size(46, 20);
            label1.TabIndex = 10;
            label1.Text = "Naziv";
            // 
            // cmbKonsignator
            // 
            cmbKonsignator.FormattingEnabled = true;
            cmbKonsignator.Location = new Point(110, 34);
            cmbKonsignator.Name = "cmbKonsignator";
            cmbKonsignator.Size = new Size(211, 28);
            cmbKonsignator.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 38);
            label2.Name = "label2";
            label2.Size = new Size(89, 20);
            label2.TabIndex = 8;
            label2.Text = "Konsignator";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(cmbDogadjaj);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(txtMesto);
            groupBox3.Controls.Add(label9);
            groupBox3.Location = new Point(682, 17);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(324, 154);
            groupBox3.TabIndex = 12;
            groupBox3.TabStop = false;
            groupBox3.Text = "Po događaju";
            // 
            // cmbDogadjaj
            // 
            cmbDogadjaj.FormattingEnabled = true;
            cmbDogadjaj.Location = new Point(97, 35);
            cmbDogadjaj.Name = "cmbDogadjaj";
            cmbDogadjaj.Size = new Size(208, 28);
            cmbDogadjaj.TabIndex = 9;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(14, 73);
            label7.Name = "label7";
            label7.Size = new Size(50, 20);
            label7.TabIndex = 8;
            label7.Text = "Mesto";
            // 
            // txtMesto
            // 
            txtMesto.Location = new Point(97, 70);
            txtMesto.Name = "txtMesto";
            txtMesto.Size = new Size(208, 27);
            txtMesto.TabIndex = 3;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(14, 38);
            label9.Name = "label9";
            label9.Size = new Size(67, 20);
            label9.TabIndex = 2;
            label9.Text = "Događaj";
            // 
            // btnPonisti
            // 
            btnPonisti.Location = new Point(912, 186);
            btnPonisti.Name = "btnPonisti";
            btnPonisti.Size = new Size(94, 29);
            btnPonisti.TabIndex = 14;
            btnPonisti.Text = "Poništi";
            btnPonisti.UseVisualStyleBackColor = true;
            // 
            // btnPretrazi
            // 
            btnPretrazi.Location = new Point(812, 186);
            btnPretrazi.Name = "btnPretrazi";
            btnPretrazi.Size = new Size(94, 29);
            btnPretrazi.TabIndex = 13;
            btnPretrazi.Text = "Pretraži";
            btnPretrazi.UseVisualStyleBackColor = true;
            // 
            // btnIzmeni
            // 
            btnIzmeni.Location = new Point(912, 436);
            btnIzmeni.Name = "btnIzmeni";
            btnIzmeni.Size = new Size(94, 31);
            btnIzmeni.TabIndex = 20;
            btnIzmeni.Text = "Izmeni";
            btnIzmeni.UseVisualStyleBackColor = true;
            // 
            // btnPrikazi
            // 
            btnPrikazi.Location = new Point(812, 436);
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
            dgvRezultati.Location = new Point(12, 234);
            dgvRezultati.Name = "dgvRezultati";
            dgvRezultati.ReadOnly = true;
            dgvRezultati.RowHeadersWidth = 51;
            dgvRezultati.Size = new Size(994, 186);
            dgvRezultati.TabIndex = 17;
            // 
            // UCPretragaKarta
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnIzmeni);
            Controls.Add(btnPrikazi);
            Controls.Add(dgvRezultati);
            Controls.Add(btnPonisti);
            Controls.Add(btnPretrazi);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "UCPretragaKarta";
            Size = new Size(1025, 490);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRezultati).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox txtSektor;
        private Label label4;
        private ComboBox cmbStatus;
        private Label label8;
        private ComboBox cmbTip;
        private Label label6;
        private GroupBox groupBox2;
        private Label label1;
        private ComboBox cmbKonsignator;
        private Label label2;
        private GroupBox groupBox3;
        private ComboBox cmbDogadjaj;
        private Label label7;
        private TextBox txtMesto;
        private Label label9;
        private Button btnPonisti;
        private Button btnPretrazi;
        private Button btnIzmeni;
        private Button btnPrikazi;
        private DataGridView dgvRezultati;
        private TextBox txtNazivKonsignatora;

        public GroupBox GroupBox1 { get => groupBox1; set => groupBox1 = value; }
        public TextBox TxtSektor { get => txtSektor; set => txtSektor = value; }
        public Label Label4 { get => label4; set => label4 = value; }
        public ComboBox CmbStatus { get => cmbStatus; set => cmbStatus = value; }
        public Label Label8 { get => label8; set => label8 = value; }
        public ComboBox CmbTip { get => cmbTip; set => cmbTip = value; }
        public Label Label6 { get => label6; set => label6 = value; }
        public GroupBox GroupBox2 { get => groupBox2; set => groupBox2 = value; }
        public Label Label1 { get => label1; set => label1 = value; }
        public ComboBox CmbKonsignator { get => cmbKonsignator; set => cmbKonsignator = value; }
        public Label Label2 { get => label2; set => label2 = value; }
        public GroupBox GroupBox3 { get => groupBox3; set => groupBox3 = value; }
        public ComboBox CmbDogadjaj { get => cmbDogadjaj; set => cmbDogadjaj = value; }
        public Label Label7 { get => label7; set => label7 = value; }
        public TextBox TxtMesto { get => txtMesto; set => txtMesto = value; }
        public Label Label9 { get => label9; set => label9 = value; }
        public Button BtnPonisti { get => btnPonisti; set => btnPonisti = value; }
        public Button BtnPretrazi { get => btnPretrazi; set => btnPretrazi = value; }
        public Button BtnIzmeni { get => btnIzmeni; set => btnIzmeni = value; }
        public Button BtnPrikazi { get => btnPrikazi; set => btnPrikazi = value; }
        public DataGridView DgvRezultati { get => dgvRezultati; set => dgvRezultati = value; }
        public TextBox TxtNazivKonsignatora { get => txtNazivKonsignatora; set => txtNazivKonsignatora = value; }
    }
}
