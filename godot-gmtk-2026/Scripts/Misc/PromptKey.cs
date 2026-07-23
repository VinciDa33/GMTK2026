using Godot;

namespace GodotGMTK2026.Scripts.Misc;

[GlobalClass]
public partial class PromptKey : Resource
{
    [Export] public string key;
    [Export] public Texture2D Texture;
}