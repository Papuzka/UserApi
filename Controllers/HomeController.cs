using Microsoft.AspNetCore.Mvc;
using UserApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("kkk")]
    public class HomeController : Controller
    {
        private readonly APIDbContext _aPIDbContext;
        
        public HomeController(APIDbContext aPIDbContext)
        {
            _aPIDbContext = aPIDbContext;
        }
        [HttpGet("GetUsers")]

        public ActionResult<IEnumerable<User>> GetAll()
        {
            var users = _aPIDbContext.Users.Include(x => x.Address).ToList();
            return Ok(users);  
        }
        [HttpGet("GetUser")]

        public ActionResult<IEnumerable<User>> GetUser([FromBody] int id)
        {
            var user = _aPIDbContext.Users.FirstOrDefault(x => x.Id == id);
            user.Address = _aPIDbContext.Address.FirstOrDefault(y => y.Id == user.AddressId);


            return Ok(user);
        }
        [HttpPost("CreateUser")]

        public IActionResult Create([FromBody] User req)
        {
            User user = new User();
            user.Address = new Address();

            Console.WriteLine(user.Address);
            user.Address.City = req.Address.City;
            user.Address.Country = req.Address.Country;
            user.Address.Street = req.Address.Street;
            user.Address.PostCode = req.Address.PostCode;
            user.Address.HouseNumber = req.Address.HouseNumber;
            _aPIDbContext.Users.Add(user);

            user.FirstName = req.FirstName;
            user.LastName = req.LastName;
            user.Gender = req.Gender;
            user.AddressId = req.AddressId;
            user.Weight = req.Weight;
            user.DateOfBirth = req.DateOfBirth;
            _aPIDbContext.Users.Add(user);
            

            _aPIDbContext.SaveChanges();
            return Ok();
        }


        [HttpPut("Update")]
        public IActionResult Update([FromBody] User req)
        {
            var user = _aPIDbContext.Users.FirstOrDefault(x => x.Id == req.Id);
            user.Address = _aPIDbContext.Address.FirstOrDefault(y => y.Id == user.AddressId);
            

            user.Address.City = req.Address.City;
            user.Address.Country = req.Address.Country;
            user.Address.Street = req.Address.Street;
            user.Address.PostCode = req.Address.PostCode;
            user.Address.HouseNumber = req.Address.HouseNumber;

            user.FirstName = req.FirstName;
            user.LastName = req.LastName;
            user.Gender = req.Gender;
           // user.AddressId = req.AddressId;

            _aPIDbContext.Entry(user).State = EntityState.Modified;
            _aPIDbContext.SaveChanges();

            var users = _aPIDbContext.Users.ToList();
            return Ok(users);

        }

        [HttpDelete("DeleteUser/{Id}")]
        public IActionResult Delete([FromRoute]int Id)
        {
            var user = _aPIDbContext.Users.FirstOrDefault(x => x.Id == Id);
            if (user == null)
            {
                return StatusCode(404, "User with id {Id} not found");
            }
            _aPIDbContext.Entry(user).State = EntityState.Deleted;
            _aPIDbContext.SaveChanges();
            return Ok();
        }
    }
}
