using Godot;
using System;

public partial class playerGuide : Button
{
	[Export] public Control GuideNode;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnButtonPressed()
	{
		if (GuideNode.Visible)
		{
			GuideNode.Visible = false;
		}
		else
		{
			GuideNode.Visible = true;
		}
	}
}
