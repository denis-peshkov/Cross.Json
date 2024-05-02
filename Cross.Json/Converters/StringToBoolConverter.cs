namespace Cross.Json.Converters;

public class StringToBoolConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                if (bool.TryParse(reader.GetString(), out var fromStringValue))
                {
                    return fromStringValue;
                }
                break;

            case JsonTokenType.True:
                return true;

            case JsonTokenType.False:
                return false;
        }

        throw new JsonException("Unable to convert value to bool.");
    }

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
        writer.WriteBooleanValue(value);
    }
}
