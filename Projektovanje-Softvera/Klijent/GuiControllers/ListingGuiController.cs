using Common.Domen;
using Common.Domen.Enumeracije;
using Common.Validacija;
using Klijent.UserControls;
using Klijent.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klijent.GuiControllers
{
    internal class ListingGuiController
    {
        private static ListingGuiController? instance;
        public static ListingGuiController Instance => instance ??= new ListingGuiController();
        private ListingGuiController() { }

        private UCListing? ucListing;
        private UCPretragaListing? ucPretragaListing;

        private List<Konsignator> UcitajKonsignatore() => Komunikacija.Instance.VratiListuSviKonsignator();
        private List<Dogadjaj> UcitajDogadjaje() => Komunikacija.Instance.VratiListuSviDogadjaj();
        private List<Broker> UcitajBrokere() => Komunikacija.Instance.VratiListuSviBroker();

        public void PrikaziFormuNova()
        {
            try
            {
                ucListing = new UCListing(FormMode.Add,
                    new Listing { Broker = Session.Instance.LoggedInBroker },
                    UcitajBrokere(), UcitajKonsignatore());
                ucListing.BtnKreiraj.Click += KreirajListing;
                ucListing.CmbKonsignator.SelectedIndexChanged += PromenjenKonsignator;

                MainCoordinator.Instance.ChangePanel(ucListing);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public void PrikaziFormuPretraga()
        {
            try
            {
                ucPretragaListing = new UCPretragaListing();
                ucPretragaListing.CmbKonsignator.DataSource = UcitajKonsignatore();
                ucPretragaListing.CmbKonsignator.SelectedIndex = -1;
                ucPretragaListing.CmbDogadjaj.DataSource = UcitajDogadjaje();
                ucPretragaListing.CmbDogadjaj.SelectedIndex = -1;

                ucPretragaListing.BtnPretrazi.Click += Pretrazi;
                ucPretragaListing.BtnPonisti.Click += Ponisti;
                ucPretragaListing.BtnPrikazi.Click += PrikaziFormuDetalji;
                ucPretragaListing.BtnIzmeni.Click += PrikaziFormuIzmena;
                ucPretragaListing.DgvRezultati.CellDoubleClick += PrikaziFormuDetalji;

                MainCoordinator.Instance.ChangePanel(ucPretragaListing);
                OsveziListu();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private Listing? VratiSelektovani()
        {
            Listing? selected = ucPretragaListing!.DgvRezultati.CurrentRow?.DataBoundItem as Listing;
            if (selected == null)
                MessageBox.Show("Niste izabrali listing!");
            return selected;
        }

        private void PrikaziFormuDetalji(object? sender, EventArgs e)
        {
            Listing? selected = VratiSelektovani();
            if (selected == null) return;

            try
            {
                Listing found = Komunikacija.Instance.PretraziListing(
                    new Listing { IdListing = selected.IdListing });
                MessageBox.Show("Sistem je našao listing.");

                ucListing = new UCListing(FormMode.Details, found, UcitajBrokere(), UcitajKonsignatore());
                ucListing.BtnNazad.Click += Odustani;

                MainCoordinator.Instance.ChangePanel(ucListing);
                UcitajSlobodneKarte();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void PrikaziFormuIzmena(object? sender, EventArgs e)
        {
            Listing? selected = VratiSelektovani();
            if (selected == null) return;

            try
            {
                Listing found = Komunikacija.Instance.PretraziListing(
                    new Listing { IdListing = selected.IdListing });
                MessageBox.Show("Sistem je našao listing.");

                ucListing = new UCListing(FormMode.Edit, found, UcitajBrokere(), UcitajKonsignatore());
                ucListing.BtnIzmeni.Click += PromeniListing;
                ucListing.BtnNazad.Click += Odustani;

                MainCoordinator.Instance.ChangePanel(ucListing);
                UcitajSlobodneKarte();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void KreirajListing(object? sender, EventArgs e)
        {
            Listing l = ucListing!.VratiObjekat();
            if (!Validiraj(l, novi: true)) return;

            try
            {
                Komunikacija.Instance.KreirajListing(l);
                MessageBox.Show("Sistem je zapamtio listing.");
                PrikaziFormuPretraga();
            }
            catch (Exception ex) { 
                MessageBox.Show("Sistem ne može da zapamti listing.\n" + ex.Message);
            }
        }

        private void PromeniListing(object? sender, EventArgs e)
        {
            Listing l = ucListing!.VratiObjekat();
            if (!Validiraj(l, novi: false)) return;

            try
            {
                Komunikacija.Instance.PromeniListing(l);
                MessageBox.Show("Sistem je zapamtio listing.");
                PrikaziFormuPretraga();
            }
            catch (Exception ex) {
                MessageBox.Show("Sistem ne može da zapamti listing.\n" + ex.Message);
            }
        }

        private void PromenjenKonsignator(object? sender, EventArgs e)
        {
            if (ucListing == null) return;

            if (ucListing.DgvKarteNaListingu.Rows.Count > 0)
            {
                MessageBox.Show("Promenom konsignatora karte su uklonjene sa listinga.");
                ucListing.IsprazniListing();
            }

            UcitajSlobodneKarte();
        }

        private void UcitajSlobodneKarte()
        {
            if (ucListing!.CmbKonsignator.SelectedItem is not Konsignator konsignator) return;

            try
            {
                List<Karta> slobodne = Komunikacija.Instance.VratiListuKarta(
                    new Karta { Konsignator = konsignator, SamoSlobodne = true });
                ucListing.PrikaziSlobodneKarte(slobodne);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Pretrazi(object? sender, EventArgs e) => OsveziListu();
        private void Odustani(object? sender, EventArgs e) => PrikaziFormuPretraga();

        private void Ponisti(object? sender, EventArgs e)
        {
            ucPretragaListing!.CmbStatus.SelectedIndex = -1;
            ucPretragaListing!.CmbSplit.SelectedIndex = -1;
            ucPretragaListing!.CmbTipKarte.SelectedIndex = -1;
            ucPretragaListing!.CmbKonsignator.SelectedIndex = -1;
            ucPretragaListing!.CmbDogadjaj.SelectedIndex = -1;
            ucPretragaListing!.DtpObjavljenOd.Checked = false;
            ucPretragaListing!.DtpObjavljenDo.Checked = false;
            ucPretragaListing!.TxtCenaOd.Clear();
            ucPretragaListing!.TxtCenaDo.Clear();
            ucPretragaListing!.TxtNazivKonsignatora.Clear();
            ucPretragaListing!.TxtMesto.Clear();
            ucPretragaListing!.TxtSektor.Clear();
            OsveziListu();
        }

        private void OsveziListu()
        {
            try
            {
                var klijentske = new List<GreskaValidacije>();
                decimal cenaOd = UnosPolja.ProveriIznos(klijentske, ucPretragaListing!.TxtCenaOd.Text,
                    nameof(Listing.CenaOd), null, "Cena od nije u odgovarajućem formatu.");
                decimal cenaDo = UnosPolja.ProveriIznos(klijentske, ucPretragaListing.TxtCenaDo.Text,
                    nameof(Listing.CenaDo), null, "Cena do nije u odgovarajućem formatu.");

                Listing kriterijum = new Listing
                {
                    Broker = Session.Instance.LoggedInBroker,
                    Status = (ucPretragaListing!.CmbStatus.SelectedItem
                        as StavkaEnuma<StatusListinga>)?.Vrednost,
                    Split = (ucPretragaListing.CmbSplit.SelectedItem
                        as StavkaEnuma<TipSplita>)?.Vrednost,
                    DatumObjaveOd = ucPretragaListing.DtpObjavljenOd.Checked
                        ? ucPretragaListing.DtpObjavljenOd.Value.Date
                        : default,
                    DatumObjaveDo = ucPretragaListing.DtpObjavljenDo.Checked
                        ? ucPretragaListing.DtpObjavljenDo.Value.Date
                        : default,
                    CenaOd = cenaOd,
                    CenaDo = cenaDo
                };

                List<GreskaValidacije> greske = ProveraUnosa.Spoji(klijentske,
                    PravilaListing.ProveriKriterijum(kriterijum));
                if (!ucPretragaListing.ProveraUnosa.Prikazi(greske, ucPretragaListing.Polja))
                    return;

                Konsignator? konsignator = ucPretragaListing.CmbKonsignator.SelectedItem as Konsignator;
                string nazivKonsignatora = ucPretragaListing.TxtNazivKonsignatora.Text.Trim();
                if (konsignator != null || !string.IsNullOrWhiteSpace(nazivKonsignatora))
                    kriterijum.Konsignator = new FizickoLice
                    {
                        IdKonsignator = konsignator == null ? 0 : konsignator.IdKonsignator,
                        NazivKriterijum = nazivKonsignatora
                    };

                Dogadjaj? dogadjaj = ucPretragaListing.CmbDogadjaj.SelectedItem as Dogadjaj;
                string mesto = ucPretragaListing.TxtMesto.Text.Trim();
                if (dogadjaj != null || !string.IsNullOrWhiteSpace(mesto))
                    kriterijum.KriterijumDogadjaj = new Dogadjaj
                    {
                        IdDogadjaj = dogadjaj == null ? 0 : dogadjaj.IdDogadjaj,
                        Mesto = mesto
                    };

                string sektor = ucPretragaListing.TxtSektor.Text.Trim();
                TipKarte? tipKarte = (ucPretragaListing.CmbTipKarte.SelectedItem
                    as StavkaEnuma<TipKarte>)?.Vrednost;
                if (!string.IsNullOrWhiteSpace(sektor) || tipKarte.HasValue)
                    kriterijum.KriterijumKarta = new Karta { Sektor = sektor, Tip = tipKarte };

                List<Listing> lista = Komunikacija.Instance.VratiListuListing(kriterijum);

                ucPretragaListing.DgvRezultati.DataSource = new BindingList<Listing>(lista);
                if (lista.Count == 0)
                    MessageBox.Show("Sistem ne može da nađe listinge po zadatim kriterijumima.");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private bool Validiraj(Listing l, bool novi)
        {
            var klijentske = new List<GreskaValidacije>();
            UnosPolja.ProveriIznos(klijentske, ucListing!.TxtCenaPoKarti.Text, nameof(Listing.CenaPoKarti),
                "Cena po karti listinga je obavezna.",
                "Cena po karti listinga nije u odgovarajućem formatu.");
            UnosPolja.ProveriIznos(klijentske, ucListing.TxtProcenatProvizije.Text, nameof(Listing.ProcenatProvizije),
                "Procenat provizije listinga je obavezan.",
                "Procenat provizije listinga nije u odgovarajućem formatu.");

            List<GreskaValidacije> greske = ProveraUnosa.Spoji(klijentske, PravilaListing.Proveri(l, novi));
            return ucListing.ProveraUnosa.Prikazi(greske, ucListing.Polja);
        }
    }
}
