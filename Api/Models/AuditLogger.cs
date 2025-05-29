namespace CleaningSaboms.Models
{
    public class AuditLogger
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Action { get; set; } = null!;
        public string PerformedBy { get; set; } = null!;
        public string Target { get; set; } = null!;
        public string? Details { get; set; }
    }
}
