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
        private Font? fontCurrentBrokerRow;

        public UCPretragaBroker()
        {
            InitializeComponent();
            SrediTabelu();

            Disposed += (s, e) => fontCurrentBrokerRow?.Dispose();
        }

        public int SelectedBrokerId { get; set; }

        public Broker? GetSelected()
            => dgvRezultati.CurrentRow?.DataBoundItem as Broker;

        public bool IsCurrentBroker(Broker? broker)
            => broker != null && broker.IdBroker == SelectedBrokerId;

        public void HighlightCurrentRow()
        {
            fontCurrentBrokerRow ??= new Font(dgvRezultati.Font, FontStyle.Bold);

            foreach (DataGridViewRow row in dgvRezultati.Rows)
            {
                bool current = IsCurrentBroker(row.DataBoundItem as Broker);
                row.DefaultCellStyle.Font = current ? fontCurrentBrokerRow : dgvRezultati.Font;
            }
        }

        public void EnableEditDeleteButtons()
        {
            bool current = IsCurrentBroker(GetSelected());

            btnIzmeni.Enabled = current;
            btnObrisi.Enabled = current;
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
