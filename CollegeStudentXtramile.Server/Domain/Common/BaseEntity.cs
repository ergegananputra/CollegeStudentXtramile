namespace CollegeStudentXtramile.Server.Domain.Common;

public abstract class BaseEntity
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }


    public void UpdateTimestamp()
    {
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void DeleteTimeStamp()
    {
        DeletedAt = DateTimeOffset.UtcNow;
    }

    public void Recover()
    {
        DeletedAt = null;
    }
}
