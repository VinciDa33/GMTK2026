using Godot;
using GodotGMTK2026.Scripts.Items;

namespace GodotGMTK2026.Scripts.Machines.RefineryMachine;

[GlobalClass]
public partial class RefineryRecipe : Resource
{
    [Export] public ItemEnum Input;
    [Export] public ItemEnum Output;
}