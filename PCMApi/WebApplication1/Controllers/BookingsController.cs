using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMApi.Data;
using PCMApi.Models;
using System.Security.Claims;

namespace PCMApi.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    [Authorize] // Yêu cầu login (Member hoặc Admin)
    public class BookingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/bookings - Danh sách booking (Admin xem hết, Member chỉ xem của mình)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy UserId từ token
            var isAdmin = User.IsInRole("Admin");

            var query = _context.Bookings
                .Include(b => b.Court)
                .Include(b => b.Member)
                .AsQueryable();

            if (!isAdmin)
            {
                var member = await _context.Members.FirstOrDefaultAsync(m => m.UserId == userId);
                if (member != null)
                {
                    query = query.Where(b => b.MemberId == member.Id);
                }
                else
                {
                    return Unauthorized("Không tìm thấy thông tin thành viên");
                }
            }

            return await query.OrderByDescending(b => b.StartTime).ToListAsync();
        }

        // POST: api/bookings - Đặt sân mới
        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking([FromBody] BookingCreateDto dto)
        {
            // Kiểm tra thời gian hợp lệ
            if (dto.StartTime < DateTime.Now)
                return BadRequest("Thời gian bắt đầu phải từ hiện tại trở đi");
            if (dto.EndTime <= dto.StartTime)
                return BadRequest("Thời gian kết thúc phải sau thời gian bắt đầu");

            // Kiểm tra trùng lịch trên cùng sân (không cho phép giao nhau)
            var overlapping = await _context.Bookings
                .AnyAsync(b => b.CourtId == dto.CourtId &&
                               b.Status != BookingStatus.Cancelled &&
                               ((dto.StartTime < b.EndTime && dto.EndTime > b.StartTime)));

            if (overlapping)
                return BadRequest("Trùng lịch với booking khác trên cùng sân");

            // Lấy MemberId từ token (người đặt sân)
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var member = await _context.Members.FirstOrDefaultAsync(m => m.UserId == userId);
            if (member == null)
                return Unauthorized("Không tìm thấy thông tin thành viên");

            var booking = new Booking
            {
                CourtId = dto.CourtId,
                MemberId = member.Id,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Status = BookingStatus.Pending, // Mặc định chờ xác nhận
                Notes = dto.Notes,
                CreatedDate = DateTime.Now
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookings), new { id = booking.Id }, booking);
        }

        // PUT: api/bookings/{id}/status - Cập nhật trạng thái (Admin/Referee)
        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin,Referee")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] BookingStatusUpdateDto dto)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return NotFound();

            booking.Status = dto.Status;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DTOs
        public class BookingCreateDto
        {
            public int CourtId { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public string? Notes { get; set; }
        }

        public class BookingStatusUpdateDto
        {
            public BookingStatus Status { get; set; }
        }
    }
}