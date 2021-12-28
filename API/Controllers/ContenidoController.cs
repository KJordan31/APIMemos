using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion;
using Aplicacion.Contenidos;
using AutoMapper;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/contenido")]

    public class ContenidoController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper _mapper;

        public ContenidoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetListado()
        {
            var data = await unitOfWork.Contenidos.ObtenerListado();
            var dataDTO = _mapper.Map<IEnumerable<ContenidoDTO>>(data);
            return Ok(dataDTO);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetByID(int id)
        {
            var data = await unitOfWork.Contenidos.ObtenerPorId(id);

            if (data == null) return NotFound($"No se encontró un recurso con el ID: {id}");
            var dataDTO= _mapper.Map<ContenidoDTO>(data);

            return Ok(dataDTO);
        }

        [HttpPost]

        public async Task<IActionResult> GuardarContenido(ContenidoDTO request)
        {
            var contenido = _mapper.Map<ContenidoMemo>(request);

            var resultado = await unitOfWork.Contenidos.Agregar(contenido);

            return Ok();
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> ActualizarContenido(int id, ContenidoDTO request)
        {
            var existeContenido = await unitOfWork.Contenidos.ObtenerPorId(id);

            if (existeContenido is null) return NotFound($"No se puede Actualizar el recurso con id {id} porque no existe");

            request.ID = id;
            
            var actualizarContenido = _mapper.Map<ContenidoMemo>(request);

            await unitOfWork.Contenidos.Actualizar(actualizarContenido);

            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var existeContenido = await unitOfWork.Contenidos.ObtenerPorId(id);
            if( existeContenido is null) return NotFound ($"No se puede borrar el recurso con id {id} porque no existe");

            var resultado = await unitOfWork.Contenidos.Borrar(id);

            return NoContent();
        }
    }
}