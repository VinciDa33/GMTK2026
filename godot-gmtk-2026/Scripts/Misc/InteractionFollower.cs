using Godot;

namespace GodotGMTK2026.Scripts.Misc;

public partial class InteractionFollower : Node3D
{
    private Node3D _target;
    [Export] public InteractionPrompt _prompt { get; private set; }
    public void SetTarget(Node3D target)
    {
        _target = target;
    }

    public override void _Process(double delta)
    {
        if (_target == null)
        {
            QueueFree();
            return;
        }

        GlobalPosition = _target.GlobalPosition + new Vector3(0f, 1f, 0f);
    }
}