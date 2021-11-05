using System.Threading.Tasks;
using Aplicacion;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("estado")]
    public class EstadoController : ControllerBase
    {
         private readonly IUnitOfWork unitOfWork;

         public EstadoController(IUnitOfWork unitOfWork)
         {
             this.unitOfWork = unitOfWork;
         }

        [HttpGet]
        public async Task<IActionResult> GetListado()
        {
           var data = await unitOfWork.Estados.ObtenerListado();
           return Ok(data);
        }


[HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Estados.ObtenerPorId(id);
            if (data == null) return NotFound($"No se encontro ID: {id}");
        return Ok(data);
        }

         [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existeEstado = await unitOfWork.Estados.ObtenerPorId(id);
            if (existeEstado is null)
            {
                return NotFound();
            }

            var resultado = await unitOfWork.Estados.Borrar(id);

            return NoContent();
        }

    }
}