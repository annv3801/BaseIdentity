// ReSharper disable All

using System.Diagnostics.CodeAnalysis;

#pragma warning disable 8618
namespace Application.DTO.ActionLog.Requests
{
    [ExcludeFromCodeCoverage]
    public class CreateActionLogRequest
    {
        public string Action { get; set; }
        public string Message { get; set; }
        public object[] MessageParams { get; set; }
        public string ExtraInfo { get; set; }
    }
}