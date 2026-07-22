using Godot;

namespace GodotGMTK2026.Scripts.Items.Inventory.UI;

public partial class InventoryUIItem : PanelContainer
{
    public Item Item { get; private set; }

    public void SetItem(Item item)
    {
        Item = item;
        if (IsNodeReady())
            GetNode<TextureRect>("MarginContainer/TextureRect").SetTexture(Item.Sprite);
    }

    public override void _Ready()
    {
        if (Item != null)
            GetNode<TextureRect>("MarginContainer/TextureRect").SetTexture(Item.Sprite);
    }
}