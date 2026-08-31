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
    public partial class UCPretragaBroker : UserControl
    {
        public UCPretragaBroker()
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
                HeaderText = "ID",
                DataPropertyName = nameof(Broker.IdBroker),
                FillWeight = 8
            });
            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colKorisnickoIme",
                HeaderText = "Korisničko ime",
                DataPropertyName = nameof(Broker.KorisnickoIme),
                FillWeight = 18
            });
            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colIme",
                HeaderText = "Ime",
                DataPropertyName = nameof(Broker.Ime),
                FillWeight = 15
            });
            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPrezime",
                HeaderText = "Prezime",
                DataPropertyName = nameof(Broker.Prezime),
                FillWeight = 18
            });
            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTelefon",
                HeaderText = "Telefon",
                DataPropertyName = nameof(Broker.Telefon),
                FillWeight = 18
            });
            dgvRezultati.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSpecijalizacije",
                HeaderText = "Specijalizacije",
                DataPropertyName = nameof(Broker.SpecijalizacijePrikaz),
                FillWeight = 23
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
