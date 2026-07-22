using Godot;
using GodotGMTK2026.Scripts.Items;

namespace GodotGMTK2026.Scripts.Machines.UpgradeMachine;

[GlobalClass]
public partial class UpgradeCost : Resource
{
    [Export] public int Amount;
    [Export] public ItemEnum ItemType;
}