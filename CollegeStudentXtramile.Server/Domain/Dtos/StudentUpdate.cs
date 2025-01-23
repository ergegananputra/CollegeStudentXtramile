namespace CollegeStudentXtramile.Server.Domain.Dtos;

public record class StudentUpdate
(
    string Id,
    string? FirstName,
    string? LastName,
    DateOnly? DateOfBirth
);
