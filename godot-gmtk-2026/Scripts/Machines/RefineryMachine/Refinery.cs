using System.Collections.Generic;
using Godot;
using GodotGMTK2026.Scripts.Items;
using GodotGMTK2026.Scripts.Items.Inventory;
using GodotGMTK2026.Scripts.Management;
using GodotGMTK2026.Scripts.Misc;

namespace GodotGMTK2026.Scripts.Machines.RefineryMachine;

public partial class Refinery : Machine
{
    public static Refinery Instance { get; private set; }
    [Export] private InteractionPrompt _interactionPrompt;
    [Export] private PackedScene _floatingText;
    [Export] private GpuParticles3D _particles;
    
    [Export] private RefineryRecipe[] _recipes;
    [Export] private float _processingTime;
    public Inventory Inventory { get; private set; }
    private Timer _processingTimer;

    private RandomNumberGenerator rng = new RandomNumberGenerator();
    public override void _Ready()
    {
        base._Ready();
        
        Instance = this;
        
        _interactionPrompt.SetupPrompt("Refine Materials", "E");
        Inventory = new Inventory(10);
        
        _processingTimer = new Timer();
        _processingTimer.WaitTime = _processingTime;
        _processingTimer.OneShot = true;
        _processingTimer.Timeout += ProcessingTimerFinished;
        AddChild(_processingTimer);
    }

    public override void _ExitTree()
    {
        if (_processingTimer == null)
            return;
        _processingTimer.Timeout -= ProcessingTimerFinished;
        Instance = null;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        
        if (_playerInRange && Input.IsActionJustPressed("interact"))
        {
            foreach (RefineryRecipe recipe in _recipes)
            {
                List<Item> items = GameState.Instance.PlayerInventory.GetAllOfType(recipe.Input);
                foreach (Item item in items)
                {
                    if (Inventory.AddItem(item))
                    {
                        GameState.Instance.PlayerInventory.RemoveItem(item);
                        GD.Print($"Refinery got: {item.Name}");
                    }
                }
            }
        }
        
        //Start a timer when the refinery inventory is not empty!
        if (Inventory.Count() > 0 && _processingTimer.IsStopped())
            _processingTimer.Start();
    }

    public override void PlayerInRange(bool isInRange)
    {
        _interactionPrompt.SetVisible(isInRange);
    }

    private void ProcessingTimerFinished()
    {
        //Whenever the timer finishes, process one item from the refinery inventory
        Item item = Inventory.GetFirst();
        if (item == null)
            return;
        
        foreach (RefineryRecipe recipe in _recipes)
            if (recipe.Input == item.Type)
            {
                Item output = ItemRegistry.Instance.GetItem(recipe.Output);
                GameState.Instance.StationInventory.AddItem(output);
                
                FloatingText ft = _floatingText.Instantiate() as FloatingText;
                ft.SetupText($"+1 {output.Name}", new Vector3(rng.RandfRange(-0.2f, 0.2f), 0f, rng.RandfRange(-0.2f, 0.2f)));
                AddChild(ft);
                ft.Position = new Vector3(0, 1f, 0);

                _particles.Emitting = true;
                
                GD.Print($"Refinery Produced: {item.Name}");
                break;
            }
        Inventory.RemoveFirst();
        Inventory.Count();
    }

    public void SetProcessingTime(float processingTime)
    {
        _processingTime = processingTime;
    }
}