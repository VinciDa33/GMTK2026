using System.Collections.Generic;
using Godot;
using GodotGMTK2026.Scripts.Items;
using GodotGMTK2026.Scripts.Machines.UpgradeMachine.UI;
using GodotGMTK2026.Scripts.Management;
using GodotGMTK2026.Scripts.Misc;

namespace GodotGMTK2026.Scripts.Machines.UpgradeMachine;

public partial class UpgradeStation : Machine
{
    public static UpgradeStation Instance { get; private set; }
    [Export] private InteractionPrompt _interactionPrompt;

    
    [ExportGroup("Upgrades")]
    [Export] private Upgrade[] _upgrades;

    [ExportGroup("UI Reference")] 
    [Export] private UpgradeStationUI _upgradeStationUI;
    
    public List<Upgrade> AvailableUpgrades { get; private set; } = new List<Upgrade>();
    public List<Upgrade> BoughtUpgrades { get; private set; } = new List<Upgrade>();

    public override void _Ready()
    {
        base._Ready();
        
        Instance = this;
        
        _interactionPrompt.SetupPrompt("Open Upgrades", "E");
        CloseUI();
        AvailableUpgrades.AddRange(_upgrades);
        _upgradeStationUI.PopulateUpgrades();
    }

    public override void _ExitTree()
    {
        Instance = null;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (_playerInRange && !_upgradeStationUI.IsVisible() && Input.IsActionPressed("interact"))
        {
            _upgradeStationUI.SetVisibility(true);
        }
    }

    public override void PlayerInRange(bool isInRange)
    {
        _interactionPrompt.SetVisible(isInRange);
        if (!isInRange)
            _upgradeStationUI.SetVisibility(false);
    }
    
    public bool BuyUpgrade(Upgrade upgrade)
    {
        //TODO: Check cost against available resources. Return false if purchase fails
        //Should be working
        foreach (UpgradeCost cost in upgrade.Cost)
        { 
            int amount = GameState.Instance.StationInventory.GetAllOfType(cost.ItemType).Count;
            if (amount < cost.Amount)
                return false;
        }
        
        foreach (UpgradeCost cost in upgrade.Cost)
        {
            for (int i = 0; i < cost.Amount; i++)
            {
                Item item = GameState.Instance.StationInventory.GetFirstItemOfType(cost.ItemType);
                GameState.Instance.StationInventory.RemoveItem(item);
            }
        }
        
        GD.Print($"Bought Upgrade {upgrade.Name}");
        AvailableUpgrades.Remove(upgrade);
        BoughtUpgrades.Add(upgrade);

        _upgradeStationUI.AddBoughtUpgrade(upgrade);

        UpgradeEffect effect = UpgradeRegistry.GetUpgradeEffect(upgrade.Id);
        if (effect == null)
            GD.PrintErr($"Upgrade id [{upgrade.Id}] did not match any effect in registry!");

        return true;
    }

    public void CloseUI()
    {
        _upgradeStationUI.SetVisibility(false);
    }
}