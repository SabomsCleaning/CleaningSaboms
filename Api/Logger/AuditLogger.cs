using CleaningSaboms.Context;
using CleaningSaboms.Interfaces;
using CleaningSaboms.Models;
using Microsoft.EntityFrameworkCore;

namespace CleaningSaboms.Logger
{
    public class AuditLogger(DataContext context) : IAuditLogger
    {
        private readonly DataContext _context = context;

        public async Task LogAsync(string action, string performedBy, string target, string? details = null)
        {
            var log = new Models.AuditLogger
            {
                Action = action,
                PerformedBy = performedBy,
                Target = target,
                Details = details,
                Timestamp = DateTime.UtcNow
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
