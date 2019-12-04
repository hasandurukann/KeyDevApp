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
    public class DepartmentController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Department> Get(int id)
        {
            // GET: api/Department
            //HttpContext.RiseError(new InvalidOperationException("Test"));
            Department model = new Department();
            DepartmentDE de = new DepartmentDE();
            return de.GetDepartment(id);
        }
        [HttpPost]
        public Department Post([FromBody] Department model)
        {
            // POST: api/User
            // this post method insert the new row or update the current row if there is a record with same ID
            DepartmentDE de = new DepartmentDE();
            Department sonuc = de.AddDepartment(model);
            return sonuc;
        }
        [HttpPut]
        public IActionResult Put([FromBody] Department model)
        {
            // POST: api/Department
            // this post method insert the new row or update the current row if there is a record with same ID
            DepartmentDE de = new DepartmentDE();
            bool sonuc = de.UpdateDepartment(model);
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
            DepartmentDE de = new DepartmentDE();
            bool sonuc = de.DeleteDepartment(id);
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