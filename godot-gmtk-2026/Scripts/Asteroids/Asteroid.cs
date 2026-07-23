using Godot;

namespace GodotGMTK2026.Scripts.Asteroids;

public partial class Asteroid : RigidBody3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var rand = new RandomNumberGenerator();

		LinearVelocity = new Vector3(rand.RandfRange(-1, 1), 0, rand.RandfRange(-1, 1));
		AngularVelocity = new Vector3(rand.RandfRange(-1, 1), rand.RandfRange(-1, 1), rand.RandfRange(-1, 1));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}