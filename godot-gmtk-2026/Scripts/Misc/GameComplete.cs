using Godot;
using GodotGMTK2026.Scripts.Management;
using GodotGMTK2026.Scripts.Player;

namespace GodotGMTK2026.Scripts.Misc;

public partial class GameComplete : Node
{
    [Export] private Control _UI;

    public override void _Ready()
    {
        _UI.SetVisible(false);
        _UI.MouseFilter = Control.MouseFilterEnum.Ignore;
    }

    public void PlayerEnteredArea(Node3D body)
    {
        if (body is not PlayerController)
            return;
        
        GameState.Instance.PlayerController.SetStopConsumption(true);
        
        _UI.MouseFilter = Control.MouseFilterEnum.Stop;
        _UI.SetVisible(true);
    }

    public void Quit()
    {
        GetTree().Quit();
    }
}