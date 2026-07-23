using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class AsteroidSpawner : Node
{
	[Export] public Camera3D PlayerCamera;
	[Export] public float SpawnDistanceMinimum = 10f;
	[Export] public float SpawnDistanceMaximum = 15f;
	[Export] public float SpawnBuffer = 5f;
	[Export] public float DespawnDistance = 20f;
	[Export] public int MaxActiveUnmineableAsteroids = 30;
	[Export] public int MaxActiveMineableAsteroids = 5;
	[Export] public int MaxActiveScrap = 5;
	[Export] public PackedScene UnMineableAsteroidScene;
	[Export] public PackedScene MineableAsteroidScene;
	[Export] public PackedScene ScrapScene;

	private List<Node3D> _activeUnmineableAsteroids = new List<Node3D>();
	private List<Node3D> _activeMineableAsteroids = new List<Node3D>();
	private List<Node3D> _activeScrap = new List<Node3D>();

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector3 spawnPosition = GetSpawnPosition();

		SpawnAsteroids(spawnPosition);
		SpawnScrap(spawnPosition);

		DespawnDistantObjects(_activeUnmineableAsteroids);
		DespawnDistantObjects(_activeMineableAsteroids);
		DespawnDistantObjects(_activeScrap);
	}

	public Vector3 GetSpawnPosition()
	{
		var rand = new RandomNumberGenerator();

		if (PlayerCamera == null)
		{
			GD.PrintErr("PlayerCamera or AsteroidScene is not assigned.");
			return Vector3.Zero;
		}

		float distance = rand.RandfRange(SpawnDistanceMinimum, SpawnDistanceMaximum);

		// Random angle in radians
		float angle = (float)rand.RandfRange(0, Mathf.Tau);

		// Calculate X and Z using polar coordinates
		float x = PlayerCamera.GlobalPosition.X + distance * Mathf.Cos(angle);
		float z = PlayerCamera.GlobalPosition.Z + distance * Mathf.Sin(angle);

		Vector3 spawnPosition = new Vector3(x, 0, z);
		return spawnPosition;
	}

	private void SpawnAsteroids(Vector3 spawnPosition)
	{
		if (_activeUnmineableAsteroids.Count >= MaxActiveUnmineableAsteroids)
		{
			Node3D newObject = UnMineableAsteroidScene.Instantiate<Node3D>();
			AddChild(newObject);
			newObject.GlobalPosition = spawnPosition;

			_activeUnmineableAsteroids.Add(newObject);
		}
		else if (_activeMineableAsteroids.Count >= MaxActiveMineableAsteroids)
		{
			Node3D newObject = MineableAsteroidScene.Instantiate<Node3D>();
			AddChild(newObject);
			newObject.GlobalPosition = spawnPosition;

			_activeMineableAsteroids.Add(newObject);
		}
	}

	private void SpawnScrap(Vector3 spawnPosition)
	{
		if (_activeScrap.Count >= MaxActiveScrap)
		{
			return;
		}

		Node3D newObject = ScrapScene.Instantiate<Node3D>();
		AddChild(newObject);
		newObject.GlobalPosition = spawnPosition;

		_activeScrap.Add(newObject);
	}

	private void DespawnDistantObjects(List<Node3D> objects)
	{
		if (PlayerCamera == null) return;

		for (int i = objects.Count - 1; i >= 0; i--)
		{
			Node3D obj = objects[i];
			if (obj == null) continue;

			// Calculate XZ-plane distance
			float distanceSquared = (obj.GlobalPosition - PlayerCamera.GlobalPosition).LengthSquared();
			float yDiff = obj.GlobalPosition.Y - PlayerCamera.GlobalPosition.Y;
			float xzDistanceSquared = distanceSquared - (yDiff * yDiff);

			if (xzDistanceSquared > DespawnDistance * DespawnDistance)
			{
				obj.QueueFree();
				objects.RemoveAt(i);
			}
		}
	}
}
