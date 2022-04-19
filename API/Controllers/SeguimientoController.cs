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
    [Route("api/seguimiento")]
    public class SeguimientoController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public SeguimientoController(IConfiguration configuration)
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
                @"SELECT Descripcion, COUNT(Descripcion) as Total
                FROM TB_Memorandum, TB_Estado  where   TB_Memorandum.Id_Estado = TB_Estado.Id_Estado
                GROUP BY Descripcion";
            
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
            
                 var profileCount = dbConnection.Query(sql).ToList();
                return Ok(profileCount);

            }


        }
     
    }
}
