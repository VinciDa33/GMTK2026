using Godot;
using System;

public partial class VideoStreamPlayer : Godot.VideoStreamPlayer
{
	[Export] public string NextScenePath = "res://Scenes/Scenes/intro_animation.tscn";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Play();
		Finished += OnVideoFinished;
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventKey || @event is InputEventMouseButton)
		{
			GoToNextScene();
		}
	}

	private void OnVideoFinished()
	{
		GoToNextScene();
	}

	private void GoToNextScene()
	{
		GetTree().ChangeSceneToFile(NextScenePath);
	}
}
