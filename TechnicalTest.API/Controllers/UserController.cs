using System.Globalization;
using System.Net;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechnicalTest.API.Data;
using TechnicalTest.API.DTO;
using TechnicalTest.API.Models;
using Document = TechnicalTest.API.Models.Document;

namespace TechnicalTest.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly EventDbContext _eventDbContext;

        public UserController(EventDbContext eventDbContext)
        {
            _eventDbContext = eventDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetDocuments()
        {
            var users = await _eventDbContext.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<UserDto>> CreateDocument([FromBody] UserDto UserDto)
        {
            var dateNow = DateTime.Now;
            dateNow = DateTime.SpecifyKind(dateNow, DateTimeKind.Utc);
            var user = new User
            {
                FirstName = UserDto.FirstName,
                LastName = UserDto.LastName,
                HireDate = dateNow,

                Id = Guid.NewGuid()
            };

            await _eventDbContext.Users.AddAsync(user);
            await _eventDbContext.SaveChangesAsync();

            return Ok(user);
        }






    }


}