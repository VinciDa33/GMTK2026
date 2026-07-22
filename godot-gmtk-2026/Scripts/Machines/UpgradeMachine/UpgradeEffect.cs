namespace GodotGMTK2026.Scripts.Machines.UpgradeMachine;

public abstract class UpgradeEffect
{
    public string Id { get; private set; }

    public UpgradeEffect(string id)
    {
        Id = id;
    }
    public abstract void ExecuteUpgrade();
}