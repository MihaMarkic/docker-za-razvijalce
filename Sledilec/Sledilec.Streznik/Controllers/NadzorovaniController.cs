using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Sledilec.Strežnik.Models;

namespace Sledilec.Strežnik.Controllers
{
    [Route("api/[controller]/[action]")]
    public class NadzorovaniController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public NadzorovaniController(IConfigurationRoot configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("Default");
        }

        [HttpGet]
        public string Test()
        {
            return connectionString;
        }

        [HttpGet]
        public IList<NadzorovaniModel> SeznamVseh()
        {
            using (var conn = new NpgsqlConnection(connectionString))
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
            using (var conn = new NpgsqlConnection(connectionString))
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
