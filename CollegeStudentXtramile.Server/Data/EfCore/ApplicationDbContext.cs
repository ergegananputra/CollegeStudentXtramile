using CollegeStudentXtramile.Server.Domain.Common;
using CollegeStudentXtramile.Server.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeStudentXtramile.Server.Data.EfCore;

public class ApplicationDbContext : DbContext
{

    public DbSet<Student> Students { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        BaseEntityConfig(modelBuilder.Entity<Student>(), entity =>
        {
            entity.HasKey(e => e.Id);
        });

    }

    private static void BaseEntityConfig<T>(EntityTypeBuilder<T> entity, Action<EntityTypeBuilder<T>>? buildAction = null) where T : BaseEntity
    {
        entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

        buildAction?.Invoke(entity);
    }


}
