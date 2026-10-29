using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Oxide.Core;
using Newtonsoft.Json;
using Rust;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;


namespace Oxide.Plugins
{
    [Info("Universal Locks", "MaxiJonson", "1.0.0")]
    [Description("Lock anything with a container")]
    class UniversalLocks : RustPlugin
    {
        #region Vars
        #endregion

        #region Oxide Hooks
        private void Init()
        {
            LoadConfig();
        }

        private void OnServerInitialized()
        {
            foreach (var entity in BaseNetworkable.serverEntities)
            {
                UpdateEntityLockableState(entity);
            }
        }

        private void OnEntitySpawned(BaseNetworkable entity)
        {
            UpdateEntityLockableState(entity);
            UpdateEntityLockPosition(entity);
        }
        #endregion

        #region Functions
        private void UpdateEntityLockableState(BaseNetworkable entity)
        {
            var storageContainer = entity as StorageContainer;
            var ioContainer = entity as ContainerIOEntity;
            BaseEntity container = (BaseEntity)storageContainer ?? ioContainer;
            if (container == null) return;

            // If the entity is not owned by a player, we don't want to make it lockable
            if (container.OwnerID == 0) return;

            var shortName = container.ShortPrefabName;
            var lockable = Configuration.LockAll
                    ? !Configuration.ExcludeShortNames.Contains(shortName) // When LockAll, only lock if the short name is not in the exclude list
                    : Configuration.IncludeShortNames.Contains(shortName); // When not LockAll, only lock if the short name is in the include list

            if (storageContainer != null) storageContainer.isLockable = lockable;
            else ioContainer.isLockable = lockable;
            container.SendNetworkUpdate();
        }

        private void UpdateEntityLockPosition(BaseNetworkable entity)
        {
            var baseLock = entity as BaseLock;
            if (baseLock == null) return;

            var parentEntity = baseLock.GetParentEntity();
            if (parentEntity == null) return;

            var shortName = parentEntity.ShortPrefabName;

            if (Configuration.LockConfiguration.ContainsKey(shortName))
            {
                var lockConfig = Configuration.LockConfiguration[shortName];
                entity.transform.localPosition = lockConfig.Position;
                entity.transform.localRotation = lockConfig.Rotation;
                entity.SendNetworkUpdate();
            }
        }
        #endregion

        #region Config
        private static PluginConfig Configuration;
        private class PluginConfig
        {
            [JsonProperty(PropertyName = "Lock all storage containers")]
            public bool LockAll { get; set; } = true;

            [JsonProperty(PropertyName = "Exclude short names (When Lock all storage containers is enabled)")]
            public HashSet<string> ExcludeShortNames { get; set; } = new HashSet<string> { "autoturret_deployed", "guntrap.deployed" };

            [JsonProperty(PropertyName = "Include short names (When Lock all storage containers is disabled)")]
            public HashSet<string> IncludeShortNames { get; set; } = new HashSet<string> { };

            [JsonProperty(PropertyName = "Lock configuration by short name")]
            public Dictionary<string, LockConfig> LockConfiguration { get; set; } = new Dictionary<string, LockConfig>
            {
                { "composter", new LockConfig { Position = new Vector3(0f, 1.3f, 0.6f), Rotation = Quaternion.Euler(0f, 90f, 0f) } },
                { "dropbox.deployed", new LockConfig { Position = Vector3.zero, Rotation = Quaternion.identity } },
                { "vendingmachine.deployed", new LockConfig { Position = Vector3.zero, Rotation = Quaternion.identity } },
                { "furnace", new LockConfig { Position = new Vector3(-0.02f, 0.3f, 0.5f), Rotation = Quaternion.Euler(0f, 90f, 0f) } },
                { "furnace.large", new LockConfig { Position = new Vector3(0.65f, 1.25f, -0.65f), Rotation = Quaternion.Euler(0f, 45f, 0f) } },
                { "refinery_small_deployed", new LockConfig { Position = new Vector3(-0.01f, 1.25f, -0.6f), Rotation = Quaternion.Euler(0f, 90f, 0f) } },
                { "bbq.deployed", new LockConfig { Position = new Vector3(0.3f, 0.75f, 0f), Rotation = Quaternion.Euler(0f, 0f, 0f) } },
                { "planter.small.deployed", new LockConfig { Position = new Vector3(0f, 0.45f, 0f), Rotation = Quaternion.Euler(0f, 0f, 90f) } },
                { "planter.large.deployed", new LockConfig { Position = new Vector3(0f, 0.45f, 0f), Rotation = Quaternion.Euler(0f, 0f, 90f) } },
                { "hitchtrough.deployed", new LockConfig { Position = new Vector3(0f, 0.4f, 0.3f), Rotation = Quaternion.Euler(0f, 90f, 0f) } },
                { "mixingtable.deployed", new LockConfig { Position = new Vector3(0, 0.8f, 0.375f), Rotation = Quaternion.Euler(0f, 90f, 0f) } },
                { "researchtable_deployed", new LockConfig { Position = new Vector3(0, 0.8f, 0.375f), Rotation = Quaternion.Euler(0f, 90f, 0f) } }
            };

            public class LockConfig
            {
                [JsonConverter(typeof(Vector3Converter))]
                [JsonProperty(PropertyName = "Position")]
                public Vector3 Position { get; set; } = Vector3.zero;

                [JsonConverter(typeof(QuaternionConverter))]
                [JsonProperty(PropertyName = "Rotation")]
                public Quaternion Rotation { get; set; } = Quaternion.identity;
            }

            public VersionNumber Version { get; set; }
        }

        protected override void SaveConfig()
        {
            Config.WriteObject(Configuration, true);
        }

        protected override void LoadDefaultConfig()
        {
            Configuration = new PluginConfig();
            SaveConfig();
        }

        protected override void LoadConfig()
        {
            base.LoadConfig();

            Configuration = Config.ReadObject<PluginConfig>();

            if (Configuration.Version < Version)
                UpdateConfigValues();

            SaveConfig();
        }

        private void UpdateConfigValues()
        {
            PrintWarning("Config update detected! Updating config values...");

            Configuration.Version = Version;

            PrintWarning("Config update completed!");
        }
        #endregion

        #region Converters
        private class Vector3Converter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(Vector3);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JObject jObject = JObject.Load(reader);

                float x = jObject["x"]?.Value<float>() ?? 0;
                float y = jObject["y"]?.Value<float>() ?? 0;
                float z = jObject["z"]?.Value<float>() ?? 0;

                return new Vector3(x, y, z);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                Vector3 vector3 = (Vector3)value;

                JObject jObject = new JObject
                {
                    { "x", MathF.Round(vector3.x, 2) },
                    { "y", MathF.Round(vector3.y, 2) },
                    { "z", MathF.Round(vector3.z, 2) }
                };

                jObject.WriteTo(writer);
            }
        }

        private class QuaternionConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(Quaternion);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JObject jObject = JObject.Load(reader);

                float x = jObject["x"]?.Value<float>() ?? 0;
                float y = jObject["y"]?.Value<float>() ?? 0;
                float z = jObject["z"]?.Value<float>() ?? 0;

                return Quaternion.Euler(x, y, z);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                Quaternion quaternion = (Quaternion)value;
                var euler = quaternion.eulerAngles;

                JObject jObject = new JObject
                {
                    { "x", MathF.Round(euler.x, 2) },
                    { "y", MathF.Round(euler.y, 2) },
                    { "z", MathF.Round(euler.z, 2) }
                };
                jObject.WriteTo(writer);
            }
        }
        #endregion
    }
}
