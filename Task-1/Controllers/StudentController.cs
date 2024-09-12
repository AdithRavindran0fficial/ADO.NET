using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_1.Models;
using Task_1.Services;

namespace Task_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IService _studentService;
        public StudentController(IService studentservice)
        {
            _studentService = studentservice;

        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var All_students = _studentService.GetStudents();
                return Ok(All_students);
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
                var row_effected = _studentService.Post(student);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("{id}")]
        public IActionResult Get_id(int id)
        {
            try
            {
                var exist = _studentService.Get_id(id);
                if (exist == null)
                {
                    throw new Exception("User not found");
                }
                return Ok(exist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Put(int id, Student student)
        {
            try
            {
                var std = _studentService.update(id, student);
                if (std == null)
                {
                    throw new Exception("User not found");
                }
                return NoContent();
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
                var val = _studentService.Delete(id);
                if (val == 0)
                {
                    return NotFound();
                }
                return Ok();
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
