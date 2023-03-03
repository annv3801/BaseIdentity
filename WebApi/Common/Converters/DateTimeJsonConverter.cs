using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApi.Common.Converters
{
    /// <summary>
    /// To format the datetime format when serialize or deserialize json data
    /// </summary>
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        /// <inheritdoc />
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Debug.Assert(typeToConvert == typeof(DateTime));
            //return DateTime.Parse(reader.GetString() ?? throw new InvalidOperationException());
            try
            {
                return DateTime.ParseExact(
                    reader.GetString() ?? throw new InvalidOperationException(),
                    Application.Common.Constants.JsonDateTimeFormat,
                    null,
                    DateTimeStyles.AdjustToUniversal
                );
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(InvalidOperationException))
                    throw;
                Console.WriteLine(e);
                throw new JsonException($"Datetime format is not correct, use {Application.Common.Constants.JsonDateTimeFormat.Replace("'", "")} format");
            }
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Application.Common.Constants.JsonDateTimeFormat));
        }
    }
}