using Godot;

namespace GodotGMTK2026.Scripts.Machines.UpgradeMachine.UI;

public partial class UpgradeUI : Node
{
    public Upgrade Upgrade { get; private set; }
    
    [Export] private Label _nameLabel;
    [Export] private RichTextLabel _descriptionLabel;
    [Export] private RichTextLabel _cost;
    
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
        _nameLabel.Text = Upgrade.Name;
        _descriptionLabel.Text = Upgrade.Description;
        string cost = "";
        for (int i = 0; i < Upgrade.Cost.Length; i++)
        {
            //TODO: Add color based on availability
            cost += Upgrade.Cost[i].Amount + " " + Upgrade.Cost[i].ItemType;
            if (i < Upgrade.Cost.Length - 1)
                cost += ", ";
        }
    }

    public void Purchase()
    {
        if (UpgradeStation.Instance.BuyUpgrade(Upgrade))
            QueueFree();
    }
}