using Godot;
using GodotGMTK2026.Scripts.SceneLoading;

namespace GodotGMTK2026.Scripts.MainMenu;

public partial class MainMenuController : Node
{
	//Called by Signal!
	private void StartClicked()
	{
		SceneLoader.Instance.LoadSceneWithTransition("res://Scenes/Scenes/game_world.tscn");
	}

	//Called by Signal!
	private void SettingsClicked()
	{
		SceneLoader.Instance.LoadSceneWithTransition("res://Scenes/Scenes/settings.tscn");
	}

	//Called by Signal!
	private void QuitClicked()
	{
		GetTree().Quit();
	}
}