using CleaningSaboms.Context;
using CleaningSaboms.Interfaces;
using CleaningSaboms.Models;
using Microsoft.EntityFrameworkCore;

namespace CleaningSaboms.Repositories
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private readonly DataContext _context;

        public ServiceTypeRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<ServiceType?> GetServiceTypeAsync(int serviceNumber)
        {
            return await _context.ServiceType.FirstOrDefaultAsync(s => s.Id == serviceNumber);
        }
    }
}
