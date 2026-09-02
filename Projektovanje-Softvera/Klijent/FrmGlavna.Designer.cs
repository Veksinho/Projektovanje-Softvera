namespace Klijent
{
    partial class FrmGlavna
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mniGlavni = new MenuStrip();
            mniDokumenti = new ToolStripMenuItem();
            mniListing = new ToolStripMenuItem();
            mniKarta = new ToolStripMenuItem();
            mniPruzalacUsluge = new ToolStripMenuItem();
            mniBroker = new ToolStripMenuItem();
            mniBrokerKreiraj = new ToolStripMenuItem();
            mniBrokerPretrazi = new ToolStripMenuItem();
            mniPrimalacUsluge = new ToolStripMenuItem();
            mniKonsignator = new ToolStripMenuItem();
            mniKonsignatorKreiraj = new ToolStripMenuItem();
            mniKonsignatorPretrazi = new ToolStripMenuItem();
            mniSifarnici = new ToolStripMenuItem();
            mniDogadjaj = new ToolStripMenuItem();
            mniDogadjajUbaci = new ToolStripMenuItem();
            mniDogadjajPretrazi = new ToolStripMenuItem();
            mniKategorijaDogadjaja = new ToolStripMenuItem();
            mniKategorijaDogadjajaUbaci = new ToolStripMenuItem();
            mniKategorijaDogadjajaPretrazi = new ToolStripMenuItem();
            podešavanjaToolStripMenuItem = new ToolStripMenuItem();
            oProgramuToolStripMenuItem = new ToolStripMenuItem();
            mniOdjava = new ToolStripMenuItem();
            stsStatus = new StatusStrip();
            lblPrijavljeniBroker = new ToolStripStatusLabel();
            pnlSadrzaj = new Panel();
            mniGlavni.SuspendLayout();
            stsStatus.SuspendLayout();
            SuspendLayout();
            // 
            // mniGlavni
            // 
            mniGlavni.ImageScalingSize = new Size(20, 20);
            mniGlavni.Items.AddRange(new ToolStripItem[] { mniDokumenti, mniPruzalacUsluge, mniPrimalacUsluge, mniSifarnici, podešavanjaToolStripMenuItem, oProgramuToolStripMenuItem, mniOdjava });
            mniGlavni.Location = new Point(0, 0);
            mniGlavni.Name = "mniGlavni";
            mniGlavni.Size = new Size(982, 28);
            mniGlavni.TabIndex = 2;
            mniGlavni.Text = "menuStrip1";
            // 
            // mniDokumenti
            // 
            mniDokumenti.DropDownItems.AddRange(new ToolStripItem[] { mniListing, mniKarta });
            mniDokumenti.Name = "mniDokumenti";
            mniDokumenti.Size = new Size(96, 24);
            mniDokumenti.Text = "Dokumenti";
            // 
            // mniListing
            // 
            mniListing.Name = "mniListing";
            mniListing.Size = new Size(224, 26);
            mniListing.Text = "Listing";
            // 
            // mniKarta
            // 
            mniKarta.Name = "mniKarta";
            mniKarta.Size = new Size(224, 26);
            mniKarta.Text = "Karta";
            // 
            // mniPruzalacUsluge
            // 
            mniPruzalacUsluge.DropDownItems.AddRange(new ToolStripItem[] { mniBroker });
            mniPruzalacUsluge.Name = "mniPruzalacUsluge";
            mniPruzalacUsluge.Size = new Size(125, 24);
            mniPruzalacUsluge.Text = "Pružalac usluge";
            // 
            // mniBroker
            // 
            mniBroker.DropDownItems.AddRange(new ToolStripItem[] { mniBrokerKreiraj, mniBrokerPretrazi });
            mniBroker.Name = "mniBroker";
            mniBroker.Size = new Size(224, 26);
            mniBroker.Text = "Broker";
            // 
            // mniBrokerKreiraj
            // 
            mniBrokerKreiraj.Name = "mniBrokerKreiraj";
            mniBrokerKreiraj.Size = new Size(142, 26);
            mniBrokerKreiraj.Text = "Kreiraj";
            // 
            // mniBrokerPretrazi
            // 
            mniBrokerPretrazi.Name = "mniBrokerPretrazi";
            mniBrokerPretrazi.Size = new Size(142, 26);
            mniBrokerPretrazi.Text = "Pretraži";
            // 
            // mniPrimalacUsluge
            // 
            mniPrimalacUsluge.DropDownItems.AddRange(new ToolStripItem[] { mniKonsignator });
            mniPrimalacUsluge.Name = "mniPrimalacUsluge";
            mniPrimalacUsluge.Size = new Size(127, 24);
            mniPrimalacUsluge.Text = "Primalac usluge";
            // 
            // mniKonsignator
            // 
            mniKonsignator.DropDownItems.AddRange(new ToolStripItem[] { mniKonsignatorKreiraj, mniKonsignatorPretrazi });
            mniKonsignator.Name = "mniKonsignator";
            mniKonsignator.Size = new Size(224, 26);
            mniKonsignator.Text = "Konsignator";
            // 
            // mniKonsignatorKreiraj
            // 
            mniKonsignatorKreiraj.Name = "mniKonsignatorKreiraj";
            mniKonsignatorKreiraj.Size = new Size(224, 26);
            mniKonsignatorKreiraj.Text = "Kreiraj";
            // 
            // mniKonsignatorPretrazi
            // 
            mniKonsignatorPretrazi.Name = "mniKonsignatorPretrazi";
            mniKonsignatorPretrazi.Size = new Size(224, 26);
            mniKonsignatorPretrazi.Text = "Pretraži";
            // 
            // mniSifarnici
            // 
            mniSifarnici.DropDownItems.AddRange(new ToolStripItem[] { mniDogadjaj, mniKategorijaDogadjaja });
            mniSifarnici.Name = "mniSifarnici";
            mniSifarnici.Size = new Size(76, 24);
            mniSifarnici.Text = "Šifarnici";
            // 
            // mniDogadjaj
            // 
            mniDogadjaj.DropDownItems.AddRange(new ToolStripItem[] { mniDogadjajUbaci, mniDogadjajPretrazi });
            mniDogadjaj.Name = "mniDogadjaj";
            mniDogadjaj.Size = new Size(229, 26);
            mniDogadjaj.Text = "Događaj";
            // 
            // mniDogadjajUbaci
            // 
            mniDogadjajUbaci.Name = "mniDogadjajUbaci";
            mniDogadjajUbaci.Size = new Size(142, 26);
            mniDogadjajUbaci.Text = "Ubaci";
            // 
            // mniDogadjajPretrazi
            // 
            mniDogadjajPretrazi.Name = "mniDogadjajPretrazi";
            mniDogadjajPretrazi.Size = new Size(142, 26);
            mniDogadjajPretrazi.Text = "Pretrazi";
            // 
            // mniKategorijaDogadjaja
            // 
            mniKategorijaDogadjaja.DropDownItems.AddRange(new ToolStripItem[] { mniKategorijaDogadjajaUbaci, mniKategorijaDogadjajaPretrazi });
            mniKategorijaDogadjaja.Name = "mniKategorijaDogadjaja";
            mniKategorijaDogadjaja.Size = new Size(229, 26);
            mniKategorijaDogadjaja.Text = "Kategorija događaja";
            // 
            // mniKategorijaDogadjajaUbaci
            // 
            mniKategorijaDogadjajaUbaci.Name = "mniKategorijaDogadjajaUbaci";
            mniKategorijaDogadjajaUbaci.Size = new Size(142, 26);
            mniKategorijaDogadjajaUbaci.Text = "Ubaci";
            // 
            // mniKategorijaDogadjajaPretrazi
            // 
            mniKategorijaDogadjajaPretrazi.Name = "mniKategorijaDogadjajaPretrazi";
            mniKategorijaDogadjajaPretrazi.Size = new Size(142, 26);
            mniKategorijaDogadjajaPretrazi.Text = "Pretraži";
            // 
            // podešavanjaToolStripMenuItem
            // 
            podešavanjaToolStripMenuItem.Name = "podešavanjaToolStripMenuItem";
            podešavanjaToolStripMenuItem.Size = new Size(105, 24);
            podešavanjaToolStripMenuItem.Text = "Podešavanja";
            // 
            // oProgramuToolStripMenuItem
            // 
            oProgramuToolStripMenuItem.Name = "oProgramuToolStripMenuItem";
            oProgramuToolStripMenuItem.Size = new Size(104, 24);
            oProgramuToolStripMenuItem.Text = "O programu";
            // 
            // mniOdjava
            // 
            mniOdjava.Name = "mniOdjava";
            mniOdjava.Size = new Size(70, 24);
            mniOdjava.Text = "Odjava";
            mniOdjava.Click += mniOdjava_Click;
            // 
            // stsStatus
            // 
            stsStatus.ImageScalingSize = new Size(20, 20);
            stsStatus.Items.AddRange(new ToolStripItem[] { lblPrijavljeniBroker });
            stsStatus.Location = new Point(0, 581);
            stsStatus.Name = "stsStatus";
            stsStatus.Size = new Size(982, 22);
            stsStatus.TabIndex = 3;
            stsStatus.Text = "statusStrip1";
            // 
            // lblPrijavljeniBroker
            // 
            lblPrijavljeniBroker.Name = "lblPrijavljeniBroker";
            lblPrijavljeniBroker.Size = new Size(0, 16);
            // 
            // pnlSadrzaj
            // 
            pnlSadrzaj.Dock = DockStyle.Fill;
            pnlSadrzaj.Location = new Point(0, 28);
            pnlSadrzaj.Name = "pnlSadrzaj";
            pnlSadrzaj.Size = new Size(982, 553);
            pnlSadrzaj.TabIndex = 4;
            // 
            // FrmGlavna
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 603);
            Controls.Add(pnlSadrzaj);
            Controls.Add(stsStatus);
            Controls.Add(mniGlavni);
            MainMenuStrip = mniGlavni;
            Name = "FrmGlavna";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistem za preprodaju karata";
            FormClosing += FrmGlavna_FormClosing;
            mniGlavni.ResumeLayout(false);
            mniGlavni.PerformLayout();
            stsStatus.ResumeLayout(false);
            stsStatus.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip mniGlavni;
        private ToolStripMenuItem mniDokumenti;
        private ToolStripMenuItem mniListing;
        private ToolStripMenuItem mniKarta;
        private ToolStripMenuItem mniPruzalacUsluge;
        private ToolStripMenuItem mniBroker;
        private ToolStripMenuItem mniPrimalacUsluge;
        private ToolStripMenuItem mniKonsignator;
        private ToolStripMenuItem mniKonsignatorKreiraj;
        private ToolStripMenuItem mniKonsignatorPretrazi;
        private ToolStripMenuItem mniSifarnici;
        private ToolStripMenuItem mniDogadjaj;
        private ToolStripMenuItem mniKategorijaDogadjaja;
        private ToolStripMenuItem podešavanjaToolStripMenuItem;
        private ToolStripMenuItem oProgramuToolStripMenuItem;
        private ToolStripMenuItem mniOdjava;
        private StatusStrip stsStatus;
        private ToolStripStatusLabel lblPrijavljeniBroker;
        private Panel pnlSadrzaj;
        private ToolStripMenuItem mniKategorijaDogadjajaUbaci;
        private ToolStripMenuItem mniKategorijaDogadjajaPretrazi;
        private ToolStripMenuItem mniDogadjajUbaci;
        private ToolStripMenuItem mniDogadjajPretrazi;
        private ToolStripMenuItem mniBrokerKreiraj;
        private ToolStripMenuItem mniBrokerPretrazi;
    }
}