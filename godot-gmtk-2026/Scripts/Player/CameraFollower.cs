using Godot;
using System;

public partial class CameraFollower : Camera3D
{
	[Export] public CharacterBody3D _target;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_target != null)
		{
			Position = new Vector3(_target.Position.X, this.Position.Y, _target.Position.Z);
		}
	}
}
