using GodotGMTK2026.Scripts.Management;

namespace GodotGMTK2026.Scripts.Machines.UpgradeMachine.UpgradeImplementations;

public class PowerfulThrusters() : UpgradeEffect("PowerfulThrusters")
{
    private readonly float _powerIncrease = 5f;
    
    public override void ExecuteUpgrade()
    {
        GameState.Instance.PlayerStats.SetThrusterPower(GameState.Instance.PlayerStats.ThrusterPower + _powerIncrease);
    }
}