namespace PCMApi.Models
{
    public class Court
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string? Description { get; set; }
    }
}