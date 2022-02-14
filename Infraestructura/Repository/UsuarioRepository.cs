using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Usuarios;
using Dapper;
using Dominio;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infraestructura.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConfiguration configuration;

        public UsuarioRepository(IConfiguration configuration)
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
            var query =
                "SELECT top 1 Id_Usuario + 1 as UltimoID from TB_Usuarios order by SistemaFecha desc";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var resultado = dbConnection.ExecuteScalar<int>(query);
                return resultado;
            }
        }

        public async Task<int> Actualizar(Usuario entity)
        {
            var sqlActualizar =
                "UPDATE TB_Usuarios set Nombre = @Nombre, Apellidos = @Apellidos, Correo = @Correo, Contraseña = @Contraseña, Super_Usuario = @Super_Usuario where Id_Usuario = @Id_Usuario";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result =
                    await dbConnection
                        .ExecuteScalarAsync<int>(sqlActualizar,
                        new {
                            Nombre = entity.Nombre,
                            Apellidos = entity.Apellidos,
                            Correo = entity.Correo,
                            Contraseña = entity.Contraseña,
                            Super_Usuario = entity.Super_Usuario,
                            Id_Usuario = entity.Id_Usuario
                        });
                return result;
            }
        }

        public async Task<int> Agregar(Usuario entity)
        {
            entity.Id_Usuario = ObtenerUltimoID();

            var queryAdd =
                "INSERT INTO TB_Usuarios(Id_Usuario, Nombre, Apellidos, Correo, Contraseña, Super_Usuario) Values (@Id_Usuario, @Nombre, @Apellidos, @Correo, @Contraseña,@Super_Usuario)";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var resultado =
                    await dbConnection.ExecuteAsync(queryAdd, entity);
                return resultado;
            }
        }

        public async Task<int> Borrar(int id)
        {
            var queryDelete =
                "DELETE FROM TB_Usuarios WHERE Id_Usuario=@Id_Usuario";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var resultado =
                    await dbConnection
                        .ExecuteAsync(queryDelete, new { Id_Usuario = id });
                return resultado;
            }
        }

        public async Task<IReadOnlyList<Usuario>> ObtenerListado()
        {
            var query = "SELECT * FROM TB_Usuarios";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var resultado = await dbConnection.QueryAsync<Usuario>(query);
                return resultado.ToList();
            }
        }

        public async Task<Usuario> ObtenerPorId(int id)
        {
            var query =
                "SELECT * From TB_Usuarios WHERE Id_Usuario=@Id_Usuario";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var resultado =
                    await dbConnection
                        .QueryAsync<Usuario>(query, new { Id_Usuario = id });
                return resultado.FirstOrDefault();
            }
        }
    }
}
