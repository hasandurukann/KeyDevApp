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
    public class CandidateKeywordController : ControllerBase
    {
        [HttpGet("{id}")]
        public IEnumerable<CandidateKeyword> Get(int id)
        {
            // GET: api/CandidateKeyword
            //HttpContext.RiseError(new InvalidOperationException("Test"));
            CandidateKeyword model = new CandidateKeyword();
            CandidateKeywordDE de = new CandidateKeywordDE();
            return de.GetCandidateKeyword(id);
        }
        [HttpPost]
        public CandidateKeyword Post([FromBody] CandidateKeyword model)
        {
            // POST: api/User
            // this post method insert the new row or update the current row if there is a record with same ID
            CandidateKeywordDE de = new CandidateKeywordDE();
            CandidateKeyword sonuc = de.AddCandidateKeyword(model);
            return sonuc;
        }
        [HttpPut]
        public IActionResult Put([FromBody] CandidateKeyword model)
        {
            // POST: api/CandidateKeyword
            // this post method insert the new row or update the current row if there is a record with same ID
            CandidateKeywordDE de = new CandidateKeywordDE();
            bool sonuc = de.UpdateCandidateKeyword(model);
            if (sonuc)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete]
        public IActionResult Delete([FromBody] CandidateKeyword model)
        {
            // DELETE: api/ApiWithActions/5
            CandidateKeywordDE de = new CandidateKeywordDE();
            bool sonuc = de.DeleteCandidateKeyword(model);
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