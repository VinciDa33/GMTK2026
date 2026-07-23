using Godot;

namespace GodotGMTK2026.Scripts.Misc;

public partial class InteractionPrompt : Node
{
    [ExportGroup("Keys")] 
    [Export] private PromptKey[] _keys;
    
    [ExportGroup("UI")]
    [Export] private Label _label;
    [Export] private TextureRect _textureRect;

    public void SetupPrompt(string text, string key)
    {
        _label.Text = text;
        foreach (PromptKey pKey in _keys)
            if (pKey.key.Equals(key))
            {
                _textureRect.Texture = pKey.Texture;
                return;
            }
    }
}