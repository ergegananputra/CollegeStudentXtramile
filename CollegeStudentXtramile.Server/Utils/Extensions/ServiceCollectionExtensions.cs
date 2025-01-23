using CollegeStudentXtramile.Server.Data.EfCore;
using CollegeStudentXtramile.Server.Data.Repositories;
using CollegeStudentXtramile.Server.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CollegeStudentXtramile.Server.Utils.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Database"));
        });
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IStudentRepository, StudentRepository>();
    }
}
