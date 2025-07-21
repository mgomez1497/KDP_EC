using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static SQLite.SQLite3;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VillagesController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        private readonly IVillages _IVillages;
        public VillagesController(JwtSettings jwtSettings, IVillages IVillages)
        {
            _jwtSettings = jwtSettings;
            _IVillages = IVillages;
        }

        [HttpGet("getVillagesbyId")]

        public IActionResult GetVillages(Guid id)
        {
            var result = _IVillages.GetVillagesbyId(id);
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron aldeas." });
            }
            return Ok(result);
        }

        [HttpGet("getVillages")]
        public IActionResult GetVillages(int page = 1, int pageSize = 100)
        {
            try
            {
                var allVillages = _IVillages.GetVillages();

                if (allVillages == null || allVillages.Count == 0)
                {
                    return NotFound(new { message = "No se encontraron aldeas." });
                }

                
                var pagedVillages = allVillages
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return Ok(new
                {
                    data = pagedVillages,
                    totalItems = allVillages.Count,
                    currentPage = page,
                    totalPages = (int)Math.Ceiling((double)allVillages.Count / pageSize)
                });
            }
            catch (Exception e)
            {
                
                return StatusCode(500, new { message = "Error interno del servidor", error = e.Message });
            }
        }

        
    }
}
