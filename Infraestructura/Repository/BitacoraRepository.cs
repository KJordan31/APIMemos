using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Bitacoras;
using Dapper;
using Dominio;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infraestructura.Repository
{
    public class BitacoraRepository : IBitacoraRepository
    {
        private readonly IConfiguration configuration;

        public BitacoraRepository(IConfiguration configuration)
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
            var sql = "Select top 1 Id_Bitacora + 1 as UltimoID from TB_Bitacora order by SistemaFecha desc";
            using (IDbConnection dbConnection = Connection)
            {
                 dbConnection.Open();
                 var result = dbConnection.ExecuteScalar<int>(sql);
                 return result;
            }
        }

        public async Task<int> Actualizar(Bitacora entity)
        {
            var sqlActualizar = "UPDATE TB_Bitacora set Observacion = @Observacion where Id_Bitacora = @Id_Bitacora";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync<int>(sqlActualizar, new { Observacion = entity.Observacion, Id_Bitacora = entity.Id_Bitacora});
                return result;
                 
            }
        }

        public async Task<int> Agregar(Bitacora entity)
        {
            entity.Id_Bitacora = ObtenerUltimoID();

            var sql = "Insert into TB_Bitacora(Id_Bitacora, Observacion, SitemaUsuario, Id_Accion) Values(@Id_Bitacora,@Observacion, @SistemaUsuario, @Id_Accion)";

            var parameters = new DynamicParameters();
            parameters.Add("@Id_Accion", entity.IdAcciones, DbType.Int32);


            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(sql, entity);
                return result;
                 
            }
        }

        public async Task<int> Borrar(int id)
        {
            var queryBorrar = "DELETE From TB_Bitacora Where Id_Bitacora = @Id_Bitacora";
            using (IDbConnection dbConnection = Connection)
            {
                 dbConnection.Open();
                 var resultado = await dbConnection.ExecuteAsync(queryBorrar, new {Id_Bitacora = id});
                 return resultado;
            }
        }

        public async Task<IReadOnlyList<Bitacora>> ObtenerListado()
        {
            var query = "SELECT * FROM TB_Bitacora";
            using (IDbConnection dbConnection = Connection)
            {
                 dbConnection.Open();
                 var resultado =  await dbConnection.QueryAsync<Bitacora>(query);
                 return resultado.ToList();
            }
        }

        public async Task<Bitacora> ObtenerPorId(int id)
        {
            var query = "SELECT * FROM TB_Bitacora WHERE Id_Bitacora = @Id_Bitacora";
            using (IDbConnection dbConnection = Connection)
            {
                 dbConnection.Open();
                 var resultado = await dbConnection.QueryFirstOrDefaultAsync<Bitacora>(query, new{Id_Bitacora = id});
                 return resultado;
            }
        }
    }
}