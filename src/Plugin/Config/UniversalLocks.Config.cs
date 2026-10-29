using UniversalLocksPlugin.Config;

namespace UniversalLocksPlugin.Plugin;

public partial class UniversalLocks
{
    private static PluginConfig Configuration = null!;

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
}
