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
            mnsGlavni = new MenuStrip();
            mniDokumenti = new ToolStripMenuItem();
            mniPruzalacUsluge = new ToolStripMenuItem();
            mniPrimalacUsluge = new ToolStripMenuItem();
            mniSifarnici = new ToolStripMenuItem();
            podešavanjaToolStripMenuItem = new ToolStripMenuItem();
            oProgramuToolStripMenuItem = new ToolStripMenuItem();
            mniOdjava = new ToolStripMenuItem();
            mniListing = new ToolStripMenuItem();
            mniKarta = new ToolStripMenuItem();
            mniBroker = new ToolStripMenuItem();
            MniKonsignator = new ToolStripMenuItem();
            mniPravnoLice = new ToolStripMenuItem();
            mniFizickoLice = new ToolStripMenuItem();
            mniDogadjaj = new ToolStripMenuItem();
            mniKategorijaDogadjaja = new ToolStripMenuItem();
            stsStatus = new StatusStrip();
            lblPrijavljeniBroker = new ToolStripStatusLabel();
            pnlSadrzaj = new Panel();
            mnsGlavni.SuspendLayout();
            stsStatus.SuspendLayout();
            SuspendLayout();
            // 
            // mnsGlavni
            // 
            mnsGlavni.ImageScalingSize = new Size(20, 20);
            mnsGlavni.Items.AddRange(new ToolStripItem[] { mniDokumenti, mniPruzalacUsluge, mniPrimalacUsluge, mniSifarnici, podešavanjaToolStripMenuItem, oProgramuToolStripMenuItem, mniOdjava });
            mnsGlavni.Location = new Point(0, 0);
            mnsGlavni.Name = "mnsGlavni";
            mnsGlavni.Size = new Size(982, 28);
            mnsGlavni.TabIndex = 2;
            mnsGlavni.Text = "menuStrip1";
            // 
            // mniDokumenti
            // 
            mniDokumenti.DropDownItems.AddRange(new ToolStripItem[] { mniListing, mniKarta });
            mniDokumenti.Name = "mniDokumenti";
            mniDokumenti.Size = new Size(96, 24);
            mniDokumenti.Text = "Dokumenti";
            // 
            // mniPruzalacUsluge
            // 
            mniPruzalacUsluge.DropDownItems.AddRange(new ToolStripItem[] { mniBroker });
            mniPruzalacUsluge.Name = "mniPruzalacUsluge";
            mniPruzalacUsluge.Size = new Size(125, 24);
            mniPruzalacUsluge.Text = "Pružalac usluge";
            // 
            // mniPrimalacUsluge
            // 
            mniPrimalacUsluge.DropDownItems.AddRange(new ToolStripItem[] { MniKonsignator });
            mniPrimalacUsluge.Name = "mniPrimalacUsluge";
            mniPrimalacUsluge.Size = new Size(127, 24);
            mniPrimalacUsluge.Text = "Primalac usluge";
            // 
            // mniSifarnici
            // 
            mniSifarnici.DropDownItems.AddRange(new ToolStripItem[] { mniDogadjaj, mniKategorijaDogadjaja });
            mniSifarnici.Name = "mniSifarnici";
            mniSifarnici.Size = new Size(76, 24);
            mniSifarnici.Text = "Šifarnici";
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
            // mniBroker
            // 
            mniBroker.Name = "mniBroker";
            mniBroker.Size = new Size(224, 26);
            mniBroker.Text = "Broker";
            // 
            // MniKonsignator
            // 
            MniKonsignator.DropDownItems.AddRange(new ToolStripItem[] { mniPravnoLice, mniFizickoLice });
            MniKonsignator.Name = "MniKonsignator";
            MniKonsignator.Size = new Size(224, 26);
            MniKonsignator.Text = "Konsignator";
            // 
            // mniPravnoLice
            // 
            mniPravnoLice.Name = "mniPravnoLice";
            mniPravnoLice.Size = new Size(224, 26);
            mniPravnoLice.Text = "Pravno lice";
            // 
            // mniFizickoLice
            // 
            mniFizickoLice.Name = "mniFizickoLice";
            mniFizickoLice.Size = new Size(224, 26);
            mniFizickoLice.Text = "Fizičko lice";
            // 
            // mniDogadjaj
            // 
            mniDogadjaj.Name = "mniDogadjaj";
            mniDogadjaj.Size = new Size(229, 26);
            mniDogadjaj.Text = "Događaj";
            // 
            // mniKategorijaDogadjaja
            // 
            mniKategorijaDogadjaja.Name = "mniKategorijaDogadjaja";
            mniKategorijaDogadjaja.Size = new Size(229, 26);
            mniKategorijaDogadjaja.Text = "Kategorija događaja";
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
            Controls.Add(mnsGlavni);
            MainMenuStrip = mnsGlavni;
            Name = "FrmGlavna";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistem za preprodaju karata";
            FormClosing += FrmGlavna_FormClosing;
            mnsGlavni.ResumeLayout(false);
            mnsGlavni.PerformLayout();
            stsStatus.ResumeLayout(false);
            stsStatus.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip mnsGlavni;
        private ToolStripMenuItem mniDokumenti;
        private ToolStripMenuItem mniListing;
        private ToolStripMenuItem mniKarta;
        private ToolStripMenuItem mniPruzalacUsluge;
        private ToolStripMenuItem mniBroker;
        private ToolStripMenuItem mniPrimalacUsluge;
        private ToolStripMenuItem MniKonsignator;
        private ToolStripMenuItem mniPravnoLice;
        private ToolStripMenuItem mniFizickoLice;
        private ToolStripMenuItem mniSifarnici;
        private ToolStripMenuItem mniDogadjaj;
        private ToolStripMenuItem mniKategorijaDogadjaja;
        private ToolStripMenuItem podešavanjaToolStripMenuItem;
        private ToolStripMenuItem oProgramuToolStripMenuItem;
        private ToolStripMenuItem mniOdjava;
        private StatusStrip stsStatus;
        private ToolStripStatusLabel lblPrijavljeniBroker;
        private Panel pnlSadrzaj;
    }
}