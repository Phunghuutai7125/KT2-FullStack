namespace PCMApi.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int CourtId { get; set; }
        public int MemberId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public string? Notes { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public virtual Court? Court { get; set; }
        public virtual Member? Member { get; set; }
    }
}