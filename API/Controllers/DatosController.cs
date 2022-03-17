using System;
using System.Data;
using System.Linq;
using Dapper;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [ApiController]
    [Route("api/datos")]
    public class DatosController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public DatosController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(configuration
                        .GetConnectionString("DefaultConnection"));
            }
        }

        [HttpGet]
        public IActionResult GetProfileCount(string profileId)
        {
            var sql =
                @"SELECT Tipo, COUNT(Tipo) as Total
                FROM TB_Memorandum, TB_Tipo_Memorandum  where   TB_Memorandum.Id_Tipo = TB_Tipo_Memorandum.Id_Tipo
                GROUP BY Tipo";
            
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
            
                 var profileCount = dbConnection.Query(sql).ToList();
                return Ok(profileCount);

            }


        }
          [HttpGet("Usuarios")]
        public IActionResult GetUserCount(string profileId)
        {
            var sql =
                @"SELECT DestinatarioUsu, COUNT(DestinatarioUsu) as Total
                FROM TB_Memorandum  where   TB_Memorandum.Id_Tipo = TB_Memorandum.Id_Tipo
                GROUP BY DestinatarioUsu";
            
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
            
                 var profileCount = dbConnection.Query(sql).ToList();
                return Ok(profileCount);

            }


        }
    }
}
