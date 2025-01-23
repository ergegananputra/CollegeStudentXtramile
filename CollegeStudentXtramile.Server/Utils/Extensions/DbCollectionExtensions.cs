using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CollegeStudentXtramile.Server.Utils.Extensions;

public static class DbCollectionExtensions
{
    public static bool IsDuplicateKeyException(this DbUpdateException exception)
    {
        if (exception.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
        {
            return true;
        }

        // Add more checks for other database providers as needed


        return false;
    }
}
