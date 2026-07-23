using GodotGMTK2026.Scripts.Management;

namespace GodotGMTK2026.Scripts.Machines.UpgradeMachine.UpgradeImplementations;

public class GigamongusTank() : UpgradeEffect("GigaTank")
{
    private readonly int _oxygenCapacityIncrease = 25;


    public override void ExecuteUpgrade()
    {
        int currentCapacity = GameState.Instance.PlayerStats.OxygenCapacity;
        GameState.Instance.PlayerStats.SetOxygenCapacity(currentCapacity + _oxygenCapacityIncrease);
    }
}