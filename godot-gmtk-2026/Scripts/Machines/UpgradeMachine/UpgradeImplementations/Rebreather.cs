using GodotGMTK2026.Scripts.Management;

namespace GodotGMTK2026.Scripts.Machines.UpgradeMachine.UpgradeImplementations;

public class Rebreather() : UpgradeEffect("Rebreather")
{
    private readonly float _oxygenEfficiencyIncrease = 0.25f; //25% increase from baseline
    
    public override void ExecuteUpgrade()
    {
        float currentEfficiency = GameState.Instance.PlayerStats.OxygenEfficiency;
        GameState.Instance.PlayerStats.SetOxygenEfficiency(currentEfficiency + _oxygenEfficiencyIncrease);
    }
}