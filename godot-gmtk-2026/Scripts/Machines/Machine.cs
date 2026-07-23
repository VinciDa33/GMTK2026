using Godot;
using GodotGMTK2026.Scripts.Management;
using GodotGMTK2026.Scripts.Player;

namespace GodotGMTK2026.Scripts.Machines;

public abstract partial class Machine : Node3D
{
    [Export] private float _playerCheckDistance = 3f;
    private Timer _checkForPlayerTimer;
    protected bool _playerInRange;

    public override void _Ready()
    {
        _checkForPlayerTimer = new Timer();
        _checkForPlayerTimer.WaitTime = 0.25f;
        _checkForPlayerTimer.OneShot = true;
        _checkForPlayerTimer.Timeout += CheckForPlayer;
        AddChild(_checkForPlayerTimer);
    }

    public override void _ExitTree()
    {
        _checkForPlayerTimer.Timeout -= CheckForPlayer;
    }

    public override void _Process(double delta)
    {
        if (_checkForPlayerTimer.IsStopped())
            _checkForPlayerTimer.Start();
    }

    public void CheckForPlayer()
    {
        PlayerController player = GameState.Instance.PlayerController;
        if (player == null)
            return;

        if (Position.DistanceTo(player.Position) <= _playerCheckDistance)
        {
            PlayerInRange(true);
            _playerInRange = true;
        }
        else
        {
            PlayerInRange(false);
            _playerInRange = false;
        }
    }

    public abstract void PlayerInRange(bool isInRange);
}