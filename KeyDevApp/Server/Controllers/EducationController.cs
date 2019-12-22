using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KeyDevApp.Shared;
using KeyDevApp.Server.DataEngine;
using Microsoft.AspNetCore.Authorization;

namespace KeyDevApp.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        [HttpGet("{id}")]
        public IEnumerable<Education> Get(int id)
        {
            // GET: api/Education
            //HttpContext.RiseError(new InvalidOperationException("Test"));
            Education model = new Education();
            EducationDE de = new EducationDE();
            return de.GetEducation(id);
        }
        [HttpPost]
        public Education Post([FromBody] Education model)
        {
            // POST: api/User
            // this post method insert the new row or update the current row if there is a record with same ID
            EducationDE de = new EducationDE();
            Education sonuc = de.AddEducation(model);
            return sonuc;
        }
        [HttpPut]
        public IActionResult Put([FromBody] Education model)
        {
            // POST: api/Education
            // this post method insert the new row or update the current row if there is a record with same ID
            EducationDE de = new EducationDE();
            bool sonuc = de.UpdateEducation(model);
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
            EducationDE de = new EducationDE();
            bool sonuc = de.DeleteEducation(id);
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