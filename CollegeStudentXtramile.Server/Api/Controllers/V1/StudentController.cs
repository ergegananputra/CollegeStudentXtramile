using CollegeStudentXtramile.Server.Api.Helpers;
using CollegeStudentXtramile.Server.Api.Resources.Responses;
using CollegeStudentXtramile.Server.Domain.Dtos;
using CollegeStudentXtramile.Server.Domain.Entities;
using CollegeStudentXtramile.Server.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace CollegeStudentXtramile.Server.Api.Controllers.V1;

[Route("api/v1/students")]
[ApiController]
public class StudentController : ControllerBase
{

    private readonly ILogger<StudentController> _logger;
    private readonly IStudentRepository _studentRepository;

    public StudentController(ILogger<StudentController> logger, IStudentRepository studentRepository)
    {
        _logger = logger;
        _studentRepository = studentRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetStudents([FromQuery] string? keyword, [FromQuery] int? page, [FromQuery] int? limit)
    {
        try
        {
            var students = await _studentRepository.Students(keyword, page, limit);

            var currentUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";

            var paginate = PaginationHelper.CreatePagination<Student>(
                baseEndpoint: currentUrl,
                pagination: students
            );

            var result = new BaseResponseJSON<BasePagination<Student>>(
                Message: "Students fetched successfully",
                Data: paginate
            );

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[StudentController/GetStudents] - An error occurred while fetching students");
            return StatusCode(500, new BaseResponseJSON<object>(
                Message: "An error occurred while fetching students",
                Data: new { }
            ));
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudent(string id)
    {
        try
        {
            var student = await _studentRepository.Student(id);
            if (student == null)
            {
                return NotFound(new BaseResponseJSON<object>(
                    Message: "Student not found",
                    Data: new { }
                ));
            }
            var result = new BaseResponseJSON<Student>(
                Message: "Student fetched successfully",
                Data: student
            );
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[StudentController/GetStudent] - An error occurred while fetching student");
            return StatusCode(500, new BaseResponseJSON<object>(
                Message: "An error occurred while fetching student",
                Data: new { }
            ));
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudent([FromBody] Student student)
    {
        try
        {
            var newStudent = await _studentRepository.CreateStudent(student);
            var result = new BaseResponseJSON<Student>(
                Message: "Student created successfully",
                Data: newStudent
            );
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[StudentController/CreateStudent] - An error occurred while creating student");
            return StatusCode(500, new BaseResponseJSON<object>(
                Message: "An error occurred while creating student",
                Data: new { }
            ));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(string id, [FromBody] StudentUpdate student)
    {
        try
        {
            student = student with { Id = id };
            var updatedStudent = await _studentRepository.UpdateStudent(student);
            if (updatedStudent == null)
            {
                return NotFound(new BaseResponseJSON<object>(
                    Message: "Student not found",
                    Data: new { }
                ));
            }
            var result = new BaseResponseJSON<Student>(
                Message: "Student updated successfully",
                Data: updatedStudent
            );
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[StudentController/UpdateStudent] - An error occurred while updating student");
            return StatusCode(500, new BaseResponseJSON<object>(
                Message: "An error occurred while updating student",
                Data: new { }
            ));
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(string id)
    {
        try
        {
            var deletedStudent = await _studentRepository.DeleteStudent(id);
            if (deletedStudent == null)
            {
                return NotFound(new BaseResponseJSON<object>(
                    Message: "Student not found",
                    Data: new { }
                ));
            }
            var result = new BaseResponseJSON<Student>(
                Message: "Student deleted successfully",
                Data: deletedStudent
            );
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[StudentController/DeleteStudent] - An error occurred while deleting student");
            return StatusCode(500, new BaseResponseJSON<object>(
                Message: "An error occurred while deleting student",
                Data: new { }
            ));
        }
    }

}
