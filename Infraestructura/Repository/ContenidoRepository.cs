using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Contenidos;
using Dapper;
using Dominio;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infraestructura.Repository
{
    public class ContenidoRepository : IContenidoRepository
    {
        private readonly IConfiguration configuration;

        public ContenidoRepository(IConfiguration configuration)
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
                "select top 1 Id_Contenido + 1 as UltimoID from TB_Contenido order by SistemaFecha desc";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = dbConnection.ExecuteScalar<int>(sql);
                return result;
            }
        }

        public async Task<int> Actualizar(ContenidoMemo entity)
        {
            var sqlActualizar =
                "UPDATE TB_Contenido set Contenido = @Contenido where Id_Contenido = @Id_Contenido";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result =
                    await dbConnection
                        .ExecuteScalarAsync<int>(sqlActualizar,
                        new {
                            Contenido = entity.Contenido,
                            Id_Contenido = entity.Id_Contenido
                        });
                return result;
            }
        }

        public async Task<int> Agregar(ContenidoMemo entity)
        {
            entity.Id_Contenido = ObtenerUltimoID();

            var sqlAdd =
                "Insert into TB_Contenido(Id_Contenido,Contenido, SistemaUsuario) Values(@Id_Contenido,@Contenido, @SistemaUsuario)";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", entity.Id, DbType.Int32);

            using (IDbConnection dbConnecion = Connection)
            {
                dbConnecion.Open();
                var result = await dbConnecion.ExecuteAsync(sqlAdd, entity);
                return result;
            }
        }

        public async Task<int> Borrar(int id)
        {
            var queryBorrar =
                "DELETE From TB_Contenido Where Id_Contenido = @Id_Contenido";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result =
                    await dbConnection
                        .ExecuteAsync(queryBorrar, new { Id_Contenido = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<ContenidoMemo>> ObtenerListado()
        {
            var query = "Select * from TB_Contenido";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result =
                    await dbConnection.QueryAsync<ContenidoMemo>(query);
                return result.ToList();
            }
        }

        public async Task<ContenidoMemo> ObtenerPorId(int id)
        {
            var query =
                "SELECT * FROM TB_Contenido WHERE Id_Contenido = @Id_Contenido";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result =
                    await dbConnection
                        .QuerySingleOrDefaultAsync<ContenidoMemo>(query,
                        new { Id_Contenido = id });
                return result;
            }
        }
    }
}
