using Godot;
using GodotGMTK2026.Scripts.Items;
using GodotGMTK2026.Scripts.Management;

namespace GodotGMTK2026.Scripts.Machines.UpgradeMachine.UI;

public partial class UpgradeUI : Node
{
    public Upgrade Upgrade { get; private set; }
    
    [ExportGroup("UI")]
    [Export] private Label _nameLabel;
    [Export] private RichTextLabel _descriptionLabel;
    [Export] private RichTextLabel _cost;

    [ExportGroup("Color")] 
    [Export] private Color _availabelColor;
    [Export] private Color _unavailableColor;
    public override void _Ready()
    {
        if (Upgrade != null)
            SetupLabels();

        GameState.Instance.StationInventory.OnItemAdded += UpdateLabels;
        GameState.Instance.StationInventory.OnItemRemoved += UpdateLabels;
    }

    public override void _ExitTree()
    {
        GameState.Instance.StationInventory.OnItemAdded -= UpdateLabels;
        GameState.Instance.StationInventory.OnItemRemoved -= UpdateLabels;
    }

    public void SetUpgrade(Upgrade upgrade)
    {
        Upgrade = upgrade;
        if (IsNodeReady())
            SetupLabels();
    }

    public void UpdateLabels(Item item)
    {
        SetupLabels();
    }

    private void SetupLabels()
    {
        _nameLabel.Text = Upgrade.Name;
        _descriptionLabel.Text = Upgrade.Description;
        string cost = "";
        for (int i = 0; i < Upgrade.Cost.Length; i++)
        {
            int amountAvailable = GameState.Instance.StationInventory.GetAllOfType(Upgrade.Cost[i].ItemType).Count;
            Color currentColor = amountAvailable >= Upgrade.Cost[i].Amount ? _availabelColor : _unavailableColor;
            
            cost += $"[color={currentColor.ToHtml()}]" + Upgrade.Cost[i].Amount + " " + Upgrade.Cost[i].ItemType + "[/color]";
            if (i < Upgrade.Cost.Length - 1)
                cost += ", ";
        }
        _cost.Text = cost;
    }

    public void Purchase()
    {
        if (UpgradeStation.Instance.BuyUpgrade(Upgrade))
            QueueFree();
    }
}