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
    public class QuestionController : ControllerBase
    {
        [HttpGet("{id}")]
        public IEnumerable<Question> Get(int id)
        {
            // GET: api/Question
            //HttpContext.RiseError(new InvalidOperationException("Test"));
            Question model = new Question();
            QuestionDE de = new QuestionDE();
            return de.GetQuestion(id);
        }
        [HttpPost]
        public Question Post([FromBody] Question model)
        {
            // POST: api/User
            // this post method insert the new row or update the current row if there is a record with same ID
            QuestionDE de = new QuestionDE();
            Question sonuc = de.AddQuestion(model);
            return sonuc;
        }
        [HttpPut]
        public IActionResult Put([FromBody] Question model)
        {
            // POST: api/Question
            // this post method insert the new row or update the current row if there is a record with same ID
            QuestionDE de = new QuestionDE();
            bool sonuc = de.UpdateQuestion(model);
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
            QuestionDE de = new QuestionDE();
            bool sonuc = de.DeleteQuestion(id);
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