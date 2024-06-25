namespace Cross.Json.Converters;

public class DateTimeUtcConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (DateTime.TryParse(reader.GetString(), out var parsedDateTime))
        {
            return parsedDateTime;
        }

        throw new ArgumentException($"{nameof(DateTimeUtcConverter)}.Read() => Cannot format {reader.GetString()} to DateTime");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-ddThh:mm:ss.fffZ", CultureInfo.InvariantCulture));
    }
}
