using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegisterAPI.Dtos;
using RegisterAPI.Models;

namespace RegisterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly RegisterContext _context;
        public StudentController(RegisterContext context)
        {
            _context = context;
        }

        [HttpPost("register")] 

        public IActionResult RegisterStudent([FromBody] RegisterStudentDto studentDto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var student = new Student
            {
                Cccd = studentDto.Cccd,
                StudentName = studentDto.StudentName,
                Email = studentDto.Email,
                Phone = studentDto.Phone,
                DateOfBirth = studentDto.DateOfBirth,
                Address = studentDto.Address,
                Prioritize = studentDto.Prioritize,
                ProvinceId = studentDto.ProvinceId,
                HighSchoolId = studentDto.HighSchoolId,
                AreaId = studentDto.AreaId,
                MajorId = studentDto.MajorId,
                Gender = studentDto.Gender,
                AspirationId = studentDto.AspirationId
            };
            try
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, new { message = "Student registered successfully", studentId = student.StudentId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error saving data: {ex.Message}");
            }
        }
    }
}
