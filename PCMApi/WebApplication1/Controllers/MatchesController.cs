using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMApi.Data;
using PCMApi.Models;
using System.Security.Claims;

namespace PCMApi.Controllers
{
    [Route("api/matches")]
    [ApiController]
    [Authorize(Roles = "Admin,Referee")] // Chỉ Admin/Referee nhập kết quả
    public class MatchesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/matches - Danh sách trận đấu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
        {
            return await _context.Matches
                .Include(m => m.Team1_Player1)
                .Include(m => m.Team1_Player2)
                .Include(m => m.Team2_Player1)
                .Include(m => m.Team2_Player2)
                .Include(m => m.Challenge)
                .ToListAsync();
        }

        // POST: api/matches - Tạo trận đấu mới (giao hữu hoặc trong challenge)
        [HttpPost]
        public async Task<ActionResult<Match>> CreateMatch([FromBody] MatchCreateDto dto)
        {
            // Kiểm tra người chơi không trùng nhau
            if (dto.Team1_Player1Id == dto.Team2_Player1Id ||
                (dto.MatchFormat == MatchFormat.Doubles &&
                 (dto.Team1_Player1Id == dto.Team1_Player2Id || dto.Team2_Player1Id == dto.Team2_Player2Id)))
                return BadRequest("Người chơi không được trùng nhau");

            var match = new Match
            {
                Date = dto.Date,
                IsRanked = dto.IsRanked,
                ChallengeId = dto.ChallengeId,
                MatchFormat = dto.MatchFormat,
                Team1_Player1Id = dto.Team1_Player1Id,
                Team1_Player2Id = dto.Team1_Player2Id,
                Team2_Player1Id = dto.Team2_Player1Id,
                Team2_Player2Id = dto.Team2_Player2Id,
                WinningSide = WinningSide.None // Chưa có kết quả
            };

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMatches), new { id = match.Id }, match);
        }

        // PUT: api/matches/{id}/result - Nhập kết quả trận đấu + cập nhật rank
        [HttpPut("{id}/result")]
        public async Task<IActionResult> UpdateMatchResult(int id, [FromBody] MatchResultDto dto)
        {
            var match = await _context.Matches
                .Include(m => m.Challenge)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (match == null)
                return NotFound();

            if (match.WinningSide != WinningSide.None)
                return BadRequest("Trận đấu đã có kết quả");

            match.WinningSide = dto.WinningSide;

            // Nếu ranked → cập nhật DUPR đơn giản (+0.1 thắng, -0.1 thua)
            if (match.IsRanked)
            {
                var winnerPlayers = dto.WinningSide == WinningSide.Team1
                    ? new[] { match.Team1_Player1Id, match.Team1_Player2Id }
                    : new[] { match.Team2_Player1Id, match.Team2_Player2Id };

                var loserPlayers = dto.WinningSide == WinningSide.Team1
                    ? new[] { match.Team2_Player1Id, match.Team2_Player2Id }
                    : new[] { match.Team1_Player1Id, match.Team1_Player2Id };

                foreach (var playerId in winnerPlayers.Where(p => p.HasValue))
                {
                    var player = await _context.Members.FindAsync(playerId.Value);
                    if (player != null)
                    {
                        player.RankLevel += 0.1;
                        player.WinMatches++;
                        player.TotalMatches++;
                    }
                }

                foreach (var playerId in loserPlayers.Where(p => p.HasValue))
                {
                    var player = await _context.Members.FindAsync(playerId.Value);
                    if (player != null)
                    {
                        player.RankLevel -= 0.1;
                        player.TotalMatches++;
                    }
                }
            }

            // Nếu thuộc challenge TeamBattle → cập nhật score phe
            if (match.ChallengeId.HasValue && match.Challenge!.GameMode == GameMode.TeamBattle)
            {
                if (dto.WinningSide == WinningSide.Team1)
                    match.Challenge.CurrentScore_TeamA++;
                else if (dto.WinningSide == WinningSide.Team2)
                    match.Challenge.CurrentScore_TeamB++;

                // Kiểm tra kết thúc challenge
                if (match.Challenge.CurrentScore_TeamA >= match.Challenge.Config_TargetWins ||
                    match.Challenge.CurrentScore_TeamB >= match.Challenge.Config_TargetWins)
                {
                    match.Challenge.Status = ChallengeStatus.Finished;
                }
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DTOs
        public class MatchCreateDto
        {
            public DateTime Date { get; set; } = DateTime.Now;
            public bool IsRanked { get; set; } = true;
            public int? ChallengeId { get; set; }
            public MatchFormat MatchFormat { get; set; }
            public int Team1_Player1Id { get; set; }
            public int? Team1_Player2Id { get; set; }
            public int Team2_Player1Id { get; set; }
            public int? Team2_Player2Id { get; set; }
        }

        public class MatchResultDto
        {
            public WinningSide WinningSide { get; set; }
        }
    }
}