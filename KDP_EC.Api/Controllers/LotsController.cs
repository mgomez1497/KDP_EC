using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using KDP_EC.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotsController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        private readonly ILots _ILot;

        public LotsController(JwtSettings jwtSettings, ILots ILot)
        {
            _jwtSettings = jwtSettings;
            _ILot = ILot;
        }

       

        [HttpGet("getLotsByFarmId")]
        public IActionResult GetLotsByFarmId(Guid FarmId,Guid? TipoLote = null,Guid? VariedadLote = null,Guid? TipoRenovacion = null)
        {
            var result = _ILot.GetLotsbyFarmId(FarmId, TipoLote, VariedadLote, TipoRenovacion);

            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron lotes." });
            }

            return Ok(result);
        }

        [HttpGet("getLotsByFarmIdApi")]

        public IActionResult GetLotsbyFarmIdAPI(Guid FarmId)
        {
            var result = _ILot.GetLotsbyFarmIdAPI(FarmId);
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron lotes." });
            }
            return Ok(result);
        }

        [HttpPost("createLots")]

        public IActionResult CreateLots([FromBody] List<Core.Models.Lots> lotes)
        {
            if (lotes == null || !lotes.Any())
            {
                return BadRequest(new { message = "Lista de lotes vacía o inválida." });
            }
            int procesados = 0;

            foreach (var lote in lotes)
            {
                var result = _ILot.CreateLots(lote);
                if (result > 0) procesados++;
            }

            return Ok(new { message = $"{procesados} lotes sincronizados correctamente." });
        }
    }
}
