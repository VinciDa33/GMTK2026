using Godot;

namespace GodotGMTK2026.Scripts.Items;

public class Item
{
    public string Name => ItemResource.Name;
    public Texture2D Sprite => ItemResource.Sprite;
    public ItemEnum Type => ItemResource.Identifier;
    
    public readonly ItemResource ItemResource;

    public Item(ItemResource item)
    {
        ItemResource = item;
    }
}