using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Destinatarios;
using Dapper;
using Dominio;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infraestructura.Repository
{
    public class TipoDestinatarioRepository : ITipoDestinatarioRepository
    {
        private readonly IConfiguration configuration;

        public TipoDestinatarioRepository(IConfiguration configuration)
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
            var sql = "select top 1 Id_Tipo_Destinatario + 1 as UltimoID from TB_Tipo_Destinatario order by SistemaFecha desc";
            using (IDbConnection dbConnection = Connection)
            {
                 dbConnection.Open();
                 var resultado = dbConnection.ExecuteScalar<int>(sql);
                 return resultado;
            }
        }

        public async Task<int> Actualizar(TipoDestinatario entity)
        {
            var sqlActualizar = "UPDATE TB_Tipo_Destinatario set Descripcion=@Descripcion,EnUso = @EnUso where Id_Tipo_Destinatario = @Id_Tipo_Destinatario";
            using (IDbConnection dbConnection = Connection)
            {
                 dbConnection.Open();
                 var resultado = await dbConnection.ExecuteScalarAsync<int>(sqlActualizar, new{descripcion = entity.Descripcion,EnUso= entity.EnUso, Id_Tipo_Destinatario = entity.Id_Tipo_Destinatario});
                 return resultado;
            }
        }

        public async Task<int> Agregar(TipoDestinatario entity)
        {
            entity.Id_Tipo_Destinatario = ObtenerUltimoID();

            var sqlAdd = "INSERT INTO TB_Tipo_Destinatario(Id_Tipo_Destinatario, Descripcion, SistemaUsuario) VALUES (@Id_Tipo_Destinatario, @Descripcion, @SistemaUsuario)";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var resultado = await dbConnection.ExecuteAsync(sqlAdd , entity);
                return resultado;
                 
            }
        }

        public async Task<int> Borrar(int id)
        {
            var sqlBorrar = "DELETE FROM TB_Tipo_Destinatario Where Id_Tipo_Destinatario = @Id_Tipo_Destinatario";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var resultado = await dbConnection.ExecuteAsync(sqlBorrar, new {Id_Tipo_Destinatario = id});
                 return resultado;
            }
        }
        
        public async Task<IReadOnlyList<TipoDestinatario>> ObtenerListado()
        {
            var sqlListar = "SELECT * FROM TB_Tipo_Destinatario";
            using (IDbConnection dbConnection = Connection)
            {
                 dbConnection.Open();
                 var resultado = await dbConnection.QueryAsync<TipoDestinatario>(sqlListar);
                 return resultado.ToList();
            }
        }


        public async Task<TipoDestinatario> ObtenerPorId(int id)
        {
            var query = "SELECT * FROM TB_Tipo_Destinatario WHERE Id_Tipo_Destinatario=@Id_Tipo_Destinatario";
            using (IDbConnection dbConnection = Connection)
            {
                 dbConnection.Open();
                 var resultado = await dbConnection.QuerySingleOrDefaultAsync<TipoDestinatario>(query, new {Id_Tipo_Destinatario = id});
                 return resultado;
            }
        }
    }
    
}