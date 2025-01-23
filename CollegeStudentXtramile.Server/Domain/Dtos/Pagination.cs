namespace CollegeStudentXtramile.Server.Domain.Dtos;

public record class Pagination<ItemDataType>
(
    int Page,
    int Limit,
    int Total,
    int TotalPages,
    IEnumerable<ItemDataType> Items
);
