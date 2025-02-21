using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web2212025.Data;
using Web2212025.Models;
using Microsoft.AspNetCore.Hosting.Server;

namespace Web2212025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentContext _context;

        public StudentsController(StudentContext context)
        {
            _context = context;
        }

        //action
        [HttpPost("CreateStudent")]
        public async Task<IActionResult> CreateStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateStudent), new { id = student.Id }, student);

        }
    }
}
