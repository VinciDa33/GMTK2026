using System.Collections.Generic;
using Godot;
using GodotGMTK2026.Scripts.Machines.UpgradeMachine.UI;

namespace GodotGMTK2026.Scripts.Machines.UpgradeMachine;

public partial class UpgradeStation : Node3D
{
    public static UpgradeStation Instance { get; private set; }

    [ExportGroup("UI")] 
    [Export] private Control _upgradeStationUI;
    [Export] private Control _availableUpgradesUI;
    [Export] private Control _boughtUpgradesUI;
    [Export] private PackedScene _upgradeUI;
    [Export] private PackedScene _purchasedUpgradeUI;
    
    [ExportGroup("Upgrades")]
    [Export] private Upgrade[] _upgrades;
    private readonly List<Upgrade> _availableUpgrades = new List<Upgrade>();
    private readonly List<Upgrade> _boughtUpgrades = new List<Upgrade>();

    public override void _Ready()
    {
        Instance = this;
        
        _availableUpgrades.AddRange(_upgrades);
        foreach (Upgrade upgrade in _availableUpgrades)
        {
            UpgradeUI u = _upgradeUI.Instantiate() as UpgradeUI;
            if (u == null)
                continue;
            
            u.SetUpgrade(upgrade);
            _availableUpgradesUI.AddChild(u);
        }
    }

    public override void _ExitTree()
    {
        Instance = null;
    }

    public bool BuyUpgrade(Upgrade upgrade)
    {
        //TODO: Check cost against available resources. Return false if purchase fails
        if (true)
        {
            _availableUpgrades.Remove(upgrade);
            _boughtUpgrades.Add(upgrade);

            if (_purchasedUpgradeUI.Instantiate() is PurchasedUpgradeUI pu)
            {
                pu.SetUpgrade(upgrade);
                _boughtUpgradesUI.AddChild(pu);
            }

            UpgradeEffect effect = UpgradeRegistry.GetUpgradeEffect(upgrade.Id);
            if (effect == null)
                GD.PrintErr($"Upgrade id [{upgrade.Id}] did not match any effect in registry!");

            return true;
        }
    }
}