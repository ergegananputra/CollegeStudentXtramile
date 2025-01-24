using CollegeStudentXtramile.Server.Data.EfCore;
using CollegeStudentXtramile.Server.Domain.Constants;
using CollegeStudentXtramile.Server.Domain.Dtos;
using CollegeStudentXtramile.Server.Domain.Entities;
using CollegeStudentXtramile.Server.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CollegeStudentXtramile.Server.Data.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _context;

    public StudentRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Student> CreateStudent(Student student)
    {
        var existingStudent = await _context.Students.FindAsync(student.Id);

        if (existingStudent != null)
        {
            if (existingStudent.DeletedAt == null)
            {
                // The code for generating error
                _context.Entry(existingStudent).State = EntityState.Detached;
                _context.Students.Add(student);
            }

            existingStudent.Recover();
            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.DateOfBirth = student.DateOfBirth;
            existingStudent.UpdateTimestamp();

        }
        else
        {
            _context.Students.Add(student);
        }

        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<Student?> DeleteStudent(string id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null || student.DeletedAt != null)
        {
            return null;
        }

        student!.DeleteTimeStamp();
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<Student?> UpdateStudent(StudentUpdate student)
    {
        var existingStudent = _context.Students.Find(student.Id);

        if (existingStudent == null || existingStudent.DeletedAt != null)
        {
            return null;
        }

        existingStudent.UpdateTimestamp();
        existingStudent.FirstName = student.FirstName ?? existingStudent.FirstName;
        existingStudent.LastName = student.LastName ?? existingStudent.LastName;
        existingStudent.DateOfBirth = student.DateOfBirth ?? existingStudent.DateOfBirth;

        await _context.SaveChangesAsync();

        return existingStudent;
    }

    public async Task<Student?> Student(string id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null || student.DeletedAt != null)
        {
            return null;
        }

        return student;
    }

    public async Task<Pagination<Student>> Students(string? keyword = null, int? page = null, int? limit = null)
    {
        var query = _context.Students.AsQueryable();
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(x => x.FirstName.Contains(keyword) || (x.LastName != null && x.LastName.Contains(keyword)));
        }

        query = query.Where(x => x.DeletedAt == null);

        var total = await query.CountAsync();
        var p = page ?? PaginationConstants.Page;
        var l = limit ??= PaginationConstants.Limit;

        var students = await query
            .OrderBy(x => x.CreatedAt)
            .Skip((p - 1) * l)
            .Take(l)
            .ToListAsync();

        int totalPages = (int)Math.Ceiling(total / (double)limit.Value);

        return new Pagination<Student>(
            Page: p, 
            Limit: l, 
            Total: total, 
            TotalPages: totalPages, 
            Items: students
            );

    }
}
