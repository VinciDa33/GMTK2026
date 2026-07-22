using Godot;
using GodotGMTK2026.Scripts.Management;

public partial class PlayerController : CharacterBody3D
{
    [Export] public float RotationSpeed = 5.0f;
	[Export] public float DampeningFactor = 0.98f; // The closer to 1 slower the dampening effect, the closer to 0 faster the dampening effect
	
	private float _acceleration;
	private bool _hasDampingUpgrade { get; set; } = false;
	private Vector3 _velocity = Vector3.Zero;
	private float _oxygenTimer = 0.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		UpdatePlayerValues();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		// Update player values from GameState so updated stats are reflected in the player controller
		UpdatePlayerValues();

		CountDownOxygen(delta);

		if (Input.IsActionPressed("rotate_left"))
		{
			RotateY(RotationSpeed * (float)delta);
		}
		else if (Input.IsActionPressed("rotate_right"))
		{
			RotateY(-RotationSpeed * (float)delta);
		}

		if (Input.IsActionPressed("fire_jetpack"))
		{
			_velocity += -Transform.Basis.X * _acceleration * (float)delta;
			Velocity = _velocity;
		}
		else if (_hasDampingUpgrade && Input.IsActionPressed("dampen_speed"))
		{
			_velocity *= DampeningFactor;
			Velocity = _velocity;
		}

		MoveAndSlide();
	}

	private void UpdatePlayerValues()
	{
		_hasDampingUpgrade = GameState.Instance.PlayerStats.HasDampingUpgrade;
		_acceleration = GameState.Instance.PlayerStats.ThrusterPower;
	}

	private void CountDownOxygen(double delta)
	{
		_oxygenTimer += (float)delta;

		if ((Input.IsActionPressed("fire_jetpack") || Input.IsActionPressed("dampen_speed")) && _oxygenTimer >= 0.5f)
		{
			GameState.Instance.PlayerStats.SetOxygenLevel(GameState.Instance.PlayerStats.OxygenLevel - 1);
			_oxygenTimer = 0.0f;
		}
		else if (_oxygenTimer >= 1.0f)
		{
			GameState.Instance.PlayerStats.SetOxygenLevel(GameState.Instance.PlayerStats.OxygenLevel - 1);
			_oxygenTimer = 0.0f;
		}
	}
}