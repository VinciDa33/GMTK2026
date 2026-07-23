using Godot;

namespace GodotGMTK2026.Scripts.SceneLoading;

public partial class SceneLoader : Node
{
	public static SceneLoader Instance { get; private set; }

	[Export] private SceneTransition _transition;

	private PackedScene _sceneToLoad;
	private string _pathToLoad;
	private bool _isLoading = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
		if (_transition != null)
			_transition.OnLoadReady += LoadQueued; //Subscribe to the transition event to know when to do the scene swapping
	}

	public override void _ExitTree()
	{
		//This should never happen, but just to be sure!
		if (_transition != null)
			_transition.OnLoadReady -= LoadQueued;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void LoadSceneWithTransition(PackedScene sceneToLoad)
	{
		if (_isLoading)
			return;
		
		_pathToLoad = null;
		_sceneToLoad = sceneToLoad;
		_transition.StartTransition();
		_isLoading = true;
	}

	public void LoadSceneWithTransition(string path) //Path example: "res://Scenes/Game.tscn";
	{
		if (_isLoading)
			return;
		
		_sceneToLoad = null;
		_pathToLoad = path;
		_transition.StartTransition();
		_isLoading = true;
	}

	public void LoadSceneImmediate(PackedScene sceneToLoad)
	{
		Error err = GetTree().ChangeSceneToPacked(sceneToLoad);
		if (err != Error.Ok)
			GD.PrintErr($"Failed to change scene! : {err}");
	}

	public void LoadSceneImmediate(string path) //Path example: "res://Scenes/Game.tscn";
	{
		Error err = GetTree().ChangeSceneToFile(path);
		if (err != Error.Ok)
			GD.PrintErr($"Failed to change scene! : {err}");
	}

	private void LoadQueued()
	{
		if (_sceneToLoad != null)
		{
			LoadSceneImmediate(_sceneToLoad);
			_isLoading = false;
			return;
		}

		if (_pathToLoad != null)
		{
			LoadSceneImmediate(_pathToLoad);
			_isLoading = false;
			return;
		}
		
		GD.PrintErr("No scene was queued for loading!");
		_isLoading = false;
	}
	
	public void SetSceneTransition(SceneTransition transition)
	{
		_transition = transition;
	}

	public SceneTransition GetSceneTransition()
	{
		return _transition; //Other classes may be interested in the event declaring the transition to be finished
	}
}