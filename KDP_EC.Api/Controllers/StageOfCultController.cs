using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StageOfCultController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        private readonly IStageOfCult _IStageOfCult;

        public StageOfCultController(JwtSettings jwtSettings, IStageOfCult IStageOfCult)
        {
            _jwtSettings = jwtSettings;
            _IStageOfCult = IStageOfCult;
        }

        [HttpGet("getStagesOfCult")]
        public IActionResult GetStageOfCults()
        {
            var result = _IStageOfCult.GetStageOfCults();
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron etapas de cultivo." });
            }
            return Ok(result);
        }

    }
}
