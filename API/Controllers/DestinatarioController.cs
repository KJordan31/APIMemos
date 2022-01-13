using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion;
using Aplicacion.DestinatariosUsu.cs;
using AutoMapper;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/desti")]
    
    public class DestinatarioController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper _mapper;

        public DestinatarioController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetListado()
        {
            var data = await unitOfWork.DestinatariosUsu.ObtenerListado();
            var dataDTO = _mapper.Map<IEnumerable<DestinatarioDTO>>(data);
            return Ok(dataDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.DestinatariosUsu.ObtenerPorId(id);

            if (data == null)
            {
                return NotFound($"No se encontró un recurso con el ID: {id}");

            }

            var dataDTO = _mapper.Map<DestinatarioDTO>(data);
            return Ok(dataDTO);
        }

        [HttpPost]

        public async Task<IActionResult> GuardarDestinatario(DestinatarioDTO request)
        {
            var destinatario = _mapper.Map<Destinatario>(request);
            var resultado = await unitOfWork.DestinatariosUsu.Agregar(destinatario);
            return Ok();
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> ActualizarDestinatario(int id, DestinatarioDTO request)
        {
            var existeDestinatario = await unitOfWork.DestinatariosUsu.ObtenerPorId(id);
            if (existeDestinatario is null)  return NotFound($"No se puede Actualizar el recurso con id {id} por que no existe");
            
            request.Id = id;
            var actualizarDesti = _mapper.Map<Destinatario>(request);
            await unitOfWork.DestinatariosUsu.Actualizar(actualizarDesti);
            return Ok();
            
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var existeDestinatario =  await unitOfWork.DestinatariosUsu.ObtenerPorId(id);
            if (existeDestinatario is null)
            {
                return NotFound($"No se puede eliminar el recurso con Id {id} por que no existe");

            }

            var resultado = await unitOfWork.DestinatariosUsu.Borrar(id);

            return NoContent();
        }
    }
}