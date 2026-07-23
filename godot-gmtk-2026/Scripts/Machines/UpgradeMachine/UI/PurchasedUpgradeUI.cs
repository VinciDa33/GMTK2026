using Godot;

namespace GodotGMTK2026.Scripts.Machines.UpgradeMachine.UI;

public partial class PurchasedUpgradeUI : Node
{
    public Upgrade Upgrade { get; private set; }
    
    [Export] private RichTextLabel _nameLabel;
    [Export] private RichTextLabel _descriptionLabel;
    
    public override void _Ready()
    {
        if (Upgrade != null)
            SetupLabels();
    }

    public void SetUpgrade(Upgrade upgrade)
    {
        Upgrade = upgrade;
        if (IsNodeReady())
            SetupLabels();
    }
    
    private void SetupLabels()
    {
        _nameLabel.Text = "[color=#9cdb2e]" + Upgrade.Name + "[/color]";
        _descriptionLabel.Text = Upgrade.Description;
    }
}