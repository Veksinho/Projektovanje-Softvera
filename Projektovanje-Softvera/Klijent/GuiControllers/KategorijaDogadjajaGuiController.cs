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
    internal class KategorijaDogadjajaGuiController
    {
        private static KategorijaDogadjajaGuiController? instance;

        public static KategorijaDogadjajaGuiController Instance
        {
            get
            {
                instance ??= new KategorijaDogadjajaGuiController();
                return instance;
            }
        }

        private KategorijaDogadjajaGuiController()
        {
        }

        private UCKategorijaDogadjaja? ucKategorijaDogadjaja;
        private UCPretragaKategorijaDogadjaja? ucPretragaKategorijaDogadjaja;

        public void PrikaziFormuNova()
        {
            ucKategorijaDogadjaja = new UCKategorijaDogadjaja(FormMode.Add, new KategorijaDogadjaja());
            ucKategorijaDogadjaja.BtnKreiraj.Click += UbaciKategoriju;

            MainCoordinator.Instance.ChangePanel(ucKategorijaDogadjaja);
        }

        public void PrikaziFormuPretraga()
        {
            ucPretragaKategorijaDogadjaja = new UCPretragaKategorijaDogadjaja();
            ucPretragaKategorijaDogadjaja.BtnPretrazi.Click += Pretrazi;
            ucPretragaKategorijaDogadjaja.BtnPonisti.Click += Ponisti;
            ucPretragaKategorijaDogadjaja.BtnIzmeni.Click += PrikaziFormuIzmena;
            ucPretragaKategorijaDogadjaja.BtnObrisi.Click += ObrisiKategoriju;
            ucPretragaKategorijaDogadjaja.DgvRezultati.CellDoubleClick += PrikaziFormuIzmena;

            MainCoordinator.Instance.ChangePanel(ucPretragaKategorijaDogadjaja);

            OsveziListu();
        }

        private void PrikaziFormuIzmena(object? sender, EventArgs e)
        {
            KategorijaDogadjaja? selected = ucPretragaKategorijaDogadjaja!.DgvRezultati.CurrentRow == null ? null : ucPretragaKategorijaDogadjaja.DgvRezultati.CurrentRow.DataBoundItem as KategorijaDogadjaja;

            if (selected == null)
            {
                MessageBox.Show("Niste izabrali kategoriju događaja!");
                return;
            }

            try
            {
                KategorijaDogadjaja found = Komunikacija.Instance.PretraziKategorijaDogadjaja(
                    new KategorijaDogadjaja { IdKategorijaDogadjaja = selected.IdKategorijaDogadjaja });

                ucKategorijaDogadjaja = new UCKategorijaDogadjaja(FormMode.Edit, found);
                ucKategorijaDogadjaja.BtnIzmeni.Click += PromeniKategoriju;
                ucKategorijaDogadjaja.BtnNazad.Click += Odustani;

                MainCoordinator.Instance.ChangePanel(ucKategorijaDogadjaja);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UbaciKategoriju(object? sender, EventArgs e)
        {
            KategorijaDogadjaja kd = ucKategorijaDogadjaja!.VratiObjekat();

            if (!Validiraj(kd))
            {
                return;
            }

            try
            {
                Komunikacija.Instance.UbaciKategorijaDogadjaja(kd);
                MessageBox.Show("Sistem je zapamtio kategoriju događaja.");

                PrikaziFormuPretraga();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PromeniKategoriju(object? sender, EventArgs e)
        {
            KategorijaDogadjaja kd = ucKategorijaDogadjaja!.VratiObjekat();

            if (!Validiraj(kd))
            {
                return;
            }

            try
            {
                Komunikacija.Instance.PromeniKategorijaDogadjaja(kd);
                MessageBox.Show("Sistem je zapamtio kategoriju događaja.");

                PrikaziFormuPretraga();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ObrisiKategoriju(object? sender, EventArgs e)
        {
            KategorijaDogadjaja? izabranaKd = ucPretragaKategorijaDogadjaja!.DgvRezultati.CurrentRow == null ? null : ucPretragaKategorijaDogadjaja.DgvRezultati.CurrentRow.DataBoundItem as KategorijaDogadjaja;

            if (izabranaKd == null)
            {
                MessageBox.Show("Niste izabrali kategoriju događaja!");
                return;
            }

            DialogResult confirmation = MessageBox.Show(
                $"Da li želite da obrišete kategoriju '{izabranaKd.Naziv}'?",
                "Potvrda",
                MessageBoxButtons.YesNo);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            try
            {
                Komunikacija.Instance.ObrisiKategorijaDogadjaja(izabranaKd);
                MessageBox.Show("Sistem je obrisao kategoriju dogadjaja.");

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
            if (string.IsNullOrWhiteSpace(ucPretragaKategorijaDogadjaja!.TxtNaziv.Text))
            {
                return;
            }

            ucPretragaKategorijaDogadjaja!.TxtNaziv.Text = string.Empty;
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
                KategorijaDogadjaja kriterijumKd = new KategorijaDogadjaja
                {
                    Naziv = ucPretragaKategorijaDogadjaja!.TxtNaziv.Text.Trim()
                };

                List<KategorijaDogadjaja> listaKd = string.IsNullOrWhiteSpace(kriterijumKd.Naziv) ?
                    Komunikacija.Instance.VratiListuSviKategorijaDogadjaja() :
                    Komunikacija.Instance.VratiListuKategorijaDogadjaja(kriterijumKd);

                ucPretragaKategorijaDogadjaja.DgvRezultati.DataSource = new BindingList<KategorijaDogadjaja>(listaKd);

                if (listaKd.Count == 0)
                {
                    MessageBox.Show("Sistem ne može da nađe kategorije događaja po zadatim kriterijumima.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool Validiraj(KategorijaDogadjaja category)
        {
            if (string.IsNullOrWhiteSpace(category.Naziv) || string.IsNullOrWhiteSpace(category.Opis))
            {
                MessageBox.Show("Sva polja moraju biti popunjena!");
                return false;
            }

            return true;
        }
    }
}
