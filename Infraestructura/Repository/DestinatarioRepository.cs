using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.DestinatariosUsu;
using Dapper;
using Dominio;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infraestructura.Repository
{
    public class DestinatarioRepository : IDestinatarioRepository
    {
        private readonly IConfiguration configuration;

        public DestinatarioRepository(IConfiguration configuration)
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
                "Select top 1 Id_Destinatario + 1 as UltimoID from TB_Destinatarios order by SistemaFecha desc";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var resultado = dbConnection.ExecuteScalar<int>(sql);
                return resultado;
            }
        }

        public async Task<int> Actualizar(Destinatario entity)
        {
            var sqlActualizar =
                "UPDATE TB_Destinatarios set Usuario = @Usuario where Id_Destinatario = @Id_Destinatario";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var resultado =
                    await dbConnection
                        .ExecuteScalarAsync<int>(sqlActualizar,
                        new {
                            Usuario = entity.Usuario,
                            Id_Destinatario = entity.Id_Destinatario
                        });
                return resultado;
            }
        }

        public async Task<int> Agregar(Destinatario entity)
        {
            entity.Id_Destinatario = ObtenerUltimoID();

            var sql =
                "Insert into TB_Destinatarios(Id_Destinatario, Usuario) Values (@Id_Destinatario,@Usuario)";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", entity.IdMemos, DbType.Int32);

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var resultado = await dbConnection.ExecuteAsync(sql, entity);
                return resultado;
            }
        }

        public async Task<int> Borrar(int id)
        {
            var queryBorrar =
                "DELETE From TB_Destinatarios Where Id_Destinatario = @Id_Destinatario";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var resultado =
                    await dbConnection
                        .ExecuteAsync(queryBorrar,
                        new { Id_Destinatario = id });
                return resultado;
            }
        }

        public async Task<IReadOnlyList<Destinatario>> ObtenerListado()
        {
            var query = "SELECT * FROM TB_Destinatarios";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var resultado =
                    await dbConnection.QueryAsync<Destinatario>(query);
                return resultado.ToList();
            }
        }

        public async Task<Destinatario> ObtenerPorId(int id)
        {
            var query =
                "SELECT * FROM TB_Destinatario WHERE Id_Destinatarios = @Id_Destinatario";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var resultado =
                    await dbConnection
                        .QuerySingleOrDefaultAsync<Destinatario>(query,
                        new { Id_Destinatario = id });
                return resultado;
            }
        }
    }
}
