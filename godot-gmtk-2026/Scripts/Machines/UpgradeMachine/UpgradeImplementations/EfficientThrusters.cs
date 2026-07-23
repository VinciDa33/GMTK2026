using GodotGMTK2026.Scripts.Management;

namespace GodotGMTK2026.Scripts.Machines.UpgradeMachine.UpgradeImplementations;

public class EfficientThrusters() : UpgradeEffect("EfficientThrusters")
{
    private readonly float _efficiencyIncrease = 0.5f;
    
    public override void ExecuteUpgrade()
    {
        GameState.Instance.PlayerStats.SetThrusterEfficiency(GameState.Instance.PlayerStats.ThrusterEfficiency - _efficiencyIncrease);
    }
}