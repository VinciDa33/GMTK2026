using Godot;
using GodotGMTK2026.Scripts.Items.Inventory;

namespace GodotGMTK2026.Scripts.GameState;

public partial class GameState : Node
{
    public Inventory PlayerInventory;
    
    public static GameState Instance { get; private set; }

    public override void _Ready()
    {
        PlayerInventory = new Inventory(5);
        Instance = this;
    }
}