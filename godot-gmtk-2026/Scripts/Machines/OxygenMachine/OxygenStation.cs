using Godot;
using GodotGMTK2026.Scripts.Management;
using GodotGMTK2026.Scripts.Misc;

namespace GodotGMTK2026.Scripts.Machines.OxygenMachine;

public partial class OxygenStation : Machine
{
    public static OxygenStation Instance { get; private set; }
    [Export] private InteractionPrompt _interactionPrompt;
    public override void _Ready()
    {
        base._Ready();
        
        Instance = this;
        _interactionPrompt.SetupPrompt("Refill Oxygen", "E");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (_playerInRange && Input.IsActionPressed("interact"))
        {
            GD.Print("Refilled Oxygen");
            GameState.Instance.PlayerStats.SetOxygenLevel(GameState.Instance.PlayerStats.OxygenCapacity);
        }

    }

    public override void _ExitTree()
    {
        Instance = null;
    }

    public override void PlayerInRange(bool isInRange)
    {
        _interactionPrompt.SetVisible(isInRange);
    }
}