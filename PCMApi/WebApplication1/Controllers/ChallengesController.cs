using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMApi.Data;
using PCMApi.Models;
using System.Security.Claims;

namespace PCMApi.Controllers
{
    [Route("api/challenges")]
    [ApiController]
    [Authorize] // Yêu cầu login
    public class ChallengesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChallengesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/challenges - Danh sách challenge
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Challenge>>> GetChallenges()
        {
            return await _context.Challenges
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
        }

        // GET: api/challenges/{id} - Chi tiết challenge
        [HttpGet("{id}")]
        public async Task<ActionResult<Challenge>> GetChallenge(int id)
        {
            var challenge = await _context.Challenges.FindAsync(id);
            if (challenge == null)
                return NotFound();

            return challenge;
        }

        // POST: api/challenges - Tạo challenge mới (Admin hoặc Referee)
        [HttpPost]
        [Authorize(Roles = "Admin,Referee")]
        public async Task<ActionResult<Challenge>> CreateChallenge([FromBody] ChallengeCreateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var member = await _context.Members.FirstOrDefaultAsync(m => m.UserId == userId);
            if (member == null)
                return Unauthorized();

            var challenge = new Challenge
            {
                Title = dto.Title,
                Type = dto.Type,
                GameMode = dto.GameMode,
                Status = ChallengeStatus.Open,
                Config_TargetWins = dto.Config_TargetWins,
                EntryFee = dto.EntryFee,
                PrizePool = dto.EntryFee * 10, // Ví dụ mặc định 10 người
                CreatedById = member.Id,
                StartDate = dto.StartDate,
                CreatedDate = DateTime.Now
            };

            _context.Challenges.Add(challenge);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChallenge), new { id = challenge.Id }, challenge);
        }

        // POST: api/challenges/{id}/join - Tham gia challenge
        [HttpPost("{id}/join")]
        public async Task<IActionResult> JoinChallenge(int id)
        {
            var challenge = await _context.Challenges.FindAsync(id);
            if (challenge == null || challenge.Status != ChallengeStatus.Open)
                return BadRequest("Challenge không tồn tại hoặc đã đóng");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var member = await _context.Members.FirstOrDefaultAsync(m => m.UserId == userId);
            if (member == null)
                return Unauthorized();

            // Kiểm tra đã tham gia chưa
            var existing = await _context.Participants.AnyAsync(p => p.ChallengeId == id && p.MemberId == member.Id);
            if (existing)
                return BadRequest("Bạn đã tham gia challenge này");

            var participant = new Participant
            {
                ChallengeId = id,
                MemberId = member.Id,
                Team = ParticipantTeam.None, // Sẽ auto chia sau
                EntryFeePaid = true,
                EntryFeeAmount = challenge.EntryFee,
                Status = ParticipantStatus.Confirmed,
                JoinedDate = DateTime.Now
            };

            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();

            return Ok("Tham gia challenge thành công!");
        }

        // POST: api/challenges/{id}/auto-divide-teams - Auto chia team cho TeamBattle
        [HttpPost("{id}/auto-divide-teams")]
        [Authorize(Roles = "Admin,Referee")]
        public async Task<IActionResult> AutoDivideTeams(int id)
        {
            var challenge = await _context.Challenges.FindAsync(id);
            if (challenge == null || challenge.GameMode != GameMode.TeamBattle)
                return BadRequest("Challenge không tồn tại hoặc không phải TeamBattle");

            var participants = await _context.Participants
                .Where(p => p.ChallengeId == id && p.Status == ParticipantStatus.Confirmed)
                .Include(p => p.Member)
                .OrderByDescending(p => p.Member!.RankLevel)
                .ToListAsync();

            if (participants.Count < 2)
                return BadRequest("Không đủ người để chia team");

            // Chia team alternating (cao nhất A, thứ 2 B, thứ 3 A, thứ 4 B...)
            for (int i = 0; i < participants.Count; i++)
            {
                participants[i].Team = (i % 2 == 0) ? ParticipantTeam.TeamA : ParticipantTeam.TeamB;
            }

            await _context.SaveChangesAsync();

            return Ok("Đã auto chia team thành công!");
        }

        // DTO cho create
        public class ChallengeCreateDto
        {
            public string Title { get; set; } = string.Empty;
            public ChallengeType Type { get; set; }
            public GameMode GameMode { get; set; }
            public int? Config_TargetWins { get; set; }
            public decimal EntryFee { get; set; }
            public DateTime? StartDate { get; set; }
        }
    }
}