namespace Klijent.UserControls
{
    partial class UCListing
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
            grpZaglavlje = new GroupBox();
            lblId = new Label();
            txtId = new TextBox();
            lblBroker = new Label();
            cmbBroker = new ComboBox();
            lblKonsignator = new Label();
            cmbKonsignator = new ComboBox();
            lblStatus = new Label();
            cmbStatus = new ComboBox();
            lblDatumObjave = new Label();
            dtpDatumObjave = new DateTimePicker();
            lblDatumIsteka = new Label();
            dtpDatumIsteka = new DateTimePicker();
            lblCenaPoKarti = new Label();
            txtCenaPoKarti = new TextBox();
            lblProcenatProvizije = new Label();
            txtProcenatProvizije = new TextBox();
            lblSplit = new Label();
            cmbSplit = new ComboBox();
            lblMinKolicina = new Label();
            numMinKolicina = new NumericUpDown();
            lblNapomena = new Label();
            txtNapomena = new TextBox();
            grpKarte = new GroupBox();
            lblSlobodne = new Label();
            dgvSlobodneKarte = new DataGridView();
            lblBrojSlobodnih = new Label();
            btnDodajKartu = new Button();
            btnUkloniKartu = new Button();
            lblNaListingu = new Label();
            dgvKarteNaListingu = new DataGridView();
            lblBrojNaListingu = new Label();
            pnlDugmad = new Panel();
            btnNazad = new Button();
            btnIzmeni = new Button();
            btnKreiraj = new Button();
            lblUkupno = new Label();
            grpZaglavlje.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMinKolicina).BeginInit();
            grpKarte.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSlobodneKarte).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvKarteNaListingu).BeginInit();
            pnlDugmad.SuspendLayout();
            SuspendLayout();
            // 
            // grpZaglavlje
            // 
            grpZaglavlje.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpZaglavlje.Controls.Add(lblId);
            grpZaglavlje.Controls.Add(txtId);
            grpZaglavlje.Controls.Add(lblBroker);
            grpZaglavlje.Controls.Add(cmbBroker);
            grpZaglavlje.Controls.Add(lblKonsignator);
            grpZaglavlje.Controls.Add(cmbKonsignator);
            grpZaglavlje.Controls.Add(lblStatus);
            grpZaglavlje.Controls.Add(cmbStatus);
            grpZaglavlje.Controls.Add(lblDatumObjave);
            grpZaglavlje.Controls.Add(dtpDatumObjave);
            grpZaglavlje.Controls.Add(lblDatumIsteka);
            grpZaglavlje.Controls.Add(dtpDatumIsteka);
            grpZaglavlje.Controls.Add(lblCenaPoKarti);
            grpZaglavlje.Controls.Add(txtCenaPoKarti);
            grpZaglavlje.Controls.Add(lblProcenatProvizije);
            grpZaglavlje.Controls.Add(txtProcenatProvizije);
            grpZaglavlje.Controls.Add(lblSplit);
            grpZaglavlje.Controls.Add(cmbSplit);
            grpZaglavlje.Controls.Add(lblMinKolicina);
            grpZaglavlje.Controls.Add(numMinKolicina);
            grpZaglavlje.Controls.Add(lblNapomena);
            grpZaglavlje.Controls.Add(txtNapomena);
            grpZaglavlje.Location = new Point(14, 16);
            grpZaglavlje.Margin = new Padding(3, 4, 3, 4);
            grpZaglavlje.Name = "grpZaglavlje";
            grpZaglavlje.Padding = new Padding(3, 4, 3, 4);
            grpZaglavlje.Size = new Size(1115, 230);
            grpZaglavlje.TabIndex = 0;
            grpZaglavlje.TabStop = false;
            grpZaglavlje.Text = "Podaci o listingu";
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Location = new Point(17, 44);
            lblId.Name = "lblId";
            lblId.Size = new Size(25, 20);
            lblId.TabIndex = 0;
            lblId.Text = "Id:";
            // 
            // txtId
            // 
            txtId.Location = new Point(137, 40);
            txtId.Margin = new Padding(3, 4, 3, 4);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(171, 27);
            txtId.TabIndex = 1;
            // 
            // lblBroker
            // 
            lblBroker.AutoSize = true;
            lblBroker.Location = new Point(331, 44);
            lblBroker.Name = "lblBroker";
            lblBroker.Size = new Size(55, 20);
            lblBroker.TabIndex = 2;
            lblBroker.Text = "Broker:";
            // 
            // cmbBroker
            // 
            cmbBroker.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBroker.Location = new Point(457, 40);
            cmbBroker.Margin = new Padding(3, 4, 3, 4);
            cmbBroker.Name = "cmbBroker";
            cmbBroker.Size = new Size(228, 28);
            cmbBroker.TabIndex = 3;
            // 
            // lblKonsignator
            // 
            lblKonsignator.AutoSize = true;
            lblKonsignator.Location = new Point(709, 44);
            lblKonsignator.Name = "lblKonsignator";
            lblKonsignator.Size = new Size(92, 20);
            lblKonsignator.TabIndex = 4;
            lblKonsignator.Text = "Konsignator:";
            // 
            // cmbKonsignator
            // 
            cmbKonsignator.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKonsignator.Location = new Point(834, 40);
            cmbKonsignator.Margin = new Padding(3, 4, 3, 4);
            cmbKonsignator.Name = "cmbKonsignator";
            cmbKonsignator.Size = new Size(262, 28);
            cmbKonsignator.TabIndex = 5;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(17, 80);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(52, 20);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Status:";
            // 
            // cmbStatus
            // 
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Location = new Point(137, 76);
            cmbStatus.Margin = new Padding(3, 4, 3, 4);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(171, 28);
            cmbStatus.TabIndex = 7;
            // 
            // lblDatumObjave
            // 
            lblDatumObjave.AutoSize = true;
            lblDatumObjave.Location = new Point(331, 80);
            lblDatumObjave.Name = "lblDatumObjave";
            lblDatumObjave.Size = new Size(106, 20);
            lblDatumObjave.TabIndex = 8;
            lblDatumObjave.Text = "Datum objave:";
            // 
            // dtpDatumObjave
            // 
            dtpDatumObjave.Format = DateTimePickerFormat.Short;
            dtpDatumObjave.Location = new Point(457, 76);
            dtpDatumObjave.Margin = new Padding(3, 4, 3, 4);
            dtpDatumObjave.Name = "dtpDatumObjave";
            dtpDatumObjave.Size = new Size(228, 27);
            dtpDatumObjave.TabIndex = 9;
            // 
            // lblDatumIsteka
            // 
            lblDatumIsteka.AutoSize = true;
            lblDatumIsteka.Location = new Point(709, 80);
            lblDatumIsteka.Name = "lblDatumIsteka";
            lblDatumIsteka.Size = new Size(99, 20);
            lblDatumIsteka.TabIndex = 10;
            lblDatumIsteka.Text = "Datum isteka:";
            // 
            // dtpDatumIsteka
            // 
            dtpDatumIsteka.Format = DateTimePickerFormat.Short;
            dtpDatumIsteka.Location = new Point(834, 76);
            dtpDatumIsteka.Margin = new Padding(3, 4, 3, 4);
            dtpDatumIsteka.Name = "dtpDatumIsteka";
            dtpDatumIsteka.Size = new Size(262, 27);
            dtpDatumIsteka.TabIndex = 11;
            // 
            // lblCenaPoKarti
            // 
            lblCenaPoKarti.AutoSize = true;
            lblCenaPoKarti.Location = new Point(17, 115);
            lblCenaPoKarti.Name = "lblCenaPoKarti";
            lblCenaPoKarti.Size = new Size(100, 20);
            lblCenaPoKarti.TabIndex = 12;
            lblCenaPoKarti.Text = "Cena po karti:";
            // 
            // txtCenaPoKarti
            // 
            txtCenaPoKarti.Location = new Point(137, 111);
            txtCenaPoKarti.Margin = new Padding(3, 4, 3, 4);
            txtCenaPoKarti.Name = "txtCenaPoKarti";
            txtCenaPoKarti.Size = new Size(171, 27);
            txtCenaPoKarti.TabIndex = 13;
            txtCenaPoKarti.TextAlign = HorizontalAlignment.Right;
            // 
            // lblProcenatProvizije
            // 
            lblProcenatProvizije.AutoSize = true;
            lblProcenatProvizije.Location = new Point(331, 115);
            lblProcenatProvizije.Name = "lblProcenatProvizije";
            lblProcenatProvizije.Size = new Size(94, 20);
            lblProcenatProvizije.TabIndex = 14;
            lblProcenatProvizije.Text = "Provizija (%):";
            // 
            // txtProcenatProvizije
            // 
            txtProcenatProvizije.Location = new Point(457, 111);
            txtProcenatProvizije.Margin = new Padding(3, 4, 3, 4);
            txtProcenatProvizije.Name = "txtProcenatProvizije";
            txtProcenatProvizije.Size = new Size(228, 27);
            txtProcenatProvizije.TabIndex = 15;
            txtProcenatProvizije.TextAlign = HorizontalAlignment.Right;
            // 
            // lblSplit
            // 
            lblSplit.AutoSize = true;
            lblSplit.Location = new Point(709, 115);
            lblSplit.Name = "lblSplit";
            lblSplit.Size = new Size(106, 20);
            lblSplit.TabIndex = 16;
            lblSplit.Text = "Način prodaje:";
            // 
            // cmbSplit
            // 
            cmbSplit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSplit.Location = new Point(834, 111);
            cmbSplit.Margin = new Padding(3, 4, 3, 4);
            cmbSplit.Name = "cmbSplit";
            cmbSplit.Size = new Size(262, 28);
            cmbSplit.TabIndex = 17;
            // 
            // lblMinKolicina
            // 
            lblMinKolicina.AutoSize = true;
            lblMinKolicina.Location = new Point(17, 151);
            lblMinKolicina.Name = "lblMinKolicina";
            lblMinKolicina.Size = new Size(95, 20);
            lblMinKolicina.TabIndex = 18;
            lblMinKolicina.Text = "Min. količina:";
            // 
            // numMinKolicina
            // 
            numMinKolicina.Location = new Point(137, 147);
            numMinKolicina.Margin = new Padding(3, 4, 3, 4);
            numMinKolicina.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numMinKolicina.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numMinKolicina.Name = "numMinKolicina";
            numMinKolicina.Size = new Size(171, 27);
            numMinKolicina.TabIndex = 19;
            numMinKolicina.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblNapomena
            // 
            lblNapomena.AutoSize = true;
            lblNapomena.Location = new Point(331, 151);
            lblNapomena.Name = "lblNapomena";
            lblNapomena.Size = new Size(86, 20);
            lblNapomena.TabIndex = 20;
            lblNapomena.Text = "Napomena:";
            // 
            // txtNapomena
            // 
            txtNapomena.Location = new Point(457, 147);
            txtNapomena.Margin = new Padding(3, 4, 3, 4);
            txtNapomena.Multiline = true;
            txtNapomena.Name = "txtNapomena";
            txtNapomena.ScrollBars = ScrollBars.Vertical;
            txtNapomena.Size = new Size(639, 63);
            txtNapomena.TabIndex = 21;
            // 
            // grpKarte
            // 
            grpKarte.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpKarte.Controls.Add(lblSlobodne);
            grpKarte.Controls.Add(dgvSlobodneKarte);
            grpKarte.Controls.Add(lblBrojSlobodnih);
            grpKarte.Controls.Add(btnDodajKartu);
            grpKarte.Controls.Add(btnUkloniKartu);
            grpKarte.Controls.Add(lblNaListingu);
            grpKarte.Controls.Add(dgvKarteNaListingu);
            grpKarte.Controls.Add(lblBrojNaListingu);
            grpKarte.Location = new Point(14, 254);
            grpKarte.Margin = new Padding(3, 4, 3, 4);
            grpKarte.Name = "grpKarte";
            grpKarte.Padding = new Padding(3, 4, 3, 4);
            grpKarte.Size = new Size(1115, 301);
            grpKarte.TabIndex = 1;
            grpKarte.TabStop = false;
            grpKarte.Text = "Karte";
            // 
            // lblSlobodne
            // 
            lblSlobodne.AutoSize = true;
            lblSlobodne.Location = new Point(17, 24);
            lblSlobodne.Name = "lblSlobodne";
            lblSlobodne.Size = new Size(203, 20);
            lblSlobodne.TabIndex = 0;
            lblSlobodne.Text = "Slobodne karte konsignatora:";
            // 
            // dgvSlobodneKarte
            // 
            dgvSlobodneKarte.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dgvSlobodneKarte.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSlobodneKarte.Location = new Point(17, 51);
            dgvSlobodneKarte.Margin = new Padding(3, 4, 3, 4);
            dgvSlobodneKarte.Name = "dgvSlobodneKarte";
            dgvSlobodneKarte.RowHeadersWidth = 51;
            dgvSlobodneKarte.Size = new Size(480, 208);
            dgvSlobodneKarte.TabIndex = 1;
            // 
            // lblBrojSlobodnih
            // 
            lblBrojSlobodnih.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblBrojSlobodnih.AutoSize = true;
            lblBrojSlobodnih.Location = new Point(17, 267);
            lblBrojSlobodnih.Name = "lblBrojSlobodnih";
            lblBrojSlobodnih.Size = new Size(92, 20);
            lblBrojSlobodnih.TabIndex = 2;
            lblBrojSlobodnih.Text = "Slobodnih: 0";
            // 
            // btnDodajKartu
            // 
            btnDodajKartu.Location = new Point(514, 104);
            btnDodajKartu.Margin = new Padding(3, 4, 3, 4);
            btnDodajKartu.Name = "btnDodajKartu";
            btnDodajKartu.Size = new Size(69, 40);
            btnDodajKartu.TabIndex = 3;
            btnDodajKartu.Text = ">>";
            btnDodajKartu.UseVisualStyleBackColor = true;
            // 
            // btnUkloniKartu
            // 
            btnUkloniKartu.Location = new Point(514, 158);
            btnUkloniKartu.Margin = new Padding(3, 4, 3, 4);
            btnUkloniKartu.Name = "btnUkloniKartu";
            btnUkloniKartu.Size = new Size(69, 40);
            btnUkloniKartu.TabIndex = 4;
            btnUkloniKartu.Text = "<<";
            btnUkloniKartu.UseVisualStyleBackColor = true;
            // 
            // lblNaListingu
            // 
            lblNaListingu.AutoSize = true;
            lblNaListingu.Location = new Point(600, 24);
            lblNaListingu.Name = "lblNaListingu";
            lblNaListingu.Size = new Size(119, 20);
            lblNaListingu.TabIndex = 5;
            lblNaListingu.Text = "Karte na listingu:";
            // 
            // dgvKarteNaListingu
            // 
            dgvKarteNaListingu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvKarteNaListingu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKarteNaListingu.Location = new Point(600, 51);
            dgvKarteNaListingu.Margin = new Padding(3, 4, 3, 4);
            dgvKarteNaListingu.Name = "dgvKarteNaListingu";
            dgvKarteNaListingu.RowHeadersWidth = 51;
            dgvKarteNaListingu.Size = new Size(498, 208);
            dgvKarteNaListingu.TabIndex = 6;
            // 
            // lblBrojNaListingu
            // 
            lblBrojNaListingu.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblBrojNaListingu.AutoSize = true;
            lblBrojNaListingu.Location = new Point(600, 267);
            lblBrojNaListingu.Name = "lblBrojNaListingu";
            lblBrojNaListingu.Size = new Size(95, 20);
            lblBrojNaListingu.TabIndex = 7;
            lblBrojNaListingu.Text = "Na listingu: 0";
            // 
            // pnlDugmad
            // 
            pnlDugmad.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlDugmad.Controls.Add(btnNazad);
            pnlDugmad.Controls.Add(btnIzmeni);
            pnlDugmad.Controls.Add(btnKreiraj);
            pnlDugmad.Controls.Add(lblUkupno);
            pnlDugmad.Location = new Point(14, 563);
            pnlDugmad.Margin = new Padding(3, 4, 3, 4);
            pnlDugmad.Name = "pnlDugmad";
            pnlDugmad.Size = new Size(1115, 49);
            pnlDugmad.TabIndex = 2;
            // 
            // btnNazad
            // 
            btnNazad.Location = new Point(691, 11);
            btnNazad.Name = "btnNazad";
            btnNazad.Size = new Size(194, 29);
            btnNazad.TabIndex = 24;
            btnNazad.Text = "Nazad na pretragu";
            btnNazad.UseVisualStyleBackColor = true;
            // 
            // btnIzmeni
            // 
            btnIzmeni.Location = new Point(948, 11);
            btnIzmeni.Name = "btnIzmeni";
            btnIzmeni.Size = new Size(150, 29);
            btnIzmeni.TabIndex = 23;
            btnIzmeni.Text = "Sačuvaj izmene";
            btnIzmeni.UseVisualStyleBackColor = true;
            // 
            // btnKreiraj
            // 
            btnKreiraj.Location = new Point(891, 11);
            btnKreiraj.Name = "btnKreiraj";
            btnKreiraj.Size = new Size(207, 29);
            btnKreiraj.TabIndex = 22;
            btnKreiraj.Text = "Kreiraj listing";
            btnKreiraj.UseVisualStyleBackColor = true;
            // 
            // lblUkupno
            // 
            lblUkupno.AutoSize = true;
            lblUkupno.Location = new Point(3, 15);
            lblUkupno.Name = "lblUkupno";
            lblUkupno.Size = new Size(67, 20);
            lblUkupno.TabIndex = 0;
            lblUkupno.Text = "Karata: 0";
            // 
            // UCListing
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(grpZaglavlje);
            Controls.Add(grpKarte);
            Controls.Add(pnlDugmad);
            Margin = new Padding(3, 4, 3, 4);
            Name = "UCListing";
            Size = new Size(1143, 625);
            grpZaglavlje.ResumeLayout(false);
            grpZaglavlje.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMinKolicina).EndInit();
            grpKarte.ResumeLayout(false);
            grpKarte.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSlobodneKarte).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvKarteNaListingu).EndInit();
            pnlDugmad.ResumeLayout(false);
            pnlDugmad.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpZaglavlje;
        private Label lblId;
        private TextBox txtId;
        private Label lblBroker;
        private ComboBox cmbBroker;
        private Label lblKonsignator;
        private ComboBox cmbKonsignator;
        private Label lblStatus;
        private ComboBox cmbStatus;
        private Label lblDatumObjave;
        private DateTimePicker dtpDatumObjave;
        private Label lblDatumIsteka;
        private DateTimePicker dtpDatumIsteka;
        private Label lblCenaPoKarti;
        private TextBox txtCenaPoKarti;
        private Label lblProcenatProvizije;
        private TextBox txtProcenatProvizije;
        private Label lblSplit;
        private ComboBox cmbSplit;
        private Label lblMinKolicina;
        private NumericUpDown numMinKolicina;
        private Label lblNapomena;
        private TextBox txtNapomena;
        private GroupBox grpKarte;
        private Label lblSlobodne;
        private DataGridView dgvSlobodneKarte;
        private Label lblBrojSlobodnih;
        private Button btnDodajKartu;
        private Button btnUkloniKartu;
        private Label lblNaListingu;
        private DataGridView dgvKarteNaListingu;
        private Label lblBrojNaListingu;
        private Panel pnlDugmad;
        private Label lblUkupno;
        private Button btnNazad;
        private Button btnIzmeni;
        private Button btnKreiraj;

        public TextBox TxtId { get => txtId; set => txtId = value; }
        public ComboBox CmbBroker { get => cmbBroker; set => cmbBroker = value; }
        public ComboBox CmbKonsignator { get => cmbKonsignator; set => cmbKonsignator = value; }
        public ComboBox CmbStatus { get => cmbStatus; set => cmbStatus = value; }
        public ComboBox CmbSplit { get => cmbSplit; set => cmbSplit = value; }
        public DateTimePicker DtpDatumObjave { get => dtpDatumObjave; set => dtpDatumObjave = value; }
        public DateTimePicker DtpDatumIsteka { get => dtpDatumIsteka; set => dtpDatumIsteka = value; }
        public TextBox TxtCenaPoKarti { get => txtCenaPoKarti; set => txtCenaPoKarti = value; }
        public TextBox TxtProcenatProvizije { get => txtProcenatProvizije; set => txtProcenatProvizije = value; }
        public NumericUpDown NumMinKolicina { get => numMinKolicina; set => numMinKolicina = value; }
        public TextBox TxtNapomena { get => txtNapomena; set => txtNapomena = value; }
        public DataGridView DgvSlobodneKarte { get => dgvSlobodneKarte; set => dgvSlobodneKarte = value; }
        public DataGridView DgvKarteNaListingu { get => dgvKarteNaListingu; set => dgvKarteNaListingu = value; }
        public Button BtnDodajKartu { get => btnDodajKartu; set => btnDodajKartu = value; }
        public Button BtnUkloniKartu { get => btnUkloniKartu; set => btnUkloniKartu = value; }
        public Button BtnKreiraj { get => btnKreiraj; set => btnKreiraj = value; }
        public Button BtnIzmeni { get => btnIzmeni; set => btnIzmeni = value; }
        public Button BtnNazad { get => btnNazad; set => btnNazad = value; }
    }
}
