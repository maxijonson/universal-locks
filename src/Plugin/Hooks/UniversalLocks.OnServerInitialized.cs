namespace UniversalLocksPlugin.Plugin;

public partial class UniversalLocks
{
    private void OnServerInitialized()
    {
        foreach (var entity in BaseNetworkable.serverEntities)
        {
            UpdateEntityLockableState(entity);
        }
    }
}
