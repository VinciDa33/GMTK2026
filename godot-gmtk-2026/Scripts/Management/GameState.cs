using Godot;
using GodotGMTK2026.Scripts.Items.Inventory;

namespace GodotGMTK2026.Scripts.Management;

public partial class GameState : Node
{
    [ExportGroup("Initial Player Stats")] 
    [Export] private int _oxygenCapacity;
    [Export] private float _oxygenEfficiency; 
    [Export] private float _thrusterEfficiency;
    [Export] private float _thrusterPower;
    
    public Inventory PlayerInventory { get; private set; }
    public PlayerStats PlayerStats { get; private set; }
    
    public static GameState Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
        PlayerInventory = new Inventory(5);
        PlayerStats = new PlayerStats(_oxygenCapacity, _oxygenEfficiency, _thrusterPower, _thrusterEfficiency);
    }

    public override void _ExitTree()
    {
        Instance = null;
    }
}