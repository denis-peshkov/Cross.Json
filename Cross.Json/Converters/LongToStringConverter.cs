namespace Cross.Json.Converters;

public class LongToStringConverter : JsonConverter<string>
{
    public override string Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.Number)
            return reader.GetString();

        var span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;

        if (Utf8Parser.TryParse(span, out long number, out var bytesConsumed) && span.Length == bytesConsumed)
            return number.ToString();

        var data = reader.GetString();

        throw new InvalidOperationException($"'{data}' is not a correct expected value") { Source = "NumericToStringConverter" };
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options) =>
        // write the string value as is
        writer.WriteStringValue(value);
}
