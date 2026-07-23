using Godot;
using GodotGMTK2026.Scripts.Items;

namespace GodotGMTK2026.Scripts.Machines.UpgradeMachine.UI;

public partial class ResourceDisplay : Node
{
    [Export] private TextureRect _textureRect;
    [Export] private Label _amountLabel;
    public ItemResource ItemResource { get; private set; }
    public int Amount { get; private set; }
    
    public override void _Ready()
    {
        if (ItemResource != null)
            _textureRect.Texture = ItemResource.Sprite;

        _amountLabel.Text = "" + Amount;
    }

    public void SetAmount(int amount)
    {
        Amount = amount;
        _amountLabel.Text = "" + Amount;
    }

    public void SetItemResource(ItemResource itemResource)
    {
        ItemResource = itemResource;
        if (ItemResource != null)
            _textureRect.Texture = ItemResource.Sprite;
    }

}