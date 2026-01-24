namespace PCMApi.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPinned { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? CreatedById { get; set; } // MemberId (tùy chọn)
    }
}