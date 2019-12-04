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
    public class CompanyPositionController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<CompanyPosition> Get(int id)
        {
            // GET: api/CompanyPosition
            //HttpContext.RiseError(new InvalidOperationException("Test"));
            CompanyPosition model = new CompanyPosition();
            CompanyPositionDE de = new CompanyPositionDE();
            return de.GetCompanyPosition(id);
        }
        [HttpPost]
        public CompanyPosition Post([FromBody] CompanyPosition model)
        {
            // POST: api/User
            // this post method insert the new row or update the current row if there is a record with same ID
            CompanyPositionDE de = new CompanyPositionDE();
            CompanyPosition sonuc = de.AddCompanyPosition(model);
            return sonuc;
        }
        [HttpPut]
        public IActionResult Put([FromBody] CompanyPosition model)
        {
            // POST: api/CompanyPosition
            // this post method insert the new row or update the current row if there is a record with same ID
            CompanyPositionDE de = new CompanyPositionDE();
            bool sonuc = de.UpdateCompanyPosition(model);
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
            CompanyPositionDE de = new CompanyPositionDE();
            bool sonuc = de.DeleteCompanyPosition(id);
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