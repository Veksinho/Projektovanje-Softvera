using Common.Domen;
using Klijent.UserControls;
using Klijent.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace Klijent.GuiControllers
{
    internal class BrokerGuiController
    {
        private static BrokerGuiController? instance;

        public static BrokerGuiController Instance =>
            instance ??= new BrokerGuiController();

        private BrokerGuiController() { }

        private UCBroker? ucBroker;
        private UCPretragaBroker? ucPretragaBroker;

        private List<KategorijaDogadjaja> UcitajKategorije()
            => Komunikacija.Instance.VratiListuSviKategorijaDogadjaja();

        public void PrikaziFormuNova()
        {
            try
            {
                ucBroker = new UCBroker(FormMode.Add, new Broker(), UcitajKategorije());
                ucBroker.BtnKreiraj.Click += KreirajBroker;

                MainCoordinator.Instance.ChangePanel(ucBroker);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PrikaziFormuPretraga()
        {
            try
            {
                ucPretragaBroker = new UCPretragaBroker();

                ucPretragaBroker.SelectedBrokerId =
                    Session.Instance.LoggedInBroker == null
                        ? 0
                        : Session.Instance.LoggedInBroker.IdBroker;

                ucPretragaBroker.CmbKategorija.DataSource = UcitajKategorije();
                ucPretragaBroker.CmbKategorija.SelectedIndex = -1;

                ucPretragaBroker.BtnPretrazi.Click += Pretrazi;
                ucPretragaBroker.BtnPonisti.Click += Ponisti;
                ucPretragaBroker.BtnPrikazi.Click += PrikaziFormuDetalji;
                ucPretragaBroker.BtnIzmeni.Click += PrikaziFormuIzmena;
                ucPretragaBroker.BtnObrisi.Click += ObrisiBroker;
                ucPretragaBroker.DgvRezultati.CellDoubleClick += PrikaziFormuDetalji;
                ucPretragaBroker.DgvRezultati.SelectionChanged += SelectionChanged;

                MainCoordinator.Instance.ChangePanel(ucPretragaBroker);

                OsveziListu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Broker? GetSelectedBroker()
        {
            Broker? selected = ucPretragaBroker!.GetSelected();

            if (selected == null)
                MessageBox.Show("Niste izabrali brokera!");

            return selected;
        }

        private static bool IsLoggedInBroker(Broker broker)
            => Session.Instance.LoggedInBroker != null
               && broker.IdBroker == Session.Instance.LoggedInBroker.IdBroker;

        private void SelectionChanged(object? sender, EventArgs e)
            => ucPretragaBroker!.EnableEditDeleteButtons();

        private void PrikaziFormuDetalji(object? sender, EventArgs e)
        {
            Broker? selected = GetSelectedBroker();

            if (selected == null) return;

            try
            {
                Broker found = Komunikacija.Instance.PretraziBroker(
                    new Broker { IdBroker = selected.IdBroker });

                MessageBox.Show("Sistem je našao brokera.");

                ucBroker = new UCBroker(FormMode.Details, found, UcitajKategorije());
                ucBroker.BtnNazad.Click += Odustani;

                MainCoordinator.Instance.ChangePanel(ucBroker);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PrikaziFormuIzmena(object? sender, EventArgs e)
        {
            Broker? selected = GetSelectedBroker();

            if (selected == null) return;

            if (!IsLoggedInBroker(selected))
            {
                MessageBox.Show("Možete izmeniti samo svoje podatke!");
                return;
            }

            try
            {
                Broker found = Komunikacija.Instance.PretraziBroker(
                    new Broker { IdBroker = selected.IdBroker });

                MessageBox.Show("Sistem je našao brokera.");

                ucBroker = new UCBroker(FormMode.Edit, found, UcitajKategorije());
                ucBroker.BtnIzmeni.Click += PromeniBroker;
                ucBroker.BtnNazad.Click += Odustani;

                MainCoordinator.Instance.ChangePanel(ucBroker);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void KreirajBroker(object? sender, EventArgs e)
        {
            Broker b = ucBroker!.VratiObjekat();

            if (!Validiraj(b)) return;

            try
            {
                Komunikacija.Instance.KreirajBroker(b);
                MessageBox.Show("Sistem je zapamtio brokera.");
                PrikaziFormuPretraga();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PromeniBroker(object? sender, EventArgs e)
        {
            Broker b = ucBroker!.VratiObjekat();

            if (!IsLoggedInBroker(b))
            {
                MessageBox.Show("Možete izmeniti samo svoje podatke!");
                return;
            }

            if (!Validiraj(b)) return;

            try
            {
                Session.Instance.LoggedInBroker = Komunikacija.Instance.PromeniBroker(b);
                MainCoordinator.Instance.OsveziPrijavljenogBrokera();

                MessageBox.Show("Sistem je zapamtio brokera.");
                PrikaziFormuPretraga();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ObrisiBroker(object? sender, EventArgs e)
        {
            Broker? selected = GetSelectedBroker();

            if (selected == null) return;

            if (!IsLoggedInBroker(selected))
            {
                MessageBox.Show("Možete obrisati samo svoj nalog!");
                return;
            }

            if (MessageBox.Show($"Da li želite da obrišete brokera '{selected.KorisnickoIme}'?",
                    "Potvrda", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            try
            {
                Broker found = Komunikacija.Instance.PretraziBroker(
                    new Broker { IdBroker = selected.IdBroker });

                MessageBox.Show("Sistem je našao brokera.");

                Komunikacija.Instance.ObrisiBroker(found);
                MessageBox.Show("Sistem je obrisao brokera.");

                MainCoordinator.Instance.OdjaviBrokera();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Pretrazi(object? sender, EventArgs e) => OsveziListu();

        private void Ponisti(object? sender, EventArgs e)
        {
            ucPretragaBroker!.TxtKorisnickoIme.Clear();
            ucPretragaBroker.TxtIme.Clear();
            ucPretragaBroker.TxtPrezime.Clear();
            ucPretragaBroker.TxtTelefon.Clear();
            ucPretragaBroker.CmbKategorija.SelectedIndex = -1;

            OsveziListu();
        }

        private void Odustani(object? sender, EventArgs e) => PrikaziFormuPretraga();

        private void OsveziListu()
        {
            try
            {
                KategorijaDogadjaja? izabranaKat =
                    ucPretragaBroker!.CmbKategorija.SelectedItem as KategorijaDogadjaja;

                Broker kriterijum = new Broker
                {
                    KorisnickoIme = ucPretragaBroker.TxtKorisnickoIme.Text.Trim(),
                    Ime = ucPretragaBroker.TxtIme.Text.Trim(),
                    Prezime = ucPretragaBroker.TxtPrezime.Text.Trim(),
                    Telefon = ucPretragaBroker.TxtTelefon.Text.Trim()
                };

                if (izabranaKat != null)
                {
                    kriterijum.Specijalizacije.Add(new BrokerKategorijaDogadjaja
                    {
                        Broker = kriterijum,
                        KategorijaDogadjaja = izabranaKat
                    });
                }

                bool bezKriterijuma =
                    string.IsNullOrWhiteSpace(kriterijum.KorisnickoIme)
                    && string.IsNullOrWhiteSpace(kriterijum.Ime)
                    && string.IsNullOrWhiteSpace(kriterijum.Prezime)
                    && string.IsNullOrWhiteSpace(kriterijum.Telefon)
                    && izabranaKat == null;

                List<Broker> lista = bezKriterijuma
                    ? Komunikacija.Instance.VratiListuSviBroker()
                    : Komunikacija.Instance.VratiListuBroker(kriterijum);

                ucPretragaBroker.DgvRezultati.DataSource = new BindingList<Broker>(lista);
                ucPretragaBroker.HighlightCurrentRow();
                ucPretragaBroker.EnableEditDeleteButtons();

                if (lista.Count == 0)
                    MessageBox.Show("Sistem ne može da nađe brokere po zadatim kriterijumima.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool Validiraj(Broker b)
        {
            if (string.IsNullOrWhiteSpace(b.KorisnickoIme)
                || string.IsNullOrWhiteSpace(b.Ime)
                || string.IsNullOrWhiteSpace(b.Prezime)
                || string.IsNullOrWhiteSpace(b.Telefon)
                || string.IsNullOrWhiteSpace(b.Sifra))
            {
                MessageBox.Show("Sva polja moraju biti popunjena!");
                return false;
            }

            return true;
        }
    }
}
