using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_Day3.Models;
using Task_Day3.Services;

namespace Task_Day3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentService _studentService;
        public StudentController(IStudentService StudentService)
        {
            _studentService = StudentService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var students = _studentService.Get_All();
                if (students == null)
                {
                    throw new Exception("Problem occured");
                }
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        public IActionResult Post(Student student)
        {
            try 
            {
                var stud = _studentService.Post(student);
                return Ok();

             }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

         }
        [HttpGet("{id}")]
        public IActionResult Get_id(int id)
        {
            try
            {
                var stud = _studentService.Get_by_id(id);
                if(stud == null)
                {
                    throw new Exception("user not found");
                }
                return Ok(stud);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult update(int id , Student student)
        {
            try
            {
                var update = _studentService.Update(id, student);
                if(update == null)
                {
                    throw new Exception("User not found");

                }
                return Ok(update);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var bol = _studentService.Delete(id);
                if (!bol)
                {
                    throw new Exception("User not found to delete");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
