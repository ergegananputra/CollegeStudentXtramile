namespace CollegeStudentXtramile.Server.Api.Resources.Responses;

public record class BaseResponseJSON<DataType>
(
    string? Message,
    DataType Data
);
