using Common.Domen;
using Common.Domen.Enumeracije;
using Klijent.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Klijent.UserControls
{
    public partial class UCListing : UserControl
    {
        private readonly Listing l;
        private readonly List<Broker> sviBrokeri;
        private readonly List<Konsignator> sviKonsignatori;
        private readonly BindingList<Karta> slobodneKarte = new BindingList<Karta>();
        private readonly BindingList<Karta> karteNaListingu = new BindingList<Karta>();

        public UCListing(FormMode mode, Listing listing, List<Broker> sviBrokeri,
            List<Konsignator> sviKonsignatori)
        {
            InitializeComponent();

            this.l = listing;
            this.sviBrokeri = sviBrokeri;
            this.sviKonsignatori = sviKonsignatori;

            cmbBroker.DataSource = sviBrokeri;
            cmbKonsignator.DataSource = sviKonsignatori;
            cmbSplit.DataSource = StavkaEnuma<TipSplita>.GetAll();
            cmbStatus.DataSource = mode == FormMode.Add
                ? StavkaEnuma<StatusListinga>.GetAll()
                    .Where(s => s.Vrednost == StatusListinga.nacrt
                             || s.Vrednost == StatusListinga.objavljen).ToList()
                : StavkaEnuma<StatusListinga>.GetAll();

            txtId.Text = mode == FormMode.Add ? "" : l.IdListing.ToString();

            cmbBroker.SelectedIndex = l.Broker == null ? -1
                : sviBrokeri.FindIndex(b => b.IdBroker == l.Broker.IdBroker);
            cmbKonsignator.SelectedIndex = l.Konsignator == null ? -1
                : sviKonsignatori.FindIndex(k => k.IdKonsignator == l.Konsignator.IdKonsignator);

            Izaberi(cmbStatus, mode == FormMode.Add ? StatusListinga.nacrt : l.Status);
            Izaberi(cmbSplit, l.Split);

            dtpDatumObjave.Value = l.DatumObjave == default ? DateTime.Today : l.DatumObjave;
            dtpDatumIsteka.Value = l.DatumIsteka == default
                ? DateTime.Today.AddDays(1)
                : l.DatumIsteka;

            txtCenaPoKarti.Text = mode == FormMode.Add ? "" : l.CenaPoKarti.ToString("N2");
            txtProcenatProvizije.Text = mode == FormMode.Add
                ? ""
                : l.ProcenatProvizije.ToString("N2");
            numMinKolicina.Value = l.MinKolicina < numMinKolicina.Minimum
                ? numMinKolicina.Minimum
                : l.MinKolicina;
            txtNapomena.Text = l.Napomena ?? "";

            foreach (Karta karta in l.Karte)
                karteNaListingu.Add(karta);

            SrediTabelu(dgvSlobodneKarte);
            SrediTabelu(dgvKarteNaListingu);
            dgvSlobodneKarte.DataSource = slobodneKarte;
            dgvKarteNaListingu.DataSource = karteNaListingu;

            btnDodajKartu.Click += DodajKartu;
            btnUkloniKartu.Click += UkloniKartu;
            txtCenaPoKarti.TextChanged += OsveziPrikazIznosa;
            txtProcenatProvizije.TextChanged += OsveziPrikazIznosa;

            OsveziPrikaz();
            SrediFormu(mode);
        }

        public void PrikaziSlobodneKarte(List<Karta> karte)
        {
            slobodneKarte.Clear();

            foreach (Karta karta in karte)
                if (!karteNaListingu.Any(k => k.IdKarta == karta.IdKarta))
                    slobodneKarte.Add(karta);

            OsveziPrikaz();
        }

        public void IsprazniListing()
        {
            karteNaListingu.Clear();
            OsveziPrikaz();
        }

        public Listing VratiObjekat()
        {
            l.Broker = cmbBroker.SelectedItem as Broker;
            l.Konsignator = cmbKonsignator.SelectedItem as Konsignator;
            l.Status = (cmbStatus.SelectedItem as StavkaEnuma<StatusListinga>)?.Vrednost;
            l.Split = (cmbSplit.SelectedItem as StavkaEnuma<TipSplita>)?.Vrednost;
            l.DatumIsteka = dtpDatumIsteka.Value.Date;
            l.CenaPoKarti = decimal.TryParse(txtCenaPoKarti.Text.Trim(), NumberStyles.Number,
                CultureInfo.CurrentCulture, out decimal cena) ? cena : -1;
            l.ProcenatProvizije = decimal.TryParse(txtProcenatProvizije.Text.Trim(),
                NumberStyles.Number, CultureInfo.CurrentCulture, out decimal provizija)
                ? provizija : -1;
            l.MinKolicina = (int)numMinKolicina.Value;
            l.Napomena = string.IsNullOrWhiteSpace(txtNapomena.Text) ? null : txtNapomena.Text.Trim();
            l.Karte = karteNaListingu.ToList();

            return l;
        }

        private void DodajKartu(object? sender, EventArgs e)
        {
            if (dgvSlobodneKarte.CurrentRow?.DataBoundItem is not Karta karta)
                return;

            slobodneKarte.Remove(karta);
            karteNaListingu.Add(karta);
            OsveziPrikaz();
        }

        private void UkloniKartu(object? sender, EventArgs e)
        {
            if (dgvKarteNaListingu.CurrentRow?.DataBoundItem is not Karta karta)
                return;

            karteNaListingu.Remove(karta);
            slobodneKarte.Add(karta);
            OsveziPrikaz();
        }

        private void OsveziPrikazIznosa(object? sender, EventArgs e) => OsveziPrikaz();

        private void OsveziPrikaz()
        {
            decimal cena = decimal.TryParse(txtCenaPoKarti.Text.Trim(), NumberStyles.Number,
                CultureInfo.CurrentCulture, out decimal c) ? c : 0m;
            decimal procenat = decimal.TryParse(txtProcenatProvizije.Text.Trim(),
                NumberStyles.Number, CultureInfo.CurrentCulture, out decimal p) ? p : 0m;
            decimal ukupno = cena * karteNaListingu.Count;

            lblBrojSlobodnih.Text = $"Slobodnih: {slobodneKarte.Count}";
            lblBrojNaListingu.Text = $"Na listingu: {karteNaListingu.Count}";
            lblUkupno.Text = $"Karata: {karteNaListingu.Count}     " +
                             $"Ukupna cena: {ukupno:N2}     " +
                             $"Provizija: {ukupno * procenat / 100m:N2}";
        }

        private static void Izaberi<T>(ComboBox cmb, T? vrednost) where T : struct, Enum
        {
            var stavke = (List<StavkaEnuma<T>>)cmb.DataSource!;
            cmb.SelectedIndex = vrednost.HasValue
                ? stavke.FindIndex(s => s.Vrednost.Equals(vrednost.Value))
                : -1;
        }

        private static void SrediTabelu(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colIdKarta",
                HeaderText = "Id",
                DataPropertyName = nameof(Karta.IdKarta),
                FillWeight = 10
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDogadjaj",
                HeaderText = "Događaj",
                DataPropertyName = nameof(Karta.Dogadjaj),
                FillWeight = 32
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSektor",
                HeaderText = "Sektor",
                DataPropertyName = nameof(Karta.Sektor),
                FillWeight = 15
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colRed",
                HeaderText = "Red",
                DataPropertyName = nameof(Karta.Red),
                FillWeight = 10
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSediste",
                HeaderText = "Sedište",
                DataPropertyName = nameof(Karta.Sediste),
                FillWeight = 13
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNominalnaCena",
                HeaderText = "Nominalna cena",
                DataPropertyName = nameof(Karta.NominalnaCena),
                FillWeight = 20,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.RowHeadersVisible = false;
        }

        private void SrediFormu(FormMode mode)
        {
            cmbBroker.Enabled = false;
            dtpDatumObjave.Enabled = false;

            switch (mode)
            {
                case FormMode.Add:
                    dtpDatumIsteka.MinDate = DateTime.Today.AddDays(1);
                    btnIzmeni.Visible = false;
                    btnNazad.Visible = false;
                    break;
                case FormMode.Edit:
                    cmbKonsignator.Enabled = false;
                    btnKreiraj.Visible = false;
                    break;
                case FormMode.Details:
                    btnKreiraj.Visible = false;
                    btnIzmeni.Visible = false;
                    cmbKonsignator.Enabled = false;
                    cmbStatus.Enabled = false;
                    cmbSplit.Enabled = false;
                    dtpDatumIsteka.Enabled = false;
                    txtCenaPoKarti.Enabled = false;
                    txtProcenatProvizije.Enabled = false;
                    numMinKolicina.Enabled = false;
                    txtNapomena.Enabled = false;
                    btnDodajKartu.Enabled = false;
                    btnUkloniKartu.Enabled = false;
                    break;
            }
        }
    }
}
