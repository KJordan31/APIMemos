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

        public async Task<int> Actualizar(Estado entity)
        {   
           var queryActualizar = "UPDATE TB_Estado SET Descripcion=@Descripcion WHERE Id_Estado=@Id_Estado)";
           using (IDbConnection dbConnection = Connection)
           {
                dbConnection.Open();
                var resultado = await dbConnection.ExecuteAsync(queryActualizar, entity);
                return resultado;
           }
        }

        public async Task<int> Agregar(Estado entity)
        {
            
           var queryAdd = "INSERT INTO TB_Estado (Id_Estado, Descripcion) Values (@Id_Estado,@Descripcion)";
           using (IDbConnection dbConnection = Connection)
           {
               dbConnection.Open();
               var resultado = await dbConnection.ExecuteAsync(queryAdd, entity);
               return resultado;
                
           }
        }

        public async Task<int> Borrar(int id)
        {
            var queryDelete = "DELETE FROM TB_Estado WHERE Id_Estado=@ID";
            using (IDbConnection dbConnection =Connection)
            {
                dbConnection.Open();
                var resultado = await dbConnection.ExecuteAsync(queryDelete, new {ID=id});
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
            var query = "SELECT * From TB_Estado WHERE Id_Estado=@ID";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var resultado = await dbConnection.QueryAsync<Estado>(query, new {ID = id});
                 return resultado.FirstOrDefault();
            }
            }
        }
    }
    