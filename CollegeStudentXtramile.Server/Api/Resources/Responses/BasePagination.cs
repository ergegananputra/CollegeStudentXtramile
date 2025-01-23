using CollegeStudentXtramile.Server.Domain.Dtos;

namespace CollegeStudentXtramile.Server.Api.Resources.Responses;

public record class BasePagination<ItemDataType>
(
    string? NextUrl,
    string? PreviousUrl,
    Pagination<ItemDataType> Pagination
) : Pagination<ItemDataType>(
    Pagination.Page, 
    Pagination.Limit, 
    Pagination.Total, 
    Pagination.TotalPages, 
    Pagination.Items
    );
