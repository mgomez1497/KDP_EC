    using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        private readonly ICompany _ICompany;

        public CompanyController(JwtSettings jwtSettings, ICompany ICompany)
        {
            _jwtSettings = jwtSettings;
            _ICompany = ICompany;
        }

        [HttpGet("getCompanies")]
        public IActionResult GetCompanies()
        {
            var result = _ICompany.GetCompanies();

            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron empresas." });
            }

            return Ok(result);
        }

        [HttpGet("getCompaniesbyId")]

        public IActionResult GetCompaniesbyId(Guid Id)
        {
            var result = _ICompany.GetCompaniesbyId(Id);
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No se encontraron empresas." });
            }
            return Ok(result);
        }
    }
}
