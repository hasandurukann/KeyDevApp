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
    public class CandidateController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Candidate> Get(int id)
        {
            // GET: api/Candidate
            //HttpContext.RiseError(new InvalidOperationException("Test"));
            Candidate model = new Candidate();
            CandidateDE de = new CandidateDE();
            return de.GetCandidate(id);
        }
        [HttpPost]
        public Candidate Post([FromBody] Candidate model)
        {
            // POST: api/User
            // this post method insert the new row or update the current row if there is a record with same ID
            CandidateDE de = new CandidateDE();
            Candidate sonuc = de.AddCandidate(model, model.User);
            return sonuc;
        }
        [HttpPut]
        public IActionResult Put([FromBody] Candidate model)
        {
            // POST: api/Candidate
            // this post method insert the new row or update the current row if there is a record with same ID
            CandidateDE de = new CandidateDE();
            bool sonuc = de.UpdateCandidate(model);
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
            CandidateDE de = new CandidateDE();
            bool sonuc = de.DeleteCandidate(id);
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