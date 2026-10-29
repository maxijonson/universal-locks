using Newtonsoft.Json;
using UnityEngine;
using UniversalLocksPlugin.Converters;

namespace UniversalLocksPlugin.Config;

public class LockConfig
{
    [JsonConverter(typeof(Vector3Converter))]
    [JsonProperty(PropertyName = "Position")]
    public Vector3 Position { get; set; } = Vector3.zero;

    [JsonConverter(typeof(QuaternionConverter))]
    [JsonProperty(PropertyName = "Rotation")]
    public Quaternion Rotation { get; set; } = Quaternion.identity;
}
