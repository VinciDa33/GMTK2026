using Godot;

namespace GodotGMTK2026.Scripts.Player;

public partial class CameraFollower : Camera3D
{
	[Export] public CharacterBody3D Target;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Target != null)
		{
			Position = new Vector3(Target.Position.X, this.Position.Y, Target.Position.Z);
		}
	}
}