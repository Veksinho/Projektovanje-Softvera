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
    public partial class UCPretragaKarta : UserControl
    {
        public UCPretragaKarta()
        {
            InitializeComponent();

            cmbTip.DataSource = StavkaEnuma<TipKarte>.GetAll();
            cmbTip.SelectedIndex = -1;
            cmbStatus.DataSource = StavkaEnuma<StatusKarte>.GetAll();
            cmbStatus.SelectedIndex = -1;

            SrediTabelu();
        }

        private void SrediTabelu()
        {
            dgvRezultati.AutoGenerateColumns = false;
            dgvRezultati.Columns.Clear();

            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                HeaderText = "Šifra",
                DataPropertyName = nameof(Karta.IdKarta),
                FillWeight = 7
            });
            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDogadjaj",
                HeaderText = "Događaj",
                DataPropertyName = nameof(Karta.Dogadjaj),
                FillWeight = 24
            });
            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSektor",
                HeaderText = "Sektor",
                DataPropertyName = nameof(Karta.Sektor),
                FillWeight = 10
            });
            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colRed",
                HeaderText = "Red",
                DataPropertyName = nameof(Karta.Red),
                FillWeight = 6
            });
            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSediste",
                HeaderText = "Sedište",
                DataPropertyName = nameof(Karta.Sediste),
                FillWeight = 8
            });
            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNominalnaCena",
                HeaderText = "Nom. cena",
                DataPropertyName = nameof(Karta.NominalnaCena),
                FillWeight = 11,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTip",
                HeaderText = "Tip",
                DataPropertyName = nameof(Karta.TipPrikaz),
                FillWeight = 10
            });
            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colFormat",
                HeaderText = "Format",
                DataPropertyName = nameof(Karta.FormatPrikaz),
                FillWeight = 10
            });
            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "Status",
                DataPropertyName = nameof(Karta.StatusPrikaz),
                FillWeight = 10
            });
            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colListing",
                HeaderText = "Listing",
                DataPropertyName = nameof(Karta.ListingPrikaz),
                FillWeight = 8,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
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
