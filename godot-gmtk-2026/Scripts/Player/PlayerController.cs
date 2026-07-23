using Godot;
using GodotGMTK2026.Scripts.Management;

namespace GodotGMTK2026.Scripts.Player;

public partial class PlayerController : CharacterBody3D
{
	[Export] public float RotationSpeed = 5.0f;
	[ExportGroup("Oxygen Consumption")]
	[Export] private float _breathingTick;
	[Export] private float _baseBreathConsumption;
	[Export] private float _thrusterTick;
	[Export] private float _baseThrusterConsumption;
	
	private Timer _breathTimer;
	private Timer _thrustTimer;
	
	private float _oxygenTimer = 0.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameState.Instance.SetPlayerController(this);
		
		_breathTimer = new Timer();
		_breathTimer.WaitTime = _breathingTick;
		_breathTimer.OneShot = true;
		_breathTimer.Timeout += TakeBreath;
		AddChild(_breathTimer);


		_thrustTimer = new Timer();
		_thrustTimer.WaitTime = _thrusterTick;
		_thrustTimer.OneShot = true;
		_thrustTimer.Timeout += TickThrusterConsumption;
		AddChild(_thrustTimer);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (_breathTimer.IsStopped())
			_breathTimer.Start();

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
			Velocity += -Transform.Basis.X * GameState.Instance.PlayerStats.ThrusterPower * (float)delta;
			
			if (_thrustTimer.IsStopped())
				_thrustTimer.Start();
		}
		else if (Input.IsActionPressed("dampen_speed"))
		{
			Velocity *= GameState.Instance.PlayerStats.DampingFactor;
			
			if (_thrustTimer.IsStopped())
				_thrustTimer.Start();
		}

		MoveAndSlide();
	}

	private void TakeBreath()
	{
		float currentOxygenLevel = GameState.Instance.PlayerStats.OxygenLevel;
		float oxygenEffeciency = GameState.Instance.PlayerStats.OxygenEfficiency;
		GameState.Instance.PlayerStats.SetOxygenLevel(currentOxygenLevel - _baseBreathConsumption * oxygenEffeciency);
	}

	private void TickThrusterConsumption()
	{
		float currentOxygenLevel = GameState.Instance.PlayerStats.OxygenLevel;
		float thrusterEfficiency = GameState.Instance.PlayerStats.ThrusterEfficiency;
		GameState.Instance.PlayerStats.SetOxygenLevel(currentOxygenLevel - _baseThrusterConsumption * thrusterEfficiency);
	}
	
	/*
	private void CountDownOxygen(double delta)
	{
		_oxygenTimer += (float)delta;
		
		if ((Input.IsActionPressed("fire_jetpack") || Input.IsActionPressed("dampen_speed")) && _oxygenTimer >= 0.5f)
		{
			GameState.Instance.PlayerStats.SetOxygenLevel(GameState.Instance.PlayerStats.OxygenLevel - 1f * GameState.Instance.PlayerStats.ThrusterEfficiency);
			_oxygenTimer = 0.0f;
		}
		else if (_oxygenTimer >= 1.0f)
		{
			GameState.Instance.PlayerStats.SetOxygenLevel(GameState.Instance.PlayerStats.OxygenLevel - 1f * GameState.Instance.PlayerStats.OxygenEfficiency);
			_oxygenTimer = 0.0f;
		}
	}
	*/
}