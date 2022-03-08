using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Adjuntos;
using Dapper;
using Dominio;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infraestructura.Repository
{
    public class AdjuntoRepository : IAdjuntoRepository
    {
        private readonly IConfiguration configuration;

        public AdjuntoRepository(IConfiguration configuration)
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
                "select top 1 Id_Adjuntos + 1 as UltimoID from TB_Adjuntos order by SistemaFecha desc";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = dbConnection.ExecuteScalar<int>(sql);
                return result;
            }
        }

        public async Task<int> Actualizar(Adjunto entity)
        {
            var sqlActualizar =
                "UPDATE TB_Adjuntos set Adjuntos = @Adjuntos where Id_Adjuntos = @Id_Adjuntos";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result =
                    await dbConnection
                        .ExecuteScalarAsync<int>(sqlActualizar,
                        new {
                            Adjuntos = entity.Adjuntos,
                            Id_Adjuntos = entity.Id_Adjuntos
                        });
                return result;
            }
        }

        public async Task<int> Agregar(Adjunto entity)
        {
            entity.Id_Adjuntos = ObtenerUltimoID();
            var sqlAdd =
                "Insert into TB_Adjuntos(Id_Adjuntos, Adjuntos, SistemaUsuario, Id) Values (@Id_Adjuntos, @Adjuntos, @SistemaUsuario, @Id)";

            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id, DbType.Int32);

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(sqlAdd, entity);
                return result;
            }
        }

        public async Task<int> Borrar(int id)
        {
            var queryBorrar =
                "DELETE From TB_Adjuntos Where Id_Adjuntos= @Id_Adjuntos";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result =
                    await dbConnection
                        .ExecuteAsync(queryBorrar, new { Id_Adjuntos = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Adjunto>> ObtenerListado()
        {
            var query = "SELECT * FROM TB_Adjuntos";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var resultado = await dbConnection.QueryAsync<Adjunto>(query);
                return resultado.ToList();
            }
        }

        public async Task<Adjunto> ObtenerPorId(int id)
        {
            var query =
                "SELECT * FROM TB_Adjuntos WHERE Id_Adjuntos = @Id_Ajuntos";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result =
                    await dbConnection
                        .QuerySingleOrDefaultAsync<Adjunto>(query,
                        new { Id_Adjuntos = id });
                return result;
            }
        }
    }
}
