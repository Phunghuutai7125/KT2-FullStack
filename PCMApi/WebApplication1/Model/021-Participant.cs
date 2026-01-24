namespace PCMApi.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public int ChallengeId { get; set; }
        public int MemberId { get; set; }
        public ParticipantTeam Team { get; set; } = ParticipantTeam.None;
        public bool EntryFeePaid { get; set; } = false;
        public decimal EntryFeeAmount { get; set; }
        public DateTime JoinedDate { get; set; } = DateTime.Now;
        public ParticipantStatus Status { get; set; } = ParticipantStatus.Pending;

        public virtual Challenge Challenge { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;
    }
}