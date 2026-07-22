using Godot;

namespace GodotGMTK2026.Scripts.Items;

public class Item
{
    public string Name => _itemResource.Name;
    public Texture Sprite => _itemResource.Sprite;
    public ItemEnum Type => _itemResource.Identifier;
    
    private readonly ItemResource _itemResource;

    public Item(ItemResource item)
    {
        _itemResource = item;
    }
}