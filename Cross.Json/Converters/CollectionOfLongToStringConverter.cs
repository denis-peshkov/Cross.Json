﻿namespace Cross.Json.Converters;

public class CollectionOfLongToStringConverter : JsonConverter<IReadOnlyCollection<string>>
{
    private static readonly JsonSerializerOptions _jsonOptions = new() { Converters = { new LongToStringConverter() } };

    public override IReadOnlyCollection<string> Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Null:
                return Array.Empty<string>();
            case JsonTokenType.StartArray:
                var list = new List<string>();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                        break;

                    list.Add(JsonSerializer.Deserialize<string>(ref reader, _jsonOptions));
                }
                return list;
            default:
                return JsonSerializer.Deserialize<IReadOnlyCollection<string>>(ref reader, options);
        }
    }

    public override void Write(Utf8JsonWriter writer, IReadOnlyCollection<string> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (var item in value)
        {
            writer.WriteStringValue(item);
        }

        writer.WriteEndArray();
    }

}
