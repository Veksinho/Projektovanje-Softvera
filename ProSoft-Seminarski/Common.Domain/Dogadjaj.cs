using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public class Dogadjaj : IDomainObject
    {
        public int DogadjajID { get; set; }
        public string Naziv { get; set; }
        public string MestoOdrzavanja { get; set; }
        public DateTime Vreme { get; set; }
        public string Grad { get; set; }
        public string Drzava { get; set; }

        public string TableName => "Dogadjaj";

        public string InsertValues => "@Naziv, @MestoOdrzavanja, @Vreme, @Grad, @Drzava";

        public IDomainObject ReadObjectRow(SqlDataReader reader)
        {
            Dogadjaj d = new Dogadjaj()
            {
                DogadjajID = (int)reader["DogadjajID"],
                Naziv = (string)reader["Naziv"],
                MestoOdrzavanja = (string)reader["MestoOdrzavanja"],
                Vreme = (DateTime)reader["Vreme"],
                Grad = (string)reader["Grad"],
                Drzava = (string)reader["Drzava"],
            };
            return d;
        }
    }
}
