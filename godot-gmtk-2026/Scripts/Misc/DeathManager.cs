using Godot;
using GodotGMTK2026.Scripts.Management;

namespace GodotGMTK2026.Scripts.Misc;

public partial class DeathManager : Node
{
    [Export] private Control _deathUI;
    private Timer _timer;

    public override void _Ready()
    {
        _timer = new Timer();
        _timer.SetWaitTime(3f);
        _timer.SetOneShot(true);
        _timer.Timeout += HideDeathScreen;
        AddChild(_timer);
    }

    public override void _Process(double delta)
    {
        if (GameState.Instance.PlayerStats.OxygenLevel <= 0f)
        {
            _deathUI.SetVisible(true);
            GameState.Instance.PlayerStats.SetOxygenLevel(GameState.Instance.PlayerStats.OxygenCapacity);
            GameState.Instance.PlayerController.Position = new Vector3(0, 0, 0);
            while (GameState.Instance.PlayerInventory.Count() > 0)
            {
                GameState.Instance.PlayerInventory.RemoveFirst();
            }
            _timer.Start();
        }
    }

    public void HideDeathScreen()
    {
        _deathUI.SetVisible(false);
    }
}