using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Estados;
using Dapper;
using Dominio;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infraestructura.Repository
{
    public class EstadoRepository : IEstadoRepository
    {

        private readonly IConfiguration configuration;
        public EstadoRepository( IConfiguration configuration)
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
            var query = "SELECT top 1 Id_Estado + 1 as UltimoID from TB_Estado order by SistemaFecha desc";
            using (IDbConnection dbConnection = Connection )
            {
                 dbConnection.Open();
                 var resultado = dbConnection.ExecuteScalar<int>(query);
                 return resultado;
            }
        }

         public async Task<int> Actualizar(Estado entity)
        {
            var sqlActualizar ="UPDATE TB_Estado set  Descripcion = @Descripcion where Id_Estado = @Id_Estado";
            using (IDbConnection dbConnection = Connection)
            {
                 dbConnection.Open();
                 var result = await dbConnection.ExecuteScalarAsync<int>(sqlActualizar, new {Descripcion = entity.Descripcion, Id_Estado = entity.Id_Estado});
                 return result;
            }
        }

        public async Task<int> Agregar(Estado entity)
        {
            entity.Id_Estado = ObtenerUltimoID();

           var queryAdd = "INSERT INTO TB_Estado (Id_Estado, Descripcion,SistemaUsuario) Values (@Id_Estado,@Descripcion,@SistemaUsuario)";
           using (IDbConnection dbConnection = Connection)
           {
               dbConnection.Open();
               var resultado = await dbConnection.ExecuteAsync(queryAdd, entity);
               return resultado;
                
           }
        }

        public async Task<int> Borrar(int id)
        {
            var queryDelete = "DELETE FROM TB_Estado WHERE Id_Estado=@Id_Estado";
            using (IDbConnection dbConnection =Connection)
            {
                dbConnection.Open();
                var resultado = await dbConnection.ExecuteAsync(queryDelete, new {Id_Estado=id});
                 return resultado;
            }
        }

        public async Task<IReadOnlyList<Estado>> ObtenerListado()
        {
            var query = "SELECT * FROM TB_Estado";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var resultado = await dbConnection.QueryAsync<Estado>(query);
                return resultado.ToList();
            }
        }

        public async Task<Estado> ObtenerPorId(int id)
        {
            var query = "SELECT * From TB_Estado WHERE Id_Estado=@Id_Estado";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var resultado = await dbConnection.QueryAsync<Estado>(query, new {Id_Estado = id});
                 return resultado.FirstOrDefault();
            }
            }
        }
    }
    