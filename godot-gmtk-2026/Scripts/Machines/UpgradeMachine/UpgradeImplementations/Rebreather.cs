using GodotGMTK2026.Scripts.Management;

namespace GodotGMTK2026.Scripts.Machines.UpgradeMachine.UpgradeImplementations;

public class Rebreather() : UpgradeEffect("Rebreather")
{
    private readonly float _oxygenEfficiencyIncrease = 0.2f; //20% increase from baseline
    
    public override void ExecuteUpgrade()
    {
        float currentEfficiency = GameState.Instance.PlayerStats.OxygenEfficiency;
        GameState.Instance.PlayerStats.SetOxygenEfficiency(currentEfficiency - _oxygenEfficiencyIncrease); //Lower number better
    }
}