using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public class SportskiDogadjaj : Dogadjaj, IDomainObject
    {
        public Dogadjaj Dogadjaj { get; set; }
        public string NazivSporta { get; set; }
        public string Takmicenje { get; set; }
        public string Sezona { get; set; }

        public new string TableName => "SportskiDogadjaj";

        public new string InsertValues => "@DogadjajID, @NazivSporta, @Takmicenje, @Sezona";

        public new IDomainObject ReadObjectRow(SqlDataReader reader)
        {
            SportskiDogadjaj sd = new SportskiDogadjaj()
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
                NazivSporta = (string)reader["NazivSporta"],
                Takmicenje = (string)reader["Takmicenje"],
                Sezona = (string)reader["Sezona"]
            };
            return sd;
        }
    }
}
