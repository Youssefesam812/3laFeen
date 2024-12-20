using _3laFeen.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _3laFeen.Domain.IRepositories
{
    public interface IRouteRepository
    {public Task<int> AddAsync(Route route);
     public Task DeleteAsync(Route route);
     public Task<(int totalCount, IEnumerable<Route> routes)> GetAllRoutesAsync(string? search, int pageNumber, int pageSize);
     public  Task<Route> GetByTitleAsync(string routeTitle);
     public   Task<string?> GetRoute(string To , string From);

    }
}
