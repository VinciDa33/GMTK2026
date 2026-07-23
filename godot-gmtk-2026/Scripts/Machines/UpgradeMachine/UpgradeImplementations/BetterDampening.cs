using GodotGMTK2026.Scripts.Management;

namespace GodotGMTK2026.Scripts.Machines.UpgradeMachine.UpgradeImplementations;

public class BetterDampening() : UpgradeEffect("BetterDampening")
{
    private readonly float _betterDampening = 0.9f;
    
    public override void ExecuteUpgrade()
    {
        GameState.Instance.PlayerStats.SetDampingFactor(_betterDampening);
    }
}