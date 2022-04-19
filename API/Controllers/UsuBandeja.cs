using System.Data;
using System.Linq;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [ApiController]
    [Route("api/usuarioban")]
    public class UsuBandejaController : ControllerBase
    {
        private readonly IConfiguration configuration;

        private readonly IMapper _mapper;

        public UsuBandejaController(IConfiguration configuration, IMapper mapper)
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
        public IActionResult GetProfileCount(string profileId, string DestinatarioUsu)
        {
            var sql =
                @"SELECT * FROM TB_Memorandum WHERE DestinatarioUsu = @DestinatarioUsu";

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();

                var profileCount =
                    dbConnection
                        .Query(sql, new { DestinatarioUsu = DestinatarioUsu })
                        .ToList();
                var contenido =
                    dbConnection.Query("Select * from TB_Contenido").ToList();
              

                foreach (var memo in profileCount)
                {
                   memo.Contenido =
                        contenido.FirstOrDefault(x => x.Id == memo.Id);
                   
                }
                return Ok(profileCount);
            }
        }
    }
}
