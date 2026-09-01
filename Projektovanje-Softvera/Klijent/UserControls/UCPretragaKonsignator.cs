using Common.Domen;
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
    public partial class UCPretragaKonsignator : UserControl
    {
        public UCPretragaKonsignator()
        {
            InitializeComponent();
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
                DataPropertyName = nameof(Konsignator.IdKonsignator),
                FillWeight = 8
            });

            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTip",
                HeaderText = "Tip",
                DataPropertyName = nameof(Konsignator.TipPrikaz),
                FillWeight = 14
            });

            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNaziv",
                HeaderText = "Naziv",
                DataPropertyName = nameof(Konsignator.Name),
                FillWeight = 24
            });

            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colEmail",
                HeaderText = "E-mail",
                DataPropertyName = nameof(Konsignator.Email),
                FillWeight = 22
            });

            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTelefon",
                HeaderText = "Telefon",
                DataPropertyName = nameof(Konsignator.Telefon),
                FillWeight = 18
            });

            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDatumRegistracije",
                HeaderText = "Registrovan",
                DataPropertyName = nameof(Konsignator.DatumRegistracije),
                FillWeight = 14,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" }
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
