using System.Diagnostics.CodeAnalysis;

#pragma warning disable 8618
namespace Application.Common.Configurations
{
    /// <summary>
    /// To manage how JWT token can be generated in the system
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class JwtConfiguration
    {
        /// <summary>
        /// Token audience
        /// </summary>
        // ReSharper disable UnusedAutoPropertyAccessor.Global

        public string Audience { get; set; } = "an.com.vn";

        /// <summary>
        /// Token issuer
        /// </summary>
        public string Issuer { get; set; } = "an.com.vn";

        /// <summary>
        /// Secret key to encrypt JWT token
        /// </summary>
        public string SymmetricSecurityKey { get; set; } = "ymF#R80S6;XHg.[*g9E+O>-Y%qzR&8sw<z5M$L#J0R*gr3Ud#7A=>7w:^EUf_=,";

        /// <summary>
        /// Expiration time (by Hour)
        /// </summary>
        public int Expires { get; set; } = 240;

        /// <summary>
        /// Refresh Token expiration time (by Hour)
        /// </summary>
        public int RefreshTokenExpires { get; set; } = 240;
    }
}