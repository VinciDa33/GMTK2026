using GodotGMTK2026.Scripts.Management;

namespace GodotGMTK2026.Scripts.Machines.UpgradeMachine.UpgradeImplementations;

public class MegaTank() : UpgradeEffect("MegaTank")
{
    private readonly int _oxygenCapacityIncrease = 15;

    
    public override void ExecuteUpgrade()
    {
        int currentCapacity = GameState.Instance.PlayerStats.OxygenCapacity;
        GameState.Instance.PlayerStats.SetOxygenCapacity(currentCapacity + _oxygenCapacityIncrease);
    }
}