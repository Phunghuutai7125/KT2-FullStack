namespace PCMApi.Models
{
    public enum TransactionType { Thu, Chi }
    public enum ChallengeType { Duel, MiniGame }
    public enum GameMode { None, TeamBattle, RoundRobin }
    public enum ChallengeStatus { Open, Ongoing, Finished }
    public enum BookingStatus { Pending, Confirmed, Cancelled }
    public enum MatchFormat { Singles, Doubles }
    public enum WinningSide { None, Team1, Team2 }
    public enum ParticipantTeam { TeamA, TeamB, None }
    public enum ParticipantStatus { Pending, Confirmed, Withdrawn }
}