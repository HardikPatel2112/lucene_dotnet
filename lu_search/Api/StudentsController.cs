using lu_search.Models;
using System.Linq;
using System.Web.Http;

namespace lu_search.Api
{
    public class StudentsController : ApiController
    {

        [HttpGet]
        [Microsoft.AspNetCore.Mvc.ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [Microsoft.AspNetCore.Mvc.ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]

        public IHttpActionResult Get(int? id)
        {
            if (id == null)
            {
                return Ok(StudentData.Students);
            }
            Student student = StudentData.Students.SingleOrDefault(x => x.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);

        }

        [HttpPost]
        [Microsoft.AspNetCore.Mvc.ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [Microsoft.AspNetCore.Mvc.ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status201Created)]

        public IHttpActionResult Create(Student student)
        {
            if (student == null)
            {
                return BadRequest();
            }
            StudentData.Students.Add(student);

            return Created(Url.Content("http://localhost:63903/api/students/Get/?id=" + student.Id), student);
        }

        [Microsoft.AspNetCore.Mvc.ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {


            Student student = StudentData.Students.SingleOrDefault(x => x.Id == id);
            if(student == null)
            {
                return NotFound();
            }
            StudentData.Students.Remove(student);
            return Ok();
        }

        [HttpPost]
        [Microsoft.AspNetCore.Mvc.ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [Microsoft.AspNetCore.Mvc.ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]

        [HttpPut]
        public IHttpActionResult Update(int id, Student student)
        {
            if (student == null || id != student.Id)
            {
                return BadRequest();
            }
            var studentindb = StudentData.Students.SingleOrDefault(x => x.Id == id);
            if (studentindb == null)
            {
                return NotFound();
            }
            studentindb.Address = student.Address;
            studentindb.Name = student.Name;
            StudentData.Students.Add(studentindb);
            return Ok();
        }

    }
}
