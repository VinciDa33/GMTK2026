using System.Collections.Generic;
using Godot;
using GodotGMTK2026.Scripts.Items;
using GodotGMTK2026.Scripts.Management;

namespace GodotGMTK2026.Scripts.Machines.UpgradeMachine.UI;

public partial class UpgradeStationUI : Control
{
    [ExportGroup("UI")] 
    [Export] private Control _availableUpgradesUI;
    [Export] private Control _boughtUpgradesUI;
    [Export] private Control _resourceDisplayHost;
    [Export] private PackedScene _upgradeUI;
    [Export] private PackedScene _purchasedUpgradeUI;
    [Export] private PackedScene _resourceDisplay;

    private List<ResourceDisplay> _resourceDisplays = new List<ResourceDisplay>();
    private ItemEnum[] _usableItems = [ItemEnum.Iron, ItemEnum.Copper];

    public override void _Ready()
    {
        if (GameState.Instance == null)
            return;
        
        GameState.Instance.StationInventory.OnItemAdded += ItemAdded;
        GameState.Instance.StationInventory.OnItemRemoved += ItemRemoved;
    }

    public override void _ExitTree()
    {
        if (GameState.Instance == null)
            return;
        
        GameState.Instance.StationInventory.OnItemAdded -= ItemAdded;
        GameState.Instance.StationInventory.OnItemRemoved -= ItemRemoved;
    }

    private void ItemAdded(Item item)
    {
        //If display already exists
        foreach (ResourceDisplay resourceDisplay in _resourceDisplays)
            if (resourceDisplay.ItemResource.Identifier == item.Type)
            {
                resourceDisplay.SetAmount(GameState.Instance.StationInventory.GetAllOfType(item.Type).Count);
                return;
            }
        
        //Else create a new display
        ResourceDisplay newDisplay = _resourceDisplay.Instantiate() as ResourceDisplay;
        if (newDisplay == null)
            return;
        newDisplay.SetItemResource(item.ItemResource);
        newDisplay.SetAmount(GameState.Instance.StationInventory.GetAllOfType(item.Type).Count);
        _resourceDisplayHost.AddChild(newDisplay);
        _resourceDisplays.Add(newDisplay);
    }

    private void ItemRemoved(Item item)
    {
        foreach (ResourceDisplay resourceDisplay in _resourceDisplays)
            if (resourceDisplay.ItemResource.Identifier == item.Type)
            {
                if (GameState.Instance.StationInventory.GetAllOfType(item.Type).Count == 0)
                {
                    _resourceDisplays.Remove(resourceDisplay);
                    resourceDisplay.QueueFree();
                }
                else
                    resourceDisplay.SetAmount(GameState.Instance.StationInventory.GetAllOfType(item.Type).Count);
                return;
            }
    }
    
    public void PopulateUpgrades()
    {
        foreach (Upgrade upgrade in UpgradeStation.Instance.AvailableUpgrades)
        {
            UpgradeUI u = _upgradeUI.Instantiate() as UpgradeUI;
            if (u == null)
                continue;
            
            u.SetUpgrade(upgrade);
            _availableUpgradesUI.AddChild(u);
        }
    }

    public void AddBoughtUpgrade(Upgrade upgrade)
    {
        if (_purchasedUpgradeUI.Instantiate() is not PurchasedUpgradeUI pu) return;
        pu.SetUpgrade(upgrade);
        _boughtUpgradesUI.AddChild(pu);
    }

    public void SetVisibility(bool visibility)
    {
        SetVisible(visibility);
        MouseFilter = visibility ? MouseFilterEnum.Stop : MouseFilterEnum.Ignore;
    }
}