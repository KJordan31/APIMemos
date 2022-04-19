using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Interfaces;
using Dominio;

namespace Aplicacion.Memos
{
    public interface IMemorandumRepository : IGenericRepository<Memorandum>
    {
        Task<Memorandum> ObtenerPorNombre(string destinatarioUsu);

       
    }
}