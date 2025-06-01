namespace CleaningSaboms.Interfaces
{
    public interface IAuditLogger
    {
        Task LogAsync(string action, string performedBy, string target, string? details = null);
    }
}
