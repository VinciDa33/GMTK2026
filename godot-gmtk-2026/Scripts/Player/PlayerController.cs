using Godot;
using GodotGMTK2026.Scripts.Management;

public partial class PlayerController : CharacterBody3D
{
    private float Acceleration = 5.0f;
    [Export] public float RotationSpeed = 5.0f;

	[Export] public float Dampening = 0.98f;
	private bool HasDampingUpgrade { get; set; } = false;

	private Vector3 _velocity = Vector3.Zero;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		UpdatePlayerValues();

		var timer = GetNode<Timer>("Timer");
		timer.Timeout += OnTimerTimeout;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		// Update player values from GameState so updated stats are reflected in the player controller
		UpdatePlayerValues();

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
			_velocity += -Transform.Basis.X * Acceleration * (float)delta;
			Velocity = _velocity;
		}
		else if (HasDampingUpgrade)
		{
			_velocity *= Dampening;
			Velocity = _velocity;
		}

		MoveAndSlide();
	}

	private void OnTimerTimeout()
	{
		CountDownOxygen();
	}

	private void UpdatePlayerValues()
	{
		HasDampingUpgrade = GameState.Instance.PlayerStats.HasDampingUpgrade;
		Acceleration = GameState.Instance.PlayerStats.ThrusterPower;
	}

	private void CountDownOxygen()
	{
		GameState.Instance.PlayerStats.SetOxygenLevel(GameState.Instance.PlayerStats.OxygenLevel - 1);

		/*if (Input.IsActionPressed("fire_jetpack"))
		{
			GameState.Instance.PlayerStats.SetOxygenLevel(GameState.Instance.PlayerStats.OxygenLevel - 1);
		}
		else
		{
			//GameState.Instance.PlayerStats.SetOxygenLevel(GameState.Instance.PlayerStats.OxygenLevel - (GameState.Instance.PlayerStats.OxygenEfficiency / 2));
		}*/
	}
}