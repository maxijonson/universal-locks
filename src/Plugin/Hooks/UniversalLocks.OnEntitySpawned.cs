namespace UniversalLocksPlugin.Plugin;

public partial class UniversalLocks
{
    private void OnEntitySpawned(BaseNetworkable entity)
    {
        UpdateEntityLockableState(entity);
        UpdateEntityLockPosition(entity);
    }
}
