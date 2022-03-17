using System.Threading.Tasks;
using Apliacacion.Plantillas;
using Aplicacion;
using AutoMapper;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/plantillas")]

    public class PlantillaController : ControllerBase
    {
         private readonly IUnitOfWork unitOfWork;
          private readonly IMapper _mapper;

         
        public PlantillaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
             _mapper = mapper;
           
        }

          [HttpGet]
        public async Task<IActionResult> GetListado()
        {
           var data = await unitOfWork.Plantillas.ObtenerListado();           
           return Ok(data);
        }

          [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Plantillas.ObtenerPorId(id);
            
            if (data == null) return NotFound($"No se encontró un recurso con el ID: {id}");
            
            
            return Ok(data);
        }
         [HttpPost]
        public async Task<IActionResult> GuardarAccion(PlantillaDTO request)
        {

            var plantilla  = _mapper.Map<Plantilla>(request);

            var resultado = await unitOfWork.Plantillas.Agregar(plantilla);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarAccion(int id, PlantillaDTO request)
        {
            var existeAccion = await unitOfWork.Acciones.ObtenerPorId(id);

            if (existeAccion is null)  return NotFound($"No se puede Actualizar el recurso con id {id} porque no existe");

            request.id = id;
            var actualizarAccion= _mapper.Map<Plantilla>(request);
            
            await unitOfWork.Plantillas.Actualizar(actualizarAccion);

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existeAccion = await unitOfWork.Plantillas.ObtenerPorId(id);
            if (existeAccion is null)
            {
                return NotFound($"No se puede borrar el recurso con id {id} porque no existe");
            }

            var resultado = await unitOfWork.Plantillas.Borrar(id);
        

            return NoContent();
        }
    }

     
}