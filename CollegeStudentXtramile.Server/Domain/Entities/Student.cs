using System.ComponentModel.DataAnnotations;
using CollegeStudentXtramile.Server.Domain.Common;

namespace CollegeStudentXtramile.Server.Domain.Entities;

public class Student : BaseEntity
{
    [Key]
    public string Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string FullName => $"{FirstName} {LastName}";
    public int Age => DateTime.UtcNow.Year - DateOfBirth.Year;

}
