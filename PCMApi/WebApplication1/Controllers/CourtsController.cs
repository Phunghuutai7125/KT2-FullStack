using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMApi.Data;
using PCMApi.Models;

namespace PCMApi.Controllers
{
    [Route("api/courts")]
    [ApiController]
    [Authorize(Roles = "Admin")] // Chỉ Admin mới được CRUD sân
    public class CourtsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CourtsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/courts - Danh sách tất cả sân
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Court>>> GetCourts()
        {
            return await _context.Courts.ToListAsync();
        }

        // GET: api/courts/{id} - Chi tiết một sân
        [HttpGet("{id}")]
        public async Task<ActionResult<Court>> GetCourt(int id)
        {
            var court = await _context.Courts.FindAsync(id);
            if (court == null)
            {
                return NotFound();
            }
            return court;
        }

        // POST: api/courts - Tạo sân mới (Admin only)
        [HttpPost]
        public async Task<ActionResult<Court>> CreateCourt([FromBody] Court court)
        {
            _context.Courts.Add(court);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCourt), new { id = court.Id }, court);
        }

        // PUT: api/courts/{id} - Chỉnh sửa sân
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourt(int id, [FromBody] Court court)
        {
            if (id != court.Id)
            {
                return BadRequest();
            }

            _context.Entry(court).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/courts/{id} - Xóa sân
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourt(int id)
        {
            var court = await _context.Courts.FindAsync(id);
            if (court == null)
            {
                return NotFound();
            }

            _context.Courts.Remove(court);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}