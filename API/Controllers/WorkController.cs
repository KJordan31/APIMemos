using System;
using System.Data;
using System.Linq;
using Aplicacion.Bitacoras;
using AutoMapper;
using Dapper;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [ApiController]
    [Route("api/work")]
    public class WorkController : ControllerBase
    {
        private readonly IConfiguration configuration;
          
        private readonly IMapper _mapper;

        public WorkController(IConfiguration configuration,  IMapper mapper)
        {
            this.configuration = configuration;
             _mapper = mapper;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(configuration
                        .GetConnectionString("DefaultConnection"));
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProfileCount(string profileId, int id)
        {
            var sql =
                @"SELECT * FROM TB_Bitacora a INNER JOIN TB_Memorandum c ON a.Id_Memorandum = c.Id
                  INNER JOIN TB_Estado b ON a.Id_Estado = b.Id_Estado
                WHERE Id_Memorandum
                IN (SELECT Id_Memorandum FROM TB_Bitacora where Id_Memorandum = @Id_Memorandum				
                GROUP BY Id_Memorandum HAVING count(Id_Memorandum) >1)
                ORDER BY Id_Memorandum ";

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();

                var profileCount =
                    dbConnection
                        .Query(sql, new { Id_Memorandum = id })
                        .ToList();
                return Ok(profileCount);
            }
        }

    }
}
