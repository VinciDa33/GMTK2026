using Godot;
using System;

public partial class AnimationPlayer : Godot.AnimationPlayer
{
	[Export] public string NextScenePath = "res://scenes/scenes/main_menu.tscn";
	
	public override void _Ready()
	{
		Play("Cutscene");
		AnimationFinished += OnAnimationFinished;
	}

	private void OnAnimationFinished(StringName animName)
	{
		if (animName == "Cutscene")
		{
			GetTree().ChangeSceneToFile(NextScenePath);
		}
	}
}
