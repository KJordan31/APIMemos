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

        //TODO ObtenerUltimoID.
        private int ObtnerUltimoID()
        {
            var sql = "select top 1 ID + 1 as UltimoID from TB_Accion order by SistemaFecha desc";
            using (IDbConnection dbConnection = Connection)
            {
                 dbConnection.Open();
                 var result = dbConnection.ExecuteScalar<int>(sql);
                 return result;
            }
        }

        public async Task<int> Actualizar(Accion entity)
        {
            var sqlActualizar = "UPDATE TB_Accion set Descripcion = @descripcion where ID = @id";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync<int>(sqlActualizar, new {descripcion = entity.Descripcion , id = entity.ID});
                return result;
            }
        }

        public async Task<int> Agregar(Accion entity)
        {

            entity.ID = ObtnerUltimoID();

            var sql = "Insert into TB_Accion(ID,Descripcion, SistemaUsuario) Values(@ID,@Descripcion, @SistemaUsuario)";
            using (IDbConnection dbConnection = Connection)
            {
                 dbConnection.Open();
                 var result = await dbConnection.ExecuteAsync(sql, entity);
                 return result;
            }
        }

        public async Task<int> Borrar(int id)
        {
            var queryBorrar = "DELETE From TB_Accion Where ID = @Id";
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
            var query = "SELECT * FROM TB_Accion WHERE Id = @Id";
            using (IDbConnection dbConnection = Connection)
            {
                 dbConnection.Open();
                 var result = await dbConnection.QuerySingleOrDefaultAsync<Accion>(query, new {Id = id});
                 return result;
            }
        }
    }
}