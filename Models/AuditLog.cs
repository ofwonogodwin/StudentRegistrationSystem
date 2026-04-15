using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationSystem.Models
{
    public class AuditLog
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Action { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string EntityName { get; set; } = string.Empty;

        [StringLength(100)]
        public string? EntityId { get; set; }

        [StringLength(500)]
        public string? Details { get; set; }

        [Required]
        [StringLength(100)]
        public string PerformedBy { get; set; } = string.Empty;

        [StringLength(20)]
        public string PerformedByRole { get; set; } = string.Empty;

        [StringLength(50)]
        public string? IpAddress { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}