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
    internal class KartaGuiController
    {
        private static KartaGuiController? instance;
        public static KartaGuiController Instance => instance ??= new KartaGuiController();
        private KartaGuiController() { }

        private UCKarta? ucKarta;
        private UCPretragaKarta? ucPretragaKarta;

        private List<Konsignator> UcitajKonsignatore() => Komunikacija.Instance.VratiListuSviKonsignator();
        private List<Dogadjaj> UcitajDogadjaje() => Komunikacija.Instance.VratiListuSviDogadjaj();

        public void PrikaziFormuNova()
        {
            try
            {
                ucKarta = new UCKarta(FormMode.Add, new Karta(), UcitajKonsignatore(), UcitajDogadjaje());
                ucKarta.BtnKreiraj.Click += KreirajKarta;
                MainCoordinator.Instance.ChangePanel(ucKarta);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public void PrikaziFormuPretraga()
        {
            try
            {
                ucPretragaKarta = new UCPretragaKarta();
                ucPretragaKarta.CmbKonsignator.DataSource = UcitajKonsignatore();
                ucPretragaKarta.CmbKonsignator.SelectedIndex = -1;
                ucPretragaKarta.CmbDogadjaj.DataSource = UcitajDogadjaje();
                ucPretragaKarta.CmbDogadjaj.SelectedIndex = -1;

                ucPretragaKarta.BtnPretrazi.Click += Pretrazi;
                ucPretragaKarta.BtnPonisti.Click += Ponisti;
                ucPretragaKarta.BtnPrikazi.Click += PrikaziFormuDetalji;
                ucPretragaKarta.BtnIzmeni.Click += PrikaziFormuIzmena;
                ucPretragaKarta.DgvRezultati.CellDoubleClick += PrikaziFormuDetalji;

                MainCoordinator.Instance.ChangePanel(ucPretragaKarta);
                OsveziListu();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private Karta? VratiSelektovanu()
        {
            Karta? selected = ucPretragaKarta!.DgvRezultati.CurrentRow?.DataBoundItem as Karta;
            if (selected == null)
                MessageBox.Show("Niste izabrali kartu!");
            return selected;
        }

        private void PrikaziFormuDetalji(object? sender, EventArgs e)
        {
            Karta? selected = VratiSelektovanu();
            if (selected == null) return;

            try
            {
                Karta found = Komunikacija.Instance.PretraziKarta(new Karta { IdKarta = selected.IdKarta });
                MessageBox.Show("Sistem je našao kartu.");

                ucKarta = new UCKarta(FormMode.Details, found, UcitajKonsignatore(), UcitajDogadjaje());
                ucKarta.BtnNazad.Click += Odustani;
                MainCoordinator.Instance.ChangePanel(ucKarta);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void PrikaziFormuIzmena(object? sender, EventArgs e)
        {
            Karta? selected = VratiSelektovanu();
            if (selected == null) return;

            try
            {
                Karta found = Komunikacija.Instance.PretraziKarta(new Karta { IdKarta = selected.IdKarta });
                MessageBox.Show("Sistem je našao kartu.");

                ucKarta = new UCKarta(FormMode.Edit, found, UcitajKonsignatore(), UcitajDogadjaje());
                ucKarta.BtnIzmeni.Click += PromeniKarta;
                ucKarta.BtnNazad.Click += Odustani;
                MainCoordinator.Instance.ChangePanel(ucKarta);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void KreirajKarta(object? sender, EventArgs e)
        {
            if (!Validiraj()) return;
            Karta ka = ucKarta!.VratiObjekat();

            try
            {
                Komunikacija.Instance.KreirajKarta(ka);
                MessageBox.Show("Sistem je zapamtio kartu.");
                PrikaziFormuPretraga();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void PromeniKarta(object? sender, EventArgs e)
        {
            if (!Validiraj()) return;
            Karta ka = ucKarta!.VratiObjekat();

            try
            {
                Komunikacija.Instance.PromeniKarta(ka);
                MessageBox.Show("Sistem je zapamtio kartu.");
                PrikaziFormuPretraga();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Pretrazi(object? sender, EventArgs e) => OsveziListu();
        private void Odustani(object? sender, EventArgs e) => PrikaziFormuPretraga();

        private void Ponisti(object? sender, EventArgs e)
        {
            ucPretragaKarta!.TxtSektor.Clear();
            ucPretragaKarta.CmbTip.SelectedIndex = -1;
            ucPretragaKarta.CmbStatus.SelectedIndex = -1;
            ucPretragaKarta.CmbKonsignator.SelectedIndex = -1;
            ucPretragaKarta.TxtNazivKonsignatora.Clear();
            ucPretragaKarta.CmbDogadjaj.SelectedIndex = -1;
            ucPretragaKarta.TxtMesto.Clear();
            OsveziListu();
        }

        private void OsveziListu()
        {
            try
            {
                string sektor = ucPretragaKarta!.TxtSektor.Text.Trim();
                string nazivKonsignatora = ucPretragaKarta.TxtNazivKonsignatora.Text.Trim();
                string mesto = ucPretragaKarta.TxtMesto.Text.Trim();

                StavkaEnuma<TipKarte>? izabraniTip =
                    ucPretragaKarta.CmbTip.SelectedItem as StavkaEnuma<TipKarte>;
                StavkaEnuma<StatusKarte>? izabraniStatus =
                    ucPretragaKarta.CmbStatus.SelectedItem as StavkaEnuma<StatusKarte>;
                Konsignator? izabraniKonsignator =
                    ucPretragaKarta.CmbKonsignator.SelectedItem as Konsignator;
                Dogadjaj? izabraniDogadjaj =
                    ucPretragaKarta.CmbDogadjaj.SelectedItem as Dogadjaj;

                bool bezKriterijuma =
                    string.IsNullOrWhiteSpace(sektor)
                    && string.IsNullOrWhiteSpace(nazivKonsignatora)
                    && string.IsNullOrWhiteSpace(mesto)
                    && izabraniTip == null
                    && izabraniStatus == null
                    && izabraniKonsignator == null
                    && izabraniDogadjaj == null;

                List<Karta> lista;

                if (bezKriterijuma)
                {
                    lista = Komunikacija.Instance.VratiListuSviKarta();
                }
                else
                {
                    Karta kriterijum = new Karta
                    {
                        Sektor = sektor,
                        Tip = izabraniTip?.Vrednost,
                        Status = izabraniStatus?.Vrednost
                    };

                    if (izabraniKonsignator != null || !string.IsNullOrWhiteSpace(nazivKonsignatora))
                    {
                        kriterijum.Konsignator = new Konsignator
                        {
                            IdKonsignator = izabraniKonsignator == null ? 0 : izabraniKonsignator.IdKonsignator,
                            NazivKriterijum = nazivKonsignatora
                        };
                    }

                    if (izabraniDogadjaj != null || !string.IsNullOrWhiteSpace(mesto))
                    {
                        kriterijum.Dogadjaj = new Dogadjaj
                        {
                            IdDogadjaj = izabraniDogadjaj == null ? 0 : izabraniDogadjaj.IdDogadjaj,
                            Mesto = mesto
                        };
                    }

                    lista = Komunikacija.Instance.VratiListuKarta(kriterijum);
                }

                ucPretragaKarta.DgvRezultati.DataSource = new BindingList<Karta>(lista);
                if (lista.Count == 0)
                    MessageBox.Show("Sistem ne može da nađe karte po zadatim kriterijumima.");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private bool Validiraj()
        {
            if (ucKarta!.CmbKonsignator.SelectedItem == null || ucKarta.CmbDogadjaj.SelectedItem == null
                || string.IsNullOrWhiteSpace(ucKarta.TxtSektor.Text)
                || string.IsNullOrWhiteSpace(ucKarta.TxtRed.Text)
                || string.IsNullOrWhiteSpace(ucKarta.TxtSediste.Text)
                || string.IsNullOrWhiteSpace(ucKarta.TxtNominalnaCena.Text)
                || ucKarta.CmbTip.SelectedItem == null
                || ucKarta.CmbFormat.SelectedItem == null
                || ucKarta.CmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Sva polja moraju biti popunjena!");
                return false;
            }

            if (!decimal.TryParse(ucKarta.TxtNominalnaCena.Text.Trim(), NumberStyles.Number,
                    CultureInfo.CurrentCulture, out decimal cena))
            {
                MessageBox.Show("Nominalna cena karte nije u odgovarajućem formatu.");
                return false;
            }

            if (cena < 0)
            {
                MessageBox.Show("Nominalna cena karte ne može biti negativna!");
                return false;
            }

            return true;
        }
    }
}
