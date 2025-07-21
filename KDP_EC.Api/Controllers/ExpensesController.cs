using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using KDP_EC.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        private readonly IExpenses _IExpenses;

        public ExpensesController(JwtSettings jwtSettings, IExpenses IExpenses)
        {
            _jwtSettings = jwtSettings;
            _IExpenses = IExpenses;
        }

        [HttpPost("postExpenses")]
        
        public IActionResult PostExpenses([FromBody] List<Expenses> expenses)
        {
            if (expenses == null || expenses.Count == 0)
            {
                return BadRequest(new { message = "La lista de gastos no puede estar vacía." });
            }
            int procesados = 0;
            foreach (var expense in expenses)
            {
                if (expense == null)
                {
                    return BadRequest(new { message = "Un gasto en la lista es nulo." });
                }
                var result = _IExpenses.CretateExpense(expense);
                if (result > 0) procesados++;


            }
            return Ok(new { message = $"{procesados} Gastos sincronizados correctamente." });
        }

        [HttpGet("getExpensesByFarmId")]

        public IActionResult GetExpensesByFarmId(Guid FarmId)
        {
            if (FarmId == Guid.Empty)
            {
                return BadRequest(new { message = "El ID de la granja no puede estar vacío." });
            }
            var result = _IExpenses.GetExpensesByFarmId(FarmId);
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron gastos para esta granja." });
            }
            return Ok(result);

        }


    }
}
