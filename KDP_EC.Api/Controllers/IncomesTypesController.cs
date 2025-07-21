using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomesTypesController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        private readonly IIncomesTypes _IIncomesTypes;

        public IncomesTypesController(JwtSettings jwtSettings, IIncomesTypes IIncomesTypes)
        {
            _jwtSettings = jwtSettings;
            _IIncomesTypes = IIncomesTypes;
        }

        [HttpGet("getIncomesTypes")]

        public IActionResult GetIncomesTypes()
        {
            var result = _IIncomesTypes.GetIncomesTypes();
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron tipos de ingresos." });
            }
            return Ok(result);
        }

    }
}
