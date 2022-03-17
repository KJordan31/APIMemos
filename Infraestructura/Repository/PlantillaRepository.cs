using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Plantillas;
using Dapper;
using Dominio;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infraestructura.Repository
{
    public class PlantillaRepository : IPlantillaRepository
    {
        private readonly IConfiguration configuration;

        public PlantillaRepository(IConfiguration configuration)
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

        private int ObtenerUltimoID()
        {
            var sql =
                "select top 1 Id + 1 as UltimoID from TB_Plantillas order by SistemaFecha desc";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = dbConnection.ExecuteScalar<int>(sql);
                return result;
            }
        }

        public async Task<int> Actualizar(Plantilla entity)
        {
            var sqlActualizar =
                "UPDATE TB_Plantillas set  Plantillas = @Plantillas where Id = @Id";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync<int>(sqlActualizar,new { Plantillas = entity.Plantillas, Id = entity.Id });
                return result;
            }
        }

        public async Task<int> Agregar(Plantilla entity)
        {
            entity.Id = ObtenerUltimoID();

            var sqlAdd =
                "INSERT INTO TB_Plantillas(Id, Plantillas) Values(@Id, @Plantillas)";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(sqlAdd, entity);
                return result;
            }
        }

        public async Task<int> Borrar(int id)
        {
            var sqlDelete = "DELETE From TB_Plantillas Where Id = @Id";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result =
                    await dbConnection.ExecuteAsync(sqlDelete, new { Id = id });
                return result;
            }
        }

        public async Task<Plantilla> ObtenerPorId(int id)
        {
            var sql = "SELECT * FROM TB_Plantillas WHERE Id = @Id";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result =
                    await dbConnection.QuerySingleOrDefaultAsync<Plantilla>(sql,new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Plantilla>> ObtenerListado()
        {
            var sql = "SELECT * FROM TB_Plantillas";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.QueryAsync<Plantilla>(sql);
                return result.ToList();
            }
        }
    }
}
