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
                return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProfileCount(string profileId, int id)
        {
            var sql =
                @"SELECT * FROM TB_Bitacora a 
                WHERE Id_Memorandum
                IN (SELECT Id_Memorandum FROM TB_Bitacora where Id_Memorandum = @Id_Memorandum				
                GROUP BY Id_Memorandum HAVING count(Id_Memorandum) >1)
                ORDER BY Id_Memorandum ";

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();

                var profileCount = dbConnection.Query(sql, new { Id_Memorandum = id }).ToList();
                 var estado =  dbConnection.Query("Select * from TB_Estado").ToList();
                   var memo =  dbConnection.Query("Select * from TB_Memorandum").ToList();

                foreach (var bitacora in profileCount)
                {
                    bitacora.Estado =
                        estado.FirstOrDefault(x => x.Id_Estado == bitacora.Id_Estado);
                    bitacora.Memorandum =
                        memo.FirstOrDefault(x => x.Id == bitacora.Id_Memorandum);
                }
                return Ok(profileCount);
            }
        }

    }
}
