using Godot;
using Godot.Collections;
using GodotGMTK2026.Scripts.Management;

namespace GodotGMTK2026.Scripts.Player;

public partial class PlayerController : CharacterBody3D
{
	[Export] private float _maxSpeed;
	[Export] private float PushForce = 0.3f;
	[Export] private float _rotationSpeed = 5.0f;
	[Export] private GpuParticles3D _particles;
	[ExportGroup("Oxygen Consumption")]
	[Export] private float _breathingTick;
	[Export] private float _baseBreathConsumption;
	[Export] private float _thrusterTick;
	[Export] private float _baseThrusterConsumption;
	[ExportGroup("Sounds")]
	[Export] private AudioStreamPlayer _collisionAudioPlayer;
	[Export] private Array<AudioStream> _collisionSounds;
	[Export] private float _collisionCooldownTime = 0.2f;
	[Export] private AudioStreamPlayer _thrusterAudioPlayer;
	[Export] private AudioStream _thruserSound;


	private Timer _breathTimer;
	private Timer _thrustTimer;
	private Timer _collisionSoundTimer;

	private float _oxygenTimer = 0.0f;
	private bool StopConsumption = true;
	private bool _collisionSoundCooldownActive = false;

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

		_collisionSoundTimer = new Timer();
		_collisionSoundTimer.WaitTime = _collisionCooldownTime;
		_collisionSoundTimer.OneShot = true;
		_collisionSoundTimer.Timeout += ResetCollisionSoundCooldown;
		AddChild(_collisionSoundTimer);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (_breathTimer.IsStopped() && !StopConsumption)
			_breathTimer.Start();

		if (Input.IsActionPressed("rotate_left"))
		{
			RotateY(_rotationSpeed * (float)delta);
		}
		else if (Input.IsActionPressed("rotate_right"))
		{
			RotateY(-_rotationSpeed * (float)delta);
		}

		if (Input.IsActionPressed("fire_jetpack"))
		{
			Velocity += -Transform.Basis.X * GameState.Instance.PlayerStats.ThrusterPower * (float)delta;

			if (_thrustTimer.IsStopped() && !StopConsumption)
				_thrustTimer.Start();

			_particles.Emitting = true;

			if (!_thrusterAudioPlayer.Playing)
			{
				_thrusterAudioPlayer.Stream = _thruserSound;
				_thrusterAudioPlayer.Play();
				GD.Print("playing thruster");
			}
		}
		else
		{
			_particles.Emitting = false;
			_thrusterAudioPlayer.Stop();
		}

		if (Input.IsActionPressed("dampen_speed"))
		{
			Velocity *= Mathf.Pow(GameState.Instance.PlayerStats.DampingFactor, (float)(delta * 60f));

			if (_thrustTimer.IsStopped())
				_thrustTimer.Start();
		}

		if (Velocity.Length() > _maxSpeed) //Dampen more above max speed
			Velocity *= Mathf.Pow(0.97f, (float)(delta * 60f));

		//Slight passive dampening
		Velocity *= Mathf.Pow(0.9999f, (float)(delta * 60f));

		MoveAndSlide();

		// Detect collision with asteroids
		int collisionCount = GetSlideCollisionCount();
		for (int i = 0; i < collisionCount; i++)
		{
			KinematicCollision3D collision = GetSlideCollision(i);
			GodotObject collider = collision.GetCollider();

			if (collider is RigidBody3D rigidBody)
			{
				Vector3 pushDirection = -collision.GetNormal();
				pushDirection.Y = 0;
				pushDirection = pushDirection.Normalized();

				Vector3 localHitPosition = collision.GetPosition() - rigidBody.GlobalPosition;
				rigidBody.ApplyImpulse(pushDirection * PushForce, localHitPosition);

				// Play a random collision sound
				if (_collisionSoundCooldownActive)
					continue;
				
				RandomNumberGenerator rng = new RandomNumberGenerator();

				int randomIndex = rng.RandiRange(0, _collisionSounds.Count - 1);
				_collisionAudioPlayer.Stream = _collisionSounds[randomIndex];
				_collisionAudioPlayer.Play();

				_collisionSoundCooldownActive = true;
				_collisionSoundTimer.Start();
			}
		}
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

	private void ResetCollisionSoundCooldown()
	{
		_collisionSoundCooldownActive = false;
	}

	public void SetStopConsumption(bool value)
	{
		StopConsumption = value;
	}
}