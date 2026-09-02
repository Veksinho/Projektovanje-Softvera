using Common.Domen;
using Common.Domen.Enumeracije;
using Klijent.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Klijent.UserControls
{
    public partial class UCKonsignator : UserControl
    {
        private readonly Konsignator k;
        private readonly FormMode mode;
        private TipKonsignatora? prikazaniTip;

        private ISubtypeControl? podtip;


        public UCKonsignator(FormMode mode, Konsignator konsignator)
        {
            InitializeComponent();

            this.k = konsignator;
            this.mode = mode;

            cmbTipKonsignatora.DataSource = StavkaTipKonsignatora.GetAll();
            cmbTipKonsignatora.SelectedIndex = k is PravnoLice ? 1 : 0;
            cmbTipKonsignatora.SelectedIndexChanged += CmbTipKonsignatora_SelectedIndexChanged;

            txtId.Text = mode == FormMode.Add ? "" : k.IdKonsignator.ToString();
            txtEmail.Text = k.Email;
            txtTelefon.Text = k.Telefon;
            txtAdresa.Text = k.Adresa;

            dtpDatumRegistracije.Value = k.DatumRegistracije == default
                ? DateTime.Today
                : k.DatumRegistracije;

            PrikaziPodtip();
            SrediFormu(mode);
        }

        public Konsignator VratiObjekat()
        {
            Konsignator obj = mode == FormMode.Add ? podtip!.NapraviPrazan() : k;

            obj.IdKonsignator = k.IdKonsignator;
            obj.Email = txtEmail.Text.Trim();
            obj.Telefon = txtTelefon.Text.Trim();
            obj.Adresa = txtAdresa.Text.Trim();

            obj.DatumRegistracije = k.DatumRegistracije;

            podtip!.Procitaj(obj);

            return obj;
        }

        private void CmbTipKonsignatora_SelectedIndexChanged(object? sender, EventArgs e)
        {
            PrikaziPodtip();
        }

        private void PrikaziPodtip()
        {
            bool pravnoLice = cmbTipKonsignatora.SelectedIndex == 1;
            TipKonsignatora tip = pravnoLice
                ? TipKonsignatora.pravno_lice
                : TipKonsignatora.fizicko_lice;

            if (prikazaniTip == tip) return;
            prikazaniTip = tip;

            pnlPodtip.Controls.Clear();
            foreach (Control c in pnlPodtip.Controls.Cast<Control>().ToArray())
            {
                c.Dispose();
            }

            UserControl podtipKontrola = pravnoLice
                ? new UCPravnoLice()
                : new UCFizickoLice();

            pnlPodtip.Controls.Add(podtipKontrola);
            podtipKontrola.Dock = DockStyle.Fill;

            podtip = (ISubtypeControl)podtipKontrola;

            podtip.Popuni(k);
            podtip.SrediFormu(mode);
        }

        private void SrediFormu(FormMode mode)
        {
            dtpDatumRegistracije.Enabled = false;

            switch (mode)
            {
                case FormMode.Add:
                    btnIzmeni.Visible = false;
                    btnNazad.Visible = false;
                    break;

                case FormMode.Edit:
                    btnKreiraj.Visible = false;
                    cmbTipKonsignatora.Enabled = false;
                    break;

                case FormMode.Details:
                    btnKreiraj.Visible = false;
                    btnIzmeni.Visible = false;
                    cmbTipKonsignatora.Enabled = false;
                    txtEmail.Enabled = false;
                    txtTelefon.Enabled = false;
                    txtAdresa.Enabled = false;
                    break;
            }
        }
    }
}
