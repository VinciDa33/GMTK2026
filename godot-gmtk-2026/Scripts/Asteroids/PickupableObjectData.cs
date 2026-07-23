using Godot;
using System;

[GlobalClass]
public partial class PickupableObjectData : Resource
{
    [Export] public Mesh Mesh { get; set; }
    [Export] public Material Material { get; set; }
    [Export] public bool IsCopper { get; set; }
    [Export] public bool IsIron { get; set; }
	[Export] public bool IsScrap { get; set; }
}