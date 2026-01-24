namespace PCMApi.Models
{
    public class TransactionCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public TransactionType Type { get; set; }
    }

    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int? CreatedById { get; set; } // MemberId
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public virtual TransactionCategory? Category { get; set; }
        public virtual Member? CreatedBy { get; set; }
    }
}