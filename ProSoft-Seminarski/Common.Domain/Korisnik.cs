using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public class Korisnik : IDomainObject
    {
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public bool Administrator { get; set; }

        public string TableName => "Korisnik";

        public string InsertValues => "@Email, @Loznika, @Ime, @Prezime, @Administrator";

        public IDomainObject ReadObjectRow(SqlDataReader reader)
        {
            Korisnik k = new Korisnik()
            {
                Email = (string)reader["Email"],
                Lozinka = (string)reader["Lozinka"],
                Ime = (string)reader["Ime"],
                Prezime = (string)reader["Prezime"],
                Administrator = (bool)reader["Administrator"]
            };
            return k;
        }
    }
}
