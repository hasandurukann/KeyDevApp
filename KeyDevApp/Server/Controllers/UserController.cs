using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KeyDevApp.Shared;
using KeyDevApp.Server.DataEngine;

namespace KeyDevApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<User> Get(int id)
        {
            // GET: api/User
            //HttpContext.RiseError(new InvalidOperationException("Test"));
            User user = new User();
            UserDE userDE = new UserDE();
            return userDE.GetUser(id);
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