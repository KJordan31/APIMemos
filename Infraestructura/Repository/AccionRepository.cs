using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Acciones;
using Dapper;
using Dominio;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infraestructura.Repository
{
    public class AccionRepository : IAccionRepository
    {
        private readonly IConfiguration configuration;

        public AccionRepository(IConfiguration configuration)
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

        public Task<int> Actualizar(Accion entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Agregar(Accion entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> Borrar(int id)
        {
            var queryBorrar = "DELETE From TB_Accion Where Id_Accion = @Id";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(queryBorrar, new {Id = id});
                return result;
            }
        }

        public async Task<IReadOnlyList<Accion>> ObtenerListado()
        {
            var query = "SELECT * FROM TB_Accion";
            using (IDbConnection dbConnection = Connection)
            {
                 dbConnection.Open();
                 var resultado = await dbConnection.QueryAsync<Accion>(query);
                 return resultado.ToList();
            }
        }

        public async Task<Accion> ObtenerPorId(int id)
        {
            var query = "SELECT * FROM TB_Accion WHERE Id_Accion = @Id";
            using (IDbConnection dbConnection = Connection)
            {
                 dbConnection.Open();
                 var result = await dbConnection.QuerySingleOrDefaultAsync<Accion>(query, new {Id = id});
                 return result;
            }
        }
    }
}