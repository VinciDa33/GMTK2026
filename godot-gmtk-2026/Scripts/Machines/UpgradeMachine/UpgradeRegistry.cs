using System.Collections.Generic;
using GodotGMTK2026.Scripts.Machines.UpgradeMachine.UpgradeImplementations;

namespace GodotGMTK2026.Scripts.Machines.UpgradeMachine;

public static class UpgradeRegistry
{
    private static readonly List<UpgradeEffect> UpgradeEffects = new List<UpgradeEffect>();
    
    static UpgradeRegistry()
    {
        //TODO: Instantiate upgrade effects here!
        UpgradeEffects.Add(new BiggerTank());
        UpgradeEffects.Add(new Rebreather());
        UpgradeEffects.Add(new OxygenScrubber());
        UpgradeEffects.Add(new MegaTank());
        UpgradeEffects.Add(new GigamongusTank());
        UpgradeEffects.Add(new BetterDampening());
        UpgradeEffects.Add(new FasterRefinery());
        UpgradeEffects.Add(new EfficientThrusters());
        UpgradeEffects.Add(new PowerfulThrusters());
    }

    public static UpgradeEffect GetUpgradeEffect(string id)
    {
        foreach (UpgradeEffect effect in UpgradeEffects)
            if (effect.Id.Equals(id))
                return effect;
        return null;
    } 
}