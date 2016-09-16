using Npgsql;
using Sledilec.Strežnik.Models;
using Sledilec.Strežnik.Properties;
using System.Collections.Generic;
using System.Web.Http;

namespace Sledilec.Strežnik.Controllers
{
    public class NadzorovaniController : ApiController
    {
        [HttpGet]
        public IList<NadzorovaniModel> SeznamVseh()
        {
            using (var conn = new NpgsqlConnection(Settings.Default.ConnectionString))
            using (var cmd = new NpgsqlCommand("SELECT id, ime, priimek FROM Nadzorovani", conn))
            {
                List<NadzorovaniModel> modeli = new List<NadzorovaniModel>();
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var model = new NadzorovaniModel
                        {
                            Id = reader.GetInt32(0),
                            Ime = reader.GetString(1),
                            Priimek = reader.GetString(2)
                        };
                        modeli.Add(model);
                    }
                }
                return modeli;
            }
        }

        [HttpPost]
        public void Dodaj([FromBody]NadzorovaniModel model)
        {
            using (var conn = new NpgsqlConnection(Settings.Default.ConnectionString))
            using (var cmd = new NpgsqlCommand("INSERT INTO Nadzorovani (ime, priimek) VALUES(:ime, :priimek)", conn))
            {
                cmd.Parameters.AddWithValue("ime", model.Ime);
                cmd.Parameters.AddWithValue("priimek", model.Priimek);
                conn.Open();
                int vrstic = cmd.ExecuteNonQuery();
            }
        }
    }
}
