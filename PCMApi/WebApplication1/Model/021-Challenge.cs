namespace PCMApi.Models
{
    public class Challenge
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public ChallengeType Type { get; set; } // Duel / MiniGame
        public GameMode GameMode { get; set; } // None / TeamBattle / RoundRobin
        public ChallengeStatus Status { get; set; } = ChallengeStatus.Open;
        public int? Config_TargetWins { get; set; } // Cho TeamBattle
        public int CurrentScore_TeamA { get; set; }
        public int CurrentScore_TeamB { get; set; }
        public decimal EntryFee { get; set; }
        public decimal PrizePool { get; set; }
        public int? CreatedById { get; set; } // MemberId
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        public virtual Member? CreatedBy { get; set; }
    }
}