using CollegeStudentXtramile.Server.Api.Resources.Responses;
using CollegeStudentXtramile.Server.Domain.Dtos;

namespace CollegeStudentXtramile.Server.Api.Helpers;

public static class PaginationHelper
{
    public static BasePagination<T> CreatePagination<T>(
        string baseEndpoint,
        Pagination<T> pagination
        ) 
    {
        var nextUrl = pagination.Page >= pagination.TotalPages
            ? null
            : $"{baseEndpoint}?page={pagination.Page + 1}&limit={pagination.Limit}";
        var page = pagination.Page;

        var previousUrl = page <= 1
            ? null
            : $"{baseEndpoint}?page={page - 1}&limit={pagination.Limit}";

        return new BasePagination<T>(
            NextUrl: nextUrl,
            PreviousUrl: previousUrl,
            Pagination: pagination
        );
    }
}
