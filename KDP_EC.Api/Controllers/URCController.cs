using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using KDP_EC.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [Route("api/[controller]")]
    public class URCController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IURC _IURC;

        public URCController(JwtSettings jwtSettings, IURC iURC)
        {
            _jwtSettings = jwtSettings;
            _IURC = iURC;
        }

        [HttpPost("createURC")]

        public IActionResult CreateURC([FromBody] URC urc)
        {
            if (urc == null)
                return BadRequest("Los datos del URC no pueden ser nulos.");
            bool isCreated = _IURC.CreateURC(urc);
            if (isCreated)
            {
                return Ok(new { message = "URC creado exitosamente." });
            }
            else
            {
                return StatusCode(500, new { message = "Error al crear el URC." });
            }
        }
    }
}
