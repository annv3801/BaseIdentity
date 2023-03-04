using System.Diagnostics.CodeAnalysis;

namespace WebApi.Common;
[ExcludeFromCodeCoverage]
public class Role
{
    public static string SuperAdmin = "Super Admin"; // Super Admin who have full power in the application
    public static string Anonymous = "Anonymous"; // Anonymous user
}
