using BackEnd.Models;
using BackEnd.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
           private readonly IParticipantRepository _participantRepository;

           public ParticipantController(IParticipantRepository participantRepository)
            {
                _participantRepository = participantRepository;
            }

            [HttpGet]

            public IActionResult GetAllParticipants()
            {
                List<Participant> participantList = _participantRepository.GetParticipantList();

                return Ok(participantList);
            }
        
    }
}
