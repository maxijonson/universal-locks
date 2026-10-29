namespace UniversalLocksPlugin.Plugin;

public partial class UniversalLocks
{
    private void UpdateEntityLockPosition(BaseNetworkable entity)
    {
        var baseLock = entity as BaseLock;
        if (baseLock == null)
            return;

        var parentEntity = baseLock.GetParentEntity();
        if (parentEntity == null)
            return;

        var shortName = parentEntity.ShortPrefabName;

        if (Configuration.LockConfiguration.ContainsKey(shortName))
        {
            var lockConfig = Configuration.LockConfiguration[shortName];
            entity.transform.localPosition = lockConfig.Position;
            entity.transform.localRotation = lockConfig.Rotation;
            entity.SendNetworkUpdate();
        }
    }
}
