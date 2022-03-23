using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Aplicacion;
using Aplicacion.Bitacoras;
using AutoMapper;
using Dapper;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [ApiController]
    [Route("api/bitacora")]

    public class BitacoraController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper _mapper;

        private readonly IConfiguration configuration;

        public BitacoraController(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }

         public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(configuration
                        .GetConnectionString("DefaultConnection"));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetListado()
        {
            var data = await unitOfWork.Bitacoras.ObtenerListado();
            var dataDTO = _mapper.Map<IEnumerable<BitacoraDTO>>(data);
            return Ok(dataDTO);

        }

         [HttpGet("{id}")]

         public async Task<IActionResult> GetById(int id)
         {
             var data = await unitOfWork.Bitacoras.ObtenerPorId(id);

             if (data == null)
             {
                 return NotFound($"No se encontro un recurso con el Id: {id}");
             }
             var dataDTO = _mapper.Map<BitacoraDTO>(data);
             return Ok(dataDTO);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarBitacora(BitacoraDTO request)
        {
            var bitacora = _mapper.Map<Bitacora>(request);

            var resutado = await unitOfWork.Bitacoras.Agregar(bitacora);

            return Ok();
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> ActualizarBitacora(int id, BitacoraDTO request)
        {
            var existeBitacora = await unitOfWork.Bitacoras.ObtenerPorId(id);

            if (existeBitacora is null) return NotFound($"No se puede actualizar el recurso con id {id} porque no existe");
            
            request.Id = id;
            var actualizarBitacora = _mapper.Map<Bitacora>(request);
            await unitOfWork.Bitacoras.Actualizar(actualizarBitacora);
            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var existeBitacora= await unitOfWork.Bitacoras.ObtenerPorId(id);
            if (existeBitacora is null)
            {
                return NotFound($"No se puede borra el recurso con id {id} porque no existe");
            }

            var resultado = await unitOfWork.Bitacoras.Borrar(id);

            return NoContent();
        }

        


    }
}