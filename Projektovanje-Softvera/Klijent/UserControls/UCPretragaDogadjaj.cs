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
    public partial class UCPretragaDogadjaj : UserControl
    {
        public UCPretragaDogadjaj()
        {
            InitializeComponent();
            SrediTabelu();
        }

        private void SrediTabelu()
        {
            DgvRezultati.AutoGenerateColumns = false;
            DgvRezultati.Columns.Clear();

            DgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                HeaderText = "ID",
                DataPropertyName = nameof(Dogadjaj.IdDogadjaj),
                FillWeight = 10
            });
            DgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNaziv",
                HeaderText = "Naziv",
                DataPropertyName = nameof(Dogadjaj.Naziv),
                FillWeight = 35
            });
            DgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDatumOdrzavanja",
                HeaderText = "Datum održavanja",
                DataPropertyName = nameof(Dogadjaj.DatumOdrzavanja),
                FillWeight = 20,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" }
            });
            DgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMesto",
                HeaderText = "Mesto",
                DataPropertyName = nameof(Dogadjaj.Mesto),
                FillWeight = 35
            });

            DgvRezultati.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DgvRezultati.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgvRezultati.MultiSelect = false;
            DgvRezultati.ReadOnly = true;
            DgvRezultati.AllowUserToAddRows = false;
            DgvRezultati.AllowUserToDeleteRows = false;
            DgvRezultati.RowHeadersVisible = false;
        }
    }
}
