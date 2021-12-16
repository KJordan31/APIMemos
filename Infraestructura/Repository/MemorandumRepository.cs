using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Memorandums;
using Dapper;
using Dominio;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infraestructura.Repository
{

    public class MemorandumRepository : IMemorandumRepository
    {
        
            private readonly IConfiguration configuration;

            public MemorandumRepository (IConfiguration configuration)
            {
                this.configuration = configuration;
            }

            public IDbConnection Connection
            {
                get
                {
                  return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
                }
            }
        
        private int ObtenerUltimoID()
        {
            var sql ="select top 1 Id + 1 as UltimoID from TB_Memorandum order by SistemaFecha desc";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = dbConnection.ExecuteScalar<int>(sql);
                return result;          
            }
        }

        public async Task<int> Actualizar(Memorandum entity)
        {
            var sqlActualizar ="UPDATE TB_Memorandum set  Asunto = @Asunto where Id = @Id";
            using (IDbConnection dbConnection = Connection)
            {
                 dbConnection.Open();
                 var result = await dbConnection.ExecuteScalarAsync<int>(sqlActualizar, new {Asunto = entity.Asunto, Id = entity.Id});
                 return result;
            }
        }

        public async Task<int> Agregar(Memorandum entity)
        {
            entity.Id = ObtenerUltimoID();

            var sqlAdd= "Insert into TB_Memorandum(Id,Asunto, SistemaUsuario, Codigo) Values(@Id,@Asunto, @SistemaUsuario, @Codigo)";
            using (IDbConnection dbConnection = Connection)
            {
                 dbConnection.Open();
                 var result = await dbConnection.ExecuteAsync(sqlAdd, entity);
                 return result;
            }
        }

        public async Task<int> Borrar (int id)
        {
            var sqlDelete = "DELETE From TB_Memorandum Where Id = @Id";
            using (IDbConnection dbConnection = Connection)
            {
                 dbConnection.Open();
                 var result = await dbConnection.ExecuteAsync(sqlDelete, new { Id = id});
                 return  result;
            }
        }

        public async Task<IReadOnlyList<Memorandum>> ObtenerListado()
        {
            var sql = "SELECT * FROM TB_Memorandum";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.QueryAsync<Memorandum>(sql);
                return result.ToList();
                 
            }
        }

        public async Task<Memorandum> ObtenerPorId(int id)
        {
            var sql = "SELECT * FROM TB_Memorandum WHERE Id = @Id";
            using (IDbConnection dbConnection = Connection)
            {
                 dbConnection.Open();
                 var result = await dbConnection.QuerySingleOrDefaultAsync<Memorandum>(sql, new {Id = id});
                 return result;
            }
        }
    }

}