using Godot;

namespace GodotGMTK2026.Scripts.Items;

[GlobalClass]
public partial class ItemResource : Resource
{
    [Export] public ItemEnum Identifier;
    [Export] public string Name;
    [Export] public Texture Sprite;
}