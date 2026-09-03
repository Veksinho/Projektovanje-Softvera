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
    public partial class UCPretragaListing : UserControl
    {
        public UCPretragaListing()
        {
            InitializeComponent();

            cmbStatus.DataSource = StavkaEnuma<StatusListinga>.GetAll();
            cmbStatus.SelectedIndex = -1;
            cmbSplit.DataSource = StavkaEnuma<TipSplita>.GetAll();
            cmbSplit.SelectedIndex = -1;
            cmbTipKarte.DataSource = StavkaEnuma<TipKarte>.GetAll();
            cmbTipKarte.SelectedIndex = -1;

            SrediTabelu();
        }

        public void PonistiKriterijume()
        {
            cmbStatus.SelectedIndex = -1;
            cmbSplit.SelectedIndex = -1;
            cmbTipKarte.SelectedIndex = -1;
            cmbKonsignator.SelectedIndex = -1;
            cmbDogadjaj.SelectedIndex = -1;
            dtpObjavljenOd.Checked = false;
            dtpObjavljenDo.Checked = false;
            txtCenaOd.Clear();
            txtCenaDo.Clear();
            txtNazivKonsignatora.Clear();
            txtMesto.Clear();
            txtSektor.Clear();
        }

        private void SrediTabelu()
        {
            dgvRezultati.AutoGenerateColumns = false;
            dgvRezultati.Columns.Clear();

            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colIdListing",
                HeaderText = "Id",
                DataPropertyName = nameof(Listing.IdListing),
                FillWeight = 7
            });
            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDogadjaj",
                HeaderText = "Događaj",
                DataPropertyName = nameof(Listing.DogadjajPrikaz),
                FillWeight = 22
            });
            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colKonsignator",
                HeaderText = "Konsignator",
                DataPropertyName = nameof(Listing.Konsignator),
                FillWeight = 15
            });
            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDatumObjave",
                HeaderText = "Objavljen",
                DataPropertyName = nameof(Listing.DatumObjave),
                FillWeight = 10,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" }
            });
            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDatumIsteka",
                HeaderText = "Ističe",
                DataPropertyName = nameof(Listing.DatumIsteka),
                FillWeight = 10,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" }
            });
            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "Status",
                DataPropertyName = nameof(Listing.StatusPrikaz),
                FillWeight = 10
            });
            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colBrojKarata",
                HeaderText = "Karata",
                DataPropertyName = nameof(Listing.BrojKarata),
                FillWeight = 7,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCenaPoKarti",
                HeaderText = "Cena po karti",
                DataPropertyName = nameof(Listing.CenaPoKarti),
                FillWeight = 10,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colProvizija",
                HeaderText = "Provizija",
                DataPropertyName = nameof(Listing.Provizija),
                FillWeight = 10,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvRezultati.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRezultati.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRezultati.MultiSelect = false;
            dgvRezultati.ReadOnly = true;
            dgvRezultati.AllowUserToAddRows = false;
            dgvRezultati.AllowUserToDeleteRows = false;
            dgvRezultati.RowHeadersVisible = false;
        }
    }
}
