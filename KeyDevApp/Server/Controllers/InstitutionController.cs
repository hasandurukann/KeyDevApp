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
    public class InstitutionController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Institution> Get(int id)
        {
            // GET: api/Institution
            //HttpContext.RiseError(new InvalidOperationException("Test"));
            Institution model = new Institution();
            InstitutionDE de = new InstitutionDE();
            return de.GetInstitution(id);
        }
        [HttpPost]
        public Institution Post([FromBody] Institution model)
        {
            // POST: api/User
            // this post method insert the new row or update the current row if there is a record with same ID
            InstitutionDE de = new InstitutionDE();
            Institution sonuc = de.AddInstitution(model);
            return sonuc;
        }
        [HttpPut]
        public IActionResult Put([FromBody] Institution model)
        {
            // POST: api/Institution
            // this post method insert the new row or update the current row if there is a record with same ID
            InstitutionDE de = new InstitutionDE();
            bool sonuc = de.UpdateInstitution(model);
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
            InstitutionDE de = new InstitutionDE();
            bool sonuc = de.DeleteInstitution(id);
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