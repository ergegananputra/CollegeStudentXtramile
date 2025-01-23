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

    public DateTimeOffset DateOfBirth { get; set; }
}
