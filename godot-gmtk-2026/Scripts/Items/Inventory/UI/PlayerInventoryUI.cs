using System.Collections.Generic;
using Godot;
using GodotGMTK2026.Scripts.Management;

namespace GodotGMTK2026.Scripts.Items.Inventory.UI;

public partial class PlayerInventoryUi : Node
{
    [Export] public int Shite;
    
    [ExportGroup("Item Display Prefab")]
    [Export] private PackedScene _inventoryItem;

    [ExportGroup("Item Host")] 
    [Export] private Control _hostNode;

    private readonly List<InventoryUIItem> _uiItems = new List<InventoryUIItem>();
    public override void _Ready()
    {
        GameState.Instance.PlayerInventory.OnItemAdded += ItemAdded;
        GameState.Instance.PlayerInventory.OnItemRemoved += ItemRemoved;
    }

    private void ItemAdded(Item item)
    {
        InventoryUIItem uiItem = _inventoryItem.Instantiate() as InventoryUIItem;
        uiItem?.SetItem(item);
        _uiItems.Add(uiItem);
        _hostNode.AddChild(uiItem);
    }

    private void ItemRemoved(Item item)
    {
        InventoryUIItem toRemove = null;
        foreach (InventoryUIItem uiItem in _uiItems)
            if (uiItem.Item == item)
            {
                toRemove = uiItem;
                break;
            }

        _uiItems.Remove(toRemove);
        toRemove?.QueueFree();
    }
}