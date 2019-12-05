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
    public class ExamController : ControllerBase
    {
        [HttpGet("{id}")]
        public IEnumerable<Exam> Get(int id)
        {
            // GET: api/Exam
            //HttpContext.RiseError(new InvalidOperationException("Test"));
            Exam model = new Exam();
            ExamDE de = new ExamDE();
            return de.GetExam(id);
        }
        [HttpPost]
        public Exam Post([FromBody] Exam model)
        {
            // POST: api/User
            // this post method insert the new row or update the current row if there is a record with same ID
            ExamDE de = new ExamDE();
            Exam sonuc = de.AddExam(model);
            return sonuc;
        }
        [HttpPut]
        public IActionResult Put([FromBody] Exam model)
        {
            // POST: api/Exam
            // this post method insert the new row or update the current row if there is a record with same ID
            ExamDE de = new ExamDE();
            bool sonuc = de.UpdateExam(model);
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
            ExamDE de = new ExamDE();
            bool sonuc = de.DeleteExam(id);
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