using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class AsteroidSpawner : Node
{
	[Export] public Camera3D PlayerCamera;
	[Export] public float SpawnDistanceMinimum = 30f;
	[Export] public float SpawnDistanceMaximum = 40;
	[Export] public float SpawnBuffer = 5f;
	[Export] public float DespawnDistance = 60f;
	[Export] public int MaxActiveAsteroids = 20;
	[Export] public PackedScene AsteroidScene; // Your asteroid scene

	private List<Node3D> _activeAsteroids = new List<Node3D>();

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector3 spawnPosition = GetSpawnPosition();
		SpawnAsteroidsInBuffer(spawnPosition);
		DespawnDistantAsteroids();
	}

	public Vector3 GetSpawnPosition()
    {
		var rand = new RandomNumberGenerator();

        if (PlayerCamera == null || AsteroidScene == null) 
		{
			GD.PrintErr("PlayerCamera or AsteroidScene is not assigned.");
			return Vector3.Zero;
		}

        float distance = rand.RandfRange(SpawnDistanceMinimum, SpawnDistanceMaximum);

        // Random angle in radians (0 to 2π)
        float angle = (float)rand.RandfRange(0, Mathf.Tau);

        // Calculate X and Z using polar coordinates
        float x = PlayerCamera.GlobalPosition.X + distance * Mathf.Cos(angle);
        float z = PlayerCamera.GlobalPosition.Z + distance * Mathf.Sin(angle);

        Vector3 spawnPosition = new Vector3(x, 0, z);
        return spawnPosition;
    }

	private void SpawnAsteroidsInBuffer(Vector3 spawnPosition)
	{
		if (_activeAsteroids.Count >= MaxActiveAsteroids)
		{
			return;
		}

		Node3D newObject = AsteroidScene.Instantiate<Node3D>();
        AddChild(newObject);
        newObject.GlobalPosition = spawnPosition;

		_activeAsteroids.Add(newObject);
	}

	private void DespawnDistantAsteroids()
    {
        if (PlayerCamera == null) return;

        for (int i = _activeAsteroids.Count - 1; i >= 0; i--)
        {
            Node3D asteroid = _activeAsteroids[i];
            if (asteroid == null) continue;

            // Calculate distance on XZ plane (ignoring Y)
            float distance = asteroid.GlobalPosition.DistanceTo(PlayerCamera.GlobalPosition);
            distance = Mathf.Sqrt(distance * distance - (asteroid.GlobalPosition.Y - PlayerCamera.GlobalPosition.Y) * (asteroid.GlobalPosition.Y - PlayerCamera.GlobalPosition.Y));

            if (distance > DespawnDistance)
            {
                asteroid.QueueFree(); // Remove from scene
                _activeAsteroids.RemoveAt(i);
            }
        }
    }
}
