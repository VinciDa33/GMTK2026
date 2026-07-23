using Godot;
using Godot.Collections;

namespace GodotGMTK2026.Scripts.Asteroids;

public partial class Asteroid : RigidBody3D
{
	[Export] public Array<Mesh> AsteroidMeshes = new();
	[Export] public MeshInstance3D MeshInstance;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MeshInstance.Mesh = AsteroidMeshes[(int)GD.RandRange(0, AsteroidMeshes.Count - 1)];

		var rand = new RandomNumberGenerator();

		LinearVelocity = new Vector3(rand.RandfRange(-1, 1), 0, rand.RandfRange(-1, 1));
		AngularVelocity = new Vector3(rand.RandfRange(-1, 1), rand.RandfRange(-1, 1), rand.RandfRange(-1, 1));
	}
}