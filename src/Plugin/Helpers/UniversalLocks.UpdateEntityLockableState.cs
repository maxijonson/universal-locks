namespace UniversalLocksPlugin.Plugin;

public partial class UniversalLocks
{
    private void UpdateEntityLockableState(BaseNetworkable entity)
    {
        var storageContainer = entity as StorageContainer;
        var ioContainer = entity as ContainerIOEntity;
        BaseEntity? container = (BaseEntity?)storageContainer ?? ioContainer;
        if (container == null)
            return;

        // If the entity is not owned by a player, we don't want to make it lockable
        if (container.OwnerID == 0)
            return;

        var shortName = container.ShortPrefabName;
        var lockable = Configuration.LockAll
            ? !Configuration.ExcludeShortNames.Contains(shortName) // When LockAll, only lock if the short name is not in the exclude list
            : Configuration.IncludeShortNames.Contains(shortName); // When not LockAll, only lock if the short name is in the include list

        if (storageContainer != null)
            storageContainer.isLockable = lockable;
        else
            ioContainer!.isLockable = lockable;
        container.SendNetworkUpdate();
    }
}
