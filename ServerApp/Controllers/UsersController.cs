using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Data;
using ServerApp.DTO;

namespace ServerApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISocialRepository _repository;

        public UsersController(ISocialRepository repository)
        {
            this._repository = repository;
        }

        public async Task<IActionResult> GetUsers()
        {
            var users = await _repository.GetUsers();
            var list = new List<UserForListDTO>();

            foreach (var user in users)
            {
                list.Add(new UserForListDTO()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Gender = user.Gender,
                });
            }

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repository.GetUser(id);
            return Ok(user);
        }
    }
}