using GodotGMTK2026.Scripts.Machines.RefineryMachine;

namespace GodotGMTK2026.Scripts.Machines.UpgradeMachine.UpgradeImplementations;

public class FasterRefinery() : UpgradeEffect("FasterRefinery")
{
    private readonly float _betterRefinerySpeed = 0.5f;
    
    public override void ExecuteUpgrade()
    {
        Refinery.Instance.SetProcessingTime(_betterRefinerySpeed);
    }
}