using Godot;

namespace GodotGMTK2026.Scripts.Misc;

public partial class GameComplete : Node
{
    [Export] private Control _UI;

    public override void _Ready()
    {
        _UI.SetVisible(false);
        _UI.MouseFilter = Control.MouseFilterEnum.Ignore;
    }

    public void ShowCompletion()
    {
        _UI.SetVisible(true);
        _UI.MouseFilter = Control.MouseFilterEnum.Stop;
    }

    public void Quit()
    {
        GetTree().Quit();
    }
}