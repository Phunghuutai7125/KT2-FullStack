using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PCMApi.Models
{
    public class Member
    {
        public int Id { get; set; }
        [Required] public string FullName { get; set; } = string.Empty;
        public DateTime JoinDate { get; set; } = DateTime.Now;
        public double RankLevel { get; set; } = 3.0;
        public bool IsActive { get; set; } = true;
        public string? UserId { get; set; } // FK to IdentityUser
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int TotalMatches { get; set; }
        public int WinMatches { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        public virtual IdentityUser? User { get; set; }
    }
}