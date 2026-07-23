using Godot;
using GodotGMTK2026.Scripts.Management;
using GodotGMTK2026.Scripts.Player;

namespace GodotGMTK2026.Scripts.SpacePod;

public partial class Pod : Node
{
    public void PlayerEntered(Node3D body)
    {
        if (body is not PlayerController)
            return;
        GD.Print("Player entered the pod");
        GameState.Instance.PlayerController.SetStopConsumption(true);
    }

    public void PlayerExited(Node3D body)
    {
        if (body is not PlayerController)
            return;
        GD.Print("Player left the pod");
        GameState.Instance.PlayerController.SetStopConsumption(false);
    }
}