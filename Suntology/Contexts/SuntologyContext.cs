using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Suntology.Contexts
{
    [Table("suntology.caste")]
    public class Caste
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Reference { get; set; }

        [Required]
        [MaxLength(255)]
        public string DisplayName { get; set; }

        public int? Rank { get; set; }
    }

    [Table("suntology.member")]
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string FormerForename { get; set; }

        [Required]
        [MaxLength(255)]
        public string FormerSurname { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1)]
        public string AssignedGender { get; set; }

        [Required]
        public int Age { get; set; }

        [ForeignKey("Caste")]
        public int CasteId { get; set; }

        public Caste Caste { get; set; }
    }

    [Table("suntology.internalcomms")]
    public class InternalComm
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Initiator")]
        public int InitiatorId { get; set; }

        public Member Initiator { get; set; }
        
        [ForeignKey("Initiatee")]
        public int InitiateeId { get; set; }

        public Member Initiatee { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
    }

    public class SuntologyContext : DbContext
    {
        public DbSet<Caste> Castes { get; set; }

        public DbSet<Member> Members { get; set; }

        public DbSet<InternalComm> InternalComms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=suntology;Trusted_Connection=true;");
        }
    }
}
