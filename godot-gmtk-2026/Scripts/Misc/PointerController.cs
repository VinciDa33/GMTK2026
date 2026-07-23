using Godot;

namespace GodotGMTK2026.Scripts.Misc;

public partial class PointerController : Node3D
{
    [Export] private Node3D _origin;
    [Export] private Node3D _target;
    [Export] private float _orbitRadius;
    public override void _Process(double delta)
    {    
        if (_origin == null || _target == null)
            return;

        if (_target.GlobalPosition.DistanceTo(_origin.GlobalPosition) < 2.5f)
        {
            SetVisible(false);
            return;
        }
        
        SetVisible(true);
        
        Vector3 direction = _target.GlobalPosition - _origin.GlobalPosition;        
        direction.Y = 0;
        direction = direction.Normalized();

        GlobalPosition = _origin.GlobalPosition + direction * _orbitRadius;
        
        LookAt(GlobalPosition + direction, Vector3.Up);
    }
}