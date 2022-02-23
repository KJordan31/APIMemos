
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion;
using Aplicacion.Acciones;
using AutoMapper;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/accion")]
    public class AccionController: ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public AccionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetListado()
        {
           var data = await unitOfWork.Acciones.ObtenerListado();
           var dataDTO = _mapper.Map<IEnumerable<AccionDTO>>(data);
           return Ok(dataDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Acciones.ObtenerPorId(id);
            
            if (data == null) return NotFound($"No se encontró un recurso con el ID: {id}");
            var dataDTO = _mapper.Map<AccionDTO>(data);
            
            return Ok(dataDTO);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarAccion(AccionDTO request)
        {

            var accion  = _mapper.Map<Accion>(request);

            var resultado = await unitOfWork.Acciones.Agregar(accion);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarAccion(int id, AccionDTO request)
        {
            var existeAccion = await unitOfWork.Acciones.ObtenerPorId(id);

            if (existeAccion is null)  return NotFound($"No se puede Actualizar el recurso con id {id} porque no existe");

            request.ID = id;
            var actualizarAccion= _mapper.Map<Accion>(request);
            
            await unitOfWork.Acciones.Actualizar(actualizarAccion);

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existeAccion = await unitOfWork.Acciones.ObtenerPorId(id);
            if (existeAccion is null)
            {
                return NotFound($"No se puede borrar el recurso con id {id} porque no existe");
            }

            var resultado = await unitOfWork.Acciones.Borrar(id);

            return NoContent();
        }
    }
}