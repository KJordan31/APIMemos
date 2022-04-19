using System.Data;
using System.Linq;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace API.Controllers{
    [ApiController]
    [Route("api/fecha")]

    public class FechaController : ControllerBase{
        private readonly IConfiguration configuration;

        private readonly IMapper _mapper;

        public FechaController(IConfiguration configuration, IMapper mapper)
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

         [HttpGet]
        public IActionResult GetProfileCount(string profileId, string SistemaFecha)
        {
            var sql =
                @"SELECT * FROM TB_Memorandum WHERE MONTH (SistemaFecha)= @SistemaFecha AND YEAR(SistemaFecha) = @SistemaFecha;";

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();

                var profileCount = dbConnection.Query(sql, new { SistemaFecha = SistemaFecha })
                        .ToList();
      
                return Ok(profileCount);
            }
        }

    }
}