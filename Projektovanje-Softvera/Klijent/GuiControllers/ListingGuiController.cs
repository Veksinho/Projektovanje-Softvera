using Common.Domen;
using Common.Domen.Enumeracije;
using Klijent.UserControls;
using Klijent.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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

        public void PrikaziFormuNova()
        {
            try
            {
                List<Broker> sviBrokeri = Komunikacija.Instance.VratiListuSviBroker();
                List<Konsignator> sviKonsignatori = Komunikacija.Instance.VratiListuSviKonsignator();

                ucListing = new UCListing(FormMode.Add,
                    new Listing { Broker = Session.Instance.LoggedInBroker },
                    sviBrokeri, sviKonsignatori);
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
                ucPretragaListing.CmbKonsignator.DataSource =
                    Komunikacija.Instance.VratiListuSviKonsignator();
                ucPretragaListing.CmbKonsignator.SelectedIndex = -1;
                ucPretragaListing.CmbDogadjaj.DataSource =
                    Komunikacija.Instance.VratiListuSviDogadjaj();
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

                List<Broker> sviBrokeri = Komunikacija.Instance.VratiListuSviBroker();
                List<Konsignator> sviKonsignatori = Komunikacija.Instance.VratiListuSviKonsignator();

                ucListing = new UCListing(FormMode.Details, found, sviBrokeri, sviKonsignatori);
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

                List<Broker> sviBrokeri = Komunikacija.Instance.VratiListuSviBroker();
                List<Konsignator> sviKonsignatori = Komunikacija.Instance.VratiListuSviKonsignator();

                ucListing = new UCListing(FormMode.Edit, found, sviBrokeri, sviKonsignatori);
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
            if (!Validiraj(l)) return;

            try
            {
                Komunikacija.Instance.KreirajListing(l);
                MessageBox.Show("Sistem je zapamtio listing.");
                PrikaziFormuPretraga();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void PromeniListing(object? sender, EventArgs e)
        {
            Listing l = ucListing!.VratiObjekat();
            if (!Validiraj(l)) return;

            try
            {
                Komunikacija.Instance.PromeniListing(l);
                MessageBox.Show("Sistem je zapamtio listing.");
                PrikaziFormuPretraga();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
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
            ucPretragaListing!.PonistiKriterijume();
            OsveziListu();
        }

        private void OsveziListu()
        {
            try
            {
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
                    CenaOd = ParsirajIznos(ucPretragaListing.TxtCenaOd.Text),
                    CenaDo = ParsirajIznos(ucPretragaListing.TxtCenaDo.Text)
                };

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

        private static decimal ParsirajIznos(string tekst) =>
            decimal.TryParse(tekst.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture,
                out decimal iznos) ? iznos : 0m;

        private bool Validiraj(Listing l)
        {
            if (l.Broker == null || l.Konsignator == null || !l.Status.HasValue || !l.Split.HasValue)
            {
                MessageBox.Show("Sva polja moraju biti popunjena!");
                return false;
            }
            if (l.CenaPoKarti < 0)
            {
                MessageBox.Show("Cena po karti nije ispravno uneta!");
                return false;
            }
            if (l.ProcenatProvizije < 0 || l.ProcenatProvizije > 100)
            {
                MessageBox.Show("Procenat provizije mora biti između 0 i 100!");
                return false;
            }
            if (l.Karte.Count == 0)
            {
                MessageBox.Show("Listing mora imati bar jednu kartu!");
                return false;
            }
            if (l.MinKolicina > l.Karte.Count)
            {
                MessageBox.Show("Minimalna količina ne može biti veća od broja karata na listingu!");
                return false;
            }
            return ValidirajSplit(l);
        }

        private static bool ValidirajSplit(Listing l)
        {
            int brojKarata = l.Karte.Count;
            string? poruka = l.Split switch
            {
                TipSplita.bez_splita when l.MinKolicina != brojKarata =>
                    "Kod prodaje bez deljenja minimalna količina mora biti jednaka broju karata!",
                TipSplita.bilo_koja_kolicina when l.MinKolicina != 1 =>
                    "Kod prodaje u bilo kojoj količini minimalna količina mora biti 1!",
                TipSplita.parne_kolicine when brojKarata % 2 != 0 || l.MinKolicina % 2 != 0 =>
                    "Kod prodaje u parnim količinama broj karata i minimalna količina moraju biti parni!",
                TipSplita.min_kolicina when l.MinKolicina <= 1 =>
                    "Kod prodaje sa minimalnom količinom ta količina mora biti veća od 1!",
                TipSplita.izbegni_usamljenu when l.MinKolicina != 1 || brojKarata < 2 =>
                    "Kod izbegavanja usamljene karte listing mora imati bar dve karte, a minimalna količina mora biti 1!",
                _ => null
            };

            if (poruka == null) return true;

            MessageBox.Show(poruka);
            return false;
        }
    }
}
