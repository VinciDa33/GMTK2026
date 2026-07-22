using Godot;

namespace GodotGMTK2026.Scripts.Items;

public partial class ItemRegistry : Node
{
    public static ItemRegistry Instance { get; private set; }
    [Export] private ItemResource[] _itemResources;
    
    public override void _Ready()
    {
        Instance = this;
    }

    public ItemResource GetItemResource(ItemEnum identifier)
    {
        foreach (ItemResource itemRes in _itemResources)
            if (itemRes.Identifier == identifier)
                return itemRes;
        return null;
    }

    public Item GetItem(ItemEnum identifier)
    {
        foreach (ItemResource itemRes in _itemResources)
            if (itemRes.Identifier == identifier)
                return new Item(itemRes);
        return null;
    }
}