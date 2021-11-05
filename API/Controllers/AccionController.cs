using System.Threading.Tasks;
using Aplicacion;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("accion")]
    public class AccionController: ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public AccionController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetListado()
        {
           var data = await unitOfWork.Acciones.ObtenerListado();
           return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Acciones.ObtenerPorId(id);
            
            if (data == null) return NotFound($"No se encontró un recurso con el ID: {id}");
            
            return Ok(data);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existeAccion = await unitOfWork.Acciones.ObtenerPorId(id);
            if (existeAccion is null)
            {
                return NotFound();
            }

            var resultado = await unitOfWork.Acciones.Borrar(id);

            return NoContent();
        }
    }
}