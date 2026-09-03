namespace Klijent.UserControls
{
    partial class UCPretragaListing
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
            gbPoListingu = new GroupBox();
            lblStatus = new Label();
            cmbStatus = new ComboBox();
            lblSplit = new Label();
            cmbSplit = new ComboBox();
            lblObjavljenOd = new Label();
            dtpObjavljenOd = new DateTimePicker();
            lblObjavljenDo = new Label();
            dtpObjavljenDo = new DateTimePicker();
            lblCenaOd = new Label();
            txtCenaOd = new TextBox();
            lblCenaDo = new Label();
            txtCenaDo = new TextBox();
            gbPoKonsignatoru = new GroupBox();
            lblKonsignator = new Label();
            cmbKonsignator = new ComboBox();
            lblNazivKonsignatora = new Label();
            txtNazivKonsignatora = new TextBox();
            gbPoDogadjaju = new GroupBox();
            lblDogadjaj = new Label();
            cmbDogadjaj = new ComboBox();
            lblMesto = new Label();
            txtMesto = new TextBox();
            gbPoKarti = new GroupBox();
            lblSektor = new Label();
            txtSektor = new TextBox();
            lblTipKarte = new Label();
            cmbTipKarte = new ComboBox();
            dgvRezultati = new DataGridView();
            btnPonisti = new Button();
            btnPretrazi = new Button();
            btnIzmeni = new Button();
            btnPrikazi = new Button();
            gbPoListingu.SuspendLayout();
            gbPoKonsignatoru.SuspendLayout();
            gbPoDogadjaju.SuspendLayout();
            gbPoKarti.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRezultati).BeginInit();
            SuspendLayout();
            // 
            // gbPoListingu
            // 
            gbPoListingu.Controls.Add(lblStatus);
            gbPoListingu.Controls.Add(cmbStatus);
            gbPoListingu.Controls.Add(lblSplit);
            gbPoListingu.Controls.Add(cmbSplit);
            gbPoListingu.Controls.Add(lblObjavljenOd);
            gbPoListingu.Controls.Add(dtpObjavljenOd);
            gbPoListingu.Controls.Add(lblObjavljenDo);
            gbPoListingu.Controls.Add(dtpObjavljenDo);
            gbPoListingu.Controls.Add(lblCenaOd);
            gbPoListingu.Controls.Add(txtCenaOd);
            gbPoListingu.Controls.Add(lblCenaDo);
            gbPoListingu.Controls.Add(txtCenaDo);
            gbPoListingu.Location = new Point(14, 16);
            gbPoListingu.Margin = new Padding(3, 4, 3, 4);
            gbPoListingu.Name = "gbPoListingu";
            gbPoListingu.Padding = new Padding(3, 4, 3, 4);
            gbPoListingu.Size = new Size(555, 160);
            gbPoListingu.TabIndex = 0;
            gbPoListingu.TabStop = false;
            gbPoListingu.Text = "Po listingu";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(17, 37);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(52, 20);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Status:";
            // 
            // cmbStatus
            // 
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Location = new Point(126, 33);
            cmbStatus.Margin = new Padding(3, 4, 3, 4);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(171, 28);
            cmbStatus.TabIndex = 1;
            // 
            // lblSplit
            // 
            lblSplit.AutoSize = true;
            lblSplit.Location = new Point(314, 37);
            lblSplit.Name = "lblSplit";
            lblSplit.Size = new Size(106, 20);
            lblSplit.TabIndex = 2;
            lblSplit.Text = "Način prodaje:";
            // 
            // cmbSplit
            // 
            cmbSplit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSplit.Location = new Point(400, 33);
            cmbSplit.Margin = new Padding(3, 4, 3, 4);
            cmbSplit.Name = "cmbSplit";
            cmbSplit.Size = new Size(148, 28);
            cmbSplit.TabIndex = 3;
            // 
            // lblObjavljenOd
            // 
            lblObjavljenOd.AutoSize = true;
            lblObjavljenOd.Location = new Point(17, 73);
            lblObjavljenOd.Name = "lblObjavljenOd";
            lblObjavljenOd.Size = new Size(97, 20);
            lblObjavljenOd.TabIndex = 4;
            lblObjavljenOd.Text = "Objavljen od:";
            // 
            // dtpObjavljenOd
            // 
            dtpObjavljenOd.Checked = false;
            dtpObjavljenOd.Format = DateTimePickerFormat.Short;
            dtpObjavljenOd.Location = new Point(126, 69);
            dtpObjavljenOd.Margin = new Padding(3, 4, 3, 4);
            dtpObjavljenOd.Name = "dtpObjavljenOd";
            dtpObjavljenOd.ShowCheckBox = true;
            dtpObjavljenOd.Size = new Size(171, 27);
            dtpObjavljenOd.TabIndex = 5;
            // 
            // lblObjavljenDo
            // 
            lblObjavljenDo.AutoSize = true;
            lblObjavljenDo.Location = new Point(314, 73);
            lblObjavljenDo.Name = "lblObjavljenDo";
            lblObjavljenDo.Size = new Size(30, 20);
            lblObjavljenDo.TabIndex = 6;
            lblObjavljenDo.Text = "do:";
            // 
            // dtpObjavljenDo
            // 
            dtpObjavljenDo.Checked = false;
            dtpObjavljenDo.Format = DateTimePickerFormat.Short;
            dtpObjavljenDo.Location = new Point(400, 69);
            dtpObjavljenDo.Margin = new Padding(3, 4, 3, 4);
            dtpObjavljenDo.Name = "dtpObjavljenDo";
            dtpObjavljenDo.ShowCheckBox = true;
            dtpObjavljenDo.Size = new Size(148, 27);
            dtpObjavljenDo.TabIndex = 7;
            // 
            // lblCenaOd
            // 
            lblCenaOd.AutoSize = true;
            lblCenaOd.Location = new Point(17, 108);
            lblCenaOd.Name = "lblCenaOd";
            lblCenaOd.Size = new Size(67, 20);
            lblCenaOd.TabIndex = 8;
            lblCenaOd.Text = "Cena od:";
            // 
            // txtCenaOd
            // 
            txtCenaOd.Location = new Point(126, 104);
            txtCenaOd.Margin = new Padding(3, 4, 3, 4);
            txtCenaOd.Name = "txtCenaOd";
            txtCenaOd.Size = new Size(171, 27);
            txtCenaOd.TabIndex = 9;
            txtCenaOd.TextAlign = HorizontalAlignment.Right;
            // 
            // lblCenaDo
            // 
            lblCenaDo.AutoSize = true;
            lblCenaDo.Location = new Point(314, 108);
            lblCenaDo.Name = "lblCenaDo";
            lblCenaDo.Size = new Size(30, 20);
            lblCenaDo.TabIndex = 10;
            lblCenaDo.Text = "do:";
            // 
            // txtCenaDo
            // 
            txtCenaDo.Location = new Point(400, 104);
            txtCenaDo.Margin = new Padding(3, 4, 3, 4);
            txtCenaDo.Name = "txtCenaDo";
            txtCenaDo.Size = new Size(148, 27);
            txtCenaDo.TabIndex = 11;
            txtCenaDo.TextAlign = HorizontalAlignment.Right;
            // 
            // gbPoKonsignatoru
            // 
            gbPoKonsignatoru.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbPoKonsignatoru.Controls.Add(lblKonsignator);
            gbPoKonsignatoru.Controls.Add(cmbKonsignator);
            gbPoKonsignatoru.Controls.Add(lblNazivKonsignatora);
            gbPoKonsignatoru.Controls.Add(txtNazivKonsignatora);
            gbPoKonsignatoru.Location = new Point(576, 16);
            gbPoKonsignatoru.Margin = new Padding(3, 4, 3, 4);
            gbPoKonsignatoru.Name = "gbPoKonsignatoru";
            gbPoKonsignatoru.Padding = new Padding(3, 4, 3, 4);
            gbPoKonsignatoru.Size = new Size(558, 77);
            gbPoKonsignatoru.TabIndex = 1;
            gbPoKonsignatoru.TabStop = false;
            gbPoKonsignatoru.Text = "Po konsignatoru";
            // 
            // lblKonsignator
            // 
            lblKonsignator.AutoSize = true;
            lblKonsignator.Location = new Point(17, 37);
            lblKonsignator.Name = "lblKonsignator";
            lblKonsignator.Size = new Size(92, 20);
            lblKonsignator.TabIndex = 0;
            lblKonsignator.Text = "Konsignator:";
            // 
            // cmbKonsignator
            // 
            cmbKonsignator.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKonsignator.Location = new Point(126, 33);
            cmbKonsignator.Margin = new Padding(3, 4, 3, 4);
            cmbKonsignator.Name = "cmbKonsignator";
            cmbKonsignator.Size = new Size(182, 28);
            cmbKonsignator.TabIndex = 1;
            // 
            // lblNazivKonsignatora
            // 
            lblNazivKonsignatora.AutoSize = true;
            lblNazivKonsignatora.Location = new Point(326, 37);
            lblNazivKonsignatora.Name = "lblNazivKonsignatora";
            lblNazivKonsignatora.Size = new Size(49, 20);
            lblNazivKonsignatora.TabIndex = 2;
            lblNazivKonsignatora.Text = "Naziv:";
            // 
            // txtNazivKonsignatora
            // 
            txtNazivKonsignatora.Location = new Point(389, 33);
            txtNazivKonsignatora.Margin = new Padding(3, 4, 3, 4);
            txtNazivKonsignatora.Name = "txtNazivKonsignatora";
            txtNazivKonsignatora.Size = new Size(148, 27);
            txtNazivKonsignatora.TabIndex = 3;
            // 
            // gbPoDogadjaju
            // 
            gbPoDogadjaju.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbPoDogadjaju.Controls.Add(lblDogadjaj);
            gbPoDogadjaju.Controls.Add(cmbDogadjaj);
            gbPoDogadjaju.Controls.Add(lblMesto);
            gbPoDogadjaju.Controls.Add(txtMesto);
            gbPoDogadjaju.Location = new Point(576, 101);
            gbPoDogadjaju.Margin = new Padding(3, 4, 3, 4);
            gbPoDogadjaju.Name = "gbPoDogadjaju";
            gbPoDogadjaju.Padding = new Padding(3, 4, 3, 4);
            gbPoDogadjaju.Size = new Size(558, 75);
            gbPoDogadjaju.TabIndex = 2;
            gbPoDogadjaju.TabStop = false;
            gbPoDogadjaju.Text = "Po događaju";
            // 
            // lblDogadjaj
            // 
            lblDogadjaj.AutoSize = true;
            lblDogadjaj.Location = new Point(17, 32);
            lblDogadjaj.Name = "lblDogadjaj";
            lblDogadjaj.Size = new Size(70, 20);
            lblDogadjaj.TabIndex = 0;
            lblDogadjaj.Text = "Događaj:";
            // 
            // cmbDogadjaj
            // 
            cmbDogadjaj.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDogadjaj.Location = new Point(126, 28);
            cmbDogadjaj.Margin = new Padding(3, 4, 3, 4);
            cmbDogadjaj.Name = "cmbDogadjaj";
            cmbDogadjaj.Size = new Size(182, 28);
            cmbDogadjaj.TabIndex = 1;
            // 
            // lblMesto
            // 
            lblMesto.AutoSize = true;
            lblMesto.Location = new Point(326, 32);
            lblMesto.Name = "lblMesto";
            lblMesto.Size = new Size(53, 20);
            lblMesto.TabIndex = 2;
            lblMesto.Text = "Mesto:";
            // 
            // txtMesto
            // 
            txtMesto.Location = new Point(389, 28);
            txtMesto.Margin = new Padding(3, 4, 3, 4);
            txtMesto.Name = "txtMesto";
            txtMesto.Size = new Size(148, 27);
            txtMesto.TabIndex = 3;
            // 
            // gbPoKarti
            // 
            gbPoKarti.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbPoKarti.Controls.Add(lblSektor);
            gbPoKarti.Controls.Add(txtSektor);
            gbPoKarti.Controls.Add(lblTipKarte);
            gbPoKarti.Controls.Add(cmbTipKarte);
            gbPoKarti.Location = new Point(14, 184);
            gbPoKarti.Margin = new Padding(3, 4, 3, 4);
            gbPoKarti.Name = "gbPoKarti";
            gbPoKarti.Padding = new Padding(3, 4, 3, 4);
            gbPoKarti.Size = new Size(555, 76);
            gbPoKarti.TabIndex = 3;
            gbPoKarti.TabStop = false;
            gbPoKarti.Text = "Po karti";
            // 
            // lblSektor
            // 
            lblSektor.AutoSize = true;
            lblSektor.Location = new Point(17, 32);
            lblSektor.Name = "lblSektor";
            lblSektor.Size = new Size(54, 20);
            lblSektor.TabIndex = 0;
            lblSektor.Text = "Sektor:";
            // 
            // txtSektor
            // 
            txtSektor.Location = new Point(126, 28);
            txtSektor.Margin = new Padding(3, 4, 3, 4);
            txtSektor.Name = "txtSektor";
            txtSektor.Size = new Size(171, 27);
            txtSektor.TabIndex = 1;
            // 
            // lblTipKarte
            // 
            lblTipKarte.AutoSize = true;
            lblTipKarte.Location = new Point(314, 31);
            lblTipKarte.Name = "lblTipKarte";
            lblTipKarte.Size = new Size(70, 20);
            lblTipKarte.TabIndex = 2;
            lblTipKarte.Text = "Tip karte:";
            // 
            // cmbTipKarte
            // 
            cmbTipKarte.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipKarte.Location = new Point(400, 27);
            cmbTipKarte.Margin = new Padding(3, 4, 3, 4);
            cmbTipKarte.Name = "cmbTipKarte";
            cmbTipKarte.Size = new Size(148, 28);
            cmbTipKarte.TabIndex = 3;
            // 
            // dgvRezultati
            // 
            dgvRezultati.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvRezultati.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRezultati.Location = new Point(14, 285);
            dgvRezultati.Margin = new Padding(3, 4, 3, 4);
            dgvRezultati.Name = "dgvRezultati";
            dgvRezultati.RowHeadersWidth = 51;
            dgvRezultati.Size = new Size(1120, 249);
            dgvRezultati.TabIndex = 8;
            // 
            // btnPonisti
            // 
            btnPonisti.Location = new Point(1040, 231);
            btnPonisti.Name = "btnPonisti";
            btnPonisti.Size = new Size(94, 29);
            btnPonisti.TabIndex = 16;
            btnPonisti.Text = "Poništi";
            btnPonisti.UseVisualStyleBackColor = true;
            // 
            // btnPretrazi
            // 
            btnPretrazi.Location = new Point(940, 231);
            btnPretrazi.Name = "btnPretrazi";
            btnPretrazi.Size = new Size(94, 29);
            btnPretrazi.TabIndex = 15;
            btnPretrazi.Text = "Pretraži";
            btnPretrazi.UseVisualStyleBackColor = true;
            // 
            // btnIzmeni
            // 
            btnIzmeni.Location = new Point(1040, 552);
            btnIzmeni.Name = "btnIzmeni";
            btnIzmeni.Size = new Size(94, 31);
            btnIzmeni.TabIndex = 22;
            btnIzmeni.Text = "Izmeni";
            btnIzmeni.UseVisualStyleBackColor = true;
            // 
            // btnPrikazi
            // 
            btnPrikazi.Location = new Point(940, 552);
            btnPrikazi.Name = "btnPrikazi";
            btnPrikazi.Size = new Size(94, 31);
            btnPrikazi.TabIndex = 21;
            btnPrikazi.Text = "Prikaži";
            btnPrikazi.UseVisualStyleBackColor = true;
            // 
            // UCPretragaListing
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnIzmeni);
            Controls.Add(btnPrikazi);
            Controls.Add(btnPonisti);
            Controls.Add(btnPretrazi);
            Controls.Add(gbPoListingu);
            Controls.Add(gbPoKonsignatoru);
            Controls.Add(gbPoDogadjaju);
            Controls.Add(gbPoKarti);
            Controls.Add(dgvRezultati);
            Margin = new Padding(3, 4, 3, 4);
            Name = "UCPretragaListing";
            Size = new Size(1148, 597);
            gbPoListingu.ResumeLayout(false);
            gbPoListingu.PerformLayout();
            gbPoKonsignatoru.ResumeLayout(false);
            gbPoKonsignatoru.PerformLayout();
            gbPoDogadjaju.ResumeLayout(false);
            gbPoDogadjaju.PerformLayout();
            gbPoKarti.ResumeLayout(false);
            gbPoKarti.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRezultati).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbPoListingu;
        private Label lblStatus;
        private ComboBox cmbStatus;
        private Label lblSplit;
        private ComboBox cmbSplit;
        private Label lblObjavljenOd;
        private DateTimePicker dtpObjavljenOd;
        private Label lblObjavljenDo;
        private DateTimePicker dtpObjavljenDo;
        private Label lblCenaOd;
        private TextBox txtCenaOd;
        private Label lblCenaDo;
        private TextBox txtCenaDo;
        private GroupBox gbPoKonsignatoru;
        private Label lblKonsignator;
        private ComboBox cmbKonsignator;
        private Label lblNazivKonsignatora;
        private TextBox txtNazivKonsignatora;
        private GroupBox gbPoDogadjaju;
        private Label lblDogadjaj;
        private ComboBox cmbDogadjaj;
        private Label lblMesto;
        private TextBox txtMesto;
        private GroupBox gbPoKarti;
        private Label lblSektor;
        private TextBox txtSektor;
        private Label lblTipKarte;
        private ComboBox cmbTipKarte;
        private DataGridView dgvRezultati;
        private Button btnPonisti;
        private Button btnPretrazi;
        private Button btnIzmeni;
        private Button btnPrikazi;

        public ComboBox CmbStatus { get => cmbStatus; set => cmbStatus = value; }
        public ComboBox CmbSplit { get => cmbSplit; set => cmbSplit = value; }
        public DateTimePicker DtpObjavljenOd { get => dtpObjavljenOd; set => dtpObjavljenOd = value; }
        public DateTimePicker DtpObjavljenDo { get => dtpObjavljenDo; set => dtpObjavljenDo = value; }
        public TextBox TxtCenaOd { get => txtCenaOd; set => txtCenaOd = value; }
        public TextBox TxtCenaDo { get => txtCenaDo; set => txtCenaDo = value; }
        public ComboBox CmbKonsignator { get => cmbKonsignator; set => cmbKonsignator = value; }
        public TextBox TxtNazivKonsignatora { get => txtNazivKonsignatora; set => txtNazivKonsignatora = value; }
        public ComboBox CmbDogadjaj { get => cmbDogadjaj; set => cmbDogadjaj = value; }
        public TextBox TxtMesto { get => txtMesto; set => txtMesto = value; }
        public TextBox TxtSektor { get => txtSektor; set => txtSektor = value; }
        public ComboBox CmbTipKarte { get => cmbTipKarte; set => cmbTipKarte = value; }
        public Button BtnPretrazi { get => btnPretrazi; set => btnPretrazi = value; }
        public Button BtnPonisti { get => btnPonisti; set => btnPonisti = value; }
        public Button BtnPrikazi { get => btnPrikazi; set => btnPrikazi = value; }
        public Button BtnIzmeni { get => btnIzmeni; set => btnIzmeni = value; }
        public DataGridView DgvRezultati { get => dgvRezultati; set => dgvRezultati = value; }
    }
}
