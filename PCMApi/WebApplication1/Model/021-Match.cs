namespace PCMApi.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public bool IsRanked { get; set; } = true;
        public int? ChallengeId { get; set; } // Null nếu giao hữu
        public MatchFormat MatchFormat { get; set; }

        // Đội 1
        public int Team1_Player1Id { get; set; }
        public int? Team1_Player2Id { get; set; } // Nullable cho Singles

        // Đội 2
        public int Team2_Player1Id { get; set; }
        public int? Team2_Player2Id { get; set; }

        public WinningSide WinningSide { get; set; } = WinningSide.None;

        public virtual Challenge? Challenge { get; set; }
        public virtual Member Team1_Player1 { get; set; } = null!;
        public virtual Member? Team1_Player2 { get; set; }
        public virtual Member Team2_Player1 { get; set; } = null!;
        public virtual Member? Team2_Player2 { get; set; }
    }
}