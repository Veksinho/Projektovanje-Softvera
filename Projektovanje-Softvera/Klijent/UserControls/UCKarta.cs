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
    public partial class UCKarta : UserControl
    {
        private readonly Karta ka;

        public UCKarta(FormMode mode, Karta karta, List<Konsignator> sviKonsignatori, List<Dogadjaj> sviDogadjaji)
        {
            InitializeComponent();
            this.ka = karta;

            cmbKonsignator.DataSource = sviKonsignatori;
            cmbDogadjaj.DataSource = sviDogadjaji;
            cmbTip.DataSource = StavkaEnuma<TipKarte>.GetAll();
            cmbFormat.DataSource = StavkaEnuma<FormatKarte>.GetAll();
            cmbStatus.DataSource = StavkaEnuma<StatusKarte>.GetAll();

            PostaviSelekcije(sviKonsignatori, sviDogadjaji);

            txtSektor.Text = ka.Sektor;
            txtRed.Text = ka.Red;
            txtSediste.Text = ka.Sediste;
            txtNominalnaCena.Text = mode == FormMode.Add ? "" : ka.NominalnaCena.ToString("N2");
            txtListing.Text = ka.ListingPrikaz;

            SrediFormu(mode);
        }

        public Karta VratiObjekat()
        {
            ka.Konsignator = cmbKonsignator.SelectedItem as Konsignator;
            ka.Dogadjaj = cmbDogadjaj.SelectedItem as Dogadjaj;
            ka.Sektor = txtSektor.Text.Trim();
            ka.Red = txtRed.Text.Trim();
            ka.Sediste = txtSediste.Text.Trim();
            ka.NominalnaCena = decimal.TryParse(txtNominalnaCena.Text.Trim(), NumberStyles.Number,
                CultureInfo.CurrentCulture, out decimal cena) ? cena : -1;
            ka.Tip = (cmbTip.SelectedItem as StavkaEnuma<TipKarte>)?.Vrednost;
            ka.Format = (cmbFormat.SelectedItem as StavkaEnuma<FormatKarte>)?.Vrednost;
            ka.Status = (cmbStatus.SelectedItem as StavkaEnuma<StatusKarte>)?.Vrednost;
            return ka;
        }

        private void PostaviSelekcije(List<Konsignator> sviKonsignatori, List<Dogadjaj> sviDogadjaji)
        {
            cmbKonsignator.SelectedIndex = ka.Konsignator == null
                ? -1
                : sviKonsignatori.FindIndex(k => k.IdKonsignator == ka.Konsignator.IdKonsignator);

            cmbDogadjaj.SelectedIndex = ka.Dogadjaj == null
                ? -1
                : sviDogadjaji.FindIndex(d => d.IdDogadjaj == ka.Dogadjaj.IdDogadjaj);

            Izaberi(cmbTip, ka.Tip);
            Izaberi(cmbFormat, ka.Format);
            Izaberi(cmbStatus, ka.Status);
        }

        private static void Izaberi<T>(ComboBox cmb, T? vrednost) where T : struct, Enum
        {
            List<StavkaEnuma<T>> stavke = (List<StavkaEnuma<T>>)cmb.DataSource!;
            cmb.SelectedIndex = vrednost.HasValue
                ? stavke.FindIndex(s => s.Vrednost.Equals(vrednost.Value))
                : -1;
        }

        private void SrediFormu(FormMode mode)
        {

            switch (mode)
            {
                case FormMode.Add:
                    Izaberi<StatusKarte>(cmbStatus, StatusKarte.u_inventaru);
                    cmbStatus.Enabled = false;
                    txtListing.Text = "Nije ni na jednom listingu";
                    btnIzmeni.Visible = false;
                    btnNazad.Visible = false;
                    break;

                case FormMode.Edit:
                    cmbKonsignator.Enabled = false;
                    cmbDogadjaj.Enabled = false;
                    btnKreiraj.Visible = false;
                    break;

                case FormMode.Details:
                    cmbKonsignator.Enabled = false;
                    cmbDogadjaj.Enabled = false;
                    txtSektor.Enabled = false;
                    txtRed.Enabled = false;
                    txtSediste.Enabled = false;
                    txtNominalnaCena.Enabled = false;
                    cmbTip.Enabled = false;
                    cmbFormat.Enabled = false;
                    cmbStatus.Enabled = false;
                    btnKreiraj.Visible = false;
                    btnIzmeni.Visible = false;
                    break;
            }
        }
    }
}
