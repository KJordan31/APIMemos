using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion;
using Aplicacion.Usuarios;
using AutoMapper;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/usuario")]

    public class UsuarioController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper _mapper;

        public UsuarioController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetListado()
        {
            var data = await unitOfWork.Usuarios.ObtenerListado();
            var dataDTO = _mapper.Map<IEnumerable<UsuarioDTO>>(data);
            return Ok(dataDTO);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Usuarios.ObtenerPorId(id);

            if ( data == null ) return NotFound($"No se encontró un recurso con el Id: {id}");
            var dataDTO = _mapper.Map<UsuarioDTO>(data);
            return Ok(dataDTO);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarUsuario(UsuarioDTO request)
        {
            var usuario = _mapper.Map<Usuario>(request);
            var resultado = await unitOfWork.Usuarios.Agregar(usuario);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, UsuarioDTO request)
        {
            var existeUsuario = await unitOfWork.Usuarios.ObtenerPorId(id);
            if (existeUsuario is null) return NotFound($"No se puede Actualizar el recurso con Id:{id} porque no existe");
             request.Id = id;
             var actualizarUsuario = _mapper.Map<Usuario>(request);
             await unitOfWork.Usuarios.Actualizar(actualizarUsuario);
             return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existeUsu = await unitOfWork.Usuarios.ObtenerPorId(id);
            if (existeUsu is null)
            {
                return NotFound($"No se puede borrar el recurso con id {id} porque no existe");
            }

            var resultado = await unitOfWork.Usuarios.Borrar(id);

            return NoContent();
        }
        
    }
}