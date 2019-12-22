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
    public class KeywordController : ControllerBase
    {
        [HttpGet("{id}")]
        public IEnumerable<Keyword> Get(int id)
        {
            // GET: api/Keyword
            //HttpContext.RiseError(new InvalidOperationException("Test"));
            Keyword model = new Keyword();
            KeywordDE de = new KeywordDE();
            return de.GetKeyword(id);
        }
        [HttpPost]
        public Keyword Post([FromBody] Keyword model)
        {
            // POST: api/User
            // this post method insert the new row or update the current row if there is a record with same ID
            KeywordDE de = new KeywordDE();
            Keyword sonuc = de.AddKeyword(model);
            return sonuc;
        }
        [HttpPut]
        public IActionResult Put([FromBody] Keyword model)
        {
            // POST: api/Keyword
            // this post method insert the new row or update the current row if there is a record with same ID
            KeywordDE de = new KeywordDE();
            bool sonuc = de.UpdateKeyword(model);
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
            KeywordDE de = new KeywordDE();
            bool sonuc = de.DeleteKeyword(id);
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