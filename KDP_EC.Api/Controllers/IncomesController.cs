using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using KDP_EC.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomesController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        private readonly IIncomes _IIncomes;

        public IncomesController(JwtSettings jwtSettings, IIncomes IIncomes)
        {
            _jwtSettings = jwtSettings;
            _IIncomes = IIncomes;
        }

        [HttpGet("getIncomes")]

        public IActionResult GetIncomes()
        {
            var result = _IIncomes.GetIncomes();
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron ingresos." });
            }
            return Ok(result);
        }

        [HttpPost("CreateIncomes")]

        public IActionResult CreateIncomes([FromBody] List<Incomes> incomes)
        {
            if (incomes == null || incomes.Count == 0)
            {
                return BadRequest(new { message = "La lista de ingresos no puede estar vacía." });
            }
            int procesados = 0;
            foreach (var income in incomes)
            {
                if (income == null)
                {
                    return BadRequest(new { message = "Un ingreso en la lista es nulo." });
                }

                var result = _IIncomes.CreateIncomes(income);
                if (result > 0)procesados++;

               
            }
            return Ok(new { message = $"{procesados} Ingresos sincronizados correctamente." });

        }

        [HttpGet("getIncomesByFarmId")]

        public IActionResult GetIncomesByFarmId(Guid FarmId)
        {
            if (FarmId == Guid.Empty)
            {
                return BadRequest(new { message = "El ID de la granja no puede estar vacío." });
            }
            var result = _IIncomes.GetIncomesByFarmId(FarmId);
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron ingresos para esta granja." });
            }
            return Ok(result);
        }
    }
}
