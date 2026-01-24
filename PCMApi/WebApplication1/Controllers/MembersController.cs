using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMApi.Data;
using PCMApi.Models;

namespace PCMApi.Controllers
{
    [Route("api/members")]
    [ApiController]
    [Authorize] // Yêu cầu login (JWT token)
    public class MembersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/members - Danh sách tất cả members (Admin hoặc Member có quyền xem)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            return await _context.Members.ToListAsync();
        }

        // GET: api/members/{id} - Chi tiết một member
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return member;
        }

        // GET: api/members/top-ranking?limit=5 - Top ranking (BXH)
        [HttpGet("top-ranking")]
        public async Task<ActionResult<IEnumerable<Member>>> GetTopRanking(int limit = 5)
        {
            var topMembers = await _context.Members
                .OrderByDescending(m => m.RankLevel)
                .ThenByDescending(m => m.WinMatches)
                .Take(limit)
                .ToListAsync();

            return topMembers;
        }

        // PUT: api/members/{id} - Chỉnh sửa thông tin member (chỉ chính mình hoặc Admin)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, [FromBody] MemberUpdateDto dto)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            // Chỉ cho phép cập nhật nếu là chính mình hoặc Admin
            var currentUserId = User.FindFirstValue("sub"); // UserId từ token
            var isAdmin = User.IsInRole("Admin");
            if (member.UserId != currentUserId && !isAdmin)
            {
                return Forbid("Bạn chỉ được chỉnh sửa thông tin của chính mình");
            }

            // Cập nhật thông tin
            member.FullName = dto.FullName ?? member.FullName;
            member.PhoneNumber = dto.PhoneNumber ?? member.PhoneNumber;
            member.DateOfBirth = dto.DateOfBirth ?? member.DateOfBirth;
            member.ModifiedDate = DateTime.Now;

            _context.Entry(member).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    // DTO cho update
    public class MemberUpdateDto
    {
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}