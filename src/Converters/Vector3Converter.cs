using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace UniversalLocksPlugin.Converters;

public class Vector3Converter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Vector3);
    }

    public override object ReadJson(
        JsonReader reader,
        Type objectType,
        object? existingValue,
        JsonSerializer serializer
    )
    {
        JObject jObject = JObject.Load(reader);

        float x = jObject["x"]?.Value<float>() ?? 0;
        float y = jObject["y"]?.Value<float>() ?? 0;
        float z = jObject["z"]?.Value<float>() ?? 0;

        return new Vector3(x, y, z);
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        Vector3 vector3 = (Vector3)value!;

        JObject jObject = new JObject
        {
            { "x", MathF.Round(vector3.x, 2) },
            { "y", MathF.Round(vector3.y, 2) },
            { "z", MathF.Round(vector3.z, 2) },
        };

        jObject.WriteTo(writer);
    }
}
