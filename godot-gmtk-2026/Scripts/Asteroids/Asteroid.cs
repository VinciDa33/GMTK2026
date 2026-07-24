using Godot;
using Godot.Collections;

namespace GodotGMTK2026.Scripts.Asteroids;

public partial class Asteroid : RigidBody3D
{
	[Export] public Array<Mesh> AsteroidMeshes = new();
	[Export] public Array<Material> AsteroidMaterials = new();
	[Export] public MeshInstance3D MeshInstance;
	[Export] public CollisionShape3D CollisionShape;
	[Export] public float MaxScaleFactor = 5f; // e.g 2 means 2x scale, 3 means 3x scale, etc.

	private const float STANDARD_SCALE = 35f; // Standard scale for asteroids, used to calculate random scale factor -- no touchy please

	private RandomNumberGenerator rng = new RandomNumberGenerator();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var random = rng.RandiRange(0, AsteroidMeshes.Count - 1);

		MeshInstance.Mesh = AsteroidMeshes[random];
        MeshInstance.MaterialOverride = AsteroidMaterials[random];

		float randomScale = rng.RandfRange(STANDARD_SCALE, STANDARD_SCALE * MaxScaleFactor);
		MeshInstance.Scale = new Vector3(randomScale, randomScale, randomScale);
		CollisionShape.Scale = new Vector3(randomScale / STANDARD_SCALE, randomScale / STANDARD_SCALE, randomScale / STANDARD_SCALE);

		LinearVelocity = new Vector3(rng.RandfRange(-1, 1), 0, rng.RandfRange(-1, 1));
		AngularVelocity = new Vector3(rng.RandfRange(-1, 1), rng.RandfRange(-1, 1), rng.RandfRange(-1, 1));
	}
}