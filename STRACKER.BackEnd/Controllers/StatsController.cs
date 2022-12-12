using BackEnd.Repositories;
using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IStatsReposistory _statsReposistory;

        public StatsController(IStatsReposistory statsReposistory)
        {
            _statsReposistory = statsReposistory;
        }

        [HttpGet]

        public IActionResult GetStats()
        {
            List<Stats> stats = _statsReposistory.GetAllStats();

            return Ok(stats);
        }

    }


}
