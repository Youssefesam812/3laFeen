using _3laFeen.Domain.Entities;
using _3laFeen.Domain.IRepositories;
using _3laFeen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3laFeen.Infrastructure.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly _3laFeenDbContext _context;

        public RouteRepository(_3laFeenDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Route route)
        {
            await _context.routes.AddAsync(route);
            await _context.SaveChangesAsync();
            return route.RouteId; // Assuming RouteId is auto-generated
        }

        public async Task DeleteAsync(Route route)
        {
            _context.routes.Remove(route);
            await _context.SaveChangesAsync();
        }

        public async Task<(int totalCount, IEnumerable<Route> routes)> GetAllRoutesAsync(string? search, int pageNumber, int pageSize)
        {
            var query = _context.routes.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(r => r.From.Contains(search) || r.To.Contains(search)); // Simple search example
            }

            var totalCount = await query.CountAsync();
            var routes = await query
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            return (totalCount, routes);
        }

        public async Task<Route> GetByTitleAsync(string routeTitleOrId)
        {
            // Try parsing to integer, assuming the ID is numeric
            if (int.TryParse(routeTitleOrId, out var id))
            {
                return await _context.routes.FindAsync(id); // Find by ID
            }

            return await _context.routes
        .FirstOrDefaultAsync(r => r.From == routeTitleOrId || r.To == routeTitleOrId); // Find by route title
        }

        public async Task<string?> GetRoute(string To, string From)
        {

   // Ensure 'to' and 'from' are correctly defined as method parameters
            var route = await _context.Set<Route>()
                .FirstOrDefaultAsync(r => r.To == To && r.From == From);

            return route?.Transportation != null ? string.Join(", ", route.Transportation) : null;




        }

      
    }
}
