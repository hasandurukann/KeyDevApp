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
    public class ExperienceController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Experience> Get(int id)
        {
            // GET: api/Experience
            //HttpContext.RiseError(new InvalidOperationException("Test"));
            Experience model = new Experience();
            ExperienceDE de = new ExperienceDE();
            return de.GetExperience(id);
        }
        [HttpPost]
        public Experience Post([FromBody] Experience model)
        {
            // POST: api/User
            // this post method insert the new row or update the current row if there is a record with same ID
            ExperienceDE de = new ExperienceDE();
            Experience sonuc = de.AddExperience(model);
            return sonuc;
        }
        [HttpPut]
        public IActionResult Put([FromBody] Experience model)
        {
            // POST: api/Experience
            // this post method insert the new row or update the current row if there is a record with same ID
            ExperienceDE de = new ExperienceDE();
            bool sonuc = de.UpdateExperience(model);
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
            ExperienceDE de = new ExperienceDE();
            bool sonuc = de.DeleteExperience(id);
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