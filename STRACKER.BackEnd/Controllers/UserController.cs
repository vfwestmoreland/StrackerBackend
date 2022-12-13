using BackEnd.Models;
using BackEnd.Repositories;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            List<User> users = _userRepository.GetUsers();

            return Ok(users);
        }

        [HttpGet("{firebaseUserId}")]
        public async Task<IActionResult> GetUserById(string firebaseUserId)
        {
            User user = _userRepository.GetUserByFirebaseId(firebaseUserId);

            var isUser = await FirebaseAuth.DefaultInstance.GetUserAsync(firebaseUserId);

            if (user == null)
            {
                User newUser = new()
                {
                    Id = 0,
                    FirebaseUserId = firebaseUserId,
                    Email = isUser.Email,
                    FirstName = "Bob",
                    LastName = "Dobalina",
                    UserTypeId = 1,
                    IsParticipant = false,

                };
                _userRepository.AddUser(newUser);
                return Ok(newUser);
            }
            return Ok(user);
        }

       
    }
}
