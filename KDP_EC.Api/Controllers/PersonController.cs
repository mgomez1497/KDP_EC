using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using KDP_EC.Core.Interfaces.Account;
using KDP_EC.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace KDP_EC.Api.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IPerson _IPerson;
        private readonly IUsersLogin _Iuser;

        public PersonController(JwtSettings jwtSettings, IPerson IPerson, IUsersLogin Iuser)
        {
            _jwtSettings = jwtSettings;
            _IPerson = IPerson;
            _Iuser = Iuser;
        }


        [HttpPost("postPersons")]
        public IActionResult PostPersons([FromBody] Person person)
        {
            if (person == null)
                return BadRequest("Los datos de la persona no pueden ser nulos.");

            bool isCreated = _IPerson.CreatePerson(person);

            if (isCreated)
            {
                return Ok(new { message = "Persona creada exitosamente." });
            }
            else
            {
                return StatusCode(500, new { message = "Error al crear la persona." });
            }
        }

        [HttpGet("getPersons")]

        public IActionResult GetPersons()
        {
            var persons = _IPerson.GetPersons();
            if (persons == null || persons.Count == 0)
            {
                return NotFound("No se encontraron personas.");
            }
            return Ok(persons);
        }
    }
}
