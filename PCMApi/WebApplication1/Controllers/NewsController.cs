using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMApi.Data;
using PCMApi.Models;

namespace PCMApi.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/news - Danh sách tin tức (có thể lọc pinned)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<News>>> GetNews([FromQuery] bool? isPinned = null)
        {
            var query = _context.News.AsQueryable();

            if (isPinned.HasValue)
            {
                query = query.Where(n => n.IsPinned == isPinned.Value);
            }

            return await query.OrderByDescending(n => n.CreatedDate).ToListAsync();
        }

        // GET: api/news/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<News>> GetNews(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null) return NotFound();
            return news;
        }

        // POST: api/news - Tạo tin mới (Admin only)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<News>> CreateNews([FromBody] NewsCreateDto dto)
        {
            var news = new News
            {
                Title = dto.Title,
                Content = dto.Content,
                IsPinned = dto.IsPinned,
                CreatedDate = DateTime.Now
            };

            _context.News.Add(news);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNews), new { id = news.Id }, news);
        }

        // PUT: api/news/{id} - Chỉnh sửa tin
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateNews(int id, [FromBody] NewsCreateDto dto)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null) return NotFound();

            news.Title = dto.Title;
            news.Content = dto.Content;
            news.IsPinned = dto.IsPinned;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/news/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null) return NotFound();

            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DTO
        public class NewsCreateDto
        {
            public string Title { get; set; } = string.Empty;
            public string Content { get; set; } = string.Empty;
            public bool IsPinned { get; set; } = false;
        }
    }
}