using System.Collections.Generic;
using Godot;
using GodotGMTK2026.Scripts.Management;

namespace GodotGMTK2026.Scripts.Items.Inventory.UI;

public partial class PlayerInventoryUI : Node
{
    [ExportGroup("Item_Display_Prefab")]
    [Export] private PackedScene _inventoryItem;

    [ExportGroup("Item_Host")]
    [Export] private Control _hostNode;

    private readonly List<InventoryItemUI> _uiItems = new List<InventoryItemUI>();
    public override void _Ready()
    {
        GameState.Instance.PlayerInventory.OnItemAdded += ItemAdded;
        GameState.Instance.PlayerInventory.OnItemRemoved += ItemRemoved;
    }

    public override void _ExitTree()
    {
        GameState.Instance.PlayerInventory.OnItemAdded -= ItemAdded;
        GameState.Instance.PlayerInventory.OnItemRemoved -= ItemRemoved;
    }

    public override void _Process(double delta)
    {
        if (Input.IsKeyPressed(Key.Space))
            GameState.Instance.PlayerInventory.AddItem(ItemRegistry.Instance.GetItem(ItemEnum.ScrapMetal));
        if (Input.IsKeyPressed(Key.S))
            GameState.Instance.PlayerInventory.RemoveItem(
                GameState.Instance.PlayerInventory.GetFirstItemOfType(ItemEnum.ScrapMetal));
    }

    private void ItemAdded(Item item)
    {
        InventoryItemUI uiItem = _inventoryItem.Instantiate() as InventoryItemUI;
        uiItem?.SetItem(item); //Make this UI item reflect the correct inventory item
        _uiItems.Add(uiItem); //Keep track of UI items
        _hostNode.AddChild(uiItem); //Add the UI item to the scene tree
    }

    private void ItemRemoved(Item item)
    {
        InventoryItemUI toRemove = null; //Storing the UI item to be removed, to not modify collection while iterating
        foreach (InventoryItemUI uiItem in _uiItems)
            if (uiItem.Item == item)
            {
                toRemove = uiItem;
                break;
            }

        _uiItems.Remove(toRemove);
        toRemove?.QueueFree();
    }
}