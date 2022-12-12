using BackEnd.Models;
using BackEnd.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private ITeamRepository _teamRepository;

        public TeamsController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        [HttpGet]

        public IActionResult GetAllTeams()
        {
            List<Team> teams = _teamRepository.GetTeamList();

            return Ok(teams);
        }
    }
}
