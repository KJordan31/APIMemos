using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Firmas;
using Dapper;
using Dominio;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infraestructura.Repository
{
    public class FirmaRepository : IFirmaRepository
    {
        private readonly IConfiguration configuration;

        public FirmaRepository(IConfiguration configuration)
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
            var sql = "Select top 1 Id_Firma + 1 as UltimoID from TB_Firma order by SistemaFecha desc";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = dbConnection.ExecuteScalar<int>(sql);
                return result;
                 
            }
        }

        public async Task<int> Actualizar(Firma entity)
        {
            var sqlActualizar = "UPDATE TB_Firma set Firmas = @Firmas where Id_Firma = @Id_Firma";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync<int>(sqlActualizar, new {Firmas = entity.Firmas, Id_Firma= entity.Id_Firma});
                return result;
                 
            }
        }

        public async Task<int> Agregar (Firma entity)
        {
            entity.Id_Firma = ObtenerUltimoID();

            var parameters = new DynamicParameters();
            parameters.Add("@Id", entity.IdMemos, DbType.Int32);

            var sql = "Insert into TB_Firma(Id_Firma,Firmas, SistemaUsuario, Id) Values(@Id_Firma,@Firmas, @SistemaUsuario, @Id)";
            using (IDbConnection dbConnection  = Connection)
            {
                dbConnection.Open();
              var result = await dbConnection.ExecuteAsync(sql, entity);                         
              return result;  
            }                
                 
        }
        

        public async Task<int> Borrar(int id)
        {
            var queryBorrar = "DELETE From TB_Firma Where Id_Firma = @Id_Firma";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(queryBorrar, new {Id_Firma = id});
                return result;
                 
            }
        }

        public async Task<IReadOnlyList<Firma>> ObtenerListado()
        {
            var query = "SELECT * FROM TB_Firma";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var resultado = await dbConnection.QueryAsync<Firma>(query);
                return resultado.ToList();
                 
            }
        }

        public async Task<Firma> ObtenerPorId(int id)
        {
            var query = "SELECT * FROM TB_Firma WHERE Id_Firma = @Id_Firma";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var resultado =  await dbConnection.QuerySingleOrDefaultAsync<Firma>(query, new {Id_Firma = id});
                return resultado;
                 
            }
        }

    
    }
}