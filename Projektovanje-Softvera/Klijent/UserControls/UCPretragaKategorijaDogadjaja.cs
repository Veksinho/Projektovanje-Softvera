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
    public partial class UCPretragaKategorijaDogadjaja : UserControl
    {
        public UCPretragaKategorijaDogadjaja()
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
                DataPropertyName = nameof(KategorijaDogadjaja.IdKategorijaDogadjaja),
                FillWeight = 15
            });

            DgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNaziv",
                HeaderText = "Naziv",
                DataPropertyName = nameof(KategorijaDogadjaja.Naziv),
                FillWeight = 30
            });

            DgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colOpis",
                HeaderText = "Opis",
                DataPropertyName = nameof(KategorijaDogadjaja.Opis),
                FillWeight = 55
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
