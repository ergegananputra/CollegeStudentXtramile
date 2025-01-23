using CollegeStudentXtramile.Server.Api.Resources.Responses;
using CollegeStudentXtramile.Server.Domain.Dtos;

namespace CollegeStudentXtramile.Server.Api.Helpers;

public static class PaginationHelper
{
    public static object ToPaginationJSON<T>(
        this Pagination<T> pagination, 
        string? nextUrl, 
        string? previousUrl
        ) 
    {
        return new
        {
            Page = pagination.Page,
            Limit = pagination.Limit,
            Total = pagination.Total,
            Url = new {
                Next = nextUrl,
                Previous = previousUrl,
            },
            TotalPages = pagination.TotalPages,
            Items = pagination.Items
        };
    }
    


    public static object CreatePagination<T>(
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

        return pagination.ToPaginationJSON(nextUrl: nextUrl, previousUrl: previousUrl);
    }
}
