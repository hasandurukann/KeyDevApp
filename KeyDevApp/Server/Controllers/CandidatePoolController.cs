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
    public class CandidatePoolController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<CandidatePool> Get(int id)
        {
            // GET: api/CandidatePool
            //HttpContext.RiseError(new InvalidOperationException("Test"));
            CandidatePool model = new CandidatePool();
            CandidatePoolDE de = new CandidatePoolDE();
            return de.GetCandidatePool(id);
        }
        [HttpPost]
        public CandidatePool Post([FromBody] CandidatePool model)
        {
            // POST: api/User
            // this post method insert the new row or update the current row if there is a record with same ID
            CandidatePoolDE de = new CandidatePoolDE();
            CandidatePool sonuc = de.AddCandidatePool(model);
            return sonuc;
        }
        [HttpPut]
        public IActionResult Put([FromBody] CandidatePool model)
        {
            // POST: api/CandidatePool
            // this post method insert the new row or update the current row if there is a record with same ID
            CandidatePoolDE de = new CandidatePoolDE();
            bool sonuc = de.UpdateCandidatePool(model);
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
            CandidatePoolDE de = new CandidatePoolDE();
            bool sonuc = de.DeleteCandidatePool(id);
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