using Godot;
using GodotGMTK2026.Scripts.SceneLoading;

namespace GodotGMTK2026.Scripts.Misc;

public partial class PauseMenu : Node
{
    [Export] private Control _menuUI;

    public override void _Ready()
    {
        _menuUI.SetVisible(false);
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("escape"))
        {
            bool visible = !_menuUI.IsVisible();
            
            _menuUI.SetVisible(visible);
            if (visible)
                _menuUI.MouseFilter = Control.MouseFilterEnum.Stop;
            else
                _menuUI.MouseFilter = Control.MouseFilterEnum.Ignore;
        }
    }

    public void QuitGame()
    {
        SceneLoader.Instance.LoadSceneWithTransition("res://Scenes/Scenes/main_menu.tscn");
    }
}