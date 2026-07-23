using GodotGMTK2026.Scripts.Management;

namespace GodotGMTK2026.Scripts.Machines.UpgradeMachine.UpgradeImplementations;

public class OxygenScrubber() : UpgradeEffect("OxygenScrubber")
{
    private readonly float _oxygenEfficiencyIncrease = 0.3f; //20% increase from baseline

    public override void ExecuteUpgrade()
    {
        float currentEfficiency = GameState.Instance.PlayerStats.OxygenEfficiency;
        GameState.Instance.PlayerStats.SetOxygenEfficiency(currentEfficiency - _oxygenEfficiencyIncrease); //Lower number better
    }
}