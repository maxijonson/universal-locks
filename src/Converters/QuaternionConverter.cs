using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace UniversalLocksPlugin.Converters;

public class QuaternionConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Quaternion);
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

        return Quaternion.Euler(x, y, z);
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        Quaternion quaternion = (Quaternion)value!;
        var euler = quaternion.eulerAngles;

        JObject jObject = new JObject
        {
            { "x", MathF.Round(euler.x, 2) },
            { "y", MathF.Round(euler.y, 2) },
            { "z", MathF.Round(euler.z, 2) },
        };
        jObject.WriteTo(writer);
    }
}
