using CollegeStudentXtramile.Server.Domain.Dtos;
using CollegeStudentXtramile.Server.Domain.Entities;

namespace CollegeStudentXtramile.Server.Domain.Interfaces;

public interface IStudentRepository
{
    public Task<Pagination<Student>> Students(string? keyword = null, int? page = null, int? limit = null);
    public Task<Student?> Student(string id);
    public Task<Student> CreateStudent(Student student);
    public Task<Student?> UpdateStudent(StudentUpdate student);
    public Task<Student?> DeleteStudent(string id);
}
