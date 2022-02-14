using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion;
using Aplicacion.Tipos;
using AutoMapper;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/TipoMemorandum")]

    public class TipoMemorandumController: ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper _mapper;

        public TipoMemorandumController (IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }


 [HttpGet]
        public async Task<IActionResult> GetListado()
        {
            var data = await unitOfWork.Tipos.ObtenerListado();
            var dataDTO = _mapper.Map<IEnumerable<TipoMemorandumDTO>>(data);
            return Ok(dataDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Tipos.ObtenerPorId(id);
            if (data == null) return NotFound($"No se encontro el recurso con el Id: {id} ");
            var dataDTO = _mapper.Map<TipoMemorandumDTO>(data);

            return Ok(dataDTO);
            
                
            }

            [HttpPost]
            public async Task<IActionResult> GuardarTipo(TipoMemorandumDTO request)
            {
               var tipo = _mapper.Map<TipoMemorandum>(request);
               
                var resultado = await unitOfWork.Tipos.Agregar(tipo);

                return Ok();


            }

            [HttpPut("{id}")]

            public async Task<IActionResult> ActualizarTipo(int id, TipoMemorandumDTO request)
            {
                var existeTipo = await unitOfWork.Tipos.ObtenerPorId(id);
                if (existeTipo is null) return NotFound($"No se puede Actualizar el recurso con id {id} porque no existe");

              request.Id = id;
              var actualizarTipo = _mapper.Map<TipoMemorandum>(request);

                await unitOfWork.Tipos.Actualizar(actualizarTipo);

                return Ok();
                
            }

            [HttpDelete("{id}")]

            public async Task<IActionResult> Delete(int id)
            {
                var existeTipo =await unitOfWork.Tipos.ObtenerPorId(id);
                if (existeTipo is null)
                {
                    return NotFound($"No se puede Borrar el recurso porque no existe el ID {id}");
                }

                var resultado = await unitOfWork.Tipos.Borrar(id);

                return NoContent();
            }
        
    }
    
}