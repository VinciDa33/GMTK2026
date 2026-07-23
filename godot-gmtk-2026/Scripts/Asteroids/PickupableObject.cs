using Godot;
using Godot.Collections;
using GodotGMTK2026.Scripts.Items;
using GodotGMTK2026.Scripts.Management;
using GodotGMTK2026.Scripts.Misc;
using GodotGMTK2026.Scripts.Player;

namespace GodotGMTK2026.Scripts.Asteroids;

public partial class PickupableObject : RigidBody3D
{
	[Export] public Array<PickupableObjectData> Objects;
	[Export] public MeshInstance3D MeshInstance;
    [Export] private float _playerCheckDistance = 1.5f;
    [Export] private PackedScene InteractionPromptFollowerScene;

    private PickupableObjectData _objectData;
    private Timer _checkForPlayerTimer;
    private bool _playerInRange;
    private InteractionPrompt _interactionPrompt;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _checkForPlayerTimer = new Timer();
        _checkForPlayerTimer.WaitTime = 0.25f;
        _checkForPlayerTimer.OneShot = true;
        _checkForPlayerTimer.Timeout += CheckForPlayer;
        AddChild(_checkForPlayerTimer);

        InteractionFollower interactionPromptFollower = InteractionPromptFollowerScene.Instantiate<InteractionFollower>();
        interactionPromptFollower.SetTarget(this);
        interactionPromptFollower._prompt.SetupPrompt("Pick Up", "E");
        _interactionPrompt = interactionPromptFollower._prompt;
        GetTree().Root.AddChild(interactionPromptFollower);

        var random = GD.RandRange(0, Objects.Count - 1);
        _objectData = Objects[random];

        if (_objectData.IsScrap)
        {
            if (_objectData.IsCopper)
            {
                MeshInstance.Scale = new Vector3(1f, 1f, 1f);
            }
            else if (_objectData.IsIron)
            {
                MeshInstance.Scale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            MeshInstance.Mesh = _objectData.Mesh;
        }
        else
        {
            MeshInstance.Mesh = _objectData.Mesh;
            MeshInstance.MaterialOverride = _objectData.Material;   
        }

		var rand = new RandomNumberGenerator();

		LinearVelocity = new Vector3(rand.RandfRange(-1, 1), 0, rand.RandfRange(-1, 1));
		AngularVelocity = new Vector3(rand.RandfRange(-1, 1), rand.RandfRange(-1, 1), rand.RandfRange(-1, 1));
	}

    public override void _Process(double delta)
    {
        if (_checkForPlayerTimer.IsStopped())
            _checkForPlayerTimer.Start();

        if (_playerInRange && Input.IsActionJustPressed("interact"))
        {
            if (_objectData.IsScrap && _objectData.IsCopper)
            {
                GameState.Instance.PlayerInventory.AddItem(ItemRegistry.Instance.GetItem(ItemEnum.ScrapCable));
            }
            else if (_objectData.IsScrap && _objectData.IsIron)
            {
                GameState.Instance.PlayerInventory.AddItem(ItemRegistry.Instance.GetItem(ItemEnum.ScrapMetal));
            }
            else if (!_objectData.IsScrap && _objectData.IsCopper)
            {
                GameState.Instance.PlayerInventory.AddItem(ItemRegistry.Instance.GetItem(ItemEnum.CopperOre));
            }
            else
            {
                GameState.Instance.PlayerInventory.AddItem(ItemRegistry.Instance.GetItem(ItemEnum.IronOre));
            }

            _interactionPrompt.SetVisible(false);
            AsteroidSpawner.Instance.PickupObject(this);
        }
    }

    public void CheckForPlayer()
    {
        PlayerController player = GameState.Instance.PlayerController;
        if (player == null)
            return;

        if (Position.DistanceTo(player.Position) <= _playerCheckDistance)
        {
            _playerInRange = true;
            _interactionPrompt.SetVisible(true);
        }
        else
        {
            _playerInRange = false;
            _interactionPrompt.SetVisible(false);
        }
    }
}