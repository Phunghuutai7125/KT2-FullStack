using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMApi.Data;
using PCMApi.Models;
using System.Security.Claims;

namespace PCMApi.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    [Authorize(Roles = "Admin,Treasurer")] // Chỉ Admin/Treasurer quản lý tài chính
    public class TransactionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/transactions - Danh sách giao dịch
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            return await _context.Transactions
                .Include(t => t.Category)
                .Include(t => t.CreatedBy)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        // POST: api/transactions - Tạo giao dịch thu/chi mới
        [HttpPost]
        public async Task<ActionResult<Transaction>> CreateTransaction([FromBody] TransactionCreateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var member = await _context.Members.FirstOrDefaultAsync(m => m.UserId == userId);
            if (member == null)
                return Unauthorized();

            var transaction = new Transaction
            {
                Date = dto.Date,
                Amount = dto.Amount,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                CreatedById = member.Id,
                CreatedDate = DateTime.Now
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTransactions), new { id = transaction.Id }, transaction);
        }

        // GET: api/transactions/summary - Tóm tắt quỹ (tổng thu, chi, số dư + cảnh báo âm)
        [HttpGet("summary")]
        public async Task<ActionResult<object>> GetSummary()
        {
            var totalIncome = await _context.Transactions
                .Where(t => t.Category.Type == TransactionType.Thu)
                .SumAsync(t => t.Amount);

            var totalExpense = await _context.Transactions
                .Where(t => t.Category.Type == TransactionType.Chi)
                .SumAsync(t => t.Amount);

            var balance = totalIncome - totalExpense;

            return Ok(new
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Balance = balance,
                IsNegative = balance < 0,
                Warning = balance < 0 ? "CẢNH BÁO: Quỹ đang âm! Cần bổ sung ngay!" : null
            });
        }

        // DTO cho create
        public class TransactionCreateDto
        {
            public DateTime Date { get; set; } = DateTime.Now;
            public decimal Amount { get; set; }
            public string Description { get; set; } = string.Empty;
            public int CategoryId { get; set; }
        }
    }
}