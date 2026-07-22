using Godot;
using System;

public partial class player_controller : CharacterBody3D
{
    [Export] public float Acceleration = 5.0f;
    [Export] public float RotationSpeed = 5.0f;

	[Export] public float Dampening = 0.98f;
	public bool HasDampingUpgrade { get; set; } = false;

	private Vector3 _velocity = Vector3.Zero;

	public override void _PhysicsProcess(double delta)
	{
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
}
