using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion;
using Aplicacion.Firmas;
using AutoMapper;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/firma")]

    public class FirmaController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper _mapper;

        public FirmaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetListado()
        {
            var data = await unitOfWork.Firmas.ObtenerListado();
            var dataDTO = _mapper.Map<IEnumerable<FirmaDTO>>(data);
            return Ok(dataDTO);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Firmas.ObtenerPorId(id);
            if (data == null)
            {
                return NotFound($"No se encontró un recurso con el ID: {id} ");

            }

            var dataDTO = _mapper.Map<FirmaDTO>(data);

            return Ok(dataDTO);
        }

        [HttpPost]

        public async Task<IActionResult> GuardarFirma(FirmaDTO request)
        {
            var firma = _mapper.Map<Firma>(request);
            var resultado = await unitOfWork.Firmas.Agregar(firma);
            return Ok();
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> ActualizarFirma(int id, FirmaDTO request)
        {
            var existeFirma = await unitOfWork.Firmas.ObtenerPorId(id);
            if (existeFirma is null)
            {
                return NotFound($"No se puede Actualizar el recurso con id {id} porque no existe");

            }

            request.Id = id;
            var actualizarFirma = _mapper.Map<Firma>(request);
            await unitOfWork.Firmas.Actualizar(actualizarFirma);
            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var existeFirma = await unitOfWork.Firmas.ObtenerPorId(id);
            if (existeFirma is null)
            {
                return NotFound($"No se puede borrar el recurso con Id {id} porque no existe");

            }

            var resultado = await unitOfWork.Firmas.Borrar(id);

            return NoContent();
        }
    }
}