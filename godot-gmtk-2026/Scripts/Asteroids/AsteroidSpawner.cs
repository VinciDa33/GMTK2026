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
	[Export] public int MaxActivePickupableObjects = 10;
	[Export] public PackedScene UnMineableAsteroidScene;
	[Export] public PackedScene PickupableObjectScene;

	private List<Node3D> _activeUnmineableAsteroids = new List<Node3D>();
	private List<Node3D> _activePickupableObjects = new List<Node3D>();

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		SpawnAsteroids();
		SpawnPickupableObjects();

		DespawnDistantAsteroids();
		DespawnDistantPickupableObjects();
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

	private void SpawnAsteroids()
	{
		Vector3 spawnPosition = GetSpawnPosition();

		if (_activeUnmineableAsteroids.Count >= MaxActiveUnmineableAsteroids)
		{
			return;
		}

		Node3D newObject = UnMineableAsteroidScene.Instantiate<Node3D>();
		AddChild(newObject);
		newObject.GlobalPosition = spawnPosition;

		_activeUnmineableAsteroids.Add(newObject);
	}

	private void SpawnPickupableObjects()
	{
		Vector3 spawnPosition = GetSpawnPosition();

		if (_activePickupableObjects.Count >= MaxActivePickupableObjects)
		{
			return;
		}

		Node3D newObject = PickupableObjectScene.Instantiate<Node3D>();
		AddChild(newObject);
		newObject.GlobalPosition = spawnPosition;

		_activePickupableObjects.Add(newObject);
	}

	private void DespawnDistantAsteroids()
	{
		if (PlayerCamera == null) return;

		for (int i = _activeUnmineableAsteroids.Count - 1; i >= 0; i--)
		{
			Node3D obj = _activeUnmineableAsteroids[i];
			if (obj == null) continue;

			// Calculate XZ-plane distance
			float distanceSquared = (obj.GlobalPosition - PlayerCamera.GlobalPosition).LengthSquared();
			float yDiff = obj.GlobalPosition.Y - PlayerCamera.GlobalPosition.Y;
			float xzDistanceSquared = distanceSquared - (yDiff * yDiff);

			if (xzDistanceSquared > DespawnDistance * DespawnDistance)
			{
				obj.QueueFree();
				_activeUnmineableAsteroids.RemoveAt(i);
			}
		}
	}

	private void DespawnDistantPickupableObjects()
	{
		if (PlayerCamera == null) return;

		for (int i = _activePickupableObjects.Count - 1; i >= 0; i--)
		{
			Node3D obj = _activePickupableObjects[i];
			if (obj == null) continue;

			// Calculate XZ-plane distance
			float distanceSquared = (obj.GlobalPosition - PlayerCamera.GlobalPosition).LengthSquared();
			float yDiff = obj.GlobalPosition.Y - PlayerCamera.GlobalPosition.Y;
			float xzDistanceSquared = distanceSquared - (yDiff * yDiff);

			if (xzDistanceSquared > DespawnDistance * DespawnDistance)
			{
				obj.QueueFree();
				_activePickupableObjects.RemoveAt(i);
			}
		}
	}
}
