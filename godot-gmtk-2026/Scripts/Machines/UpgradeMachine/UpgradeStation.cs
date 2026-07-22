using System.Collections.Generic;
using Godot;

namespace GodotGMTK2026.Scripts.Machines.UpgradeMachine;

public partial class UpgradeStation : Node3D
{
    public static UpgradeStation Instance { get; private set; }
    
    [Export] private Upgrade[] _upgrades;
    private List<Upgrade> _availableUpgrades = new List<Upgrade>();
    private List<Upgrade> _boughtUpgrades = new List<Upgrade>();

    public override void _Ready()
    {
        _availableUpgrades.AddRange(_upgrades);
    }

    public override void _ExitTree()
    {
        Instance = null;
    }

    public void BuyUpgrade(Upgrade upgrade)
    {
        //TODO: Check cost against available resources
        if (true)
        {
            _availableUpgrades.Remove(upgrade);
            _boughtUpgrades.Add(upgrade);

            UpgradeEffect effect = UpgradeRegistry.GetUpgradeEffect(upgrade.Id);
            if (effect == null)
                GD.PrintErr($"Upgrade id {upgrade.Id} did not match any effect in registry!");
        }
    }
}