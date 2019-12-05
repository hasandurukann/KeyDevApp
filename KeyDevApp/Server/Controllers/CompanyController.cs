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
    public class CompanyController : ControllerBase
    {
        [HttpGet("{id}")]
        public IEnumerable<Company> Get(int id)
        {
            // GET: api/Company
            //HttpContext.RiseError(new InvalidOperationException("Test"));
            Company model = new Company();
            CompanyDE de = new CompanyDE();
            return de.GetCompany(id);
        }
        [HttpPost]
        public Company Post([FromBody] Company model)
        {
            // POST: api/User
            // this post method insert the new row or update the current row if there is a record with same ID
            CompanyDE de = new CompanyDE();
            Company sonuc = de.AddCompany(model, model.User);
            return sonuc;
        }
        [HttpPut]
        public IActionResult Put([FromBody] Company model)
        {
            // POST: api/Company
            // this post method insert the new row or update the current row if there is a record with same ID
            CompanyDE de = new CompanyDE();
            bool sonuc = de.UpdateCompany(model);
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
            CompanyDE de = new CompanyDE();
            bool sonuc = de.DeleteCompany(id);
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