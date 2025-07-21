using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using KDP_EC.Core.ModelView;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarmsController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        private readonly IFarms _IFarm;

        public FarmsController(JwtSettings jwtSettings, IFarms IFarm)
        {
            _jwtSettings = jwtSettings;
            _IFarm = IFarm;
        }

        [HttpGet("getFarmsByPersonId")]

        public IActionResult GetFarmsByPersonId(string identification)
        {
            var result = _IFarm.GetFarmsByPersonId(identification);
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron fincas." });
            }
            return Ok(result);
        }

        [HttpGet("getFarmbyIdentiAPI")]

        public IActionResult GetFarmbyIdentiAPI(string identification)
        {
            var result = _IFarm.GetFarmbyIdentiAPI(identification);
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron fincas." });
            }
            return Ok(result);
        }

        [HttpPost("UpdateFarmLocation")]
        public IActionResult UpdateFarmLocation([FromBody] FarmUpdateViewModel model)
        {
            var result = _IFarm.UpdateFarmLocation(model.Id, model.Latitude, model.Longitude, model.UpdatedAt);
            return Ok(new { success = result, message = result ? "Ubicación actualizada correctamente." : "No se encontró la finca." });
        }
    }
}
