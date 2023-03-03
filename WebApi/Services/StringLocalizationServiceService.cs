using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Application.Common.Interfaces;
using Microsoft.Extensions.Localization;
using WebApi.Localization;

namespace WebApi.Services
{
    /// <summary>
    /// Implement localization service
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class StringLocalizationServiceService : IStringLocalizationService
    {
        private readonly IStringLocalizer<SharedLocalization> _localizer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="localizer"></param>
        public StringLocalizationServiceService(IStringLocalizer<SharedLocalization> localizer)
        {
            _localizer = localizer;
        }

        /// <summary>
        /// Get translation by key
        /// </summary>
        /// <param name="key"></param>
        public LocalizedString this[string key] => _localizer[key];

        /// <summary>
        /// Get translation by key and its params
        /// </summary>
        /// <param name="key"></param>
        /// <param name="args"></param>
        public LocalizedString this[string key, params object[] args] => _localizer[key, args];

        /// <summary>
        /// To get translation by key and its params under the given culture
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cultureInfo"></param>
        /// <param name="args"></param>
        public LocalizedString this[string key, CultureInfo cultureInfo, params object[] args] =>
            throw new System.NotImplementedException();
    }
}