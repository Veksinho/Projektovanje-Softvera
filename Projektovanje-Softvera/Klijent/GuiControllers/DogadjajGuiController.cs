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
    internal class DogadjajGuiController
    {
        private static DogadjajGuiController? instance;

        public static DogadjajGuiController Instance
        {
            get
            {
                instance ??= new DogadjajGuiController();
                return instance;
            }
        }
        private DogadjajGuiController() { }

        private UCDogadjaj? ucDogadjaj;
        private UCPretragaDogadjaj? ucPretragaDogadjaj;

        public void PrikaziFormuNova()
        {
            ucDogadjaj = new UCDogadjaj(FormMode.Add, new Dogadjaj());
            ucDogadjaj.BtnKreiraj.Click += UbaciDogadjaj;
            MainCoordinator.Instance.ChangePanel(ucDogadjaj);
        }

        public void PrikaziFormuPretraga()
        {
            ucPretragaDogadjaj = new UCPretragaDogadjaj();
            ucPretragaDogadjaj.BtnPretrazi.Click += Pretrazi;
            ucPretragaDogadjaj.BtnPonisti.Click += Ponisti;
            ucPretragaDogadjaj.BtnPrikazi.Click += PrikaziFormuDetalji;
            ucPretragaDogadjaj.BtnIzmeni.Click += PrikaziFormuIzmena;
            ucPretragaDogadjaj.BtnObrisi.Click += ObrisiDogadjaj;
            ucPretragaDogadjaj.DgvRezultati.CellDoubleClick += PrikaziFormuDetalji;
            MainCoordinator.Instance.ChangePanel(ucPretragaDogadjaj);
            OsveziListu();
        }

        private void PrikaziFormuDetalji(object? sender, EventArgs e)
        {
            Dogadjaj? selected = ucPretragaDogadjaj!.DgvRezultati.CurrentRow?.DataBoundItem as Dogadjaj;
            if (selected == null) { MessageBox.Show("Niste izabrali događaj!"); return; }

            try
            {
                Dogadjaj found = Komunikacija.Instance.PretraziDogadjaj(
                    new Dogadjaj { IdDogadjaj = selected.IdDogadjaj });

                MessageBox.Show("Sistem je našao događaj.");

                ucDogadjaj = new UCDogadjaj(FormMode.Details, found);
                ucDogadjaj.BtnNazad.Click += Odustani;
                MainCoordinator.Instance.ChangePanel(ucDogadjaj);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void PrikaziFormuIzmena(object? sender, EventArgs e)
        {
            Dogadjaj? selected = ucPretragaDogadjaj!.DgvRezultati.CurrentRow?.DataBoundItem as Dogadjaj;
            if (selected == null) { MessageBox.Show("Niste izabrali događaj!"); return; }

            try
            {
                Dogadjaj found = Komunikacija.Instance.PretraziDogadjaj(
                    new Dogadjaj { IdDogadjaj = selected.IdDogadjaj });

                MessageBox.Show("Sistem je našao događaj.");

                ucDogadjaj = new UCDogadjaj(FormMode.Edit, found);
                ucDogadjaj.BtnIzmeni.Click += PromeniDogadjaj;
                ucDogadjaj.BtnNazad.Click += Odustani;
                MainCoordinator.Instance.ChangePanel(ucDogadjaj);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void UbaciDogadjaj(object? sender, EventArgs e)
        {
            Dogadjaj d = ucDogadjaj!.VratiObjekat();
            if (!Validiraj(d)) return;
            try
            {
                Komunikacija.Instance.UbaciDogadjaj(d);
                MessageBox.Show("Sistem je zapamtio događaj.");
                PrikaziFormuPretraga();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void PromeniDogadjaj(object? sender, EventArgs e)
        {
            Dogadjaj d = ucDogadjaj!.VratiObjekat();
            if (!Validiraj(d)) return;
            try
            {
                Komunikacija.Instance.PromeniDogadjaj(d);
                MessageBox.Show("Sistem je zapamtio događaj.");
                PrikaziFormuPretraga();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ObrisiDogadjaj(object? sender, EventArgs e)
        {
            Dogadjaj? izabran = ucPretragaDogadjaj!.DgvRezultati.CurrentRow?.DataBoundItem as Dogadjaj;
            if (izabran == null) { MessageBox.Show("Niste izabrali događaj!"); return; }

            if (MessageBox.Show($"Da li želite da obrišete događaj '{izabran.Naziv}'?", "Potvrda",
                    MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try
            {
                Komunikacija.Instance.ObrisiDogadjaj(izabran);
                MessageBox.Show("Sistem je obrisao događaj.");
                OsveziListu();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Pretrazi(object? sender, EventArgs e)
        {
            OsveziListu();
        }

        private void Ponisti(object? sender, EventArgs e)
        {
            ucPretragaDogadjaj!.TxtNaziv.Clear();
            ucPretragaDogadjaj.TxtMesto.Clear();
            ucPretragaDogadjaj.DtpDatumOdrzavanja.Checked = false;
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
                Dogadjaj kriterijumD = new Dogadjaj
                {
                    Naziv = ucPretragaDogadjaj!.TxtNaziv.Text.Trim(),
                    Mesto = ucPretragaDogadjaj.TxtMesto.Text.Trim(),
                    DatumOdrzavanja = ucPretragaDogadjaj.DtpDatumOdrzavanja.Checked
                        ? ucPretragaDogadjaj.DtpDatumOdrzavanja.Value.Date
                        : default
                };

                bool bezKriterijuma =
                    string.IsNullOrWhiteSpace(kriterijumD.Naziv)
                    && string.IsNullOrWhiteSpace(kriterijumD.Mesto)
                    && kriterijumD.DatumOdrzavanja == default;

                List<Dogadjaj> lista = bezKriterijuma
                    ? Komunikacija.Instance.VratiListuSviDogadjaj()
                    : Komunikacija.Instance.VratiListuDogadjaj(kriterijumD);

                ucPretragaDogadjaj.DgvRezultati.DataSource = new BindingList<Dogadjaj>(lista);
                if (lista.Count == 0)
                    MessageBox.Show("Sistem ne može da nađe događaje po zadatim kriterijumima.");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private bool Validiraj(Dogadjaj d)
        {
            if (string.IsNullOrWhiteSpace(d.Naziv) || string.IsNullOrWhiteSpace(d.Mesto))
            {
                MessageBox.Show("Sva polja moraju biti popunjena!");
                return false;
            }
            if (d.DatumOdrzavanja.Date < DateTime.Today)
            {
                MessageBox.Show("Datum održavanja ne može biti u prošlosti!");
                return false;
            }
            return true;
        }
    }
}
