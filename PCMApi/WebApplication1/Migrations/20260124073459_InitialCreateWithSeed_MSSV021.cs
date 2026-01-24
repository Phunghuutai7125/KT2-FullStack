using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithSeed_MSSV021 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "021_Courts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_021_Courts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "021_News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPinned = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_021_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "021_TransactionCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_021_TransactionCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "021_Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RankLevel = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalMatches = table.Column<int>(type: "int", nullable: false),
                    WinMatches = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_021_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_021_Members_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "021_Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourtId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_021_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_021_Bookings_021_Courts_CourtId",
                        column: x => x.CourtId,
                        principalTable: "021_Courts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_021_Bookings_021_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "021_Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "021_Challenges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    GameMode = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Config_TargetWins = table.Column<int>(type: "int", nullable: true),
                    CurrentScore_TeamA = table.Column<int>(type: "int", nullable: false),
                    CurrentScore_TeamB = table.Column<int>(type: "int", nullable: false),
                    EntryFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrizePool = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_021_Challenges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_021_Challenges_021_Members_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "021_Members",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "021_Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_021_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_021_Transactions_021_Members_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "021_Members",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_021_Transactions_021_TransactionCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "021_TransactionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "021_Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRanked = table.Column<bool>(type: "bit", nullable: false),
                    ChallengeId = table.Column<int>(type: "int", nullable: true),
                    MatchFormat = table.Column<int>(type: "int", nullable: false),
                    Team1_Player1Id = table.Column<int>(type: "int", nullable: false),
                    Team1_Player2Id = table.Column<int>(type: "int", nullable: true),
                    Team2_Player1Id = table.Column<int>(type: "int", nullable: false),
                    Team2_Player2Id = table.Column<int>(type: "int", nullable: true),
                    WinningSide = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_021_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_021_Matches_021_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "021_Challenges",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_021_Matches_021_Members_Team1_Player1Id",
                        column: x => x.Team1_Player1Id,
                        principalTable: "021_Members",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_021_Matches_021_Members_Team1_Player2Id",
                        column: x => x.Team1_Player2Id,
                        principalTable: "021_Members",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_021_Matches_021_Members_Team2_Player1Id",
                        column: x => x.Team2_Player1Id,
                        principalTable: "021_Members",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_021_Matches_021_Members_Team2_Player2Id",
                        column: x => x.Team2_Player2Id,
                        principalTable: "021_Members",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "021_Participants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChallengeId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    Team = table.Column<int>(type: "int", nullable: false),
                    EntryFeePaid = table.Column<bool>(type: "bit", nullable: false),
                    EntryFeeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    JoinedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_021_Participants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_021_Participants_021_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "021_Challenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_021_Participants_021_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "021_Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "021_Courts",
                columns: new[] { "Id", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "Sân chính", true, "Sân 1" },
                    { 2, "Sân phụ", true, "Sân 2" }
                });

            migrationBuilder.InsertData(
                table: "021_Members",
                columns: new[] { "Id", "CreatedDate", "DateOfBirth", "Email", "FullName", "IsActive", "JoinDate", "ModifiedDate", "PhoneNumber", "RankLevel", "TotalMatches", "UserId", "WinMatches" },
                values: new object[,]
                {
                    { 2, new DateTime(2026, 1, 24, 14, 34, 59, 216, DateTimeKind.Local).AddTicks(3797), null, "vana@pcm.com", "Nguyễn Văn A", true, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 24, 14, 34, 59, 216, DateTimeKind.Local).AddTicks(3797), "0901234567", 4.5, 0, null, 0 },
                    { 3, new DateTime(2026, 1, 24, 14, 34, 59, 216, DateTimeKind.Local).AddTicks(3799), null, "thib@pcm.com", "Trần Thị B", true, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 24, 14, 34, 59, 216, DateTimeKind.Local).AddTicks(3799), "0912345678", 3.7999999999999998, 0, null, 0 },
                    { 4, new DateTime(2026, 1, 24, 14, 34, 59, 216, DateTimeKind.Local).AddTicks(3801), null, "vanc@pcm.com", "Lê Văn C", true, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 24, 14, 34, 59, 216, DateTimeKind.Local).AddTicks(3801), "0923456789", 4.2000000000000002, 0, null, 0 },
                    { 5, new DateTime(2026, 1, 24, 14, 34, 59, 216, DateTimeKind.Local).AddTicks(3803), null, "thid@pcm.com", "Phạm Thị D", true, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 24, 14, 34, 59, 216, DateTimeKind.Local).AddTicks(3803), "0934567890", 3.5, 0, null, 0 },
                    { 6, new DateTime(2026, 1, 24, 14, 34, 59, 216, DateTimeKind.Local).AddTicks(3805), null, "vane@pcm.com", "Hoàng Văn E", true, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 24, 14, 34, 59, 216, DateTimeKind.Local).AddTicks(3805), "0945678901", 4.0, 0, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "021_TransactionCategories",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "Tiền sân", 0 },
                    { 2, "Quỹ tháng", 0 },
                    { 3, "Nước", 1 },
                    { 4, "Phạt", 1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "role-admin", null, "Admin", "ADMIN" },
                    { "role-member", null, "Member", "MEMBER" },
                    { "role-referee", null, "Referee", "REFEREE" },
                    { "role-treasurer", null, "Treasurer", "TREASURER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "admin-user-id", 0, "f853394e-b2a4-4c04-a63c-0c13c6cfd311", "admin@pcm.com", true, false, null, "ADMIN@PCM.COM", "ADMIN@PCM.COM", "AQAAAAIAAYagAAAAEEupq26bE00c43ZUZLQSe0AEZ+oJK25avrPkUdCW2chCr6lgS5FikqXxuke3f0k6kQ==", null, false, "f24997ed-eeb1-43eb-9189-d97b3006009b", false, "admin@pcm.com" });

            migrationBuilder.InsertData(
                table: "021_Members",
                columns: new[] { "Id", "CreatedDate", "DateOfBirth", "Email", "FullName", "IsActive", "JoinDate", "ModifiedDate", "PhoneNumber", "RankLevel", "TotalMatches", "UserId", "WinMatches" },
                values: new object[] { 1, new DateTime(2026, 1, 24, 14, 34, 59, 216, DateTimeKind.Local).AddTicks(3790), null, "admin@pcm.com", "Admin CLB", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 24, 14, 34, 59, 216, DateTimeKind.Local).AddTicks(3790), "0987654321", 5.0, 0, "admin-user-id", 0 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "role-admin", "admin-user-id" });

            migrationBuilder.InsertData(
                table: "021_Challenges",
                columns: new[] { "Id", "Config_TargetWins", "CreatedById", "CreatedDate", "CurrentScore_TeamA", "CurrentScore_TeamB", "EndDate", "EntryFee", "GameMode", "ModifiedDate", "PrizePool", "StartDate", "Status", "Title", "Type" },
                values: new object[] { 1, 5, 1, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, null, 50000m, 1, new DateTime(2026, 1, 24, 14, 34, 59, 216, DateTimeKind.Local).AddTicks(4017), 500000m, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Mini-game Team Battle Tháng 1", 1 });

            migrationBuilder.InsertData(
                table: "021_Transactions",
                columns: new[] { "Id", "Amount", "CategoryId", "CreatedById", "CreatedDate", "Date", "Description" },
                values: new object[,]
                {
                    { 1, 1000000m, 2, 1, new DateTime(2026, 1, 24, 14, 34, 59, 216, DateTimeKind.Local).AddTicks(3989), new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thu quỹ tháng 1" },
                    { 2, 200000m, 3, 1, new DateTime(2026, 1, 24, 14, 34, 59, 216, DateTimeKind.Local).AddTicks(3995), new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chi nước" }
                });

            migrationBuilder.InsertData(
                table: "021_Matches",
                columns: new[] { "Id", "ChallengeId", "Date", "IsRanked", "MatchFormat", "Team1_Player1Id", "Team1_Player2Id", "Team2_Player1Id", "Team2_Player2Id", "WinningSide" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 0, 2, null, 3, null, 1 },
                    { 2, 1, new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 4, 5, 6, 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "021_Participants",
                columns: new[] { "Id", "ChallengeId", "EntryFeeAmount", "EntryFeePaid", "JoinedDate", "MemberId", "Status", "Team" },
                values: new object[,]
                {
                    { 1, 1, 50000m, true, new DateTime(2026, 1, 24, 14, 34, 59, 216, DateTimeKind.Local).AddTicks(4042), 1, 1, 0 },
                    { 2, 1, 50000m, true, new DateTime(2026, 1, 24, 14, 34, 59, 216, DateTimeKind.Local).AddTicks(4045), 2, 1, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_021_Bookings_CourtId",
                table: "021_Bookings",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_021_Bookings_MemberId",
                table: "021_Bookings",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_021_Challenges_CreatedById",
                table: "021_Challenges",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_021_Matches_ChallengeId",
                table: "021_Matches",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_021_Matches_Team1_Player1Id",
                table: "021_Matches",
                column: "Team1_Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_021_Matches_Team1_Player2Id",
                table: "021_Matches",
                column: "Team1_Player2Id");

            migrationBuilder.CreateIndex(
                name: "IX_021_Matches_Team2_Player1Id",
                table: "021_Matches",
                column: "Team2_Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_021_Matches_Team2_Player2Id",
                table: "021_Matches",
                column: "Team2_Player2Id");

            migrationBuilder.CreateIndex(
                name: "IX_021_Members_UserId",
                table: "021_Members",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_021_Participants_ChallengeId",
                table: "021_Participants",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_021_Participants_MemberId",
                table: "021_Participants",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_021_Transactions_CategoryId",
                table: "021_Transactions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_021_Transactions_CreatedById",
                table: "021_Transactions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "021_Bookings");

            migrationBuilder.DropTable(
                name: "021_Matches");

            migrationBuilder.DropTable(
                name: "021_News");

            migrationBuilder.DropTable(
                name: "021_Participants");

            migrationBuilder.DropTable(
                name: "021_Transactions");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "021_Courts");

            migrationBuilder.DropTable(
                name: "021_Challenges");

            migrationBuilder.DropTable(
                name: "021_TransactionCategories");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "021_Members");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
