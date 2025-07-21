using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : ControllerBase
    {

        private readonly JwtSettings _jwtSettings;
        private readonly IActivities _IActivities;

        public ActivitiesController(JwtSettings jwtSettings, IActivities IActivities)
        {
            _jwtSettings = jwtSettings;
            _IActivities = IActivities;
        }

        [HttpGet("getActivities")]
        public IActionResult GetActivities()
        {
            var result = _IActivities.GetActivities();
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron actividades." });
            }
            return Ok(result);
        }
    }
}
