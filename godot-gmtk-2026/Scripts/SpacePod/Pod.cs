using Godot;
using GodotGMTK2026.Scripts.Management;
using GodotGMTK2026.Scripts.Player;

namespace GodotGMTK2026.Scripts.SpacePod;

public partial class Pod : Node
{
	[Export] public MeshInstance3D spacePodTop;
	[Export] public float Speed = 2.0f;
	
	private float _targetTransparency = 0f;
	
	public override void _Process(double delta)
	{
		if (spacePodTop == null)
			return;

		float current = spacePodTop.Transparency;
		spacePodTop.Transparency = Mathf.Lerp(current, _targetTransparency, (float)delta * Speed);
	}
	
	public void PlayerEntered(Node3D body)
	{
		if (body is not PlayerController)
			return;
		GD.Print("Player entered the pod");
		GameState.Instance.PlayerController.SetStopConsumption(true);
		_targetTransparency = 1f;
	}

	public void PlayerExited(Node3D body)
	{
		if (body is not PlayerController)
			return;
		GD.Print("Player left the pod");
		GameState.Instance.PlayerController.SetStopConsumption(false);
		_targetTransparency = 0f;
	}
}
