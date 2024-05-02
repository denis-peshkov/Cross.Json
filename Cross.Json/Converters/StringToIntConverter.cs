namespace Cross.Json.Converters;

public class StringToIntConverter : JsonConverter<int>
{
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                if (int.TryParse(reader.GetString(), out int intFromString))
                {
                    return intFromString;
                }
                break;

            case JsonTokenType.Number:
                if (reader.TryGetInt32(out int numberValue))
                {
                    return numberValue;
                }
                break;
        }

        throw new JsonException("Unable to convert value to int.");
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}
