using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion;
using Aplicacion.Destinatarios;
using AutoMapper;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/tipodestinatario")]

    public class TipoDestinatarioController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper _mapper;

        public TipoDestinatarioController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

[HttpGet]
        public async Task<IActionResult> GetListado()
        {
            var data = await unitOfWork.Destinatarios.ObtenerListado();
            var dataDTO = _mapper.Map<IEnumerable<TipoDestinatarioDTO>>(data);
            return Ok(dataDTO);
        }

[HttpGet("{id}")]
public async Task<IActionResult> GetById(int id)
{
    var data = await unitOfWork.Destinatarios.ObtenerPorId(id);

    if (data == null)  return NotFound($"No se encontro un recurso con ID {id}");
    var dataDTO = _mapper.Map<TipoDestinatarioDTO>(data);
    
    return Ok(dataDTO);
}


  [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarDestinatario(int id, TipoDestinatarioDTO request)
        {
            var existeAccion = await unitOfWork.Destinatarios.ObtenerPorId(id);

            if (existeAccion is null)  return NotFound($"No se puede Actualizar el recurso con id {id} porque no existe");

            request.Id = id;
            var actualizarAccion= _mapper.Map<TipoDestinatario>(request);
            
            await unitOfWork.Destinatarios.Actualizar(actualizarAccion);

            return Ok();
        }

[HttpPost]

   public async Task<IActionResult> GuardarDestinatario(TipoDestinatarioDTO request)
        {
            var accion  = _mapper.Map<TipoDestinatario>(request);

            var resultado = await unitOfWork.Destinatarios.Agregar(accion);

            return Ok();
        }


[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
    var existeDestinatario =  await unitOfWork.Destinatarios.ObtenerPorId(id);
    if (existeDestinatario is null)
    {
        return NotFound($"No se puede Borrar el recurso con el ID: {id} por que no existe");
    }
    var resultado = await unitOfWork.Destinatarios.Borrar(id);
    return NoContent();
}


    }
    
}