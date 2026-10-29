using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Core;
using UnityEngine;

namespace UniversalLocksPlugin.Config;

public class PluginConfig
{
    [JsonProperty(PropertyName = "Lock all storage containers")]
    public bool LockAll { get; set; } = true;

    [JsonProperty(PropertyName = "Exclude short names (When Lock all storage containers is enabled)")]
    public HashSet<string> ExcludeShortNames { get; set; } =
        new HashSet<string> { "autoturret_deployed", "guntrap.deployed" };

    [JsonProperty(PropertyName = "Include short names (When Lock all storage containers is disabled)")]
    public HashSet<string> IncludeShortNames { get; set; } = new HashSet<string> { };

    [JsonProperty(PropertyName = "Lock configuration by short name")]
    public Dictionary<string, LockConfig> LockConfiguration { get; set; } =
        new Dictionary<string, LockConfig>
        {
            {
                "composter",
                new LockConfig { Position = new Vector3(0f, 1.3f, 0.6f), Rotation = Quaternion.Euler(0f, 90f, 0f) }
            },
            {
                "dropbox.deployed",
                new LockConfig { Position = Vector3.zero, Rotation = Quaternion.identity }
            },
            {
                "vendingmachine.deployed",
                new LockConfig { Position = Vector3.zero, Rotation = Quaternion.identity }
            },
            {
                "furnace",
                new LockConfig { Position = new Vector3(-0.02f, 0.3f, 0.5f), Rotation = Quaternion.Euler(0f, 90f, 0f) }
            },
            {
                "furnace.large",
                new LockConfig
                {
                    Position = new Vector3(0.65f, 1.25f, -0.65f),
                    Rotation = Quaternion.Euler(0f, 45f, 0f),
                }
            },
            {
                "refinery_small_deployed",
                new LockConfig
                {
                    Position = new Vector3(-0.01f, 1.25f, -0.6f),
                    Rotation = Quaternion.Euler(0f, 90f, 0f),
                }
            },
            {
                "bbq.deployed",
                new LockConfig { Position = new Vector3(0.3f, 0.75f, 0f), Rotation = Quaternion.Euler(0f, 0f, 0f) }
            },
            {
                "planter.small.deployed",
                new LockConfig { Position = new Vector3(0f, 0.45f, 0f), Rotation = Quaternion.Euler(0f, 0f, 90f) }
            },
            {
                "planter.large.deployed",
                new LockConfig { Position = new Vector3(0f, 0.45f, 0f), Rotation = Quaternion.Euler(0f, 0f, 90f) }
            },
            {
                "hitchtrough.deployed",
                new LockConfig { Position = new Vector3(0f, 0.4f, 0.3f), Rotation = Quaternion.Euler(0f, 90f, 0f) }
            },
            {
                "mixingtable.deployed",
                new LockConfig { Position = new Vector3(0, 0.8f, 0.375f), Rotation = Quaternion.Euler(0f, 90f, 0f) }
            },
            {
                "researchtable_deployed",
                new LockConfig { Position = new Vector3(0, 0.8f, 0.375f), Rotation = Quaternion.Euler(0f, 90f, 0f) }
            },
        };

    public VersionNumber Version { get; set; }
}
