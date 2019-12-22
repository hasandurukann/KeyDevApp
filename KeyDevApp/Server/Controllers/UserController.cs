using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KeyDevApp.Shared;
using KeyDevApp.Server.DataEngine;
using KeyDevApp.Server.Service;
using Microsoft.AspNetCore.Authorization;

namespace KeyDevApp.Server.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public IEnumerable<User> Get(int id)
        {
            // GET: api/User
            //HttpContext.RiseError(new InvalidOperationException("Test"));
            User user = new User();
            UserDE userDE = new UserDE();
            return userDE.GetUser(id);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            User umodel = new User();
            umodel.Mail = model.Email;
            umodel.Password = model.Password;
            var user = _userService.Authenticate(umodel);

            if (user == null)
                return BadRequest(new { message = "Login failed, incorrect information." });

            return Ok(user);
        }

        [HttpPut]
        public IActionResult Put([FromBody] User user)
        {
            // POST: api/User
            // this post method insert the new row or update the current row if there is a record with same ID
            UserDE userDE = new UserDE();
            bool sonuc = userDE.UpdateUser(user,user.Candidate,user.Company);
            if (sonuc)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // DELETE: api/ApiWithActions/5
            UserDE userDE = new UserDE();
            bool sonuc = userDE.DeleteUser(id);
            if (sonuc)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}