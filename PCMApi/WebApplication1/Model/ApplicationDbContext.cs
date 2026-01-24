using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PCMApi.Models;

namespace PCMApi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ===================== DbSet =====================
        public DbSet<Member> Members { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ===================== TABLE NAMES =====================
            builder.Entity<Member>().ToTable("021_Members");
            builder.Entity<News>().ToTable("021_News");
            builder.Entity<TransactionCategory>().ToTable("021_TransactionCategories");
            builder.Entity<Transaction>().ToTable("021_Transactions");
            builder.Entity<Court>().ToTable("021_Courts");
            builder.Entity<Booking>().ToTable("021_Bookings");
            builder.Entity<Challenge>().ToTable("021_Challenges");
            builder.Entity<Match>().ToTable("021_Matches");
            builder.Entity<Participant>().ToTable("021_Participants");

            // ===================== FIX MULTIPLE CASCADE PATHS =====================
            builder.Entity<Match>()
                .HasOne(m => m.Team1_Player1)
                .WithMany()
                .HasForeignKey(m => m.Team1_Player1Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Match>()
                .HasOne(m => m.Team1_Player2)
                .WithMany()
                .HasForeignKey(m => m.Team1_Player2Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Match>()
                .HasOne(m => m.Team2_Player1)
                .WithMany()
                .HasForeignKey(m => m.Team2_Player1Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Match>()
                .HasOne(m => m.Team2_Player2)
                .WithMany()
                .HasForeignKey(m => m.Team2_Player2Id)
                .OnDelete(DeleteBehavior.NoAction);

            // ===================== SEEDING DATA =====================

            // 1. Roles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "role-admin", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "role-treasurer", Name = "Treasurer", NormalizedName = "TREASURER" },
                new IdentityRole { Id = "role-referee", Name = "Referee", NormalizedName = "REFEREE" },
                new IdentityRole { Id = "role-member", Name = "Member", NormalizedName = "MEMBER" }
            );

            // 2. Admin User
            var hasher = new PasswordHasher<IdentityUser>();
            var adminUser = new IdentityUser
            {
                Id = "admin-user-id",
                UserName = "admin@pcm.com",
                NormalizedUserName = "ADMIN@PCM.COM",
                Email = "admin@pcm.com",
                NormalizedEmail = "ADMIN@PCM.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin@123")
            };
            builder.Entity<IdentityUser>().HasData(adminUser);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "admin-user-id",
                    RoleId = "role-admin"
                }
            );

            // 3. Members
            builder.Entity<Member>().HasData(
                new Member { Id = 1, FullName = "Admin CLB", JoinDate = new DateTime(2024, 1, 1), RankLevel = 5.0, Email = "admin@pcm.com", PhoneNumber = "0987654321", UserId = "admin-user-id" },
                new Member { Id = 2, FullName = "Nguyễn Văn A", JoinDate = new DateTime(2024, 6, 1), RankLevel = 4.5, Email = "vana@pcm.com", PhoneNumber = "0901234567" },
                new Member { Id = 3, FullName = "Trần Thị B", JoinDate = new DateTime(2024, 9, 1), RankLevel = 3.8, Email = "thib@pcm.com", PhoneNumber = "0912345678" },
                new Member { Id = 4, FullName = "Lê Văn C", JoinDate = new DateTime(2024, 10, 1), RankLevel = 4.2, Email = "vanc@pcm.com", PhoneNumber = "0923456789" },
                new Member { Id = 5, FullName = "Phạm Thị D", JoinDate = new DateTime(2024, 11, 1), RankLevel = 3.5, Email = "thid@pcm.com", PhoneNumber = "0934567890" },
                new Member { Id = 6, FullName = "Hoàng Văn E", JoinDate = new DateTime(2024, 12, 1), RankLevel = 4.0, Email = "vane@pcm.com", PhoneNumber = "0945678901" }
            );

            // 4. Courts
            builder.Entity<Court>().HasData(
                new Court { Id = 1, Name = "Sân 1", IsActive = true, Description = "Sân chính" },
                new Court { Id = 2, Name = "Sân 2", IsActive = true, Description = "Sân phụ" }
            );

            // 5. TransactionCategories
            builder.Entity<TransactionCategory>().HasData(
                new TransactionCategory { Id = 1, Name = "Tiền sân", Type = TransactionType.Thu },
                new TransactionCategory { Id = 2, Name = "Quỹ tháng", Type = TransactionType.Thu },
                new TransactionCategory { Id = 3, Name = "Nước", Type = TransactionType.Chi },
                new TransactionCategory { Id = 4, Name = "Phạt", Type = TransactionType.Chi }
            );

            // 6. Transactions
            builder.Entity<Transaction>().HasData(
                new Transaction { Id = 1, Date = new DateTime(2024, 12, 20), Amount = 1000000m, Description = "Thu quỹ tháng 1", CategoryId = 2, CreatedById = 1 },
                new Transaction { Id = 2, Date = new DateTime(2024, 12, 25), Amount = 200000m, Description = "Chi nước", CategoryId = 3, CreatedById = 1 }
            );

            // 7. Challenge
            builder.Entity<Challenge>().HasData(
                new Challenge
                {
                    Id = 1,
                    Title = "Mini-game Team Battle Tháng 1",
                    Type = ChallengeType.MiniGame,
                    GameMode = GameMode.TeamBattle,
                    Status = ChallengeStatus.Ongoing,
                    Config_TargetWins = 5,
                    CurrentScore_TeamA = 2,
                    CurrentScore_TeamB = 1,
                    EntryFee = 50000m,
                    PrizePool = 500000m,
                    CreatedById = 1,
                    StartDate = new DateTime(2025, 1, 10),
                    CreatedDate = new DateTime(2025, 1, 10)
                }
            );

            // 8. Participants
            builder.Entity<Participant>().HasData(
                new Participant { Id = 1, ChallengeId = 1, MemberId = 1, Team = ParticipantTeam.TeamA, EntryFeePaid = true, EntryFeeAmount = 50000m, Status = ParticipantStatus.Confirmed },
                new Participant { Id = 2, ChallengeId = 1, MemberId = 2, Team = ParticipantTeam.TeamB, EntryFeePaid = true, EntryFeeAmount = 50000m, Status = ParticipantStatus.Confirmed }
            );

            // 9. Matches
            builder.Entity<Match>().HasData(
                new Match { Id = 1, Date = new DateTime(2025, 1, 15), IsRanked = true, ChallengeId = 1, MatchFormat = MatchFormat.Singles, Team1_Player1Id = 2, Team2_Player1Id = 3, WinningSide = WinningSide.Team1 },
                new Match { Id = 2, Date = new DateTime(2025, 1, 16), IsRanked = true, ChallengeId = 1, MatchFormat = MatchFormat.Doubles, Team1_Player1Id = 4, Team1_Player2Id = 5, Team2_Player1Id = 6, Team2_Player2Id = 1, WinningSide = WinningSide.Team2 }
            );
        }
    }
}
