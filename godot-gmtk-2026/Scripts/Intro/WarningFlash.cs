using Godot;
using System;

public partial class WarningFlash : CsgMesh3D
{
	[Export] public Color DarkColor = new Color(0.30f, 0.22f, 0.00f);
	[Export] public Color BrightColor = new Color(1.00f, 0.88f, 0.15f);
	[Export] public float Speed = 2.0f;
	[Export] public float EmissionStrength = 2.0f;
	[Export] public bool HardBlink = false;
 
	private StandardMaterial3D _mat;
	private float _t;
 
	public override void _Ready()
	{
		_mat = new StandardMaterial3D
		{
			AlbedoColor = DarkColor,
			EmissionEnabled = true,
			Emission = DarkColor,
			EmissionEnergyMultiplier = EmissionStrength
		};
 
		Material = _mat;
	}
 
	public override void _Process(double delta)
	{
		_t += (float)delta * Speed;
 
		float wave = Mathf.Sin(_t * Mathf.Tau);
		float w = HardBlink
			? (wave > 0f ? 1f : 0f)
			: (wave * 0.5f + 0.5f);
 
		Color c = DarkColor.Lerp(BrightColor, w);
 
		_mat.AlbedoColor = c;
		_mat.Emission = c;
	}
}
