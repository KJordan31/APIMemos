using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion;
using Aplicacion.Adjuntos;
using AutoMapper;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/adjuntos")]

    public class AdjuntoController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper _mapper;

        public AdjuntoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetListado()
        {
            var data = await unitOfWork.Adjuntos.ObtenerListado();
            var dataDTO = _mapper.Map<IEnumerable<AdjuntoDTO>>(data);
            return Ok(dataDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Adjuntos.ObtenerPorId(id);

            if (data == null) return NotFound($"No se encontro un recurso con el ID: {id}");


            var dataDTO = _mapper.Map<AdjuntoDTO>(data);
            return Ok(dataDTO);

        }

        [HttpPost]

        public async Task<IActionResult> GuardarAdjunto(AdjuntoDTO request)
        {
            var adjunto = _mapper.Map<Adjunto>(request);
            var resultado = await unitOfWork.Adjuntos.Agregar(adjunto);
            return Ok();
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> ActualizarAdjunto(int id, AdjuntoDTO request)
        {
            var existeAdjunto = await unitOfWork.Adjuntos.ObtenerPorId(id);
            if (existeAdjunto is null) return NotFound($"No se puedo Actualizar el recurso con Id {id} porque no existe");       
            
            request.Id = id;
            var actualizarAdjunto = _mapper.Map<Adjunto>(request);
            await unitOfWork.Adjuntos.Actualizar(actualizarAdjunto);
            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var existeAdjunto = await unitOfWork.Adjuntos.ObtenerPorId(id);
            if (existeAdjunto is null)
            {
                return NotFound($"No se puede borra el recurso con el Id {id} porque no existe");
            }

            var resultado = await unitOfWork.Adjuntos.Borrar(id);
            return NoContent();
        }
    }
}