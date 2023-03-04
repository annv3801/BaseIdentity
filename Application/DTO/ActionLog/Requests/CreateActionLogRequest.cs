using System.Diagnostics.CodeAnalysis;

namespace Application.DTO.ActionLog.Requests;
[ExcludeFromCodeCoverage]
public class CreateActionLogRequest
{
    public string Action { get; set; }
    public string Message { get; set; }
    public object[] MessageParams { get; set; }
    public string ExtraInfo { get; set; }
}
