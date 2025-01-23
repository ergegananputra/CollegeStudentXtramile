namespace CollegeStudentXtramile.Server.Api.Resources.Responses;

public class ErrorResponseJSON
{
    public string? TraceId { get; set; }
    public string Title { get; set; }
    public Dictionary<string, List<string>> Errors { get; set; }

    public ErrorResponseJSON(string TraceId, string Title, Dictionary<string, List<string>> Errors)
    {
        this.TraceId = TraceId;
        this.Title = Title;
        this.Errors = Errors;
    }

}
