using System;
using System.Threading.Tasks;
using Aplicacion.Interfaces;
using Dominio;

namespace Aplicacion.Usuarios
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {

        Task<Usuario> LoginAsync(string correo, string contraseña);
       
        
    }
}
