using _3laFeen.Domain.Entities;
using _3laFeen.Domain.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Route = _3laFeen.Domain.Entities.Route;

namespace _3laFeen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteRepository _routeRepository;

        public RouteController(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        // Add a new route
        [HttpPost]
        public async Task<IActionResult> AddRoute([FromBody] Route route)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var routeId = await _routeRepository.AddAsync(route);
            return Ok(new { RouteId = routeId, Message = "Route added successfully" });
        }

        // Delete a route
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoute(int id)
        {
            var route = await _routeRepository.GetByTitleAsync(id.ToString());
            if (route == null)
                return NotFound(new { Message = "Route not found" });

            await _routeRepository.DeleteAsync(route);
            return Ok(new { Message = "Route deleted successfully" });
        }

        // Get all routes with optional search and pagination
        [HttpGet]
        public async Task<IActionResult> GetAllRoutes([FromQuery] string? search, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var (totalCount, routes) = await _routeRepository.GetAllRoutesAsync(search, pageNumber, pageSize);

            return Ok(new
            {
                TotalCount = totalCount,
                Routes = routes
            });
        }

        // Get a specific route by title or ID
        [HttpGet("{routeTitleOrId}")]
        public async Task<IActionResult> GetRouteByTitleOrId(string routeTitleOrId)
        {
            var route = await _routeRepository.GetByTitleAsync(routeTitleOrId);

            if (route == null)
                return NotFound(new { Message = "Route not found" });

            return Ok(route);
        }

        // Get transportation details between two locations
        [HttpGet("GetRoute")]
        public async Task<IActionResult> GetRoute([FromQuery] string from, [FromQuery] string to)
        {
            var transportation = await _routeRepository.GetRoute(to, from);

            if (transportation == null)
                return NotFound(new { Message = "Route not found" });

            return Ok(new
            {
                From = from,
                To = to,
                Transportation = transportation
            });
        }
    }
}
