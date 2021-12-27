using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion;
using Aplicacion.Memos;
using AutoMapper;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/memorandum")]
    public class MemorandumController: ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public MemorandumController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetListado()
        {
           var data = await unitOfWork.Memos.ObtenerListado();
           var dataDTO = _mapper.Map<IEnumerable<MemorandumDTO>>(data);
           return Ok(dataDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Memos.ObtenerPorId(id);
            
            if (data == null) return NotFound($"No se encontró un recurso con el ID: {id}");
            var dataDTO = _mapper.Map<MemorandumDTO>(data);
            
            return Ok(dataDTO);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarMemorandum(MemorandumDTO request)
        {
            var Memorandum  = _mapper.Map<Memorandum>(request);

            var resultado = await unitOfWork.Memos.Agregar(Memorandum);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarMemorandum(int id, MemorandumDTO request)
        {
            var existeMemorandum = await unitOfWork.Memos.ObtenerPorId(id);

            if (existeMemorandum is null)  return NotFound($"No se puede Actualizar el recurso con id {id} porque no existe");

            request.Id = id;
            var actualizarMemorandum= _mapper.Map<Memorandum>(request);
            
            await unitOfWork.Memos.Actualizar(actualizarMemorandum);

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existeMemorandum = await unitOfWork.Memos.ObtenerPorId(id);
            if (existeMemorandum is null)
            {
                return NotFound($"No se puede borrar el recurso con id {id} porque no existe");
            }

            var resultado = await unitOfWork.Memos.Borrar(id);

            return NoContent();
        }
    }
}