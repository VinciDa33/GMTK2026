using Godot;

namespace GodotGMTK2026.Scripts.Misc;

public partial class FloatingText : Node3D
{
    [Export] private Label _label;
    private Vector3 _direction = Vector3.Zero;
    private Timer _timer;

    public override void _Ready()
    {
        _timer = new Timer();
        _timer.OneShot = true;
        _timer.WaitTime = 1.5f;
        _timer.Timeout += Kill;
        AddChild(_timer);
        _timer.Start();
    }


    private void Kill()
    {
        QueueFree();
    }
    
    public void SetupText(string text, Vector3 direction)
    {
        _label.Text = text;
        _direction = direction;
    }

    public override void _Process(double delta)
    {
        GlobalPosition += _direction * (float) delta;
    }
}