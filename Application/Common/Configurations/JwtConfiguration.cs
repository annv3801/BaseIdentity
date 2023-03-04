using System.Diagnostics.CodeAnalysis;

namespace Application.Common.Configurations;
[ExcludeFromCodeCoverage]
public class JwtConfiguration
{
    public string Audience { get; set; } = "an.com.vn";
    public string Issuer { get; set; } = "an.com.vn";
    public string SymmetricSecurityKey { get; set; } = "ymF#R80S6;XHg.[*g9E+O>-Y%qzR&8sw<z5M$L#J0R*gr3Ud#7A=>7w:^EUf_=,";

    public int Expires { get; set; } = 240;
    public int RefreshTokenExpires { get; set; } = 240;
}
