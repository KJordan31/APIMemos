using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Tipos;
using Dapper;
using Dominio;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infraestructura.Repository
{
    public class TipoMemorandumRepository : ITipoMemorandumRepository
    {
        private readonly IConfiguration configuration;

        public TipoMemorandumRepository(IConfiguration configuration)
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
            var sql =  "SELECT TOP 1 Id_Tipo + 1 as UltimoID from TB_Tipo_Memorandum";
            using (IDbConnection dbConnection = Connection)
            {
                 dbConnection.Open();
                 var resultado = dbConnection.ExecuteScalar<int>(sql);
                 return resultado;
            }
        }

        public async Task<int> Actualizar(TipoMemorandum entity)
        {
            var sqlActualizar = "UPDATE TB_Tipo_Memorandum set Tipo=@Tipo, EnUso = @EnUso where Id_Tipo = @Id_Tipo";
            using (IDbConnection dbConnection = Connection)
            {
                 dbConnection.Open();
                 var resultado = await dbConnection.ExecuteScalarAsync<int>(sqlActualizar, new {tipo = entity.Tipo,EnUso = entity.EnUso, Id_Tipo = entity.Id_Tipo });
                 return resultado;
            }
            
        }


        public async Task<int> Agregar(TipoMemorandum entity)
        {
            entity.Id_Tipo = ObtenerUltimoID();

            var sqlAdd = "INSERT INTO TB_Tipo_Memorandum(Id_Tipo,Tipo,SistemaUsuario) VALUES (@Id_Tipo,@Tipo,@SistemaUsuario)";
       using (IDbConnection dbConnection = Connection)
       {
           dbConnection.Open();
           var resultado = await dbConnection.ExecuteAsync(sqlAdd, entity);
             return resultado;
       }
        }

        public async Task<int> Borrar(int id)
        {
            var sqlBorrar = "DELETE From TB_Tipo_Memorandum Where Id_Tipo=@Id_Tipo";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var resultado = await dbConnection.ExecuteAsync(sqlBorrar, new {Id_Tipo = id});
                 return resultado;
            }
        }

        public async Task<IReadOnlyList<TipoMemorandum>> ObtenerListado()
        {
            var sqlListar = "SELECT * FROM TB_Tipo_Memorandum";
            using (IDbConnection dbConnection = Connection)
            {
                 dbConnection.Open();
                 var resultado = await dbConnection.QueryAsync<TipoMemorandum>(sqlListar);
                 return resultado.ToList();

            }
        }

        public async Task<TipoMemorandum> ObtenerPorId(int id)
        {
            var query = "SELECT * FROM TB_Tipo_Memorandum WHERE Id_Tipo = @Id_Tipo";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var resultado = await dbConnection.QuerySingleOrDefaultAsync<TipoMemorandum>(query , new {Id_Tipo = id});
                return resultado;
                 
            }

        }




    }
}