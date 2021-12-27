using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion;
using Aplicacion.Estados;
using AutoMapper;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/estado")]
    public class EstadoController : ControllerBase
    {
         private readonly IUnitOfWork unitOfWork;

         private readonly IMapper _mapper;

         public EstadoController(IUnitOfWork unitOfWork, IMapper mapper)
         {
             this.unitOfWork = unitOfWork;
             _mapper = mapper;
         }

        [HttpGet]
        public async Task<IActionResult> GetListado()
        {
           var data = await unitOfWork.Estados.ObtenerListado();
           var dataDTO = _mapper.Map<IEnumerable<EstadoDTO>>(data);
           return Ok(dataDTO);
        }


[HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Estados.ObtenerPorId(id);
            if (data == null) return NotFound($"No se encontro ID: {id}");
            var dataDTO = _mapper.Map<EstadoDTO>(data);

        return Ok(dataDTO);
        }

         [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existeEstado = await unitOfWork.Estados.ObtenerPorId(id);
            if (existeEstado is null)
            {
                return NotFound($"No se puede borrar por no existe el Id {id}");
            }

            var resultado = await unitOfWork.Estados.Borrar(id);

            return NoContent();
        }

        [HttpPost]
       
        public async Task<IActionResult> GuardarEstado(EstadoDTO request)
        {
        var estado = _mapper.Map<Estado>(request);
        var resultado = await unitOfWork.Estados.Agregar(estado);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarEstado(int id, EstadoDTO request)
        {
            var existeEstado = await unitOfWork.Estados.ObtenerPorId(id);

            if (existeEstado is null)  return NotFound($"No se puede Actualizar el recurso con id {id} porque no existe");

          request.Id = id;
          var actualizarEstado = _mapper.Map<Estado>(request);

            await unitOfWork.Estados.Actualizar(actualizarEstado);

            return Ok();
        }



    }
}