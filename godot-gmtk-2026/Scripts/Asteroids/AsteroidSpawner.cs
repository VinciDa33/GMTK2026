using System.Collections.Generic;
using Godot;
using GodotGMTK2026.Scripts.Management;

namespace GodotGMTK2026.Scripts.Asteroids;

public partial class AsteroidSpawner : Node
{
	public static AsteroidSpawner Instance { get; private set; }
	
	[Export] public float SpawnDistanceMinimum = 10f;
	[Export] public float SpawnDistanceMaximum = 15f;
	[Export] public float SpawnBuffer = 5f;
	[Export] public float DespawnDistance = 20f;
	[Export] public int MaxActiveUnmineableAsteroids = 30;
	[Export] public int MaxActivePickupableObjects = 10;
	[Export] public PackedScene UnMineableAsteroidScene;
	[Export] public PackedScene PickupableObjectScene;

	private List<Node3D> _activeUnmineableAsteroids = new List<Node3D>();
	private List<PickupableObject> _activePickupableObjects = new List<PickupableObject>();

	public override void _Ready()
	{
		Instance = this;
	}

	public override void _ExitTree()
	{
		Instance = null;
	}

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

		if (GameState.Instance.PlayerController == null)
		{
			GD.PrintErr("Player Controller or AsteroidScene is not assigned.");
			return Vector3.Zero;
		}

		float distance = rand.RandfRange(SpawnDistanceMinimum, SpawnDistanceMaximum);

		// Random angle in radians
		float angle = (float)rand.RandfRange(0, Mathf.Tau);

		// Calculate X and Z using polar coordinates
		float x = GameState.Instance.PlayerController.GlobalPosition.X + distance * Mathf.Cos(angle);
		float z = GameState.Instance.PlayerController.GlobalPosition.Z + distance * Mathf.Sin(angle);

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

		PickupableObject newObject = PickupableObjectScene.Instantiate<PickupableObject>();
		AddChild(newObject);
		newObject.GlobalPosition = spawnPosition;

		_activePickupableObjects.Add(newObject);
	}

	private void DespawnDistantAsteroids()
	{
		if (GameState.Instance.PlayerController == null) return;

		for (int i = _activeUnmineableAsteroids.Count - 1; i >= 0; i--)
		{
			Node3D obj = _activeUnmineableAsteroids[i];
			if (obj == null) continue;

			if (GameState.Instance.PlayerController.GlobalPosition.DistanceTo(obj.GlobalPosition) > DespawnDistance)
			{
				obj.QueueFree();
				_activeUnmineableAsteroids.RemoveAt(i);
			}
		}
	}

	private void DespawnDistantPickupableObjects()
	{
		if (GameState.Instance.PlayerController == null) return;

		for (int i = 0; i < _activePickupableObjects.Count; i++)
		{
			Node3D obj = _activePickupableObjects[i];
			if (obj == null || obj.IsQueuedForDeletion()) continue;

			if (GameState.Instance.PlayerController.GlobalPosition.DistanceTo(obj.GlobalPosition) > DespawnDistance)
			{
				obj.QueueFree();
				_activePickupableObjects.RemoveAt(i);
			}
		}
	}

	public void PickupObject(PickupableObject obj)
	{
		_activePickupableObjects.Remove(obj);
		obj.QueueFree();
	}
}