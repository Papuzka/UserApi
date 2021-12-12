using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserApi.Entities;
namespace UserApi.Controllers
{
    [Route("a")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly APIDbContext _aPIDbContext;
        public ValuesController(APIDbContext aPIDbContext)
        {
            _aPIDbContext = aPIDbContext;

        }
        [HttpPost("CreateUser")]
        public IActionResult Create([FromBody] User user)
        {
           // var newUser = _aPIDbContext.Users(user);
            _aPIDbContext.Users.Add(user);
            _aPIDbContext.SaveChanges();
            return Ok();
        }
    }
}
