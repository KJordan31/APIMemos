using System.Threading.Tasks;
using System.Collections.Generic;

namespace Aplicacion.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
         Task<T> ObtenerPorId(int id);

         Task<IReadOnlyList<T>> ObtenerListado();         

         Task<int> Agregar(T entity);

         Task<int> Actualizar(T entity);

         Task<int> Borrar(int id);
    }
}