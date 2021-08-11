using JWT_Demo.Interface;
using JWT_Demo.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT_Demo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IJwtAuth jwtAuth;

        private readonly List<Member> lstMember = new List<Member>()
        {
            new Member{Id=1, Name="Akshay", City = "Bharuch"},
            new Member {Id=2, Name="Rahul", City = "Vadodara" },
            new Member {Id=3, Name="Ankit", City = "Vadodara"},
            new Member {Id=4, Name="Nikhil", City = "Ahmedabad"}
        };
        public MemberController(IJwtAuth jwtAuth)
        {
            this.jwtAuth = jwtAuth;
        }

        // GET: api/<MembersController>
        [HttpGet]
        public IEnumerable<Member> AllMembers()
        {
            return lstMember;
        }

        // GET api/<MembersController>/5
        [HttpGet("{id}")]
        public Member MemberByid(int id)
        {
            return lstMember.Find(x => x.Id == id);
        }

        [AllowAnonymous]
        // POST api/<MembersController>
        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] UserCredential userCredential)
        {
            var token = jwtAuth.Authentication(userCredential.UserName, userCredential.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
