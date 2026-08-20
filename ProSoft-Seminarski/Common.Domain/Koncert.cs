using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public class Koncert : Dogadjaj, IDomainObject
    {
        public Dogadjaj Dogadjaj { get; set; }
        public string Zanr { get; set; }
        public string NazivIzvodjaca { get; set; }

        public new string TableName => "Koncert";

        public new string InsertValues => "@DogadjajID, @Zanr, @NazivIzvodjaca";

        public new IDomainObject ReadObjectRow(SqlDataReader reader)
        {
            Koncert k = new Koncert()
            {
                Dogadjaj = new Dogadjaj
                {
                    DogadjajID = (int)reader["DogadjajID"],
                    Naziv = (string)reader["Naziv"],
                    MestoOdrzavanja = (string)reader["MestoOdrzavanja"],
                    Vreme = (DateTime)reader["Vreme"],
                    Grad = (string)reader["Grad"],
                    Drzava = (string)reader["Drzava"],
                },
                Zanr = (string)reader["Zanr"],
                NazivIzvodjaca = (string)reader["NazivIzvodjaca"]
            };
            return k;
        }
    }
}
