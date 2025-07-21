using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Lots_TypeController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ILots_Type _ILots_Type;
        public Lots_TypeController(JwtSettings jwtSettings, ILots_Type ILots_Type)
        {
            _jwtSettings = jwtSettings;
            _ILots_Type = ILots_Type;
        }
        [HttpGet("getLotsType")]
        public IActionResult GetLotsType()
        {
            var result = _ILots_Type.GetLotsType();
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron tipos de lotes." });
            }
            return Ok(result);
        }
    
    }
}
