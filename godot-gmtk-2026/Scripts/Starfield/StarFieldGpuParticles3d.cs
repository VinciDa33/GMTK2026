using Godot;
using System;

public partial class StarFieldGpuParticles3d : GpuParticles3D
{
	[Export(PropertyHint.Range, "0,0.2")]
	public float ParallaxStrength = 0.05f;

	[Export] public Vector3 WrapSize = new Vector3(8, 8, 8);

	private Vector3 _offset = Vector3.Zero;
	private Vector3 _basePosition;
	private Vector3 _lastCamPos;

	public override void _Ready()
	{
		_basePosition = Position;                      // remember the (0,0,-11)
		_lastCamPos = GetParent<Node3D>().GlobalPosition;
	}

	public override void _Process(double delta)
	{
		Node3D cam = GetParent<Node3D>();
		Vector3 camPos = cam.GlobalPosition;
		Vector3 camDelta = camPos - _lastCamPos;
		_lastCamPos = camPos;

		// express the world-space movement in the camera's local axes
		Vector3 localDelta = cam.GlobalTransform.Basis.Inverse() * camDelta;

		_offset -= localDelta * ParallaxStrength;

		_offset.X = Wrap(_offset.X, WrapSize.X);
		_offset.Y = Wrap(_offset.Y, WrapSize.Y);
		_offset.Z = Wrap(_offset.Z, WrapSize.Z);

		Position = _basePosition + _offset;
	}

	private static float Wrap(float value, float size)
	{
		if (size <= 0f)
			return 0f;                       // axis disabled — no offset at all
		return Mathf.PosMod(value + size * 0.5f, size) - size * 0.5f;
	}
}
