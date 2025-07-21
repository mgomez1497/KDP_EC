using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using KDP_EC.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FloweringRecordsController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        private readonly IFloweringRecords _IfloweringRecords;

        public FloweringRecordsController(JwtSettings jwtSettings, IFloweringRecords floweringRecords)
        {
            _jwtSettings = jwtSettings;
            _IfloweringRecords = floweringRecords;
        }

        [HttpPost("createFloweringRecord")]
        public IActionResult CreateFloweringRecord([FromBody] List<FloweringRecords> floweringRecords)
        {
            if (floweringRecords == null || !floweringRecords.Any())
            {
                return BadRequest(new { message = "Lista de registros vacía." });
            }

            int successCount = 0;
            foreach (var record in floweringRecords)
            {
                int result = _IfloweringRecords.CreateFloweringRecord(record);
                if (result > 0)
                    successCount++;
            }

            return Ok(new { message = $"{successCount} registros sincronizados correctamente." });
        }

        [HttpGet("GetFloweringRecordsByUserId")]

        public IActionResult GetFloweringRecordsByUserId(Guid UserId)
        {
            var result = _IfloweringRecords.GetfloweringRecordsByUserId(UserId);
            if (result == null || result.Count==0)
            {
                return NotFound(new { message = "No se encontraron floraciones." });
            }

            return Ok(result);
        }

    }
}
