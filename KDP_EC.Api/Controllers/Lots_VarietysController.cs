using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Lots_VarietysController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        private readonly ILots_Varietys _ILots_Varietys;

        public Lots_VarietysController(JwtSettings jwtSettings, ILots_Varietys ILots_Varietys)
        {
            _jwtSettings = jwtSettings;
            _ILots_Varietys = ILots_Varietys;
        }

        [HttpGet("getLotsVarietys")]

        public IActionResult GetLotsVarietys()
        {
            var result = _ILots_Varietys.GetLots_Varietys();
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron variedades." });
            }
            return Ok(result);
        }
    }
}
