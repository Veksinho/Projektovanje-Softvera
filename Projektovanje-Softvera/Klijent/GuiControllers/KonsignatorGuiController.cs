using Common.Domen;
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
    internal class KonsignatorGuiController
    {
        private static KonsignatorGuiController? instance;

        public static KonsignatorGuiController Instance
        {
            get
            {
                instance ??= new KonsignatorGuiController();
                return instance;
            }
        }

        private KonsignatorGuiController()
        {
        }

        private UCKonsignator? ucKonsignator;
        private UCPretragaKonsignator? ucPretragaKonsignator;

        public void PrikaziFormuNova()
        {
            ucKonsignator = new UCKonsignator(FormMode.Add, new FizickoLice());
            ucKonsignator.BtnKreiraj.Click += KreirajKonsignator;

            MainCoordinator.Instance.ChangePanel(ucKonsignator);
        }

        public void PrikaziFormuPretraga()
        {
            ucPretragaKonsignator = new UCPretragaKonsignator();

            ucPretragaKonsignator.CmbTipKonsignatora.DataSource = StavkaTipKonsignatora.GetAll();
            ucPretragaKonsignator.CmbTipKonsignatora.SelectedIndex = -1;

            ucPretragaKonsignator.BtnPretrazi.Click += Pretrazi;
            ucPretragaKonsignator.BtnPonisti.Click += Ponisti;
            ucPretragaKonsignator.BtnPrikazi.Click += PrikaziFormuDetalji;
            ucPretragaKonsignator.BtnIzmeni.Click += PrikaziFormuIzmena;
            ucPretragaKonsignator.BtnObrisi.Click += ObrisiKonsignator;
            ucPretragaKonsignator.DgvRezultati.CellDoubleClick += PrikaziFormuDetalji;

            MainCoordinator.Instance.ChangePanel(ucPretragaKonsignator);

            OsveziListu();
        }

        private void PrikaziFormuDetalji(object? sender, EventArgs e)
        {
            Konsignator? selected = VratiSelektovanog();

            if (selected == null) return;

            try
            {
                Konsignator found = Komunikacija.Instance.PretraziKonsignator(
                    new Konsignator { IdKonsignator = selected.IdKonsignator });

                MessageBox.Show("Sistem je našao konsignatora.");

                ucKonsignator = new UCKonsignator(FormMode.Details, found);
                ucKonsignator.BtnNazad.Click += Odustani;

                MainCoordinator.Instance.ChangePanel(ucKonsignator);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PrikaziFormuIzmena(object? sender, EventArgs e)
        {
            Konsignator? selected = VratiSelektovanog();

            if (selected == null) return;

            try
            {
                Konsignator found = Komunikacija.Instance.PretraziKonsignator(
                    new Konsignator { IdKonsignator = selected.IdKonsignator });

                MessageBox.Show("Sistem je našao konsignatora.");

                ucKonsignator = new UCKonsignator(FormMode.Edit, found);
                ucKonsignator.BtnIzmeni.Click += PromeniKonsignator;
                ucKonsignator.BtnNazad.Click += Odustani;

                MainCoordinator.Instance.ChangePanel(ucKonsignator);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void KreirajKonsignator(object? sender, EventArgs e)
        {
            Konsignator k = ucKonsignator!.VratiObjekat();

            if (!Validiraj(k)) return;

            try
            {
                Komunikacija.Instance.KreirajKonsignator(k);
                MessageBox.Show("Sistem je zapamtio konsignatora.");
                PrikaziFormuPretraga();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PromeniKonsignator(object? sender, EventArgs e)
        {
            Konsignator k = ucKonsignator!.VratiObjekat();

            if (!Validiraj(k)) return;

            try
            {
                Komunikacija.Instance.PromeniKonsignator(k);
                MessageBox.Show("Sistem je zapamtio konsignatora.");
                PrikaziFormuPretraga();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ObrisiKonsignator(object? sender, EventArgs e)
        {
            Konsignator? selected = VratiSelektovanog();

            if (selected == null) return;

            if (MessageBox.Show($"Da li želite da obrišete konsignatora '{selected.Name}'?",
                    "Potvrda", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            try
            {
                Konsignator found = Komunikacija.Instance.PretraziKonsignator(
                    new Konsignator { IdKonsignator = selected.IdKonsignator });

                MessageBox.Show("Sistem je našao konsignatora.");

                Komunikacija.Instance.ObrisiKonsignator(found);
                MessageBox.Show("Sistem je obrisao konsignatora.");
                OsveziListu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Pretrazi(object? sender, EventArgs e)
        {
            OsveziListu();
        }

        private void Ponisti(object? sender, EventArgs e)
        {
            ucPretragaKonsignator!.CmbTipKonsignatora.SelectedIndex = -1;
            ucPretragaKonsignator.TxtNaziv.Clear();
            ucPretragaKonsignator.TxtEmail.Clear();
            ucPretragaKonsignator.TxtTelefon.Clear();

            OsveziListu();
        }

        private void Odustani(object? sender, EventArgs e)
        {
            PrikaziFormuPretraga();
        }

        private void OsveziListu()
        {
            try
            {
                var izabraniTip = ucPretragaKonsignator!.CmbTipKonsignatora.SelectedItem
                    as StavkaTipKonsignatora;

                Konsignator kriterijum = new Konsignator
                {
                    NazivKriterijum = ucPretragaKonsignator.TxtNaziv.Text.Trim(),
                    Email = ucPretragaKonsignator.TxtEmail.Text.Trim(),
                    Telefon = ucPretragaKonsignator.TxtTelefon.Text.Trim(),
                    TipKriterijum = izabraniTip?.Tip
                };

                bool bezKriterijuma =
                    string.IsNullOrWhiteSpace(kriterijum.NazivKriterijum)
                    && string.IsNullOrWhiteSpace(kriterijum.Email)
                    && string.IsNullOrWhiteSpace(kriterijum.Telefon)
                    && izabraniTip == null;

                List<Konsignator> lista = bezKriterijuma
                    ? Komunikacija.Instance.VratiListuSviKonsignator()
                    : Komunikacija.Instance.VratiListuKonsignator(kriterijum);

                ucPretragaKonsignator.DgvRezultati.DataSource =
                    new BindingList<Konsignator>(lista);

                if (lista.Count == 0)
                    MessageBox.Show("Sistem ne može da nađe konsignatore po zadatim kriterijumima.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Konsignator? VratiSelektovanog()
        {
            Konsignator? selected = ucPretragaKonsignator!.DgvRezultati.CurrentRow?.DataBoundItem
                as Konsignator;

            if (selected == null)
                MessageBox.Show("Niste izabrali konsignatora!");

            return selected;
        }

        private bool Validiraj(Konsignator k)
        {
            if (string.IsNullOrWhiteSpace(k.Email)
                || string.IsNullOrWhiteSpace(k.Telefon)
                || string.IsNullOrWhiteSpace(k.Adresa))
            {
                MessageBox.Show("Sva polja moraju biti popunjena!");
                return false;
            }

            if (!k.Email.Contains('@'))
            {
                MessageBox.Show("E-mail nije u odgovarajućem formatu!");
                return false;
            }

            if (k is FizickoLice fl)
            {
                if (string.IsNullOrWhiteSpace(fl.Jmbg)
                    || string.IsNullOrWhiteSpace(fl.Ime)
                    || string.IsNullOrWhiteSpace(fl.Prezime)
                    || string.IsNullOrWhiteSpace(fl.BrojLicneKarte))
                {
                    MessageBox.Show("Sva polja moraju biti popunjena!");
                    return false;
                }
            }
            else if (k is PravnoLice pl)
            {
                if (string.IsNullOrWhiteSpace(pl.NazivFirme)
                    || string.IsNullOrWhiteSpace(pl.Pib)
                    || string.IsNullOrWhiteSpace(pl.MaticniBroj))
                {
                    MessageBox.Show("Sva polja moraju biti popunjena!");
                    return false;
                }
            }

            return true;
        }
    }
}
