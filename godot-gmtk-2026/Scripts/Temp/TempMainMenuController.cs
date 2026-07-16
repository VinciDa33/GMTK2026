using Godot;
using GodotGMTK2026.Scripts.SceneLoading;

namespace GodotGMTK2026.Scripts.Temp;

public partial class TempMainMenuController : Node
{
    public void SettingsClicked()
    {
        SceneLoader.Instance.LoadSceneWithTransition("res://Scenes/Scenes/settings.tscn");
    }
}