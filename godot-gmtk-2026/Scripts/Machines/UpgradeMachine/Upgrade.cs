using Godot;

namespace GodotGMTK2026.Scripts.Machines.UpgradeMachine;

[GlobalClass]
public partial class Upgrade : Resource
{
    [Export] public string Id;
    [Export] public string Name;
    [Export(PropertyHint.MultilineText)] public string Description;
    [Export] public UpgradeCost[] Cost;
}