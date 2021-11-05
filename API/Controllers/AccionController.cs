using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion;
using Aplicacion.Acciones;
using Dominio;
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

        [HttpPost]
        public async Task<IActionResult> GuardarAccion(AccionDTO request)
        {
            Accion accion = new()
            {
                Descripcion = request.Descripcion,
                SistemaUsuario = request.Usuario
            };
            var resultado = await unitOfWork.Acciones.Agregar(accion);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarAccion(int id, AccionDTO request)
        {
            var existeAccion = await unitOfWork.Acciones.ObtenerPorId(id);

            if (existeAccion is null)  return NotFound($"No se puede Actualizar el recurso con id {id} porque no existe");

            Accion actulizarAccion =  existeAccion;

            actulizarAccion.Descripcion = request.Descripcion;

            await unitOfWork.Acciones.Actualizar(actulizarAccion);

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