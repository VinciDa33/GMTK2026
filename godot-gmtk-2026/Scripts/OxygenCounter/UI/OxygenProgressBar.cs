using Godot;
using GodotGMTK2026.Scripts.Management;
using System;

public partial class OxygenProgressBar : ProgressBar
{
	[Export] public Label _valueLabel;
	[Export] public ProgressBar _progressBar;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		UpdateProgressBar();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		UpdateProgressBar();
	}

	private void UpdateProgressBar()
	{
		var oxygenCapacity = GameState.Instance.PlayerStats.OxygenCapacity;
		var oxygenLevel = GameState.Instance.PlayerStats.OxygenLevel;

		_progressBar.SetValueNoSignal(oxygenLevel);
		_progressBar.MaxValue = oxygenCapacity;
		_valueLabel.Text = $"{(int)oxygenLevel}/{(int)oxygenCapacity}";
	}
}
